using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private Text score;

    private int counterOfProgress = 1;

    void Start()
    {
        score.text = "score: " + counterOfProgress;
    }

    public void AddScore()
    {
        counterOfProgress++;
        score.text = "score: " + counterOfProgress;
    }
}
