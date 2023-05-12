using HsLib.Common.MeleeAttack;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Common
{
    public class Turn : IWithEvent<TurnEventArgs>
    {
        /// <summary>
        /// Starts with 1.
        /// 0 means game is not even started.
        /// </summary>
        public int No { get; private set; } = 0;

        public bool IsStarted()
        {
            return No > 0;
        }

        public event EventHandler<TurnEventArgs>? Event;

        /// <summary>
        /// Current Pid.
        /// </summary>
        public Pid Pid
        {
            get => No switch
            {
                <= 0 => Pid.None,
                _ => No % 2 == 1 ? Pid.P1 : Pid.P2,
            };
        }

        public void Start()
        {
            if (IsStarted()) { return; }
            Next();
        }

        public void Next()
        {
            Event?.Invoke(this, new TurnEndEventArgs());
            No++;
            Event?.Invoke(this, new TurnStartEventArgs());
        }

        public void Skip(Pid pid)
        {
            if (pid == Pid.None) throw new Exception("pid is None");

            do
            {
                Next();
            }
            while (Pid != pid);
        }

        public void Skip(int turns)
        {
            while (turns-- > 0)
            {
                Next();
            }
        }

        public bool IsFirstTurn(int turnAdded)
        {
            return turnAdded == No;
        }

        public bool IsActivePid(Pid pid)
        {
            return pid == Pid;
        }
    }
}
