using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    public static InventoryUI instance;

    private void Awake()
    {
        _inventoryUI.SetActive(false);
        if (instance != null)
        {
            Debug.LogError("More than one instance of InventoryUI found!");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private Transform _itemParent;
    [SerializeField] private InventorySlot _slotPrefab;

    private InventorySlot[] _slots;
    private Inventory _inventory;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
        }
    }

    public void SetInventory(Inventory newInventory)
    {
        _inventory = newInventory;
        _inventory.OnItemChanged += itemChanged;
        var childs = _itemParent.GetComponentsInChildren<InventorySlot>();
        foreach(var child in childs)
        {
            Destroy(child.gameObject);
        }

        _slots = new InventorySlot[_inventory.Space];
        for (int i = 0; i < _inventory.Space; i++)
        {
            _slots[i] = Instantiate(_slotPrefab, _itemParent);
            _slots[i].Inventory = _inventory;
            if (i < _inventory.items.Count) _slots[i].SetItem(_inventory.items[i]);
            else _slots[i].ClearSlot();
        }
    }

    private void ItemChanged(UnityEngine.Networking.SyncList<Item>.Operation op, int itemIndex)
    {
        for(var i = 0; i < _slots.Length; i++)
        {
            if (i < _inventory.items.Count) _slots[i].SetItem(_inventory.items[i]);
            else _slots[i].ClearSlot();
        }
    }
}
