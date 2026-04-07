using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Straight, CurveLeft, CurveRight }

public class Bullet : MonoBehaviour
{
    float spd = 3.5f;
    public BulletType bulletType = BulletType.Straight;

    float elapsed = 0f;

    private void Update()
    {
        elapsed += Time.deltaTime;

        switch (bulletType)
        {
            case BulletType.Straight:
                transform.Translate(Vector3.up * spd * Time.deltaTime);
                break;

            case BulletType.CurveLeft:
            case BulletType.CurveRight:
                // 앞으로 나아가면서 좌우로 휘는 포물선
                float dir = (bulletType == BulletType.CurveLeft) ? -1f : 1f;
                float horizontal = dir * Mathf.Sin(elapsed * spd) * spd * Time.deltaTime;
                float vertical = spd * Time.deltaTime;
                transform.Translate(new Vector3(horizontal, vertical, 0f));
                break;
        }
    }
}
