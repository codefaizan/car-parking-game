using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject pauseScreen;
    public GameObject levelWonPanel;
    public GameObject levelFailedPanel;
    private SpriteRenderer parkingSpot;
    public Image parkLoadingSign;
    public TMP_Text healthCounter;
    public TMP_Text timerTextObj;
    public TMP_Text rewardTextObj;
    bool gameEnded;
    bool isInParking;
    float parkingTime;
    int levelTime;
    int carHealth;
    int reward;
    //GameController gameController;
    //Levels levelConfig;
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
        parkingSpot = GameObject.Find("Parking Spot").GetComponent<SpriteRenderer>();
        //gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        Invoke("DisableLoadingPanel", 0.5f);
        


        parkLoadingSign.transform.parent.gameObject.SetActive(false);
        levelWonPanel.SetActive(false);
        levelFailedPanel.SetActive(false);
        pauseScreen.SetActive(false);

        healthCounter.text = "Car Health: " + carHealth;
        timerTextObj.text = "00:" + levelTime;
    }
    public void InitializeUI(Levels _curLevel)
    {
        parkingTime = _curLevel.carParkTime;
        levelTime = _curLevel.secondsLeft;
        carHealth = _curLevel.carHealth;
        reward = _curLevel.reward;
    }
    

    public void UpdateCarDamage(int _damageVal)
    {
        // the health of car reduces when it has a collision
        healthCounter.text = " Car Health: " + _damageVal;
    }
    public void DisableLoadingPanel()
    {
        loadingScreen.SetActive(false);
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

        if (_completed)
        {
            rewardTextObj.text = "Reward: " + reward + " gold";
            GameController.instance.OnGameWin();
        }
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
            if (levelTime >= 10)
                timerTextObj.text = "00:" + levelTime;
            else if (levelTime < 10 && levelTime > 0)
                timerTextObj.text = "00:0" + levelTime;
            else
                OnGameEnd(false);

            yield return new WaitForSeconds(1);
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }
}
