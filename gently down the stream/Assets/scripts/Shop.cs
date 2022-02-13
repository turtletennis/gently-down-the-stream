using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI Components")]
    public TMP_Text rudderButtonText;
    public TMP_Text moreHealthButtonText;
    public TMP_Text coinText;
    [Space(10)]
    [Header("Upgrades")]
    public int rudderCostIncrement = 10;
    public int healthCostPer10 = 100;
    public int reduceAccelerationCostIncrement = 200;
    [Space(10)]
    public float baseRudderPower = 0.5f;
    public float baseRudderAngleChange = 5;
    public int baseHealth = 100;
    public float baseAcceleration = 0.1f;

    private int RudderCost 
    { 
        get
        {
            return rudderCostIncrement * (ShopPurchases.rudderUpgrades + 1);
        } 
    }

    private int HealthCost
    {
        get
        {
            return healthCostPer10 * (ShopPurchases.healthUpgrades + 1);
        }
    }

    private int ReduceAccelerationCost
    {
        get
        {
            return reduceAccelerationCostIncrement * (ShopPurchases.accelerationUpgrades + 1);
        }
    }

    void Start()
    {
        UpdateButtonTexts();
        RecalculatePlayerStats();
    }

    void UpdateButtonTexts()
    {
        rudderButtonText.text = $"Better Rudder ({ShopPurchases.rudderUpgrades}) {RudderCost} coins";
        moreHealthButtonText.text = $"More health ({ShopPurchases.healthUpgrades}) {HealthCost} coins";

        coinText.SetText("Coins: " + PlayerStats.coins);
    }

    public void BuyRudderUpgrade()
    {
        if(PlayerStats.coins >= RudderCost)
        {
            PlayerStats.coins -= RudderCost;
            ShopPurchases.rudderUpgrades++;
        }
        RecalculatePlayerStats();
    }

    public void BuyHealthUpgrade()
    {
        if (PlayerStats.coins >= HealthCost)
        {
            PlayerStats.coins -= HealthCost;
            ShopPurchases.healthUpgrades++;
        }
        RecalculatePlayerStats();
    }

    void RecalculatePlayerStats()
    {
        PlayerStats.acceleration = baseAcceleration * Mathf.Pow(0.95f, ShopPurchases.accelerationUpgrades);
        PlayerStats.totalHealth = baseHealth + 10 * ShopPurchases.healthUpgrades;
        PlayerStats.steeringPower = baseRudderPower * Mathf.Pow(1.1f, ShopPurchases.rudderUpgrades);
        PlayerStats.steeringAngleChange = baseRudderAngleChange * Mathf.Pow(1.1f, ShopPurchases.rudderUpgrades);
        PlayerStats.initialised = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonTexts();
    }
}
