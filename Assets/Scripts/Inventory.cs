using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour
{
    public Transform DropPoint;
    public int Space = 20;
    public event SyncList<Item>.SyncListChanged OnItemChanged;

    public SyncListItem items = new SyncListItem();

    public override void OnStartLocalPlayer()
    {
        items.Callback += OnItemChanged;
    }

    private void ItemChanged(SyncList<Item>.Operation op, int itemIndex)
    {
        OnItemChanged?.Invoke(op, itemIndex);
    }

    public bool Add(Item item)
    {
        if(items.Count < Space)
        {
            items.Add(item);
            return true;
        }
        return false;
    }

    public void Remove(Item item)
    {
        CmdRemoveItem(items.IndexOf(item));
    }

    [Command]
    private void CmdRemoveItem(int index)
    {
        if (items[index] == null) return;
        Drop(items[index]);
        items.RemoveAt(index);
    }

    private void Drop(Item item)
    {
        var pickupItem = Instantiate(item.pickupPrefab, DropPoint.position, Quaternion.identity);
        pickupItem.item = item;
        NetworkServer.Spawn(pickupItem.gameObject);
    }
}
