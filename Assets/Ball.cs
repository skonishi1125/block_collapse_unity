using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector3(speed, speed, 0);
    }

}
