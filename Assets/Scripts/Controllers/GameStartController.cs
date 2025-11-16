using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartController : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private TextMeshProUGUI countdown;
    [SerializeField] private GameObject retryBtn;

    private bool hasShownClear = false;
    private bool hasShownGameOver = false;

    private Coroutine countdownCo;

    // 一度しか走らない処理なのでStopCoroutineチェックは不要だが、メモとして置いておく
    private void Start()
    {
        if (countdownCo != null)
            StopCoroutine(countdownCo);

        countdownCo = StartCoroutine(CountdownCo());
    }

    private IEnumerator CountdownCo()
    {
        if (ball != null)
            ball.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        countdown.gameObject.SetActive(true);

        for(int i = 3; i >= 1; i--)
        {
            countdown.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdown.text = "GO!";
        GameManager.Instance.GameStart();

        if (ball != null)
            ball.Launch();

        yield return new WaitForSeconds(0.3f);

        countdown.gameObject.SetActive(false);
    }

    // FixedUpdateは物理に関する処理の際に使う感じなので、Update()で
    private void Update()
    {
        if (! hasShownClear && GameManager.Instance.isGameClear)
        {
            hasShownClear = true;
            countdown.text = "CLEAR!";
            countdown.gameObject.SetActive(true);

            // もう一度遊ぶボタンの表示
            retryBtn.SetActive(true);

            if (ball != null)
                ball.StopBall();

        }

        if (! hasShownGameOver && GameManager.Instance.isGameOver)
        {
            hasShownGameOver = true;
            countdown.text = "GAME OVER";
            countdown.gameObject.SetActive(true);

            // もう一度遊ぶボタンの表示
            retryBtn.SetActive(true);

            if (ball != null)
                ball.StopBall();
        }
    }

    // 「もう一度遊ぶ」ボタン押下処理
    public void OnClickRetry()
    {
        // クリアフラグなどをデフォルトに戻す
        GameManager.Instance.ResetGameState();

        // ブロック復活、ボールデフォルト配置などの処理
        // 今回は、シーンを再ロードして対応する
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }


}
