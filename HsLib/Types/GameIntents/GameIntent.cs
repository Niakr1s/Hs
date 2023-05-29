using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.GameIntents
{
    public abstract class GameIntent
    {
        protected GameIntent(IBoard board, ICard actor)
        {
            Board = board;
            Actor = actor;
        }

        protected IBoard Board { get; }

        public ICard Actor { get; }
    }
}
