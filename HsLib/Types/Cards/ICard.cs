using HsLib.Systems;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public interface ICard
    {
        Mp Mp { get; }

        PlaceInContainer? PlaceInContainer { get; set; }
        bool ShouldBeRemovedFromCurrentContainer();

        void OnTurnEnd(Battlefield bf);
        void OnTurnStart(Battlefield bf);

        void OnPreAttack(Battlefield bf, IAttacker attacker, IDamageable defender);



        void Subscribe(Battlefield bf);
        void Unsubscribe(Battlefield bf, Place previousPlace);
    }
}