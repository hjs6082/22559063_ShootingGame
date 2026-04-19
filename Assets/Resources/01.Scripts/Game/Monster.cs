using UnityEngine;

public class Monster : MonoBehaviour
{
    public float spd = 1.0f;

    public GameObject target;
    Vector3 direct = Vector3.down;

    public GameObject prefabsExplosion;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
        if (target != null) playerFlash = target.GetComponent<ColoredFlash>();
        int rndNum = Random.Range(0, 10);
        if (rndNum % 3 == 0)
        {
            direct = target.transform.position - transform.position;
            direct.Normalize();
        }
    }

    private void Update()
    {
        transform.position = transform.position + direct * spd * Time.deltaTime;
    }

    void TryDropItem()
    {
        MonsterManager.Instance.TryDropItem(transform.position);
    }

    ColoredFlash playerFlash;
    bool isDead = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet == null || bullet.isDestroyed) return;
            bullet.isDestroyed = true;

            isDead = true;
            GameManager.Instance.AddScore(1);

            GameObject explosionObj = Instantiate(prefabsExplosion);
            explosionObj.transform.position = transform.position;

            SoundManager.Instance.PlaySFX("Fruit");
            TryDropItem();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            isDead = true;
            if (PlayerStatus.Instance == null || !PlayerStatus.Instance.IsInvincible)
            {
                GameManager.Instance.TakeDamage(1);
                if (playerFlash != null) playerFlash.Flash(Color.red);
            }
            Destroy(gameObject);
        }
    }
}
