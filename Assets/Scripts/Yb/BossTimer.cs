using System.Collections;
using UnityEngine;

namespace Yb
{
    public class BossTimer : MonoBehaviour
    {
        [SerializeField] private GameObject bossPrefab; //보스 프리팹
        [SerializeField] private float spawnTime; //보스 스폰을 위해 필요한 시간
        [SerializeField] private Transform spawnPos; //보스 스폰 위치
        public static bool BossSpawned;
        private ScoreBoard _scoreBoard;

        private void Start() {
            _scoreBoard = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
            StartCoroutine(SpawnBoss());
        }

        private IEnumerator SpawnBoss()
        {
            yield return new WaitForSeconds(spawnTime * _scoreBoard.GetEnemySpawnBoost());
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            BossSpawned = true;
            var o = Instantiate(bossPrefab, spawnPos.position, Quaternion.identity);
            var velocity = 0.105f;
            for (var i = 0f; i < 2; i += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
                o.transform.position += Vector3.down * (velocity *= 0.96f);
            }

            yield return new WaitWhile(() => BossSpawned);
            StartCoroutine(SpawnBoss());
        }
    }
}