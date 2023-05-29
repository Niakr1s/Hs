using HsLib.Types.GameActions;

namespace HsLib.Systems
{
    public class ActionService : Service
    {
        public ActionService(Board board) : base(board)
        {
        }

        public void ProcessAction(GameAction action)
        {
            ProcessActions(new GameAction[] { action });
        }

        public void ProcessActions(IEnumerable<GameAction> actions)
        {
            actions.ToList().ForEach(action =>
            {
                if (!action.CanProcess(Board)) { return; }
                action.Process(Board);
            });
            Board.CleanService.CleanInactiveCards();
        }
    }
}
