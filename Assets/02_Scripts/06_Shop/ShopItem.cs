using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int price;
    public GameObject elementPrefab; // Das Element (z. B. Bild oder Video), das gespawnt wird

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
