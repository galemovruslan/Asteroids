using UnityEngine;

public abstract class PoolItem : MonoBehaviour
{
    public Pool LinkedPool { get; set; }

    public bool IsActive{ get => this.isActiveAndEnabled; }

    public void Activate(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ReturnToPool()
    {
        LinkedPool.ReturnToPool(this);
    }

}
