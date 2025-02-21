using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public Transform shopPanel;
    public GameObject shopItemPrefab;
    public List<ShopItem> availableItems = new List<ShopItem>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateShopUI();
    }

    public void BuyItem(ShopItem item)
    {
        if (CoinManager.instance.score >= item.price)
        {
            CoinManager.instance.AddScore(-item.price);
            item.OnPurchase();
            availableItems.Remove(item);
            UpdateShopUI();
        }
        else
        {
            Debug.Log("Nicht genug Coins!");
        }
    }

    private void UpdateShopUI()
    {
        foreach (Transform child in shopPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Mathf.Min(3, availableItems.Count); i++)
        {
            ShopItem item = availableItems[i];
            GameObject shopItemGO = Instantiate(shopItemPrefab, shopPanel);
            shopItemGO.GetComponent<ShopItemUI>().Setup(item);
        }
    }
}
