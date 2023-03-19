using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarHealthAndCollisionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
            UIController.instance.StartParking();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
        {
            UIController.instance.ExitParking();
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        // the health of car reduces when it has a collision
        GameController.instance.OnDamage();
    }
}
