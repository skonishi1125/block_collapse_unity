using UnityEngine;

public class Item : MonoBehaviour
{
    private float newWidth = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("アイテム取得");
        var player = GameManager.Instance.Player;
        if (player != null)
            player.ChangeWidth(newWidth);

        Destroy(gameObject);
    }
}
