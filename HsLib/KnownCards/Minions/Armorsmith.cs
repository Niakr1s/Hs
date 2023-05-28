using HsLib.Types.BattlefieldSubscribers;
using HsLib.Types.Cards;

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

    public class ArmorsmithEffectSource : BattlefieldSubscriber<Minion>
    {
        public ArmorsmithEffectSource(Minion owner) : base(owner)
        {
        }

        protected override bool ShoudBeSubscribed(Minion card)
        {
            return card.PlaceInContainer is not null && Owner.PlaceInContainer is not null &&
                card.PlaceInContainer.Pid == Owner.PlaceInContainer.Pid;
        }

        protected override void DoSubscribeCard(Minion card)
        {
            card.Hp.Decreased += Hp_Decreased;
        }

        protected override void DoUnsubscribeCard(Minion card)
        {
            card.Hp.Decreased -= Hp_Decreased;
        }

        private void Hp_Decreased(object? sender, Types.Stats.StatDecreasedEventArgs e)
        {
            Bf![Owner.PlaceInContainer!.Pid].Hero.Armor.Increase(1);
        }
    }
}
