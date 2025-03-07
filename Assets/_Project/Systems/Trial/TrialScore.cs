using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrialScore : MonoBehaviour
{
    private Animator animator;
    private TextMeshProUGUI scoreText;

    public static int score = 0;
    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            value = value < 0 ? 0 : value;
            score = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerGun.OnShotHit += IncrementScore;
    }

    private void OnDisable()
    {
        PlayerGun.OnShotHit -= IncrementScore;
    }

    public void IncrementScore()
    {
        Score++;
        OnIncrementScore();
    }

    private void OnIncrementScore()
    {
        scoreText.text = Score.ToString();
        animator.SetTrigger("scoreIncreased");
    }
}
