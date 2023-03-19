using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject[] cars;
    public GameObject[] levels;
    public Levels[] avlLevels;
    public Levels curLevel;
    public GameObject player;

    public static GameController instance;
    private void Awake()
    {
        instance = this;
        foreach(Levels level in avlLevels)
        {
            level.levelObj.SetActive(false);
        }
        levels[GameManager.levelID].SetActive(true);
        SetCurrentLevel();
        SpawnCar();
        UIController.instance.InitializUI(curLevel);
    }
    void SetCurrentLevel()
    {
        curLevel = avlLevels[GameManager.levelID];
        curLevel.levelObj.SetActive(true);
    }
    public void SpawnCar()
    {
        //spawn the car selected in GameManager
        player = Instantiate(cars[GameManager.carID], curLevel.carSpawnPoint.position,
            curLevel.carSpawnPoint.rotation);
    }
    public void OnDamage()
    {
        curLevel.carHealth--;
        UIController.instance.UpdateCarDamage(curLevel.carHealth);
        if (curLevel.carHealth <= 0)
        {
            //LevelFailed
            UIController.instance.OnGameEnd(false);
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        if (GameManager.levelID + 1 == levels.Length)
            GameManager.levelID = 0;
        else
            GameManager.levelID += 1;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

[System.Serializable]
public class Levels
{
    public string levelName;
    public int carHealth;
    public int reward;
    public int secondsLeft;   //game timer
    public float carParkTime;   //Time to hold car in parking area
    public GameObject levelObj;
    public Transform carSpawnPoint;
}
