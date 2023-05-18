using System.Collections.Specialized;

namespace HsLib.Types.Containers
{
    public class CollectionChangedRecorder
    {
        public CollectionChangedRecorder(INotifyCollectionChanged emitter)
        {
            _emitter = emitter;
        }

        private readonly INotifyCollectionChanged _emitter;

        private readonly List<(object?, NotifyCollectionChangedEventArgs)> _recorded = new();
        public List<(object?, NotifyCollectionChangedEventArgs)> Recorded => new(_recorded);

        public IEnumerable<object?> NewItems
        {
            get
            {
                foreach ((object? sender, NotifyCollectionChangedEventArgs e) in _recorded)
                {
                    if (e.NewItems is not null)
                    {
                        yield return e.NewItems;
                    }
                }
            }
        }

        public IEnumerable<object?> OldItems
        {
            get
            {
                foreach ((object? sender, NotifyCollectionChangedEventArgs e) in _recorded)
                {
                    if (e.OldItems is not null)
                    {
                        foreach (var oldItem in e.OldItems)
                        {
                            yield return oldItem;
                        }
                    }
                }
            }
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _recorded.Add((sender, e));
        }

        public void Start()
        {
            _recorded.Clear();
            _emitter.CollectionChanged += OnCollectionChanged;
        }

        public void Stop()
        {
            _emitter.CollectionChanged -= OnCollectionChanged;
        }

        /// <summary>
        /// Starts, runs action and then stops.
        /// </summary>
        /// <param name="action"></param>
        public void Record(Action action)
        {
            Start();
            action();
            Stop();
        }
    }
}
