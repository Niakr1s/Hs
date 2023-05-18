namespace HsLib.Functions
{
    internal static class Do
    {
        /// <summary>
        /// Does action once, till when predicate pass.
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="sub">example: h => bf.Turn.Event += h</param>
        /// <param name="unsub">example: h => bf.Turn.Event -= h</param>
        /// <param name="predicate"></param>
        /// <param name="action"></param>
        public static void Once<TEventArgs>(Action<EventHandler<TEventArgs>> sub, Action<EventHandler<TEventArgs>> unsub,
            Predicate<TEventArgs> predicate, Action action)
            where TEventArgs : EventArgs
        {
            void Subscription(object? sender, TEventArgs e)
            {
                if (predicate(e))
                {
                    action();
                    unsub(Subscription);
                }
            }
            sub(Subscription);
        }
    }
}
