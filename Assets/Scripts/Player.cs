using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Paddle status")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float xInput = 0;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float width;
    [SerializeField] private float halfWidth;

    // アイテム関連
    [Header("Origin Info")]
    [SerializeField] private float originWidth;
    [SerializeField] private Color originColor;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Camera cam;
    private Vector3 bottomLeft;
    private Vector3 topRight;
    private float worldLeft;
    private float worldRight;

    private Coroutine powerUpWidthCo;

    private void Awake()
    {
        GameManager.Instance.RegisterPlayer(this);

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originWidth = transform.localScale.x;
        originColor = sr.color;

        SetScreenLength();

        ChangeWidth(width);
    }

    private void Update()
    {
        // 入力待ち
        xInput = 0f;
        if (Input.GetKey(KeyCode.A)) xInput = -1f;
        if (Input.GetKey(KeyCode.D)) xInput = 1f;
    }

    private void FixedUpdate()
    {
        var next = rb.position + Vector2.right * (xInput * speed * Time.fixedDeltaTime);

        // ここでクランプ（中心 + 半幅 が画面外に出ないように）
        next.x = Mathf.Clamp(next.x, leftLimit, rightLimit);

        rb.MovePosition(next);
    }

    private void SetScreenLength()
    {
        cam = Camera.main;

        // 画面の左下(0,0)と右上(1,1)をワールド座標に変換できる 
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        topRight = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // 画面の左端のx, 右端のxを取得できたので、パドルがはみ出さないように半幅ぶんだけ内側にオフセット
        worldLeft = bottomLeft.x;
        worldRight = topRight.x;
    }

    public void ChangeWidth(float width)
    {
        // 開始時にInspectorで横幅を決められるようにする
        // 現在のScale値を取得(transformはVector3なので、これで保管）
        Vector3 scale = transform.localScale;
        scale.x = width;
        transform.localScale = scale; // gameObjectへ反映

        // パドルの長さを取得し、移動上限を割り当て
        //halfWidth = bc.bounds.extents.x;
        halfWidth = width / 2;
        Debug.Log(halfWidth);
        leftLimit = worldLeft + halfWidth;
        rightLimit = worldRight - halfWidth;

    }

    public void PowerUpWidthCo(float newWidth, float duration)
    {
        // 重複を防ぐ
        if(powerUpWidthCo != null)
            StopCoroutine(powerUpWidthCo);

        powerUpWidthCo = StartCoroutine(PowerUpWidth(newWidth, duration));
    }

    private IEnumerator PowerUpWidth(float newWidth, float duration)
    {
        ChangeWidth(newWidth);

        // --- 点滅処理 ---
        float elapsed = 0f;
        float blinkTime = 1f;
        yield return new WaitForSeconds(duration - blinkTime);

        while (elapsed < blinkTime)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }
        sr.color = originColor;

        //yield return new WaitForSeconds(duration); // 指定時間待って、
        ChangeWidth(originWidth); // 元の長さに戻す
    }


}
