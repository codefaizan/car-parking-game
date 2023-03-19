using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    Transform carSpawnPoint;
    public GameObject[] cars;
    public GameObject[] levels;
    public Levels[] avlLevels;
    public Levels curLevel;
    public GameObject player;
    private void Awake()
    {
        foreach(Levels level in avlLevels)
        {
            level.levelObj.SetActive(false);
        }
        levels[GameManager.levelID].SetActive(true);
        SetCurrentLevel();
        SpawnCar();
        UIController.instance.InitializUI(curLevel);
        //---------------------------------------------------------------------
        //levelConfig = new Levels();
        //float damage = (float) 1/(levels.Length - GameManager.levelID);
        //levelConfig.SetDamage(damage);
        //print(damage + " , " + levelConfig.damage);
        //---------------------------------------------------------------------
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

    public void LoadGarage()
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
    public int secondsLeft = 30;
    public float carParkTime;   //Time to hold car in parking area
    public GameObject levelObj;
    public Transform carSpawnPoint;
    public void SetDamage(int damage)
    {
        //if(damage > 0.5f)
        //{
        //    damage = 0.5f;
        //}
        //else if (damage < 0.25f)
        //{
        //    damage = 0.25f;
        //}
        //this.damage = damage;

        if (damage < 2)
            damage = 2;
        else if (damage > 4)
            damage = 4;
        this.carHealth = damage;
    }
}
