using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Enemies
{
    public class CoronaBoss : Boss
    {
        private BossAttack[] _attacks;

        public override EnemyType GetEnemyType() => EnemyType.CoronaBoss;

        protected override void Start() {
            var player = GameObject.Find("Player").GetComponent<Player>();
            var mul = player.bulletLevel switch {
                1 => 60f,
                2 => 75f,
                3 => 90f,
                4 => 110f,
                5 => 130f,
                6 => 170f,
                7 => 210f,
                8 => 260f,
                _ => 50
            };
            SetMaxHealth(mul);
            SetHealth(mul);
            _attacks = new[]
            {
                new BossAttack(2, () =>
                {
                    for (var i = 150; i <= 210; i += 12)
                    {
                        var o = Instantiate(bullets[0], transform.position, Quaternion.Euler(0, 0, Random.Range(i - 6f, i + 6f)));
                        o.GetComponent<Toxic>().SetDamage(GetDamage());
                    }
                }),
                new BossAttack(5, () => StartCoroutine(Push(Spawn(bullets[Random.Range(1, 5)]))))
            };
            base.Start();
        }

        private GameObject Spawn(GameObject o) {
            return Instantiate(o, transform.position,
                o.GetComponent<Enemy>().GetEnemyType() == EnemyType.Bacteria
                ? Quaternion.Euler(0, 0, -45)
                : transform.rotation);
        }

        private IEnumerator Push(GameObject o)
        {
            var q = Quaternion.Euler(0, 0, Random.Range(150, 210f));
            var velocity = 5f;
            for (var i = 0f; i <= 2; i += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
                if(o != null) break;
                o.transform.position += q * Vector3.forward * (velocity *= 0.975f);
            }
        }

        protected override int GetAttackCount() => _attacks.Length;

        protected override BossAttack GetAttack(int index) => _attacks[index];
    }
}