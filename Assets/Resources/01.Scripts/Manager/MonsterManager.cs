using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }

    public GameObject prefabsMonster;

    public List<Mesh> meshes = new List<Mesh>();
    public List<GameObject> explosionPrefabs = new List<GameObject>();

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRangeX = 5f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 4f;

    float nowTime;
    float minTime = 1f;
    float maxTime = 2f;
    [SerializeField] private float difficultyInterval = 30f; // 난이도 증가 주기
    [SerializeField] private float timeDecreaseAmount = 0.2f; // 줄어드는 시간
    [SerializeField] private float minTimeFloor = 0.3f; // 최소 간격 하한
    float difficultyTimer = 0f;

    public float createTime = 1f;

    bool isSpawning = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        createTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if (!isSpawning) return;

        nowTime += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (difficultyTimer >= difficultyInterval)
        {
            difficultyTimer = 0f;
            minTime = Mathf.Max(minTimeFloor, minTime - timeDecreaseAmount);
            maxTime = Mathf.Max(minTimeFloor + 0.1f, maxTime - timeDecreaseAmount);
        }

        if (nowTime > createTime)
        {
            GameObject monster = Instantiate(prefabsMonster);
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
            spawnPos.x += Random.Range(-spawnRangeX, spawnRangeX);
            monster.transform.position = spawnPos;
            Monster m = monster.GetComponent<Monster>();
            m.spd = Random.Range(minSpeed, maxSpeed);
            if (explosionPrefabs.Count > 0)
                m.prefabsExplosion = explosionPrefabs[Random.Range(0, explosionPrefabs.Count)];

            int randomIndex = Random.Range(0, meshes.Count);
            monster.GetComponentInChildren<MeshFilter>().mesh = meshes[randomIndex];
            string materialPath = "08.Materials/Fruit/" + meshes[randomIndex].name;
            monster.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>(materialPath);

            createTime = Random.Range(minTime, maxTime);

            nowTime = 0;
        }
    }

    public void TryDropItem(Vector3 position)
    {
        if (itemPrefab == null) return;
        if (Random.Range(0, 100) >= 30) return;

        Instantiate(itemPrefab, position, Quaternion.identity);
    }

    public void StopSpawning()
    {
        isSpawning = false;
        // 씬에 남아있는 몬스터 전부 제거
        foreach (var m in FindObjectsByType<Monster>(FindObjectsSortMode.None))
            Destroy(m.gameObject);
    }

    public void ResetSpawning()
    {
        isSpawning = true;
        nowTime = 0;
        difficultyTimer = 0;
        minTime = 1f;
        maxTime = 5f;
        createTime = Random.Range(minTime, maxTime);
    }
}
