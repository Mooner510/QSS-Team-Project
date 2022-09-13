using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text painText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text scoreText;

    private static float _pain;
    private static int _score;
    private static float _timerStart;

    private void Start()
    {
        _timerStart = Time.realtimeSinceStartup;
        _pain = 0;
        _score = 0;
    }

    private void Update()
    {
        healthText.text = $"{(int) GameObject.Find("Player").GetComponent<Player>().GetHealth()}% HP";
        painText.text = $"Pain {(int) _pain}%";

        var time = (int) (Time.realtimeSinceStartup - _timerStart);
        timerText.text = $"{time / 60}m {Utils.TimeFormat(time % 60)}s";
        scoreText.text = $"{_score}";
    }

    public static float GetPain() => _pain;

    public static void SetPain(float pain) => _pain = pain;

    public static void AddPain(float pain) => _pain += pain;

    public static int GetScore() => _score;

    public static void SetScore(int score) => _score = score;

    public static void AddScore(int score) => _score += score;
}