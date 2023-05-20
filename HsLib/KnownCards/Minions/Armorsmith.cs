using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.LingeringEffects;
using HsLib.Types.Places;
using System.Collections.Specialized;

namespace HsLib.KnownCards.Minions
{
    public class Armorsmith : Minion
    {
        public Armorsmith() : base(2, 1, 4)
        {
            ArmorsmithEffectSource effectSource = new(this);
            FieldEffectSources.Add(effectSource);
        }
    }

    public class ArmorsmithEffectSource : LingeringEffectSource<Minion>
    {
        public ArmorsmithEffectSource(Minion owner) : base(owner)
        {
        }

        private readonly List<IDamageable> _subs = new();

        protected override void DoSubscribe(Battlefield bf)
        {
            foreach (IDamageable card in bf.Cards.OfType<IDamageable>())
            {
                SubscribeCard(card);
            }

            bf.CollectionChanged += Bf_CollectionChanged;
        }

        protected override void DoUnsubscribe(Battlefield bf, Place previousPlace)
        {
            bf.CollectionChanged -= Bf_CollectionChanged;
            Clear();
        }

        private void Bf_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
            {
                foreach (IDamageable card in e.OldItems.OfType<IDamageable>())
                {
                    UnsubscribeCard(card);
                }
            }

            if (e.NewItems is not null)
            {
                foreach (IDamageable card in e.NewItems.OfType<IDamageable>())
                {
                    SubscribeCard(card);
                }
            }
        }

        protected bool ShoudBeSubscribed(IDamageable card)
        {
            return card is Minion &&
                card.PlaceInContainer is not null && Owner.PlaceInContainer is not null &&
                card.PlaceInContainer.Pid == Owner.PlaceInContainer.Pid;
        }

        private void SubscribeCard(IDamageable card)
        {
            if (ShoudBeSubscribed(card))
            {
                card.Hp.Decreased += Hp_Decreased;
                _subs.Add(card);
            }
        }

        private void UnsubscribeCard(IDamageable card)
        {
            int index = _subs.IndexOf(card);
            if (index != -1)
            {
                card.Hp.Decreased -= Hp_Decreased;
                _subs.RemoveAt(index);
            }
        }

        private void Hp_Decreased(object? sender, Types.Stats.StatDecreasedEventArgs e)
        {
            Bf![Owner.PlaceInContainer!.Pid].Hero.Card.Armor.Increase(1);
        }

        private void Clear()
        {
            _subs.ToList().ForEach(UnsubscribeCard);
            _subs.Clear();
        }
    }
}
