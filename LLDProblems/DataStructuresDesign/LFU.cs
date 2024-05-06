public class LFU
{
    private int _capacity;
    SortedDictionary<int, LinkedList<DataModel>> _freqLookup;
    Dictionary<int, LinkedListNode<DataModel>> _keyAddressLookup;

    public LFU(int capacity)
    {
        _capacity = capacity;
        _freqLookup = new SortedDictionary<int, LinkedList<DataModel>>();
        _keyAddressLookup = new Dictionary<int, LinkedListNode<DataModel>>();
    }

    public int Get(int key)
    {
        if (!_keyAddressLookup.ContainsKey(key))
            return -1;

        MoveKeyToDifferenctFreqBucket(key);
        var cacheData = _keyAddressLookup[key];
        return cacheData.Value.Value;
    }

    public void Put(int key, int value)
    {
        //when key is alredy present
        if (_keyAddressLookup.ContainsKey(key))
        {
            var cacheData = _keyAddressLookup[key];
            cacheData.Value.Value = value;
            MoveKeyToDifferenctFreqBucket(key);
            return;
        }

        //when key is not present and the data will be stored first time

        //Delete least frequently user and least recently used data.
        if (_keyAddressLookup.Count == _capacity)
        {
            var dictData = _freqLookup.First();
            var cachedData = dictData.Value.Last();
            dictData.Value.Remove(cachedData);
            int frq = cachedData.Freq;

            if (_freqLookup[frq].Count == 0)
                _freqLookup.Remove(frq);

            _keyAddressLookup.Remove(cachedData.Key);
        }

        var cacheObject = new DataModel(key, value, 1);
        if (!_freqLookup.ContainsKey(1))
            _freqLookup[1] = new LinkedList<DataModel>();

        var nodeRef = _freqLookup[1].AddFirst(cacheObject);
        _keyAddressLookup.Add(key, nodeRef);
    }

    private void MoveKeyToDifferenctFreqBucket(int key)
    {
        var cachedData = _keyAddressLookup[key];
        int oldFreq = cachedData.Value.Freq;
        int newFreq = oldFreq + 1;

        _freqLookup[oldFreq].Remove(cachedData);
        if (_freqLookup[oldFreq].Count == 0)
            _freqLookup.Remove(oldFreq);

        if (!_freqLookup.ContainsKey(newFreq))
            _freqLookup[newFreq] = new LinkedList<DataModel>();

        cachedData.Value.Freq = newFreq;
        _freqLookup[newFreq].AddFirst(cachedData);
    }

    class DataModel
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public int Freq { get; set; }

        public DataModel(int key, int value, int freq)
        {
            Key = key;
            Value = value;
            Freq = freq;
        }
    }
}