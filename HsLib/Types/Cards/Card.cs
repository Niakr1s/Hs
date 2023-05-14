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
    public abstract class Card : IWithPlace, IWithTurn
    {
        protected Card(int mp)
        {
            Mp = new Mp(mp);
        }

        public Mp Mp { get; }

        public PlaceInContainer? Place { get; set; }

        /// <summary>
        /// Is set by Container.
        /// </summary>
        public int TurnAdded { get; set; }

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

        public void PlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null)
        {
            // TODO: add mp check
            DoPlayFromHand(bf, fieldIndex, effectTarget);
        }

        protected abstract void DoPlayFromHand(Battlefield bf, int? fieldIndex = null, Card? effectTarget = null);
    }
}
