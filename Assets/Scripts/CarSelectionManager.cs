using UnityEngine;

public class CarSelectionManager : MonoBehaviour
{
    public GameObject[] cars;
    int _currentCar;

    public CarsBluePrint[] carsDetail;
    public CarsBluePrint currentCar;

    private void Start()
    {
        // selecting car in showroom
        _currentCar = PlayerPrefs.GetInt("SelectedCar", 0);
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }
        cars[_currentCar].SetActive(true);
        GameManager.instance.carNameLabel.text = carsDetail[_currentCar].name;
        GameManager.instance.buyButtonText.text = carsDetail[_currentCar].price.ToString();


        // unlocking purchased cars
        foreach(CarsBluePrint car in carsDetail)
        {
            if (car.price == 0)
            {
                car.isUnlocked = true;
            }
            else
                car.isUnlocked = PlayerPrefs.GetInt(car.name, 0) == 0 ? false : true;
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0.2f, 0f));
    }

    public void ToggleNextCar()
    {
        // select next car
        if (_currentCar < cars.Length - 1)
        {
            _currentCar++;
            UpdateUI();
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].SetActive(false);
            }
            cars[_currentCar].SetActive(true);
            GameManager.instance.carNameLabel.text = carsDetail[_currentCar].name;
            GameManager.instance.buyButtonText.text = carsDetail[_currentCar].price.ToString();
        }
    }
    public void TogglePreviousCar()
    {
        // select previous car
        if (_currentCar > 0)
        {
            _currentCar--;
            UpdateUI();
            for (int i = 0; i < cars.Length; i++)
            {
                cars[i].SetActive(false);
            }
            cars[_currentCar].SetActive(true);
            GameManager.instance.carNameLabel.text = carsDetail[_currentCar].name;
            GameManager.instance.buyButtonText.text = carsDetail[_currentCar].price.ToString();
        }
    }

    public void SelectCar()
    {
        PlayerPrefs.SetInt("SelectedCar", _currentCar);
        GameManager.currentCar = _currentCar;
    }

    void UpdateUI()
    {
        if (!carsDetail[_currentCar].isUnlocked)
        {
            GameManager.instance.buyButton.gameObject.SetActive(true);
            GameManager.instance.selectCarButton.interactable = false;

            if(carsDetail[_currentCar].price <= ScoreManager.instance.currencyAmount)
            {
                GameManager.instance.buyButton.interactable = true;
            }
            else
            {
                GameManager.instance.buyButton.interactable = false;
            }
        }
        else
        {
            GameManager.instance.buyButton.gameObject.SetActive(false);
            GameManager.instance.selectCarButton.interactable = true;
        }
    }

    public void BuyCar()
    {
        if(carsDetail[_currentCar].price <= ScoreManager.instance.currencyAmount)
        {
            ScoreManager.instance.currencyAmount -= carsDetail[_currentCar].price;
            ScoreManager.instance.UpdateCurrency();
            carsDetail[_currentCar].isUnlocked = true;
            PlayerPrefs.SetInt(carsDetail[_currentCar].name, 1);
            UpdateUI();
        }
    }
}



[System.Serializable]
public class CarsBluePrint
{
    public string name;
    public int ID;
    public int price;
    public int requiredLevel;
    public bool isUnlocked;
}
