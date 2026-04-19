using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<Bullet> pool = new Queue<Bullet>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            Bullet b = CreateBullet();
            b.gameObject.SetActive(false);
            pool.Enqueue(b);
        }
    }

    private Bullet CreateBullet()
    {
        GameObject go = Instantiate(bulletPrefab, transform);
        return go.GetComponent<Bullet>();
    }

    public Bullet Get(Vector3 position, BulletType type, float curveDir)
    {
        Bullet b = pool.Count > 0 ? pool.Dequeue() : CreateBullet();
        b.transform.position = position;
        b.bulletType = type;
        b.curveDir = curveDir;
        b.gameObject.SetActive(true);
        b.ResetState();
        return b;
    }

    public void Return(Bullet b)
    {
        b.gameObject.SetActive(false);
        pool.Enqueue(b);
    }
}
