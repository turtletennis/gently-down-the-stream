using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{

    public CanvasGroup mainMenuPanel;
    public CanvasGroup shopPanel;    // Start is called before the first frame update
    
    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        
        mainMenuPanel.alpha = 1;
        
        shopPanel.alpha = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenShop()
    {
        Debug.Log("Hiding main menu, showing shop");
        mainMenuPanel.alpha = 0;
        
        shopPanel.alpha = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StreamGame");
    }
}
