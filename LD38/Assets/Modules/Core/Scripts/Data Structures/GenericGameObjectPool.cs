using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericGameObjectPool
{
    private List<GameObject> _objects;

    private GameObject _prefab;

    private int _freeHeadIndex = -1;
    private int _count = -1;

    public int FreeCount
    {
        get { return _freeHeadIndex; }
    }

    public int UsedCount
    {
        get { return _count- _freeHeadIndex; }
    }

    public GenericGameObjectPool(GameObject prefab, int baseInstances, Transform parent)
    {
        _prefab = prefab;
        _freeHeadIndex = -1;
        _count = 0;

        _objects = new List<GameObject>(baseInstances);

        for (int a = 0; a < baseInstances; ++a)
        {
            AllocInstance(parent);
        }
    }

    private void AllocInstance(Transform parentTransform)
    {
        var newObj = GameObject.Instantiate(_prefab, parentTransform, false);
        newObj.gameObject.SetActive(false); 
        _freeHeadIndex++;
        _objects.Add(newObj);
    }

    public GameObject GetInstance(Transform parent)
    {
        if(_freeHeadIndex >= 0)
        {
            var obj = _objects[_freeHeadIndex];
            _freeHeadIndex--;
            return obj;
        }
        else
        {
            var newObj = GameObject.Instantiate<GameObject>(_prefab, parent,false);
            return newObj;
        }
    }

    public void ReleaseInstance(GameObject obj)
    {
        if(obj != null)
        {
            _freeHeadIndex++;
            if (_freeHeadIndex < _objects.Count)
            {
                _objects[_freeHeadIndex] = obj;
            }
            else
            {
                _objects.Add(obj);
            }
        }
    }

    public override string ToString()
    {
        return UsedCount+" / "+_count;
    }
}
