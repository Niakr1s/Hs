using Models.Common;

namespace Models.Reactive
{
    public static class Do
    {
        public static void Once<TEventArgs>(IWithEvent<TEventArgs> source, Predicate<EventArgs> predicate, Action action)
            where TEventArgs : EventArgs
        {
            void Sub(object? sender, EventArgs e)
            {
                if (predicate(e))
                {
                    action();
                    source.Event -= Sub;
                }
            }
            source.Event += Sub;
        }
    }
}
