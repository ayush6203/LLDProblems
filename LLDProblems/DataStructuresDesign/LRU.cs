using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LRUCache
{
    private int _size;
    private int _currCount;
    private Dictionary<int, LinkedListNode<CacheNode>> _keyValueStore;
    private LinkedList<CacheNode> _deQueue;

    public LRUCache(int capacity)
    {
        _currCount = 0;
        _size = capacity;
        _keyValueStore = new Dictionary<int, LinkedListNode<CacheNode>>();
        _deQueue = new LinkedList<CacheNode>();
    }

    public int Get(int key)
    {
        if (!_keyValueStore.ContainsKey(key))
            return -1;

        LinkedListNode<CacheNode> node = _keyValueStore[key];
        MoveNodeToFirst(node);
        return node.Value._value; ;
    }

    public void Put(int key, int value)
    {
        if (_keyValueStore.ContainsKey(key))
        {
            LinkedListNode<CacheNode> node = _keyValueStore[key];
            node.Value._value = value;

            MoveNodeToFirst(node);
            return;
        }

        ++_currCount;
        LinkedListNode<CacheNode> node1 = _deQueue.AddFirst(new CacheNode(key, value));
        _keyValueStore.Add(key, node1);

        if (_currCount > _size)
        {
            --_currCount;
            LinkedListNode<CacheNode> node = _deQueue.Last;
            _keyValueStore.Remove(node.Value._key);
            _deQueue.Remove(node);
        }
    }

    private void MoveNodeToFirst(LinkedListNode<CacheNode> node)
    {
        _deQueue.Remove(node);
        _deQueue.AddFirst(node);
    }

    class CacheNode
    {
        public int _key, _value;
        public CacheNode(int key, int value)
        {
            _key = key;
            _value = value;
        }
    }
}

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */