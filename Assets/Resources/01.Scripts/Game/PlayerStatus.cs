using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    public bool IsInvincible { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void ApplyInvincible()
    {
        StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        IsInvincible = true;
        UIManager.Instance.ShowNotice("무적 상태입니다!");
        yield return new WaitForSeconds(2f);
        IsInvincible = false;
    }
}
