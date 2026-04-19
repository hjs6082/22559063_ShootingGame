using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI_Pause : MonoBehaviour
{
    [SerializeField] private Slider bgSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button lobbyButton;

    void OnEnable()
    {
        Time.timeScale = 0f;

        if (SoundManager.Instance.Mixer.GetFloat("BG", out float bgDb))
            bgSlider.SetValueWithoutNotify(Mathf.Pow(10f, bgDb / 20f));

        if (SoundManager.Instance.Mixer.GetFloat("SFX", out float sfxDb))
            sfxSlider.SetValueWithoutNotify(Mathf.Pow(10f, sfxDb / 20f));
    }

    void Start()
    {
        bgSlider.onValueChanged.AddListener(SoundManager.Instance.BGSoundVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SFXSoundVolume);
        resumeButton.onClick.AddListener(OnClickResume);
        lobbyButton.onClick.AddListener(OnClickLobby);
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void OnClickResume()
    {
        gameObject.SetActive(false);
    }

    private void OnClickLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LobbyScene");
    }
}
