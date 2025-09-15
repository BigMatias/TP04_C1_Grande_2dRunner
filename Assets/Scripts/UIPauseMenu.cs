using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject OptionsPanel;

    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnMenu;

    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.SetActive(false);

        btnPlay.onClick.AddListener(OnBtnPlayClicked);
        btnOptions.onClick.AddListener(OnBtnOptionsClicked);
        btnMenu.onClick.AddListener(OnBtnExitClicked);
    }

    private void OnBtnPlayClicked()
    {
        GameManager.PauseGame();
        gameObject.SetActive(false);
    }

    private void OnBtnOptionsClicked()
    {
        gameObject.SetActive(true);
        OptionsPanel.gameObject.SetActive(true);
    }

    private void OnBtnExitClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
