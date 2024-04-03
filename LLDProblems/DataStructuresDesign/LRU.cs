using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProblems.DataStructuresDesign
{
    public class LRU
    {
        private int _size;
        private int _currCount;
        private Dictionary<string, CacheNode> _keyValueStore;
        private LinkedList<CacheNode> _deQueue;

        public LRU(int size)
        {
            _currCount = 0;
            _size = size;
            _keyValueStore = new Dictionary<string, CacheNode>();
            _deQueue = new LinkedList<CacheNode>();
        }

        public string GetValue(string key)
        {
            if (!_keyValueStore.ContainsKey(key))
                return "key doesn't exist!";

            CacheNode requestedObj = _keyValueStore[key];
            _deQueue.Remove(requestedObj);
            _deQueue.AddLast(requestedObj);
            return requestedObj._value;
        }

        public void AddKeyValue(string key, string value)
        {
            if (_keyValueStore.ContainsKey(key))
            {
                CacheNode existingNode = _keyValueStore[key];
                _deQueue.Remove(existingNode);
                _deQueue.AddLast(existingNode);
                return;
            }

            if(_currCount == _size)
            {
                CacheNode leastUsedNode = _deQueue.First();
                _keyValueStore.Remove(leastUsedNode._key);
                _deQueue.Remove(leastUsedNode);

                CacheNode newNode = new CacheNode(key, value);
                _deQueue.AddLast(newNode);
                _keyValueStore.Add(key, newNode);
                return;
            }

            ++_currCount;
            CacheNode node = new CacheNode(key, value);
            _deQueue.AddLast(node);
            _keyValueStore.Add(key, node);

        }

    }

    class CacheNode
    {
        public string _key, _value;
        public CacheNode(string key, string value)
        {
            _key = key;
            _value = value;
        }
    }
}

/*
 This is generally used in Caching strategy, in which the value which is used long back is removed and the latest value is pushed back.


 Actions
 - Get value based on the provided key
 - Store key value information
    - If inserted first time and space is avalable simple insert
    - If inserted first time and space is not available
    - If already present, mark it as latest used 
*/
