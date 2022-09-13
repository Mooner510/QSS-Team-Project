using System.Collections;
using LivingEntity;
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
        StartCoroutine(Spawn(bacteria, startBacteria, endBacteria));
        StartCoroutine(Spawn(normalBacteria, startNormalBacteria, endNormalBacteria));
        StartCoroutine(Spawn(cancer, startCancer, endCancer));
        StartCoroutine(Spawn(virus, startVirus, endVirus));
    }

    private IEnumerator Spawn(GameObject o, float maxCycle, float minCycle)
    {
        if (GameObject.Find("Player").GetComponent<Player>().IsDeath()) yield return null;
        yield return new WaitForSeconds(Random.Range(maxCycle, minCycle));
        var enemy = Instantiate(o, new Vector2(Random.Range(-2.75f, 2.75f), 6), transform.rotation);
        // enemy.GetComponent<Entity>().
        if(!GameObject.Find("Player").GetComponent<Player>().IsDeath()) StartCoroutine(Spawn(o, maxCycle, minCycle));
    }
}