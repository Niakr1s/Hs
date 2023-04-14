using Models.Common;
using Models.Containers;
using Models.Stats;

namespace Models.Cards
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
        /// It will be called after card is added to container.
        /// </summary>
        public virtual void AfterContainerAdded()
        {

        }

        /// <summary>
        /// It will be called before card was removed from container.
        /// </summary>
        public virtual void BeforeContainerRemoved()
        {

        }
    }
}
