using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float newWidth;
    [SerializeField] private float powerUpTime;
    [SerializeField] private float hopPower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // linearVelocityとは違う、力の加え方
        // ぽよんと弾ませるならこっちかも？
        rb.AddForce(Vector2.up * hopPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("アイテム取得");
            var player = GameManager.Instance.Player;
            if (player != null)
                player.PowerUpWidthCo(newWidth, powerUpTime);

            Destroy(gameObject);

        }

    }
}
