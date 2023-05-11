using HsLib.Common.MeleeAttack;

namespace HsLib.Events
{
    public abstract class BattleEventArgs : EventArgs
    {
    }

    public class BattleGotDamageEventArgs : BattleEventArgs
    {
        public BattleGotDamageEventArgs(IDamageable defender, int amount)
        {
            Defender = defender;
            Amount = amount;
        }

        public IDamageable Defender { get; }
        public int Amount { get; }
    }

    public class BattleMeleePreAttackEventArgs : BattleEventArgs
    {
        public BattleMeleePreAttackEventArgs(IAttacker attacker, IDamageable defender)
        {
            Attacker = attacker;
            Defender = defender;
        }

        public IAttacker Attacker { get; }
        public IDamageable Defender { get; }
    }
}
