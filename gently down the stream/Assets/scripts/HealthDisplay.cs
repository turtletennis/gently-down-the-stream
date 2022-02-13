using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float TotalHealth { get; set; }
    public TMP_Text HealthText;
    // Start is called before the first frame update
    void Start()
    {
        HealthText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Updating health");
        HealthText.text = string.Format("Health: {0,0:F1}/{1,0:F1}",CurrentHealth,TotalHealth);
    }
}
