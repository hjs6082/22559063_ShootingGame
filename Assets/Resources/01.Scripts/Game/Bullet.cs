using UnityEngine;

public enum BulletType { Straight, CurveShot }

public class Bullet : MonoBehaviour
{
    float spd = 3.5f;
    public BulletType bulletType = BulletType.Straight;
    public float curveDir = 1f;

    [HideInInspector] public bool isDestroyed = false;

    float elapsed = 0f;

    void Start()
    {
        SoundManager.Instance.PlaySFX("Throw");
    }

    public void ResetState()
    {
        elapsed = 0f;
        isDestroyed = false;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= 4f)
        {
            BulletPoolManager.Instance.Return(this);
            return;
        }

        switch (bulletType)
        {
            case BulletType.Straight:
                transform.Translate(Vector3.up * spd * Time.deltaTime);
                break;

            case BulletType.CurveShot:
                float horizontal = curveDir * Mathf.Sin(elapsed * spd) * spd * Time.deltaTime;
                float vertical = spd * Time.deltaTime;
                transform.Translate(new Vector3(horizontal, vertical, 0f));
                break;
        }
    }
}
