using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarHealthAndCollisionController : MonoBehaviour
{
    UIController uiController;
    private void Start()
    {
        
        uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
            uiController.StartParking();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
        {
            uiController.ExitParking();
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        //uiController.healthBar.fillAmount -= 0.1f;
        //uiController.healthBar.fillAmount -= gameController.levelConfig.lifes;

        // the health of car reduces when it has a collision
        uiController.GetDamage();
    }
}
