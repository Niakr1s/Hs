using HsLib.Systems;

namespace HsLib.Types.Events
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
