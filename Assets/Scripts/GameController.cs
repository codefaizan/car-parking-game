using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Transform carSpawnPoint;
    public GameObject[] cars;
    private void Start()
    {
        SpawnCar();
    }

    public void SpawnCar()
    {
        Instantiate(cars[GameManager.carID], carSpawnPoint.position, Quaternion.identity);
        print("spawned");
    }//spawns selected car

    public void LoadGarage()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
