using UnityEngine;

public class ShopElement : MonoBehaviour
{
    public Vector2 position; // Position, an der das Element erscheinen soll

    public virtual void OnSpawn()
    {
        transform.SetParent(GameObject.Find("Canvas").transform, false);
        GetComponent<RectTransform>().anchoredPosition = position;
        Debug.Log(gameObject.name + " wurde gespawnt!");
    }
}
