using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEngine;
#endif

public class UIMainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button BtnPlay;
    [SerializeField] private Button BtnOptions;
    [SerializeField] private Button BtnExit;
    [SerializeField] private Image Logo;
    [SerializeField] private GameObject Options;

    private AudioSource audioSource;

    private void Awake()
    {
        Options.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        BtnPlay.onClick.AddListener(StartGame);
        BtnOptions.onClick.AddListener(ShowOptions);
        BtnExit.onClick.AddListener(QuitGame);
    }

    private void OnDestroy()
    {
        BtnPlay.onClick.AddListener(StartGame);
        BtnOptions.onClick.AddListener(ShowOptions);
        BtnExit.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
        audioSource.Stop();
    }

    private void ShowOptions()
    {
        Options.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void QuitGame()
    {
        //Sale del estado "Play" del editor si estamos en el editor, de lo contrario sale de la aplicación si esta es una build.  
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
