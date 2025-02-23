using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image itemImage;
    public Button buyButton;
    public Outline itemOutline;

    private ShopItem currentItem;

    private void OnEnable()
    {
        CoinManager.instance.OnScoreChanged += UpdateItemUI;
    }

    private void OnDisable()
    {
        if (CoinManager.instance != null)
            CoinManager.instance.OnScoreChanged -= UpdateItemUI;
    }

    public void Setup(ShopItem item)
    {
        currentItem = item;
        itemImage.sprite = item.itemImage;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => ShopManager.instance.BuyItem(currentItem));

        UpdateItemUI();
    }

    private void UpdateItemUI()
    {
        if (CoinManager.instance.score >= currentItem.price)
        {

            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);
        }
        else
        {

            itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0.1f);
        }
    }
}
