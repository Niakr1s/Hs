using HsLib.Battle;
using HsLib.Common.Interfaces;
using HsLib.Common.Place;

namespace HsLib.Cards.KnownCards.Minions
{
    public class Abomintaion : Minion, IWithDeathrattle
    {
        public Abomintaion() : base(5, 4, 4)
        {
            _deathrattleTargets = new Target
            {
                Locs = new() { Loc.Field, Loc.Hero },
                Sides = new() { PidSide.Me, PidSide.He },
            };
        }

        private readonly Target _deathrattleTargets;

        public override bool ActivateDeathrattle(Battlefield bf)
        {
            var damageabletargets = bf.TargetChooser.Choose(this, _deathrattleTargets)
                .Select(c => c as IDamageable).ToList();
            IList<IDamageable> targets = bf.TargetChooser.Choose(this, _deathrattleTargets)
                .Select(c => c as IDamageable).Where(c => c is not null).Select(c => c!).ToList();

            foreach (IDamageable target in targets)
            {
                bf.BattleService.DealDamage(2, target);
            }

            return true;
        }
    }
}
