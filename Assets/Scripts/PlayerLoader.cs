using UnityEngine;
using UnityEngine.Networking;

public class PlayerLoader : NetworkBehaviour
{
    [SerializeField] GameObject _unitPrefab;
    [SerializeField] PlayerController _controller;

    [SyncVar(hook = nameof(HookUnitIdentity))] NetworkIdentity _unitIdentity;
    
    public override void OnStartAuthority()
    {
        if (isServer)
        {
            SpawnPlayer(true);
        }
        else
        {
            CmdCreatePlayer();
        }
    }

    [Command]
    public void CmdCreatePlayer()
    {
        SpawnPlayer(false);
    }

    [ClientCallback]
    private void HookUnitIdentity(NetworkIdentity unit)
    {
        if (!isLocalPlayer) return;
        _unitIdentity = unit;

        Character character = unit.GetComponent<Character>();
        _controller.SetCharacter(character, true);
        InventoryUI.instance.SetInventory(character.inventory);
    }

    public void SpawnPlayer(bool isLocalPlayer)
    {
        var unit = Instantiate(_unitPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(unit);
        _unitIdentity = unit.GetComponent<NetworkIdentity>();
        _controller.SetCharacter(unit.GetComponent<Character>(), isLocalPlayer);

        Character character = unit.GetComponent<Character>();
        InventoryUI.Instance.SetInventory(character.inventory);
    }
}