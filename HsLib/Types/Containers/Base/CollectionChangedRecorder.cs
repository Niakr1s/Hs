using System.Collections.Specialized;

namespace HsLib.Types.Containers.Base
{
    public class CollectionChangedRecorder
    {
        public CollectionChangedRecorder(INotifyCollectionChanged emitter)
        {
            _emitters = emitter;
        }

        private readonly INotifyCollectionChanged _emitters;

        private readonly List<(object?, NotifyCollectionChangedEventArgs)> _recorded = new();
        public List<(object?, NotifyCollectionChangedEventArgs)> Recorded => new(_recorded);

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _recorded.Add((sender, e));
        }

        public void Start()
        {
            _recorded.Clear();
            _emitters.CollectionChanged += OnCollectionChanged;
        }

        public void Stop()
        {
            _emitters.CollectionChanged += OnCollectionChanged;
        }
    }
}
