using Force.DeepCloner;
using HsLib.Systems;
using HsLib.Types.GameActions;
using HsLib.Types.GameIntents;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    /// <summary>
    /// Parent for all cards.<br/><br/>
    /// Card reacts on 4 board events:<br/>
    /// <see cref="Subscribe(IBoard)"/><br/>
    /// <see cref="Unsubscribe(IBoard, Place)"/><br/>
    /// <see cref="OnTurnEnd(Board)"/><br/>
    /// <see cref="OnTurnStart(Board)"/>.<br/><br/>
    /// </summary>
    public abstract class Card : ICard, IWithPlace
    {
        protected Card(int mp)
        {
            Mp = new Mp(mp);
            Place = new();
        }

        public Place Place { get; set; }

        public int? AddedTurnNo { get; private set; }

        protected IBoard? Board { get; private set; }

        public Mp Mp { get; protected set; }

        public virtual bool ShouldBeCleaned() { return false; }


        #region reactive

        public virtual void Subscribe(IBoard board)
        {
            Board = board;
            AddedTurnNo = board.Turn.No;
        }

        public virtual void Unsubscribe(IBoard board, Place previousPlace)
        {
            Board = null;
            AddedTurnNo = null;
        }

        protected virtual void OnTurnStart() { }

        protected virtual void OnTurnEnd() { }

        public virtual ICard Clone()
        {
            // todo
            Card cloned = this.DeepClone();
            cloned.Place = new();
            cloned.Mp = (Mp)Mp.Clone();
            return cloned;
        }

        public virtual bool CanProcessIntent(GameIntent intent)
        {
            return false;
        }

        public virtual IEnumerable<GameAction>? ProcessIntent(GameIntent intent)
        {
            return null;
        }

        #endregion
    }
}
