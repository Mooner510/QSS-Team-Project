using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text painText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text scoreText;
    [SerializeField] private float enemyAttackBoost;
    [SerializeField] private float enemySpawnBoost;
    [SerializeField] private float speedBoostForPlayer;
    [SerializeField] public GameObject particle;
    [SerializeField] public GameObject item;

    private static int _score;
    private static float _timerStart;

    public float GetEnemyAttackBoost() => enemyAttackBoost;

    public float GetEnemySpawnBoost()
    {
        var time = Time.realtimeSinceStartup - _timerStart;
        return enemySpawnBoost * (1 - time / (time + 120));
    }

    public float GetSpeedBoostForPlayer() => speedBoostForPlayer;

    private void Start()
    {
        _timerStart = Time.realtimeSinceStartup;
        _score = 0;
    }

    private void Update()
    {
        var player = GameObject.Find("Player").GetComponent<Player>();
        healthText.text = $"{(int) player.GetHealth()}% HP";
        painText.text = $"Pain {(int) player.GetPain()}%";

        var time = (int) (Time.realtimeSinceStartup - _timerStart);
        timerText.text = $"{time / 60}m {Utils.TimeFormat(time % 60)}s";
        scoreText.text = $"{_score}";
    }

    public static int GetScore() => _score;

    public static void SetScore(int score) => _score = score;

    public static void AddScore(int score) => _score += score;
}