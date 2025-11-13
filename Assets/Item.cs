using UnityEngine;

public class Item : MonoBehaviour
{
    private float newWidth = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("アイテム取得");
        Player.Instance.ChangeWidth(newWidth);
        Destroy(gameObject);
    }
}
