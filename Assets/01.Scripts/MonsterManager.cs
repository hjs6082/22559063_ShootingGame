using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject prefabsMonster;

    public List<Mesh> meshes = new List<Mesh>();

    float nowTime;
    float minTime = 1f;
    float maxTime = 5f;

    public float createTime = 1f;

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        nowTime = nowTime + Time.deltaTime;

        if (nowTime > createTime)
        {
            GameObject monster = Instantiate(prefabsMonster);
            monster.transform.position = transform.position;

            int randomIndex = Random.Range(0, meshes.Count);
            monster.GetComponentInChildren<MeshFilter>().mesh = meshes[randomIndex];
            string materialPath = "Materials/Fruit/" + meshes[randomIndex].name;
            monster.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>(materialPath);

            createTime = Random.Range(minTime, maxTime);

            nowTime = 0;
        }
    }
}
