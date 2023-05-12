using HsLib.Common.MeleeAttack;

namespace HsLib.Reactive
{
    internal static class Do
    {
        public static void Once<TEventArgs>(IWithEvent<TEventArgs> source, Predicate<TEventArgs> predicate, Action action)
            where TEventArgs : EventArgs
        {
            void Sub(object? sender, TEventArgs e)
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
