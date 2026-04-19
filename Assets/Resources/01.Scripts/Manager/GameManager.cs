using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxHp = 5;
    public int maxWave = 5;

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Transform characterSpawnPoint;

    int hp;
    int score;
    int wave = 1;

    public Action<int> OnHpChanged;
    public Action<int> OnScoreChanged;
    public Action<int, int> OnWaveChanged;
    public Action OnGameOver;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        hp = maxHp;
        score = 0;
        wave = 1;

        OnHpChanged?.Invoke(hp);
        OnScoreChanged?.Invoke(score);
        OnWaveChanged?.Invoke(wave, maxWave);
    }

    public void AddScore(int amount = 1)
    {
        score += amount;
        int best = PlayerPrefs.GetInt("bestscore", 0);
        if (score > best)
            PlayerPrefs.SetInt("bestscore", score);

        PlayerPrefs.SetInt("currentscore", score);
        OnScoreChanged?.Invoke(score);
    }

    public void ResetGame()
    {
        hp = maxHp;
        score = 0;
        wave = 1;
        PlayerPrefs.SetInt("currentscore", 0);

        var oldChar = GameObject.FindWithTag("Player");
        if (oldChar != null) Destroy(oldChar);
        Instantiate(characterPrefab, characterSpawnPoint.position, Quaternion.identity);

        OnHpChanged?.Invoke(hp);
        OnScoreChanged?.Invoke(score);
        OnWaveChanged?.Invoke(wave, maxWave);
    }

    public void TakeDamage(int amount = 1)
    {
        hp = Mathf.Max(0, hp - amount);
        OnHpChanged?.Invoke(hp);

        if (hp <= 0)
            GameOver();
    }

    public void NextWave()
    {
        wave = Mathf.Min(wave + 1, maxWave);
        OnWaveChanged?.Invoke(wave, maxWave);
    }

    void GameOver()
    {
        MonsterManager.Instance.StopSpawning();
        UIManager.Instance.StopTimer();
        OnGameOver?.Invoke();
    }
}
