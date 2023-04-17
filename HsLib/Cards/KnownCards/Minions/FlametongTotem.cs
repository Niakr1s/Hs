﻿using HsLib.Battle;
using HsLib.Common.Place;
using HsLib.Containers;
using HsLib.Events;
using HsLib.Stats.Base;

namespace HsLib.Cards.KnownCards.Minions
{
    public class FlametongTotem : Minion
    {
        public FlametongTotem() : base(2, 0, 3)
        {
        }

        public override void AfterContainerInsert(Battlefield bf)
        {
            base.AfterContainerInsert(bf);
            if (Loc == Loc.Field)
            {
                ReapplyEnchants(bf);
                bf.Event += Bf_Event;
            }
        }

        public override void AfterContainerRemove(Battlefield bf)
        {
            base.AfterContainerRemove(bf);
            bf.Event -= Bf_Event;
            ReapplyEnchants(bf);
        }

        private void Bf_Event(object? sender, BattlefieldEventArgs e)
        {
            ReapplyEnchants(e.Bf);
        }

        private readonly List<IEnchantHandler> _appliedEnchants = new();

        private void ReapplyEnchants(Battlefield bf)
        {
            _appliedEnchants.ForEach(e => e.Active = false);
            _appliedEnchants.Clear();

            foreach (Minion target in GetEnchantTargets(bf))
            {
                _appliedEnchants.Add(target.Atk.AddAura(2));
            }
        }

        private IEnumerable<Minion> GetEnchantTargets(Battlefield bf)
        {
            if (Pid == Pid.None) yield break;
            Field f = bf[Pid].Field;
            Minion? left = f.Left(this);
            Minion? right = f.Right(this);
            if (left is not null) yield return left;
            if (right is not null) yield return right;
        }
    }
}