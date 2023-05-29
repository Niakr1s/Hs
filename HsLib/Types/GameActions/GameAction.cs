using HsLib.Systems;

namespace HsLib.Types.GameActions
{
    public abstract class GameAction
    {
        public virtual bool CanProcess(Board board) { return true; } // todo in children

        public abstract void Process(Board board);
    }
}
