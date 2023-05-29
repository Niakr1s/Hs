using HsLib.Types.GameActions;

namespace HsLib.Systems
{
    internal class ActionService : Service
    {
        public ActionService(Board board) : base(board)
        {
        }

        public void ProcessAction(GameAction action)
        {
            if (!action.CanProcess(Board)) { return; }
            action.Process(Board);
        }
    }
}
