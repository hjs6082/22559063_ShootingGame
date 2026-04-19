using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUI_Result : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button lobbyButton;

    void Start()
    {
        restartButton.onClick.AddListener(OnClickRestart);
        lobbyButton.onClick.AddListener(OnClickLobby);
    }

    void OnEnable()
    {
        if (UIManager.Instance != null && timeText != null)
            timeText.text = $"생존 시간 : {UIManager.Instance.GetTimeString()}";

        int score = PlayerPrefs.GetInt("currentscore", 0);
        if (scoreText != null)
            scoreText.text = $"현재 점수 : {score}점";

        int best = PlayerPrefs.GetInt("bestscore", 0);
        if (bestScoreText != null)
            bestScoreText.text = $"최고 점수 : {best}점";
    }

    void OnClickRestart()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ResetGame();
        MonsterManager.Instance.ResetSpawning();
        UIManager.Instance.ResetTimer();
    }

    void OnClickLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
