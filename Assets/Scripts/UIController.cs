using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{
    GameObject splashScreen;
    GameObject pauseScreen;
    GameObject levelWonPanel;
    GameObject levelFailedPanel;
    //internal Image healthBar;
    public SpriteRenderer parkingSpot;
    Image parkLoadingSign;
    TMP_Text healthCounter;
    TMP_Text timer;
    internal bool levelWon;
    bool levelFailed;

    GameController gameController;
    Levels levelConfig;
    private void Start()
    {
        // finding GameObject references
        splashScreen = GameObject.Find("Splash Screen");
        pauseScreen = GameObject.Find("Pause Screen");
        parkingSpot = GameObject.Find("Parking Spot").GetComponent<SpriteRenderer>();
        parkLoadingSign = GameObject.Find("Park Loading").GetComponent<Image>();
        healthCounter = GameObject.Find("Health Text").GetComponent<TMP_Text>();
        timer = GameObject.Find("Timer Text").GetComponent<TMP_Text>();
        //healthBar = GameObject.Find("Healthbar").GetComponent<Image>();
        levelWonPanel = GameObject.Find("Level Completed Panel");
        levelFailedPanel = GameObject.Find("Level Failed Panel");
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        Invoke("DisableLoadingPanel", 2f);


        parkLoadingSign.transform.parent.gameObject.SetActive(false);
        levelWonPanel.SetActive(false);
        levelFailedPanel.SetActive(false);
        pauseScreen.SetActive(false);

        levelConfig = new Levels();
        int lifes = gameController.levels.Length - GameManager.levelID;
        levelConfig.SetDamage(lifes);
        healthCounter.text = "Car Health: " + (levelConfig.carHealth);
        timer.text = "00:" + levelConfig.secondsLeft;
        
    }
    // the health of car reduces when it has a collision
    public void GetDamage()
    {
        levelConfig.carHealth -= 1;
        healthCounter.text = " Car Health: " + levelConfig.carHealth;
    }

    public void DisableLoadingPanel()
    {
        splashScreen.SetActive(false);
        // start game timer when "splash screen" disappears
        StartCoroutine(Timer());
    }

    private void Update()
    {
        // check if car is parked
        if (parkLoadingSign.fillAmount >= 1f && !levelWon && !levelFailed)
        {
            levelWonPanel.SetActive(true);
            levelWon = true;
        }
        //if (healthBar.fillAmount == 0f && !levelFailed)
        //{
        //    levelFailedPanel.SetActive(true);
        //    levelFailed = true;
        //}

        // check if car is destroyed or time is ended
        if ((levelConfig.carHealth <= 0 || levelConfig.secondsLeft <= 0) && !levelFailed && !levelWon)
        {
            levelFailedPanel.SetActive(true);
            levelFailed = true;
        }
    }

    public void StartParking()
    {
        parkLoadingSign.transform.parent.gameObject.SetActive(true);
        parkingSpot.color = Color.green;
        parkLoadingSign.fillAmount += 0.7f * Time.deltaTime;
    }

    public void ExitParking()
    {
        parkingSpot.color = Color.white;
        parkLoadingSign.fillAmount = 0f;
        parkLoadingSign.transform.parent.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator Timer()
    {
        while (levelConfig.secondsLeft > 0)
        {
            levelConfig.secondsLeft -= 1;
            if (levelConfig.secondsLeft < 10)
                timer.text = "00:0" + levelConfig.secondsLeft;
            else
                timer.text = "00:" + levelConfig.secondsLeft;
            yield return new WaitForSeconds(1);
        }
    }
}
