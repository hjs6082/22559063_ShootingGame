using UnityEngine;

public class MonsterDropper : MonoBehaviour
{
    [System.Serializable]
    public class DropTable
    {
        public ItemData itemData;
        [Range(0f, 1f)] public float dropRate = 0.5f;
        public int minCount = 1;
        public int maxCount = 1;
    }

    public GameObject dropPrefab;
    public DropTable[] dropTables;

    public void Drop()
    {
        if (dropPrefab == null || dropTables == null) return;

        // dropTables를 반복문으로 돌기
        foreach (var dropTable in dropTables)
        {
            // Random.value로 드랍 확률 검사
            if (Random.value > dropTable.dropRate)
                continue;

            int count = Random.Range(dropTable.minCount, dropTable.maxCount + 1);
            GameObject go = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            go.GetComponent<DropItem>().Setup(dropTable.itemData, count);
        }
    }
}
