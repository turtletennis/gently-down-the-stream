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
    public TMP_Text oarsButtonText;
    [Space(10)]
    [Header("Upgrades")]
    public int rudderCostIncrement = 10;
    public int healthCostPer10 = 20;
    public int reduceAccelerationCostIncrement = 200;
    public int oarPowerCostIncrement = 50;
    [Space(10)]
    public float baseRudderPower = 0.5f;
    public float baseRudderAngleChange = 5;
    public int baseHealth = 100;
    public float baseAcceleration = 0.1f;
    public float baseOarPower = 0;

    private CanvasGroup shopCanvas;


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

    private int OarsCost
    {
        get
        {
            return oarPowerCostIncrement * (ShopPurchases.oarsUpgrades + 1);
        }
    }

    void Start()
    {
        UpdateButtonTexts();
        RecalculatePlayerStats();
        shopCanvas = GetComponent<CanvasGroup>();
    }

    private bool ShopIsActive
    {
        get
        {
            return shopCanvas.alpha > 0;
        }
    }

    void UpdateButtonTexts()
    {
        rudderButtonText.text = $"Better Rudder ({ShopPurchases.rudderUpgrades}) {RudderCost} coins";
        moreHealthButtonText.text = $"More health ({ShopPurchases.healthUpgrades}) {HealthCost} coins";

        string oarsText = "Oars";
        if(ShopPurchases.oarsUpgrades<3)
        {
            oarsText = "Oars";
        }
        else if(ShopPurchases.oarsUpgrades<8)
        {
            oarsText = "Steam power";
        }
        else if(ShopPurchases.oarsUpgrades<15)
        {
            oarsText = "Petrol engine";
        }
        else
        {
            oarsText = "jet engine";
        }
        oarsButtonText.text = $"{oarsText} ({ShopPurchases.oarsUpgrades}) {OarsCost} coins";
        
    }



    public void BuyRudderUpgrade()
    {
        if(ShopIsActive && PlayerStats.coins >= RudderCost)
        {
            PlayerStats.coins -= RudderCost;
            ShopPurchases.rudderUpgrades++;
        }
        RecalculatePlayerStats();
    }

    public void BuyHealthUpgrade()
    {
        if (ShopIsActive && PlayerStats.coins >= HealthCost)
        {
            PlayerStats.coins -= HealthCost;
            ShopPurchases.healthUpgrades++;
        }
        RecalculatePlayerStats();
    }

    public void BuyOarsUpgrade()
    {
        if (ShopIsActive && PlayerStats.coins >= OarsCost)
        {
            PlayerStats.coins -= OarsCost;
            ShopPurchases.oarsUpgrades++;
        }
        RecalculatePlayerStats();
    }

    void RecalculatePlayerStats()
    {
        PlayerStats.acceleration = baseAcceleration * Mathf.Pow(0.95f, ShopPurchases.accelerationUpgrades);
        PlayerStats.totalHealth = baseHealth + 10 * ShopPurchases.healthUpgrades;
        PlayerStats.steeringPower = baseRudderPower * Mathf.Pow(1.1f, ShopPurchases.rudderUpgrades);
        PlayerStats.steeringAngleChange = baseRudderAngleChange * Mathf.Pow(1.1f, ShopPurchases.rudderUpgrades);
        PlayerStats.oarPower = baseOarPower + 0.1f * ShopPurchases.oarsUpgrades;
        PlayerStats.initialised = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonTexts();
    }
}
