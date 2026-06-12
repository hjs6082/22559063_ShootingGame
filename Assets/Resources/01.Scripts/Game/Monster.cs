using UnityEngine;

[RequireComponent(typeof(MonsterDropper))]
public class Monster : MonoBehaviour
{
    public float spd = 1.0f;

    public GameObject target;
    Vector3 direct = Vector3.down;

    public GameObject prefabsExplosion;

    MonsterDropper monsterDropper;

    private void Start()
    {
        monsterDropper = GetComponent<MonsterDropper>();
        target = GameObject.FindWithTag("Player");
        if (target != null) playerFlash = target.GetComponent<ColoredFlash>();
        int rndNum = Random.Range(0, 10);
        if (0 == rndNum % 3)
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
        monsterDropper.Drop();
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
