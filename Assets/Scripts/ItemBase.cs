using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    #region Singleton
    public static ItemCollection collection;

    private void Awake()
    {
        if (collection != null)
        {
            if (_collectionLink != collection) Debug.LogError("More than one ItemCollection found!");
            return;
        }
        collection = _collectionLink;
    }
    #endregion

    [SerializeField] private ItemCollection _collectionLink;
    
    public static int GetItemId(Item item)
    {
        for (var i = 0; i < collection.items.Length; i++)
        {
            if (item == collection.items[i]) return i;
        }
        if (item != null) Debug.LogError($"Items {item.name} not found in ItemBase!");
        return -1;
    }

    public static Item GetItem(int id)
    {
        return id == -1 ? null : collection.items[id];
    }
}
