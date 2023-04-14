using Models.Services.Battle;

namespace Models.Events
{
    public abstract class BattleEventArgs : BattlefieldEventArgs
    {
    }

    public class GotDamageEventArgs : BattleEventArgs
    {
        public GotDamageEventArgs(IDefender defender, int amount)
        {
            Defender = defender;
            Amount = amount;
        }

        public IDefender Defender { get; }
        public int Amount { get; }
    }

    public class MeleePreAttackEventArgs : BattleEventArgs
    {
        public MeleePreAttackEventArgs(IAttacker attacker, IDefender defender)
        {
            Attacker = attacker;
            Defender = defender;
        }

        public IAttacker Attacker { get; }
        public IDefender Defender { get; }
    }
}
