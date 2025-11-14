using System.Collections;
using TMPro;
using UnityEngine;

public class GameStartController : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private TextMeshProUGUI countdown;

    private Coroutine countdownCo;


    private void Start()
    {
        if (countdownCo != null)
            StopCoroutine(countdownCo);

        StartCoroutine(CountdownCo());
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
        yield return new WaitForSeconds(0.5f);

        countdown.gameObject.SetActive(false);

        if (ball != null)
            ball.Launch();

    }


}
