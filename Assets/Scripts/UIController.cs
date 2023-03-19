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
    bool gameEnded;
    bool isInParking;
    float parkingTime;
    int levelTime;
    GameController gameController;
    Levels levelConfig;
    public static UIController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
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
    public void InitializUI(Levels _curLevel)
    {
        parkingTime = _curLevel.carParkTime;
        levelTime = _curLevel.secondsLeft;
    }
    // the health of car reduces when it has a collision
    public void GetDamage()
    {
        levelConfig.carHealth -= 1;
        healthCounter.text = " Car Health: " + levelConfig.carHealth;
    }
    public void UpdateCarDamage(int _damageVal)
    {
        healthCounter.text = " Car Health: " + _damageVal;

    }
    public void DisableLoadingPanel()
    {
        splashScreen.SetActive(false);
        // start game timer when "splash screen" disappears
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (gameEnded)
            return;

        if (isInParking)
        {
            parkLoadingSign.fillAmount += (Time.deltaTime / parkingTime);
            if (parkLoadingSign.fillAmount >= 1f)
            {
                OnGameEnd(true);
            }
        }

    }
    public void OnGameEnd(bool _completed)
    {
        gameEnded = true;
        levelWonPanel.SetActive(_completed);
        levelFailedPanel.SetActive(!_completed);
    }
    public void StartParking()
    {
        parkLoadingSign.transform.parent.gameObject.SetActive(true);
        parkingSpot.color = Color.green;
        isInParking = true;
    }

    public void ExitParking()
    {
        parkingSpot.color = Color.white;
        parkLoadingSign.fillAmount = 0f;
        parkLoadingSign.transform.parent.gameObject.SetActive(false);
        isInParking = false;
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
        while (levelTime > 0 && !gameEnded)
        {
            levelTime -= 1;
            if (levelTime < 10)
                timer.text = "00:0" + levelConfig.secondsLeft;
            else
                timer.text = "00:" + levelConfig.secondsLeft;
            yield return new WaitForSeconds(1);
        }
    }
}
