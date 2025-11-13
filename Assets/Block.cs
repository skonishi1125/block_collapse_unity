using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
            Destroy(this.gameObject);
    }
}
