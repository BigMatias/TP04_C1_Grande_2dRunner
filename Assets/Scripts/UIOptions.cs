using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button backBtn;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PauseMenu;

    [Header("Sound Sliders")]
    [SerializeField] private Slider Master;
    [SerializeField] private Slider Sfx;
    [SerializeField] private Slider Music;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        gameObject.SetActive(false);

        //Inicializar valor de los sliders con valor actual de su grupo de sonido
        float masterVol;
        audioMixer.GetFloat("VolumeMaster", out masterVol);
        Master.value = masterVol;

        float sfxVol;
        audioMixer.GetFloat("VolumeSfx", out sfxVol);
        Sfx.value = sfxVol;

        float musicVol;
        audioMixer.GetFloat("VolumeMusic", out musicVol);
        Music.value = musicVol;

        backBtn.onClick.AddListener(BackBtnClicked);
        Master.onValueChanged.AddListener(OnSliderMasterChanged);
        Sfx.onValueChanged.AddListener(OnSliderSfxChanged);
        Music.onValueChanged.AddListener(OnSliderMusicChanged);

    }
    private void OnSliderMasterChanged(float value)
    {
        audioMixer.SetFloat("VolumeMaster", value);
    }

    private void OnSliderMusicChanged(float value)
    {
        audioMixer.SetFloat("VolumeMusic", value);
    }

    private void OnSliderSfxChanged(float value)
    {
        audioMixer.SetFloat("VolumeSfx", value);

    }


    private void BackBtnClicked()
    {
        gameObject.SetActive(false);
        Scene escena = SceneManager.GetActiveScene();
        if (escena.name == "Game")
        {
            PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }

}
