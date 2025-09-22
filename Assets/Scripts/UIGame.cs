using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] TextMeshProUGUI potionTimer;
    [SerializeField] TextMeshProUGUI coinCounter;
    [SerializeField] GameObject uiPotion;
    [SerializeField] Spawner Obstacles;
    [SerializeField] PlayerData PlayerData;
    [SerializeField] ParallaxData ParallaxData;
    [SerializeField] PlayerMovement PlayerMovement;

    [Header("GameOver")]
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Button restartBtn;
    [SerializeField] Button noRestartBtn;

    [NonSerialized] public bool gameStarted = false;
    [NonSerialized] public int gameTime;

    private bool jumpPressed = false;
    private float timeAux;
    private float potionTimerVar = 10f;
    private int coinCounterAux;

    private void Awake()
    {
        restartBtn.onClick.AddListener(RestartClicked);
        noRestartBtn.onClick.AddListener(MainMenuClicked);
    }

    void Start()
    {
        coinCounterAux = 0;
        coinCounter.text = 0.ToString();
        uiPotion.gameObject.SetActive(false);
        potionTimer.gameObject.SetActive(false);
        startText.gameObject.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        restartBtn.onClick.AddListener(RestartClicked);
        noRestartBtn.onClick.AddListener(MainMenuClicked);
    }


    private void Update()
    {
        PotionPickedUp();

        if (Input.GetKeyDown(PlayerData.Jump) && jumpPressed == false)
        {
            startText.gameObject.SetActive(false);
            jumpPressed = true;
            gameStarted = true;

            StartCoroutine(IncreaseBgSpeed());
            Obstacles.IncreaseSpeedOverTime();
            Obstacles.CreatePlatformAndPotionPub();
        }

        if (gameStarted)
        {
            timeAux += 1 * Time.deltaTime;
            gameTime = Mathf.RoundToInt(timeAux);
            timeTxt.text = gameTime.ToString();
        }
    }

    private IEnumerator IncreaseBgSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            ParallaxData.currentFloorSpeed += 0.002f;
            ParallaxData.currentL1Speed += 0.002f;
            ParallaxData.currentL3Speed += 0.002f;

        }
    }

    private void PotionPickedUp()
    {
        if (PlayerMovement.potionPickedUp)
        {
            potionTimerVar -= Time.deltaTime;
            potionTimer.gameObject.SetActive(true);
            potionTimer.text = potionTimerVar.ToString("0");
            uiPotion.gameObject.SetActive(true);
        }
        else
        {
            potionTimerVar = 10f;
            potionTimer.gameObject.SetActive(false);
            uiPotion.gameObject.SetActive(false);
        }
    }

    public void CoinPickedUp()
    {
        coinCounterAux += 1;
        coinCounter.text = coinCounterAux.ToString();
    }

    public void GameOver()
    {
        GameOverPanel.gameObject.SetActive(true);
    }

    private void RestartClicked()
    {
        SceneManager.LoadScene("Game");
    }

    private void MainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
