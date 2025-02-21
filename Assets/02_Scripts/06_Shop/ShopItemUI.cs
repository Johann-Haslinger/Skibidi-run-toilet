using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    private ShopItem currentItem;

    public void Setup(ShopItem item)
    {
        currentItem = item;
        itemNameText.text = item.itemName;
        priceText.text = item.price + " Coins";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => ShopManager.instance.BuyItem(currentItem));
    }
}
