using System;
using LivingEntity;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected GameObject bullet;
        private bool _attackable;
        private float _lastAttack;
        private ScoreBoard _scoreBoard;
        
        protected override void Start()
        {
            base.Start();
            _attackable = GetAttackDelay() > 0;
            _scoreBoard = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
            _lastAttack = 0;
        }

        private void Update()
        {
            if(Time.realtimeSinceStartup < 2) return;
            transform.position += Vector3.down * Time.deltaTime * GetSpeed() * Math.Min(GetHealth() / GetMaxHealth() + 0.1f, 1);
            if(!_attackable) return;
            if (_lastAttack + GetAttackDelay() * _scoreBoard.GetEnemyAttackBoost() >= Time.realtimeSinceStartup) return;
            _lastAttack = Time.realtimeSinceStartup;
            Attack();
        }

        private void OnBecameInvisible() => Destroy(gameObject);

        public virtual EnemyType GetEnemyType() => EnemyType.Unknown;

        protected virtual float GetAttackDelay() => 0;

        public bool IsAttackAble() => _attackable;

        protected virtual void Attack() {}

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Capsule")) return;
            ScoreBoard.AddScore(4);

            Damage(GameObject.Find("Player").GetComponent<Player>().GetDamage());
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color.a = GetHealth() / GetMaxHealth();
            spriteRenderer.color = color;

            Destroy(col.gameObject);
        }
    }
}