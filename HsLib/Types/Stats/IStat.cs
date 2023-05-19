namespace HsLib.Types.Stats
{
    public interface IStat
    {
        event EventHandler<StatChangedEventArgs>? StatChanged;

        void Reset();
    }
}