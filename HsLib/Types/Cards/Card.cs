using Force.DeepCloner;
using HsLib.Systems;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    /// <summary>
    /// Parent for all cards.<br/><br/>
    /// Card reacts on 4 Battlefield events:<br/>
    /// <see cref="Subscribe(Battlefield)"/><br/>
    /// <see cref="Unsubscribe(Battlefield, Place)"/><br/>
    /// <see cref="OnTurnEnd(Battlefield)"/><br/>
    /// <see cref="OnTurnStart(Battlefield)"/>.<br/><br/>
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

        protected Battlefield? Bf { get; private set; }

        public Mp Mp { get; protected set; }

        public virtual bool ShouldBeCleaned() { return false; }


        #region reactive

        public virtual void Subscribe(Battlefield bf)
        {
            Bf = bf;
            AddedTurnNo = bf.Turn.No;
        }

        public virtual void Unsubscribe(Battlefield bf, Place previousPlace)
        {
            Bf = null;
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

        #endregion
    }
}
