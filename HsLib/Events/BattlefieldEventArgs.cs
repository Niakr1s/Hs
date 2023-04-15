using HsLib.Battle;

namespace HsLib.Events
{
    public class BattlefieldEventArgs : EventArgs
    {
        public BattlefieldEventArgs(Battlefield bf, EventArgs eventArgs)
        {
            Bf = bf;
            EventArgs = eventArgs;
        }

        public Battlefield Bf { get; }

        public EventArgs EventArgs { get; }
    }
}
