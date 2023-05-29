using HsLib.Types.Cards;
using HsLib.Types.GameActions;

namespace HsLib.Types.GameIntents
{
    public abstract class GameIntent
    {
        protected GameIntent(ICard actor)
        {
            Actor = actor;
        }

        public ICard Actor { get; }

        public abstract bool CanBeProcessed();

        public abstract IEnumerable<GameAction>? Process();
    }
}
