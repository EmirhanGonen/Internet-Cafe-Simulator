using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSytem : MonoBehaviour
{
    public static ShopSytem instance;
    public float currentCash;
    [SerializeField] float UpgradeWallsPrice;
    [SerializeField] Transform DeliveryArea;
    [SerializeField] TMP_Text currentCashText;
    [SerializeField] GameObject delivery;
    void Awake()
    {
        instance = this;
    }
    public void OpenShop(GameObject shopPanel)
    {
        if (!shopPanel.activeSelf) { shopPanel.SetActive(true); SetCashText("$ " + currentCash); return; }
        shopPanel.SetActive(false);
        SetCashText("");
    }
    void SetCashText(string stringVar)
    {
        currentCashText.SetText(stringVar);
    }
    public void BuyItem(ItemSo itemSo)
    {   
        if (currentCash < itemSo.Basecost) return;
        currentCash -= itemSo.Basecost;
        SetCashText("$ " + currentCash);
        GameObject purschasedItem = Instantiate(delivery, DeliveryArea.position, Quaternion.identity);
        purschasedItem.GetComponent<Delivery>().Object = itemSo.purchasedObject;
    }
    public void UpgradeWalls(GameObject UpgradingWalls)
    {
        if (currentCash < UpgradeWallsPrice) return;
        UpgradingWalls.SetActive(true);
    }
    public void setActiveWall(GameObject DestroyedWalls)
    {
        DestroyedWalls.SetActive(false);
    }
}
