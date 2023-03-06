using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarHealthAndCollisionController : MonoBehaviour
{
    //parking
    Image parkLoadingSign;

    UIController uiController;
    private void Start()
    {
        parkLoadingSign = GameObject.Find("Park Loading").GetComponent<Image>();
        uiController = GameObject.Find("UI Controller").GetComponent<UIController>();
    }


    private void Update()
    {
        if (parkLoadingSign.fillAmount >= 1f && !uiController.levelWon)
        {
            uiController.levelWon = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
            parkLoadingSign.fillAmount += 0.2f * Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Parking"))
            parkLoadingSign.fillAmount = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        uiController.healthBar.fillAmount -= 0.1f;
    }


}
