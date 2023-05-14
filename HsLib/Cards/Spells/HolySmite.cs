using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Spells
{
    public class HolySmite : Spell
    {
        public HolySmite() : base(1)
        {
        }

        public override void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            if (target is IDamageable m)
            {
                m.GetDamage(2);
            }
        }

        private readonly Targets _targets = new Targets
        {
            Locs = Loc.Field | Loc.Hero,
            Sides = PidSide.He | PidSide.Me
        };

        public override EffectType EffectType => EffectType.Solo;

        public override IEnumerable<ICard> UseEffectTargets(Battlefield bf, Pid pid)
        {
            return _targets.GetValidTargets(Place!.Pid, bf.Cards);
        }
    }
}
