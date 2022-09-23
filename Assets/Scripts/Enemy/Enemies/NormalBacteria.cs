using System.Collections;
using UnityEngine;

namespace Enemy.Enemies
{
    public class NormalBacteria : Enemy
    {
        [SerializeField] private float attackDelay;
        private bool _right;
        private float _turnChance;

        public override EnemyType GetEnemyType() => EnemyType.NormalBacteria;

        protected override float GetAttackDelay() => attackDelay;

        protected override Vector3 Movement()
        {
            var to = transform.position;
            var from = GameObject.Find("Player").GetComponent<Player>().transform.position;
            return (from - to).normalized / 4 + Vector3.down;
        }

        protected override void Attack()
        {
            var rot = transform.rotation;
            var x = transform.position.x;
            if (x <= -2.3) _right = true;
            else if (x >= -2.3) _right = false;
            else if (Random.value <= 0.05f + _turnChance)
            {
                _turnChance = 0;
                _right = !_right;
            }
            _turnChance += 0.005f * Time.deltaTime;
            rot.z += Random.Range(0, 1f) * (_right ? 1 : -1);
            transform.rotation = rot;
        }
    }
}