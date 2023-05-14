﻿using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Spells
{
    public class MindControl : Spell
    {
        public MindControl() : base(10)
        {
        }

        public override void UseEffect(Battlefield bf, Pid pid, Card? target)
        {
            if (target is null) return;

            if (target is Minion m && m.Place is not null && m.Place.Pid == pid.He())
            {
                bf[pid.He()].Field.Remove(m);
                bf[pid].Field.Add(m);
            }
        }

        private readonly Targets _targets = new Targets { Locs = Loc.Field, Sides = PidSide.He };

        public override EffectType EffectType => EffectType.Solo;

        public override IEnumerable<Card> UseEffectTargets(Battlefield bf, Pid pid)
        {
            return _targets.GetValidTargets(pid, bf.Cards);
        }
    }
}
