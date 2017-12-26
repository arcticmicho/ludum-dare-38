using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PriorityQueueType
{
    Min,
    Max
}

public class PriorityQueue<T> : IEnumerable<T>
{
    private List<T> _elements = new List<T>();
    private List<double> _priorities = new List<double>();

    private PriorityQueueType _type = PriorityQueueType.Min;

    #region Get/Set

    public int Count
    {
        get { return _elements.Count; }
    }

    public List<T> Elements
    {
        get{return _elements;}
    }

    #endregion
    public PriorityQueue(PriorityQueueType type)
    {
        _type = type;
    }

    public void Clear()
    {
        _elements.Clear();
        _priorities.Clear();
    }

    public void Enqueue(T item, double priority)
    {
        if(_type == PriorityQueueType.Min)
        {
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                if (priority < _priorities[i])
                {
                    _elements.Insert(i+1, item);
                    _priorities.Insert(i+1, priority);
                    return;
                }
            }
            _elements.Insert(0, item);
            _priorities.Insert(0, priority);
        }
        else
        {
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                if (priority > _priorities[i])
                {
                    _elements.Insert(i+1, item);
                    _priorities.Insert(i+1, priority);
                    return;
                }
            }
            _elements.Insert(0, item);
            _priorities.Insert(0, priority);
        }
    }

    public T Peek()
    {
        if (_elements.Count > 0)
        {
            return _elements[_elements.Count - 1];
        }
        return default(T);
    }

    public T Dequeue()
    {
        if (_elements.Count > 0)
        {
            int index = (_elements.Count - 1);

            var element = _elements[index];
            _elements.RemoveAt(index);
            _priorities.RemoveAt(index);
            return element;
        }
        return default(T);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
