using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int currentCar;
    public static int levelID;
    
    public Button[] levelButtons;
    public static GameManager instance;
    // UI Panels
    public GameObject splashScreen;
    public GameObject carsMenuPanel;
    public GameObject levelsScreen;
    public Animator animator;

    // updated from CarSelectionManager
    public TMP_Text carNameLabel;
    public Button buyButton;
    public Button selectCarButton;
    public TMP_Text buyButtonText;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        
    }

    private void Start()
    {
        levelsScreen.SetActive(false);
        Invoke("DisableLoadingPanel", 2f);

        // level locking
        int levelReached = PlayerPrefs.GetInt("levelReached", 0);
        for(int i=0; i<levelButtons.Length; i++)
        {
            if(i > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void DisableLoadingPanel()
    {
        splashScreen.SetActive(false);
    }


    public void SelectLevel(int levelId)
    {
        levelID = levelId;
        StartCoroutine(LoadLevel());
    }// this method is assigned to the level buttons. the selected button takes the levelID to GameController and activate that level

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
        animator.Rebind();
        animator.Update(0f);
    }
}