using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject bulletFireObject;
    private Animator animator;

    public BulletType currentBulletType = BulletType.Straight;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        UIManager.Instance.OnWeaponChanged += UIManager.Instance.UpdateWeaponUI;
        UIManager.Instance.OnWeaponChanged?.Invoke(currentBulletType);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Throw");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentBulletType = (BulletType)(((int)currentBulletType + 1) % System.Enum.GetValues(typeof(BulletType)).Length);
            UIManager.Instance.OnWeaponChanged?.Invoke(currentBulletType);
        }
    }

    // 애니메이션 이벤트에서 호출
    public void FireByType()
    {
        switch (currentBulletType)
        {
            case BulletType.Straight:
                SpawnBullet(BulletType.Straight, 0f);
                break;

            case BulletType.CurveShot:
                SpawnBullet(BulletType.CurveShot, -1f);
                SpawnBullet(BulletType.CurveShot,  1f);
                break;
        }
    }

    void SpawnBullet(BulletType type, float curveDir)
    {
        BulletPoolManager.Instance.Get(bulletFireObject.transform.position, type, curveDir);
    }
}
