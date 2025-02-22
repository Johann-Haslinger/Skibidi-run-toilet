using UnityEngine;

public class ShopElement : MonoBehaviour
{

    public virtual void OnSpawn()
    {
        transform.SetParent(GameObject.Find("Canvas").transform, false);
        Debug.Log(gameObject.name + " wurde gespawnt!");
    }
}
