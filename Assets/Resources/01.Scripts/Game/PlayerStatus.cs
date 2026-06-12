using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    public bool IsInvincible { get; private set; }
    public bool IsScoreDouble { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public bool ApplyInvincible(float duration = 2f)
    {
        if (IsInvincible)
        {
            UIManager.Instance.ShowNotice("무적 효과가 이미 적용 중입니다!");
            return false;
        }
        StartCoroutine(InvincibleRoutine(duration));
        return true;
    }

    public bool ApplyScoreDouble(float duration = 5f)
    {
        if (IsScoreDouble)
        {
            UIManager.Instance.ShowNotice("점수 2배 효과가 이미 적용 중입니다!");
            return false;
        }
        StartCoroutine(ScoreDoubleRoutine(duration));
        return true;
    }

    private IEnumerator InvincibleRoutine(float duration)
    {
        IsInvincible = true;
        UIManager.Instance.ShowNotice("무적 상태입니다!");
        yield return new WaitForSeconds(duration);
        IsInvincible = false;
        UIManager.Instance.ShowNotice("무적 종료");
    }

    private IEnumerator ScoreDoubleRoutine(float duration)
    {
        IsScoreDouble = true;
        UIManager.Instance.ShowNotice("점수 2배!");
        yield return new WaitForSeconds(duration);
        IsScoreDouble = false;
        UIManager.Instance.ShowNotice("점수 2배 종료");
    }
}
