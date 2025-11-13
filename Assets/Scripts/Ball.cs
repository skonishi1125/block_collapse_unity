using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector3(speed * 0.5f, speed, 0);
    }

}
