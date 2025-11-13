using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float xInput = 0;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;

    [SerializeField] private float width;
    [SerializeField] private float halfWidth;

    private Rigidbody2D rb;
    private BoxCollider2D co;
    private Camera cam;
    private Vector3 bottomLeft;
    private Vector3 topRight;
    private float worldLeft;
    private float worldRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        co = GetComponent<BoxCollider2D>();
        cam = Camera.main;

        // 画面の左下(0,0)と右上(1,1)をワールド座標に変換
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // 画面の左端のx, 右端のxを取得できたので、パドルがはみ出さないように半幅ぶんだけ内側にオフセット
        worldLeft = bottomLeft.x;
        worldRight = topRight.x;

        // 開始時にInspectorで横幅を決められるようにする
        // 現在のScale値を取得(transformはVector3なので、これで保管）
        Vector3 scale = transform.localScale;
        scale.x = width;
        transform.localScale = scale; // gameObjectへ反映
    }

    private void Start()
    {

    }

    private void Update()
    {
        // 入力待ち
        xInput = 0f;
        if (Input.GetKey(KeyCode.A)) xInput = -1f;
        if (Input.GetKey(KeyCode.D)) xInput = 1f;

        // パドルの長さを取得し、移動上限を割り当て
        Vector3 scale = transform.localScale;
        scale.x = width;
        transform.localScale = scale;

        halfWidth = co.bounds.extents.x;
        leftLimit = worldLeft + halfWidth;
        rightLimit = worldRight - halfWidth;
    }

    private void FixedUpdate()
    {
        var next = rb.position + Vector2.right * (xInput * speed * Time.fixedDeltaTime);

        // ここでクランプ（中心 + 半幅 が画面外に出ないように）
        next.x = Mathf.Clamp(next.x, leftLimit, rightLimit);

        rb.MovePosition(next);
    }
}
