using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Transform carSpawnPoint;
    public GameObject[] cars;
    public GameObject[] levels;
    public Levels levelConfig;

    private void Awake()
    {
        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }
        levels[GameManager.levelID].SetActive(true);
        SpawnCar();
        //---------------------------------------------------------------------
        //levelConfig = new Levels();
        //float damage = (float) 1/(levels.Length - GameManager.levelID);
        //levelConfig.SetDamage(damage);
        //print(damage + " , " + levelConfig.damage);
        //---------------------------------------------------------------------
    }

    public void SpawnCar()
    {
        //spawn the car selected in GameManager
        carSpawnPoint = GameObject.Find("Spawn Point").transform;
        GameObject player = Instantiate(cars[GameManager.carID], carSpawnPoint.position, Quaternion.identity);

        //The car is made the child of the "spawn point" gameobject to change car rotation in each level
        player.transform.SetParent(carSpawnPoint);
        player.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
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
    public int carHealth;
    public int reward;
    public int secondsLeft = 30;
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
