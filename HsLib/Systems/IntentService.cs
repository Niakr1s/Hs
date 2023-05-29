using HsLib.Types.GameIntents;

namespace HsLib.Systems
{
    internal class IntentService : Service
    {
        public IntentService(Board board) : base(board)
        {
        }

        public event EventHandler<IntentEventArgs>? Intent;

        public void ProcessIntent(GameIntent intent)
        {
            IntentEventArgs intentEventArgs = new(intent);
            Intent?.Invoke(this, intentEventArgs);

            GameIntent? intentToProcess = intentEventArgs.Intent;
            if (intentToProcess is null) { return; }

            if (intentToProcess.Actor.CanProcessIntent(intentToProcess))
            {
                var actions = intentToProcess.Actor.ProcessIntent(intentToProcess);
                if (actions is not null)
                {
                    Board.ActionService.ProcessActions(actions);
                }
            }
        }
    }

    internal class IntentEventArgs : EventArgs
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
