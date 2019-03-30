using UnityEngine;
using UnityEngine.Networking;

public class UnitStats : NetworkBehaviour
{
    [SerializeField] private int _maxHealth;
    [SyncVar] private int _curHealth;

    public Stat Damage;

    public int CurHealth => _curHealth;

    public override void OnStartServer()
    {
        SetHealthRate(1);
    }

    public virtual void TakeDamage(int damage)
    {
        _curHealth -= damage;
        if (_curHealth <= 0)
        {
            _curHealth = 0;
        }
    }

    public void SetHealthRate(float rate)
    {
        _curHealth = rate == 0 ? 0 : (int)(_maxHealth * rate);
    }
}