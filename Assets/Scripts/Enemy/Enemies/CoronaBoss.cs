using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Enemies
{
    public class CoronaBoss : Boss
    {
        private BossAttack[] _attacks;

        public override EnemyType GetEnemyType() => EnemyType.CoronaBoss;

        protected override void Start()
        {
            _attacks = new[]
            {
                new BossAttack(2, () =>
                {
                    Debug.Log("Attack 1");
                    for (var i = 150; i <= 210; i += 12)
                    {
                        var o = Instantiate(bullets[0], transform.position, Quaternion.Euler(0, 0, i));
                        o.GetComponent<Toxic>().SetDamage(GetDamage());
                    }
                }),
                new BossAttack(3, () =>
                {
                    Debug.Log("Attack 2");
                    StartCoroutine(Push(Instantiate(bullets[Random.Range(1, 5)], transform.position, Quaternion.identity)));
                })
            };
            base.Start();
        }

        private static IEnumerator Push(GameObject o)
        {
            var q = Quaternion.Euler(0, 0, Random.Range(150, 210f));
            var velocity = 0.15f;
            for (var i = 0f; i <= 2; i += Time.deltaTime)
            {
                yield return null;
                o.transform.position += q * Vector3.forward * (velocity *= 0.975f);
            }
        }

        protected override int GetAttackCount() => _attacks.Length;

        protected override BossAttack GetAttack(int index) => _attacks[index];
    }
}