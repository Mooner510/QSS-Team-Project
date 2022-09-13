using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTimer : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;//보스 프리팹
    [SerializeField] private float spawnTime;//보스 스폰을 위해 필요한 시간
    [SerializeField] private Transform spawnPos;//보스 스폰 위치

    private void Start()
    {
        StartCoroutine(SpawnBoss());
    }

    public IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(spawnTime);
        Instantiate(bossPrefab, spawnPos.position, Quaternion.identity);
    }
}
