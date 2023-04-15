using HsLib.Services.Battle;

namespace HsLib.Events
{
    public abstract class BattleEventArgs : BattlefieldEventArgs
    {
    }

    public class GotDamageEventArgs : BattleEventArgs
    {
        public GotDamageEventArgs(IDamageable defender, int amount)
        {
            Defender = defender;
            Amount = amount;
        }

        public IDamageable Defender { get; }
        public int Amount { get; }
    }

    public class MeleePreAttackEventArgs : BattleEventArgs
    {
        public MeleePreAttackEventArgs(IAttacker attacker, IDamageable defender)
        {
            Attacker = attacker;
            Defender = defender;
        }

        public IAttacker Attacker { get; }
        public IDamageable Defender { get; }
    }
}
