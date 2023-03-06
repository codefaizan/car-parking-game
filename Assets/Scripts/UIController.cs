using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    GameObject splashScreen;
    public Image healthBar;
    public GameObject levelWonPanel;
    internal bool levelWon;
    public GameObject levelFailedPanel;
    bool levelFailed;


    private void Start()
    {
        splashScreen = GameObject.Find("Splash Screen");
        healthBar = GameObject.Find("Healthbar").GetComponent<Image>();
        levelWonPanel = GameObject.Find("Level Completed Panel");
        levelFailedPanel = GameObject.Find("Level Failed Panel");
        Invoke("DisableLoadingPanel", 2f);

        levelWonPanel.SetActive(false);
        levelFailedPanel.SetActive(false);

    }

    public void DisableLoadingPanel()
    {
        print("garage loaded");
        splashScreen.SetActive(false);
    }

    private void Update()
    {

        if (healthBar.fillAmount == 0f && !levelFailed)
        {
            levelFailedPanel.SetActive(true);
            levelFailed = true;
        }

        if (levelWon)
        {
            levelWonPanel.SetActive(true);
            levelWon = false;
        }

    }

    
}
