using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Cards.Minions
{
    public class Abomintaion : Minion, IWithDeathrattle
    {
        public Abomintaion() : base(5, 4, 4)
        {
            _deathrattleTargets = new Targets
            {
                Locs = Loc.Field | Loc.Hero,
                Sides = PidSide.Me | PidSide.He,
            };
        }

        private readonly Targets _deathrattleTargets;

        public override bool ActivateDeathrattle(Battlefield bf)
        {
            var damageabletargets = _deathrattleTargets.GetValidTargets(this, bf.Cards)
                .Select(c => c as IDamageable).ToList();
            IList<IDamageable> targets = _deathrattleTargets.GetValidTargets(this, bf.Cards)
                .Select(c => c as IDamageable).Where(c => c is not null).Select(c => c!).ToList();

            foreach (IDamageable target in targets)
            {
                bf.BattleService.DealDamage(2, target);
            }

            return true;
        }
    }
}
