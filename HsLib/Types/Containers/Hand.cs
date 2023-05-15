using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;
using HsLib.Types.Stats;

namespace HsLib.Types.Containers
{
    public class Hand : MultiContainer<IPlayableFromHand>
    {
        public Hand(Battlefield bf, Pid pid) : base(bf, new Place(pid, Loc.Hand), limit: 10)
        {
        }

        /// <summary>
        /// Plays from hand.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="MpException"></exception>
        /// <returns>Action, that does actual play</returns>
        public Action Play(int index, int? fieldIndex = null, ICard? effectTarget = null)
        {
            IPlayableFromHand card = (IPlayableFromHand)this[index];

            Mp mp = Bf[Place.Pid].Mp;
            if (!mp.IsEnough(card.Mp)) { throw new MpException(); }

            Action playFromHandAction = card.PlayFromHand(Bf, fieldIndex, effectTarget);

            return () =>
            {
                playFromHandAction();
                mp.Set(mp.Value - card.Mp.Value);
            };
        }
    }
}
