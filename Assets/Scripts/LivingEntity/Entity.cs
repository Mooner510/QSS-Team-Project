using System;
using UnityEngine;

namespace LivingEntity
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        private float _health;
        private float _defaultSpeed;

        protected virtual void Start()
        {
            _health = maxHealth;
            _defaultSpeed = speed;
        }

        public float GetMaxHealth() => maxHealth;

        public void SetMaxHealth(float hp) => maxHealth = hp;

        public void AddMaxHealth(float hp) => maxHealth += hp;

        public float GetHealth() => _health;

        public void SetHealth(float hp) => _health = Math.Min(Math.Max(hp, 0), maxHealth);

        public void Damage(float hp)
        {
            if (hp < 0) throw new ArgumentException("Cannot Input Negative Number");
            _health = Math.Max(_health - hp, 0);
            if(IsDeath()) Kill();
        }

        public void Heal(float hp)
        {
            if (hp < 0) throw new ArgumentException("Cannot Input Negative Number");
            _health = Math.Min(_health + hp, 100);
        }

        public float GetDamage() => damage;

        public void SetDamage(float dmg) 
        {
            if (dmg < 0) throw new ArgumentException("Cannot Input Negative Number");
            damage = dmg;
        }

        public float GetSpeed() => speed;

        public void SetSpeed(float spd)
        {
            if (spd < 0) throw new ArgumentException("Cannot Input Negative Number");
            speed = Math.Min(Math.Max(spd, 0), 100);
        }

        public void AddSpeed(float spd) => speed = Math.Min(Math.Max(_health + spd, 0), 100);

        public void ResetSpeed() => speed = _defaultSpeed;

        public float GetDefaultSpeed() => _defaultSpeed;

        public bool IsDeath() => _health <= 0;

        public virtual void Kill() => Destroy(gameObject);
    }
}