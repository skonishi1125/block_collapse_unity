using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    
    [Range(0f, 1f)]
    [SerializeField] private float dropChance = 0.2f; // 20% でドロップ


    private void Start()
    {
        GameManager.Instance.RegisterBlock();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            // GameManager側のカウントを減らす
            GameManager.Instance.CollapseBlock();

            // 一定確率でアイテム生成
            if (Random.value < dropChance)
                Instantiate(itemPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }



}
