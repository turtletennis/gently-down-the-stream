using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BestTimeTextDisplay : MonoBehaviour
{

    private TMP_Text textComponent;
    int bestTime = -1;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bestTime<0 || PlayerStats.longestSurvivalTime > bestTime)
        {
            bestTime = PlayerStats.longestSurvivalTime;
            textComponent.SetText($"Longest ride time is {bestTime} seconds");
        }
    }
}
