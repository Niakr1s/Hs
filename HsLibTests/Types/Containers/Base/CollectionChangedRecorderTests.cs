using HsLib.Types.Containers.Base;
using System.Collections.ObjectModel;

namespace HsLibTests.Types.Containers.Base
{
    [TestClass()]
    public class CollectionChangedRecorderTests
    {
        private CollectionChangedRecorder _recorder = null!;
        private ObservableCollection<int> _collection = null!;

        [TestInitialize()]
        public void Setup()
        {
            _collection = new ObservableCollection<int>();
            _recorder = new(_collection);

            Assert.AreEqual(0, _recorder.OldItems.Count());
            Assert.AreEqual(0, _recorder.NewItems.Count());
        }

        [TestMethod()]
        public void StartStopTest()
        {
            _recorder.Start();

            Assert.AreEqual(0, _recorder.Recorded.Count);
            Assert.AreEqual(0, _recorder.OldItems.Count());
            Assert.AreEqual(0, _recorder.NewItems.Count());

            _collection.Add(0);

            Assert.AreEqual(1, _recorder.Recorded.Count);
            Assert.AreEqual(0, _recorder.OldItems.Count());
            Assert.AreEqual(1, _recorder.NewItems.Count());

            _collection[0] = 1;
            Assert.AreEqual(2, _recorder.Recorded.Count);
            Assert.AreEqual(1, _recorder.OldItems.Count());
            Assert.AreEqual(2, _recorder.NewItems.Count());

            _collection.RemoveAt(0);
            Assert.AreEqual(3, _recorder.Recorded.Count);
            Assert.AreEqual(2, _recorder.OldItems.Count());
            Assert.AreEqual(2, _recorder.NewItems.Count());

            _recorder.Stop();
            Assert.AreEqual(3, _recorder.Recorded.Count);
            Assert.AreEqual(2, _recorder.OldItems.Count());
            Assert.AreEqual(2, _recorder.NewItems.Count());
        }

        [TestMethod()]
        public void RecordTest()
        {
            _recorder.Record(() =>
            {
                _collection.Add(0);
                _collection[0] = 1;
                _collection.RemoveAt(0);
            });

            Assert.AreEqual(3, _recorder.Recorded.Count);
            Assert.AreEqual(2, _recorder.OldItems.Count());
            Assert.AreEqual(2, _recorder.NewItems.Count());

            _recorder.Record(() => { });
            Assert.AreEqual(0, _recorder.Recorded.Count);
            Assert.AreEqual(0, _recorder.OldItems.Count());
            Assert.AreEqual(0, _recorder.NewItems.Count());
        }
    }
}