using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int carID;
    static GameManager instance;

    // UI Panels
    GameObject splashScreen;
    GameObject carsMenuPanel;
    GameObject mainMenuPanel;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        splashScreen = GameObject.Find("Splash Screen");
        Invoke("DisableLoadingPanel", 2f);
        carsMenuPanel = GameObject.Find("Cars Menu");
        mainMenuPanel = GameObject.Find("Main Menu Panel");
        carsMenuPanel.SetActive(false);
    }

    public void DisableLoadingPanel()
    {
        print("garage loaded");

        splashScreen.SetActive(false);
    }

    public void EnableCarsMenu()
    {
        mainMenuPanel.SetActive(false);
        carsMenuPanel.SetActive(true);
    }// opens cars menu

    public void EnableMainMenu()
    {
        carsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SelectCar(int carId)
    {
        carID = carId;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

}