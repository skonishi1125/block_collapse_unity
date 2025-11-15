using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Move Adjustment")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float minimumSpeed;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float minX; // 最低限の横方向成分
    [SerializeField] private float minY; // 最低限の縦方向成分

    private bool isLaunched = false;
    private bool isCleared = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch()
    {
        isLaunched = true;

        // ゲーム開始時の適当な初速（／）
        rb.linearVelocity = new Vector3(defaultSpeed, defaultSpeed, 0);
    }


    private void FixedUpdate()
    {
        // スタート前の場合は止まっていてほしいので、補正をかけない
        if (!isLaunched)
            return;


        Vector2 v = rb.linearVelocity;

        // 最低速度チェック
        // 負荷を考慮してv.magnitudeでなくsqlMagnitudeを使う
        // 例えば(0.1, 0.1)なら、0.1² + 0.1²で0.02になる
        float speedSqrMagnitude = v.sqrMagnitude;
        if (speedSqrMagnitude < 0.01f)
        {
            Debug.Log("最低速度補正");
            v = v.normalized * minimumSpeed;
        }
        // 最大速度チェック
        // 例えば(5, 5)の時は、5² + 5² = 50になる
        else if (speedSqrMagnitude > 100f)
        {
            Debug.Log("最高速度補正");
            v = v.normalized * maximumSpeed;
        }

        // 浅すぎる跳ねの角度チェック
        // 絶対値で最低のyと比較して、足りなければyに力を足してやる
        if (Mathf.Abs(v.y) < minY)
            v.y = Mathf.Sign(v.y) * minimumSpeed;

        // 垂直すぎる跳ねの角度チェック
        // 絶対値で最低のxと比較して、足りなければxに力を足してやる
        if (Mathf.Abs(v.x) < minX)
            v.x = Mathf.Sign(v.x) * minimumSpeed;

        rb.linearVelocity = v;
    }

    private void StopBall()
    {
        rb.linearVelocity = Vector2.zero;
    }

}
