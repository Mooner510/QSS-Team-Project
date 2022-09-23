using System.Collections;
using UnityEngine;

namespace Yb
{
    public class BossTimer : MonoBehaviour
    {
        [SerializeField] private GameObject bossPrefab;//보스 프리팹
        [SerializeField] private float spawnTime;//보스 스폰을 위해 필요한 시간
        [SerializeField] private Transform spawnPos;//보스 스폰 위치

        private void Start()
        {
            StartCoroutine(SpawnBoss());
        }

        private IEnumerator SpawnBoss()
        {
            yield return new WaitForSeconds(spawnTime);
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            var o = Instantiate(bossPrefab, spawnPos.position, Quaternion.identity);
            var velocity = 0.105f;
            for (var i = 0f; i < 2; i += Time.deltaTime)
            {
                yield return null;
                o.transform.position += Vector3.down * (velocity *= 0.96f);
            }
        }
    }
}
