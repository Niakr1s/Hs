using HsLib.Common.Place;
using HsLib.Containers;
using HsLib.Stats;

namespace HsLib.Cards
{
    public abstract class Card : IWithPlace
    {
        protected Card(int mp)
        {
            Mp = new Mp(mp);
        }

        public Mp Mp { get; }

        public Loc Loc { get; set; } = Loc.None;

        public Pid Pid { get; set; } = Pid.None;

        /// <summary>
        /// Is set by Container.
        /// </summary>
        public int TurnAdded { get; set; }

        /// <summary>
        /// It will be called after card is added to container.
        /// </summary>
        public virtual void AfterContainerInsert(Battlefield bf)
        {

        }

        /// <summary>
        /// It will be called before card was removed from container.
        /// </summary>
        public virtual void AfterContainerRemove(Battlefield bf)
        {

        }

        public virtual void OnTurnEnd(Battlefield bf) { }

        public virtual void OnTurnStart(Battlefield bf) { }
    }
}
