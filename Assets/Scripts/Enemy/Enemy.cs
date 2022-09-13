using LivingEntity;
using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : Entity
    {
        [SerializeField] protected GameObject bullet;
        private bool _attackable;
        private float _lastAttack;
        
        protected override void Start()
        {
            base.Start();
            _attackable = GetAttackDelay() > 0;
            _lastAttack = 0;
        }

        private void Update()
        {
            if(Time.realtimeSinceStartup < 2) return;
            transform.Translate(Vector3.down * Time.deltaTime * GetSpeed());
            if(!_attackable) return;
            if (_lastAttack + GetAttackDelay() >= Time.realtimeSinceStartup) return;
            _lastAttack = Time.realtimeSinceStartup;
            Attack();
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        public abstract EnemyType GetEnemyType();

        protected abstract float GetAttackDelay();

        public bool IsAttackAble() => _attackable;

        protected virtual void Attack() {}

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            var player = GameObject.Find("Player");
            player.GetComponent<Player>().Damage(GetDamage());

            Destroy(gameObject);
        }
    }
}