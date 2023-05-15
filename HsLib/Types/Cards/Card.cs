using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    /// <summary>
    /// Parent for all cards.<br/><br/>
    /// Card reacts on 4 Battlefield events:<br/>
    /// <see cref="AfterContainerInsert(Battlefield)"/><br/>
    /// <see cref="AfterContainerRemove(Battlefield)"/><br/>
    /// <see cref="OnTurnEnd(Battlefield)"/><br/>
    /// <see cref="OnTurnStart(Battlefield)"/>.<br/><br/>
    /// </summary>
    public abstract class Card : ICard
    {
        protected Card(int mp)
        {
            Mp = new Mp(mp);
        }

        public PlaceInContainer? Place { get; set; }

        public Mp Mp { get; }

        /// <summary>
        /// Use this to subscribe to events.
        /// </summary>
        public virtual void AfterContainerInsert(Battlefield bf)
        {
        }

        /// <summary>
        /// Use this to unsubscribe from events.
        /// </summary>
        public virtual void AfterContainerRemove(Battlefield bf)
        {
        }

        public virtual void OnTurnEnd(Battlefield bf) { }

        public virtual void OnTurnStart(Battlefield bf) { }
    }
}
