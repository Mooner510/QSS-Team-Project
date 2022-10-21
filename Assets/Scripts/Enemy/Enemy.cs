using System;
using System.Collections;
using LivingEntity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected GameObject bullet;
        [SerializeField] private int score;
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

        protected virtual Vector3 Movement() => -transform.up + Vector3.down;

        private void Update()
        {
            if(Time.realtimeSinceStartup < 2) return;
            transform.position += Movement() * Time.deltaTime * GetSpeed() *
                                  Math.Min(GetHealth() / GetMaxHealth() + 0.1f, 1);
            if(!_attackable) return;
            if (_lastAttack + GetAttackDelay() * _scoreBoard.GetEnemyAttackBoost() >= Time.realtimeSinceStartup) return;
            _lastAttack = Time.realtimeSinceStartup;
            StartCoroutine(StartAttackDelay());
        }

        private void OnBecameInvisible()
        {
            if(!gameObject.activeSelf) return;
            GameObject.Find("Player").GetComponent<Player>().AddPain(GetDamage() / 2);
            Destroy(gameObject);
        }

        public virtual EnemyType GetEnemyType() => EnemyType.Unknown;

        protected virtual float GetAttackDelay() => 0;
        
        public bool IsAttackAble() => _attackable;

        protected IEnumerator StartAttackDelay() {
            yield return new WaitForSeconds(GetAttackDelay() * _scoreBoard.GetEnemyAttackBoost());
            Attack();
        }

        protected virtual void Attack() {}

        public override void Kill()
        {
            var o = GameObject.Find("ScoreBoard");
            var scoreBoard = o.GetComponent<ScoreBoard>();
            var particle = scoreBoard.particle;
            for (var i = 0; i < 6; i++)
            {
                var o2 = Instantiate(particle, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
                o2.GetComponent<SpriteRenderer>().color = GetEnemyType().GetRandomColor();
                o2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                o2.GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.6f, 0.9f);
            }

            if (GetEnemyType() != EnemyType.Cancer && Random.value <= 0.3f)
                Instantiate(scoreBoard.item, transform.position, Quaternion.identity);
            base.Kill();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Capsule")) return;
            ScoreBoard.AddScore(score);

            Damage(GameObject.Find("Player").GetComponent<Player>().GetDamage());
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var color = spriteRenderer.color;
            color.a = GetHealth() / GetMaxHealth() * 0.8f + 0.2f;
            spriteRenderer.color = color;
            
            var scoreboard = GameObject.Find("ScoreBoard");
            var particle = scoreboard.GetComponent<ScoreBoard>().particle;
            for (var i = 0; i < 2; i++)
            {
                var o = Instantiate(particle, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), Quaternion.identity);
                o.GetComponent<SpriteRenderer>().color = Color.white;
                o.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                o.GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.1f, 0.3f);
            }

            Destroy(col.gameObject);
        }
    }
}