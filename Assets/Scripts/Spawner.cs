using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject bacteria;
    [SerializeField] private GameObject normalBacteria;
    [SerializeField] private GameObject cancer;
    [SerializeField] private GameObject virus;

    [SerializeField] private float startBacteria;
    [SerializeField] private float startNormalBacteria;
    [SerializeField] private float startCancer;
    [SerializeField] private float startVirus;

    [SerializeField] private float endBacteria;
    [SerializeField] private float endNormalBacteria;
    [SerializeField] private float endCancer;
    [SerializeField] private float endVirus;

    private void Start()
    {
        var e = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GetEnemySpawnBoost();
        StartCoroutine(Spawn(bacteria, startBacteria * e, endBacteria * e));
        StartCoroutine(Spawn(normalBacteria, startNormalBacteria * e, endNormalBacteria * e));
        StartCoroutine(Spawn(cancer, startCancer * e, endCancer * e));
        StartCoroutine(Spawn(virus, startVirus * e, endVirus * e));
    }

    private IEnumerator Spawn(GameObject o, float maxCycle, float minCycle)
    {
        if (GameObject.Find("Player").GetComponent<Player>().IsDeath()) yield return null;
        yield return new WaitForSeconds(Random.Range(maxCycle, minCycle));
        Instantiate(o, new Vector2(Random.Range(-2.75f, 2.75f), 6), transform.rotation);
        // if (enemy.GetComponent<Enemy.Enemy>().GetEnemyType() == EnemyType.Cancer)
        //     enemy.GetComponent<SpriteRenderer>().color = new Color(0.15f, 0.6f, 0.15f);
        if(!GameObject.Find("Player").GetComponent<Player>().IsDeath())
            StartCoroutine(Spawn(o, maxCycle, minCycle));
    }
}