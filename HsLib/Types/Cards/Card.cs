using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Events;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    /// <summary>
    /// Parent for all cards.<br/><br/>
    /// Card reacts on 4 Battlefield events:<br/>
    /// <see cref="AfterContainerInsert(Battlefield)"/><br/>
    /// <see cref="AfterContainerRemove(Battlefield, Place)"/><br/>
    /// <see cref="OnTurnEnd(Battlefield)"/><br/>
    /// <see cref="OnTurnStart(Battlefield)"/>.<br/><br/>
    /// </summary>
    public abstract class Card : ICard, IWithPlaceInContainer
    {
        protected Card(int mp)
        {
            Mp = new Mp(mp);
        }

        public PlaceInContainer? PlaceInContainer { get; set; }

        public Mp Mp { get; }

        public virtual void AfterContainerInsert(Battlefield bf) { }
        public virtual void AfterContainerRemove(Battlefield bf, Place previousPlace) { }

        public virtual void OnTurnEnd(Battlefield bf) { }

        public virtual void OnTurnStart(Battlefield bf) { }

        public virtual void OnGotDamage(Battlefield bf, BattleGotDamageEventArgs e) { }

        public virtual void OnPreAttack(Battlefield bf, BattleMeleePreAttackEventArgs e) { }
    }
}
