using HsLib.Types.GameIntents;

namespace HsLib.Systems
{
    public class IntentService : Service
    {
        public IntentService(Board board) : base(board)
        {
        }

        public event EventHandler<IntentEventArgs>? Intent;

        public void ProcessIntent(GameIntent intent)
        {
            if (!intent.CanBeProcessed()) { throw new ValidationException("can't be processed"); }

            IntentEventArgs intentEventArgs = new(intent);
            Intent?.Invoke(this, intentEventArgs);

            GameIntent? intentToProcess = intentEventArgs.Intent;
            if (intentToProcess is null) { return; }

            if (intentToProcess.CanBeProcessed())
            {
                var actions = intentToProcess.Process();
                if (actions is not null)
                {
                    Board.ActionService.ProcessActions(actions);
                }
            }
        }
    }

    public class IntentEventArgs : EventArgs
    {
        public IntentEventArgs(GameIntent intent)
        {
            Intent = intent;
        }

        /// <summary>
        /// Subscriber is free to change this intent
        /// </summary>
        public GameIntent? Intent { get; set; }
    }
}
