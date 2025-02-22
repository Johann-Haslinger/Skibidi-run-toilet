using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;
    public GameObject elementPrefab; // Das Element, das gespawnt wird
    public bool canBePurchasedMultipleTimes = false; // Legt fest, ob das Item mehrmals gekauft werden kann
    public Sprite itemImage;

    public virtual void OnPurchase()
    {
        if (elementPrefab != null)
        {
            GameObject spawnedElement = Instantiate(elementPrefab);
            spawnedElement.GetComponent<ShopElement>()?.OnSpawn();
        }
        else
        {
            Debug.LogWarning("Kein Element zum Spawnen angegeben!");
        }
    }
}
