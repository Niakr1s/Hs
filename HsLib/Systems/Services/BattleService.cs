using HsLib.Interfaces.CardTraits;
using HsLib.Types.Events;

namespace HsLib.Systems.Services
{
    public class BattleService
    {
        public BattleService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        public event EventHandler<BattleEventArgs>? Event;

        /// <summary>
        /// Deals damage and invokes <see cref="BattleGotDamageEventArgs"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defender"></param>
        /// <returns>Amount of damage received</returns>
        public int DealDamage(int value, IDamageable defender)
        {
            // todo: return action
            int dmg = defender.GetDamage(value);
            Event?.Invoke(this, new BattleGotDamageEventArgs(defender, dmg));
            return dmg;
        }
    }
}