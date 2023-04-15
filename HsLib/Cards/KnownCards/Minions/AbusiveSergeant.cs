using HsLib.Battle;
using HsLib.Cards.Effects;
using HsLib.Common.Place;
using HsLib.Events;
using HsLib.Reactive;
using HsLib.Stats.Base;

namespace HsLib.Cards.KnownCards.Minions
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
                Do.Once(bf, e => e.EventArgs is TurnEndEventArgs, () => buff.Active = false);
            }
        }
    }
}
