using System;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreMessage;
    [SerializeField] private TextMeshProUGUI scorePoint;
    [SerializeField] private float score = 0;
    [SerializeField] private float currentTime;
    [SerializeField] private float defaultTimeScore = 100f;
    [SerializeField] private float defaultItemScore = 300f;

    private bool hasDisplayedScore = false;

    private void FixedUpdate()
    {

        currentTime += Time.deltaTime;

        if (!hasDisplayedScore)
            if (GameManager.Instance.isGameClear || GameManager.Instance.isGameOver)
                DisplayScore();

    }

    public void DisplayScore()
    {
        score = CalculateScore();
        scorePoint.text = Math.Max(0, (int)score).ToString();

        scoreMessage.gameObject.SetActive(true);
        scorePoint.gameObject.SetActive(true);

        hasDisplayedScore = true;
    }

    private float CalculateScore()
    {
        float timeScore = defaultTimeScore - currentTime;
        float itemScore = defaultItemScore - (GameManager.Instance.acquiredItems * 50f);

        float finalScore = timeScore + itemScore;

        return finalScore;
    }
}
