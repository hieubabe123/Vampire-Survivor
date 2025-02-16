using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Define the diffirent states of game
    public enum GameState
    {
        GamePlay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;
    public GameState previousState;


    [Header("Damage Text Settings")]
    public Canvas damageTextCanvas;
    public float textFontSize = 20f;
    public TMP_FontAsset textFont;
    public Camera referenceCamera;



    [Header("Screen")]
    public GameObject pauseScreen;
    public GameObject resultScreen;
    public GameObject levelUpScreen;
    public GameObject virtualJoystick;


    [Header("Current Stat Displays")]
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentMoveSpeedDisplay;
    public TMP_Text currentMightDisplay;
    public TMP_Text currentProjectileSpeedDisplay;
    public TMP_Text currentMagnetDisplay;


    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public TMP_Text chosenCharacterName;
    public TMP_Text levelReachedDisplay;
    public TMP_Text timeSurvivedDisplay;
    public List<Image> chosenWeaponUI = new List<Image>(6);
    public List<Image> chosenPassiveItemUI = new List<Image>(6);


    [Header("Stop Watch")]
    public float timeLimit;
    private float stopWatchTime;
    public TMP_Text stopWatchDisplay;

    public bool isGameOver = false;


    //Check if player chosen the upgrade
    public bool chosingUpgrade;

    //Reference to the player's game object
    public GameObject playerObject;


    private void Awake()
    {
        //Singleton Pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }
        DisableScreen();
    }


    private void Update()
    {
        ShowCurrentState();
    }

    private void ShowCurrentState()
    {
        switch (currentState)
        {
            case GameState.GamePlay:
                CheckForPausedAndResume();
                UpdateStopwatch();
                virtualJoystick.SetActive(true);
                break;

            case GameState.Paused:
                CheckForPausedAndResume();
                virtualJoystick.SetActive(false);
                break;

            case GameState.GameOver:
                if (!isGameOver)
                {
                    EndGame();
                }
                break;

            case GameState.LevelUp:
                if (!chosingUpgrade)
                {
                    UpgradingLevel();

                    //Audio Clip
                    AudioManager.instance.PlaySFX(FindObjectOfType<AudioManager>().upgradeItem);
                }
                break;

            default:
                Debug.LogWarning("State not exist");
                break;
        }
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!instance.damageTextCanvas)
        {
            return;
        }

        if (!instance.referenceCamera)
        {
            instance.referenceCamera = Camera.main;
        }
        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed));
    }

    private IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        //Generating the floating text
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text = text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmPro.fontSize = textFontSize;
        if (textFont)
        {
            tmPro.font = textFont;
        }
        rectTransform.position = referenceCamera.WorldToScreenPoint(target.position);

        Destroy(textObj, duration);

        textObj.transform.SetParent(instance.damageTextCanvas.transform);

        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        float time = 0;
        float yOffset = 0;
        while (time < duration)
        {
            yield return wait;
            time += Time.deltaTime;

            if (rectTransform == null)
            {
                yield break;
            }

            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1 - time / duration);

            yOffset += speed * Time.deltaTime;
            rectTransform.transform.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset));
        }


    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            currentState = GameState.Paused;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            virtualJoystick.SetActive(false);
            Debug.Log("Game is paused");
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            currentState = previousState;
            currentState = GameState.GamePlay;
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
            virtualJoystick.SetActive(true);
            Debug.Log("Game is Playing");
        }
    }

    private void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("Game over");
        virtualJoystick.SetActive(false);
        DisplayResults();
    }

    private void UpgradingLevel()
    {
        chosingUpgrade = true;
        Time.timeScale = 0f;
        Debug.Log("Upgrades shown");
        virtualJoystick.SetActive(false);
        levelUpScreen.SetActive(true);
    }

    private void CheckForPausedAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvivedDisplay.text = stopWatchDisplay.text;
        ChangeState(GameState.GameOver);
    }


    private void DisplayResults()
    {
        resultScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponsAndPassiveItemsUI(List<Image> chosenWeaponData, List<Image> chosenPassiveItemData)
    {
        if (chosenWeaponData.Count != chosenWeaponUI.Count || chosenPassiveItemData.Count != chosenWeaponUI.Count)
        {
            Debug.Log("Chosen Weapon or Passive Item data lists have diffirent lenghts");
            return;
        }

        for (int i = 0; i < chosenWeaponUI.Count; i++)
        {
            if (chosenWeaponData[i].sprite)
            {
                chosenWeaponUI[i].enabled = true;
                chosenWeaponUI[i].sprite = chosenWeaponData[i].sprite;
            }
            else
            {
                chosenWeaponUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemUI.Count; i++)
        {
            if (chosenPassiveItemData[i].sprite)
            {
                chosenPassiveItemUI[i].enabled = true;
                chosenPassiveItemUI[i].sprite = chosenPassiveItemData[i].sprite;
            }
            else
            {
                chosenPassiveItemUI[i].enabled = false;
            }
        }
    }

    private void UpdateStopwatch()
    {
        stopWatchTime += Time.deltaTime;
        UpdateStopwatchDisplay();
        if (stopWatchTime >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }
    }

    private void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopWatchTime / 60);
        int seconds = Mathf.FloorToInt(stopWatchTime % 60);

        stopWatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrade");
    }

    public void EndLevelUp()
    {
        chosingUpgrade = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.GamePlay);

    }

}
