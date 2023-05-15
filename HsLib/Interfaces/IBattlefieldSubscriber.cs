using HsLib.Systems;
using HsLib.Types.Events;

namespace HsLib.Interfaces
{
    public interface IBattlefieldSubscriber
    {
        void AfterContainerInsert(Battlefield bf);
        void AfterContainerRemove(Battlefield bf);
        void OnTurnEnd(Battlefield bf);
        void OnTurnStart(Battlefield bf);

        void OnGotDamage(Battlefield bf, BattleGotDamageEventArgs e);
        void OnPreAttack(Battlefield bf, BattleMeleePreAttackEventArgs e);
    }
}
