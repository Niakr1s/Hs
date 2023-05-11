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
            Battlecry = new AbusiveSergeantBattlecry(this);
        }
    }

    file class AbusiveSergeantBattlecry : Battlecry
    {
        public AbusiveSergeantBattlecry(Minion m) : base(m)
        {
            EffectTargets = new Target
            {
                Locs = new() { Loc.Field },
                Sides = new() { PidSide.Me, PidSide.He }
            };
        }

        public override bool EffectMustHaveTarget => true;

        public override bool CanUseEffect(Battlefield bf)
        {
            throw new NotImplementedException();
        }

        public override void UseEffect(Battlefield bf, Card? target)
        {
            if (target is not null && target is Minion m)
            {
                Enchant<int> buff = m.Atk.AddBuff(2);
                Do.Once(bf, e => e.EventArgs is TurnEndEventArgs, () => buff.Active = false);
            }
        }
    }
}
