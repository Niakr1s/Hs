using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Systems
{
    public interface IBattlefieldSubscriber
    {
        void Subscribe(Battlefield bf);
        void Unsubscribe(Battlefield bf, Place previousPlace);

        void OnTurnEnd(Battlefield bf);
        void OnTurnStart(Battlefield bf);

        void OnPreAttack(Battlefield bf, IAttacker attacker, IDamageable defender);
    }
}
