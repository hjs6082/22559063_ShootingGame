using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float spd = 1.0f;

    public GameObject target;
    Vector3 direct = Vector3.down;

    private void Start()
    {
        target = GameObject.Find("Character");
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

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);

        Destroy(gameObject);
    }
}
