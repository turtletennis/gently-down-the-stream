using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextDisplay : MonoBehaviour
{
    private TMP_Text textComponent;
    int currentCoins=-1;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCoins<0 || currentCoins!=PlayerStats.coins)
        {
            currentCoins = PlayerStats.coins;
            textComponent.SetText("Coins: " + currentCoins);
            
        }
    }
}
