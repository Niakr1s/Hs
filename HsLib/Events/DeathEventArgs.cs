using HsLib.Common.Interfaces;

namespace HsLib.Events
{
    public abstract class DeathEventArgs : EventArgs
    {
    }

    public class DeathMortalDiedEventArgs : BattleEventArgs
    {
        public DeathMortalDiedEventArgs(IMortal mortal)
        {
            Mortal = mortal;
        }

        public IMortal Mortal { get; }
    }
}
