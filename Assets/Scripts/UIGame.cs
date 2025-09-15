using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] TextMeshProUGUI timeTxt;
    [SerializeField] GameObject uiPotion;
    [SerializeField] Obstacles Obstacles;
    [SerializeField] Floor Floor;
    [SerializeField] PlayerData PlayerData;
    [SerializeField] PlayerMovement PlayerMovement;

    [Header("GameOver")]
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Button restartBtn;
    [SerializeField] Button noRestartBtn;

    [NonSerialized] public bool gameStarted = false;

    private bool jumpPressed = false;
    private float timeAux;
    public int gameTime;

    private void Awake()
    {
        restartBtn.onClick.AddListener(RestartClicked);    
        noRestartBtn.onClick.AddListener(MainMenuClicked);    
    }

    private void OnDestroy()
    {
        restartBtn.onClick.AddListener(RestartClicked);
        noRestartBtn.onClick.AddListener(MainMenuClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        uiPotion.gameObject.SetActive(false);
        startText.gameObject.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    private void Update()
    {
        PotionPickedUp();

        if (Input.GetKeyDown(PlayerData.Jump) && jumpPressed == false)
        {
            startText.gameObject.SetActive(false);
            jumpPressed = true;
            gameStarted = true;
            Floor.IncreaseSpeedOverTime();
            Obstacles.IncreaseSpeedOverTime();
        }

        if (gameStarted)
        {
            timeAux += 1 * Time.deltaTime;
            gameTime = Mathf.RoundToInt(timeAux);
            timeTxt.text = gameTime.ToString();            
        }
    }

    private void PotionPickedUp()
    {
        if (PlayerMovement.potionPickedUp)
        {
          uiPotion.gameObject.SetActive(true);
        }
        else
        {
            uiPotion.gameObject.SetActive(false);
        }
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
