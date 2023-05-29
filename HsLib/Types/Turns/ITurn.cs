using HsLib.Types.Places;

namespace HsLib.Types.Turns
{
    public interface ITurn
    {
        int No { get; }
        Pid Pid { get; }

        event EventHandler<TurnEventArgs>? Event;

        bool IsActivePid(Pid pid);
        bool IsFirstTurn(int? turnAdded);
        bool IsStarted();
    }
}