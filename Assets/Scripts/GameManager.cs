using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int carID;
    public static int levelID;
    static GameManager instance;
    // UI Panels
    public GameObject splashScreen;
    public GameObject carsMenuPanel;
    public GameObject mainMenuPanel;
    public GameObject levelsScreen;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //Invoke("DisableLoadingPanel", 2f);
        carsMenuPanel.SetActive(false);
        levelsScreen.SetActive(false);
        DisableLoadingPanel();
    }

    public void DisableLoadingPanel()
    {
        splashScreen.SetActive(false);
    }

    public void EnableCarsMenu()
    {
        mainMenuPanel.SetActive(false);
        carsMenuPanel.SetActive(true);
    }

    public void EnableMainMenu()
    {
        mainMenuPanel.SetActive(true);
        carsMenuPanel.SetActive(false);
        levelsScreen.SetActive(false);
    }

    public void EnableLevelsMenu()
    {
        mainMenuPanel.SetActive(false);
        levelsScreen.SetActive(true);
    }

    public void SelectCar(int carId)
    {
        carID = carId;
    }// this method is assigned to the car buttons.the selected button takes the carID to GameController and spawns that car

    public void SelectLevel(int levelId)
    {
        levelID = levelId;
        splashScreen.SetActive(true);
        SceneManager.LoadScene(1);
        splashScreen.SetActive(true);
    }// this method is assigned to the level buttons. the selected button takes the levelID to GameController and activate that level

}