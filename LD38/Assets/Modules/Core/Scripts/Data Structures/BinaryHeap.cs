﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeapType
{
    MinHeap,
    MaxHeap
}

public class BinaryHeap<T>
{
    List<T> _items;

    public HeapType _heapType { get; private set; }

    System.Func<T, T, int> _compareFunc;

    public T Root
    {
        get { return _items[0]; }
    }

    public BinaryHeap(HeapType type, System.Func<T,T,int> compareFunc)
    {
        _items = new List<T>();
        _heapType = type;
        _compareFunc = compareFunc;
    }

    public void Insert(T item)
    {
        _items.Add(item);

        int i = _items.Count - 1;

        bool _maxheap = true;
        if (_heapType == HeapType.MaxHeap)
        {
            _maxheap = false;
        }

        while (i > 0)
        {
            int i2 = (i - 1) / 2;
            if ((_compareFunc(_items[i], _items[i2]) > 0) ^ _maxheap)
            {
                T temp = _items[i];
                _items[i] = _items[i2];
                _items[i2] = temp;
                i = i2;
            }
            else
            {
                break;
            }
        }
    }

    public void DeleteRoot()
    {
        int i = _items.Count - 1;

        _items[0] = _items[i];
        _items.RemoveAt(i);

        i = 0;

        bool maxHeap = true;
        if (_heapType == HeapType.MaxHeap)
        {
            maxHeap = false;
        }
         
        while (true)
        {
            int leftInd = 2 * i + 1;
            int rightInd = 2 * i + 2;
            int largest = i;

            if (leftInd < _items.Count)
            {
                if ((_compareFunc(_items[leftInd],_items[largest]) > 0) ^ maxHeap)
                    largest = leftInd;
            }

            if (rightInd < _items.Count)
            {
                if ((_compareFunc(_items[rightInd],_items[largest]) > 0) ^ maxHeap)
                    largest = rightInd;
            }

            if (largest != i)
            {
                T temp = _items[largest];
                _items[largest] = _items[i];
                _items[i] = temp;
                i = largest;
            }
            else
            {
                break;
            }
        }
    }

    public T PopRoot()
    {
        T result = _items[0];

        DeleteRoot();

        return result;
    }
}