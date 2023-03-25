using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text currencyText;
    public int currencyAmount;

    public static ScoreManager instance;
    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        //DontDestroyOnLoad(this.gameObject);

        currencyAmount = PlayerPrefs.GetInt("Currency Amount");
        currencyText.text = (int.Parse(currencyText.text) + currencyAmount).ToString();
        //PlayerPrefs.DeleteAll();
    }

    public void AddRewardToCurrency(int _reward)
    {
        currencyAmount += _reward;
        PlayerPrefs.SetInt("Currency Amount", currencyAmount);
    } // currency is updated from GameController

    public void UpdateCurrency()
    {
        currencyText.text = (currencyAmount).ToString();
        PlayerPrefs.SetInt("Currency Amount", currencyAmount);
    }
}
