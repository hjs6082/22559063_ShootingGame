using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject bulletFireObject;

    void Update()
    {
        bool isFire = Input.GetButtonDown("Jump");
        if (isFire)
        {
            //기존 직선 코드
            SpawnBullet(BulletType.Straight);

            //프로그래머 추가 과제 - 포물선을 그리면서 날아가는 Bullet
            SpawnBullet(BulletType.CurveLeft);
            SpawnBullet(BulletType.CurveRight);
        }
    }

    void SpawnBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletObject);
        bullet.transform.position = bulletFireObject.transform.position;
        bullet.GetComponent<Bullet>().bulletType = type;
    }
}
