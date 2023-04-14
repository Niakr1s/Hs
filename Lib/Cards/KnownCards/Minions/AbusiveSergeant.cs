using Models.Cards.Effects;
using Models.Containers;
using Models.Events;
using Models.Reactive;
using Models.Stats.Base;

namespace Models.Cards.KnownCards.Minions
{
    public class AbusiveSergeant : Minion
    {
        public AbusiveSergeant() : base(2, 2, 1)
        {
            Battlecry = new AbusiveSergeantBattlecry();
        }
    }

    file class AbusiveSergeantBattlecry : Battlecry
    {
        public AbusiveSergeantBattlecry()
        {
            Target = new Target
            {
                Container = TargetContainer.Field,
                Side = TargetSide.Me | TargetSide.He
            };
        }

        protected override void DoUseEffect(Battlefield bf, Card owner, Card? target)
        {
            if (target is not null && target is Minion m)
            {
                Enchant<int> buff = m.Atk.AddBuff(2);
                Do.Once(bf.Turn, e => e is TurnEndEventArgs, () => buff.Active = false);
            }
        }
    }
}
