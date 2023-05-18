using HsLib.Types.Events;
using HsLib.Types.Places;

namespace HsLib.Systems
{
    public interface IBattlefieldSubscriber
    {
        void AfterContainerInsert(Battlefield bf);
        void AfterContainerRemove(Battlefield bf, Place previousPlace);
        void OnTurnEnd(Battlefield bf);
        void OnTurnStart(Battlefield bf);

        void OnGotDamage(Battlefield bf, BattleGotDamageEventArgs e);
        void OnPreAttack(Battlefield bf, BattleMeleePreAttackEventArgs e);
    }
}
