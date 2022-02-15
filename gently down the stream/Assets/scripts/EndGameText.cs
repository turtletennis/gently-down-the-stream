using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameText : MonoBehaviour
{
    private TMP_Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        Hide();
    }

    public void Hide()
    {
        textComponent.enabled = false;
    }

    public void Show()
    {
        textComponent.enabled = true;
    }

    public void Win(int seconds, float remainingHealth,bool newRecord)
    {
        string newRecordSuffix = "";
        if(newRecord)
        {
            newRecordSuffix = "New record! ";
        }
        //textComponent.text = string.Format("You won in {0} seconds, with {1,0F1} health remaining",seconds,remainingHealth);
        textComponent.text = string.Format("{0}You won in {1} seconds.", newRecordSuffix, seconds);
        Show();
    }

    public void Lose(int seconds, bool newRecord)
    {
        string newRecordSuffix = ".";
        if (newRecord)
        {
            newRecordSuffix = " New record! ";
        }
        textComponent.text = string.Format("{0}You lasted {1} seconds better luck next time.", newRecordSuffix, seconds);
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
