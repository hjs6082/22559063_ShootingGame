using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Main Text")]
    public TMP_Text mainText;

    [Header("Time")]
    public TMP_Text timeText;

    [Header("HP")]
    public List<Image> hpImages;
    public TMP_Text hpText;

    [Header("무기 UI")]
    public Image currentWeaponIcon;
    public Image nextWeaponIcon;

    [Header("Result")]
    [SerializeField] private GameObject resultPanel;

    [Header("Notice")]
    [SerializeField] private TMP_Text noticeText;

    public Action<BulletType> OnWeaponChanged;

    int currentScore = 0;
    int currentHp;
    float elapsedTime = 0f;
    bool timerRunning = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        currentHp = hpImages.Count;
        elapsedTime = 0f;
        timerRunning = true;

        GameManager gm = GameManager.Instance;
        gm.OnHpChanged    += UpdateHpUI;
        gm.OnScoreChanged += UpdateScoreUI;
        gm.OnWaveChanged  += UpdateWaveUI;
        gm.OnGameOver     += ShowResultPanel;

        UpdateHpUI(currentHp);
        RefreshMainText();
    }

    void Update()
    {
        if (!timerRunning) return;
        elapsedTime += Time.deltaTime;
        if (timeText != null)
        {
            int min  = (int)(elapsedTime / 60);
            int sec  = (int)(elapsedTime % 60);
            int ms   = (int)(elapsedTime * 100 % 100);
            timeText.text = $"{min:00}:{sec:00}:{ms:00}";
        }
    }

    public void StopTimer() => timerRunning = false;
    public void ResetTimer()
    {
        elapsedTime = 0f;
        timerRunning = true;
    }

    public string GetTimeString()
    {
        int min = (int)(elapsedTime / 60);
        int sec = (int)(elapsedTime % 60);
        int ms  = (int)(elapsedTime * 100 % 100);
        return $"{min:00}:{sec:00}:{ms:00}";
    }

    public void ShowNotice(string message)
    {
        if (noticeText == null) return;

        noticeText.text = message;
        noticeText.alpha = 1f;
        noticeText.DOFade(0f, 1.5f).SetDelay(1f).SetUpdate(true);
    }

    void ShowResultPanel()
    {
        if (resultPanel != null)
            resultPanel.SetActive(true);
    }

    void OnDestroy()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnHpChanged    -= UpdateHpUI;
        GameManager.Instance.OnScoreChanged -= UpdateScoreUI;
        GameManager.Instance.OnWaveChanged  -= UpdateWaveUI;
    }

    void UpdateHpUI(int hp)
    {
        currentHp = hp;
        for (int i = 0; i < hpImages.Count; i++)
            hpImages[i].enabled = i < hp;

        if (hpText != null)
            hpText.text = $"체력 {hp}/{hpImages.Count}";
    }

    void UpdateScoreUI(int score)
    {
        currentScore = score;
        RefreshMainText();
    }

    void UpdateWaveUI(int wave, int total)
    {
        RefreshMainText();
    }

    void RefreshMainText()
    {
        if (mainText != null)
        {
            int bestScore = PlayerPrefs.GetInt("bestscore", 0);
            mainText.text = $"현재점수 : {currentScore}\n최고점수 : {bestScore}";
        }
    }

    public void UpdateWeaponUI(BulletType type)
    {
        (currentWeaponIcon.transform.position, nextWeaponIcon.transform.position) =
            (nextWeaponIcon.transform.position, currentWeaponIcon.transform.position);

        (currentWeaponIcon, nextWeaponIcon) = (nextWeaponIcon, currentWeaponIcon);

        if (type == BulletType.Straight)
        {
            // 쿠나이 현재, 쌍칼 다음
            currentWeaponIcon.rectTransform.sizeDelta = new Vector2(28f, 142f);
            nextWeaponIcon.rectTransform.sizeDelta    = new Vector2(70f, 70.8f);
        }
        else
        {
            // 쌍칼 현재, 쿠나이 다음
            currentWeaponIcon.rectTransform.sizeDelta = new Vector2(117f, 118f);
            nextWeaponIcon.rectTransform.sizeDelta    = new Vector2(17f, 89f);
        }

        currentWeaponIcon.color = Color.white;
        nextWeaponIcon.color    = Color.black;
    }
}
