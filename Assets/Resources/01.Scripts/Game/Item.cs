using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 3f;

    private void Update()
    {
        transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (PlayerStatus.Instance != null) PlayerStatus.Instance.ApplyInvincible();
        Destroy(gameObject);
    }
}
