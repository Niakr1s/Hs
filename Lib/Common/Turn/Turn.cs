namespace Models.Common.Turn
{
    public enum Status
    {
        PreGame,
        Running,
        EndGame,
    }

    public class Turn : IWithEvent<TurnEventArgs>
    {
        public Turn()
        {
            Status = Status.PreGame;
        }


        public Status Status { get; private set; }

        /// <summary>
        /// Starts with 1.
        /// 0 means game is not even started.
        /// </summary>
        public int TurnNo { get; private set; } = 0;

        public event EventHandler<TurnEventArgs>? Event;

        /// <summary>
        /// Current Pid.
        /// </summary>
        public Pid Pid
        {
            get => TurnNo switch
            {
                <= 0 => Pid.None,
                _ => TurnNo % 2 == 1 ? Pid.P1 : Pid.P2,
            };
        }

        public bool Next()
        {
            if (Status is Status.EndGame) { return false; }

            if (Status is Status.Running)
            {
                Event?.Invoke(this, new TurnEndEventArgs());
            }

            if (Status is Status.PreGame)
            {
                Status = Status.Running;
                Event?.Invoke(this, new TurnStatusChangedEventArgs());
            }

            TurnNo++;
            Event?.Invoke(this, new TurnStartEventArgs());

            return true;
        }

        public bool Stop()
        {
            if (Status is Status.EndGame or Status.PreGame) { return false; }

            Event?.Invoke(this, new TurnEndEventArgs());

            Status = Status.EndGame;
            TurnNo = -1;
            Event?.Invoke(this, new TurnStatusChangedEventArgs());

            return true;
        }
    }
}
