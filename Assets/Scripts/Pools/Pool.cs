using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour 
{
    [SerializeField] private PoolItem _prototipe;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private Vector3 _waitPosition = new Vector3(200, 200, 0);

    private List<PoolItem> _pool;

    private GameObject _itemParent;
    private readonly string _parentName = "Pooled Items";

    private void Awake()
    {
        _pool = new List<PoolItem>(_poolSize);
        RepopulatePool();
    }

    public PoolItem GetItem()
    {
        foreach (var item in _pool)
        {
            if (!item.IsActive)
            {
                item.Activate(true);
                return item;
            }
        }

        PoolItem newObject = CreateItem();
        ExpandPool(newObject);
        newObject.Activate(true);
        return newObject;
    }

    public void ReturnToPool(PoolItem returnItem)
    {
        returnItem.Activate(false);
        returnItem.transform.position = _waitPosition;
    }

    public void ResetContents()
    {
        foreach (var item in _pool)
        {
            ReturnToPool(item);
        }
    }

    private void RepopulatePool()
    {
        _itemParent = GameObject.Find(_parentName);
        if (_itemParent == null)
        {
            _itemParent = new GameObject(_parentName);
        }

        for (int i = 0; i < _poolSize; i++)
        {
            ExpandPool(CreateItem());
        }
    }

    private PoolItem CreateItem()
    {
        PoolItem newObject = Instantiate<PoolItem>(_prototipe, _waitPosition, Quaternion.identity, _itemParent.transform);
        newObject.LinkedPool = this;
        newObject.Activate(false);
        return newObject;
    } 

    private void ExpandPool(PoolItem newObject)
    {
        _pool.Add(newObject);
    }



}
