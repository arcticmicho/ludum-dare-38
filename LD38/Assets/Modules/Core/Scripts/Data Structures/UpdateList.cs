using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateList<T>
{
    private T[] _elements;
    private int _size;
    private int _sizeAlloc;

    public int Count
    {
        get { return _size; }
    }

    public T[] Elements 
    {
        get { return _elements; }
    }

    public UpdateList(int initialCount = 100, int sizeAlloc = 100)
    {
        _elements = new T[initialCount];
        _sizeAlloc = sizeAlloc;
        _size = 0;
    }

    public void Add(T element)
    {
        if(_elements.Length - 1 < _size)
        {
            Array.Resize(ref _elements, _size + _sizeAlloc);
        }

        _size++;
        _elements[_size - 1] = element;
    }

    public int RemoveAt(int a)
    {
        _elements[a] = _elements[_size - 1];
        _size--;
        a--;
        return a; 
    }

}
