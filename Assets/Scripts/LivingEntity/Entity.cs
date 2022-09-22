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

        public virtual float GetMaxHealth() => maxHealth;

        public void SetMaxHealth(float hp) => maxHealth = Math.Max(hp, 0);

        public void AddMaxHealth(float hp) => maxHealth = Math.Max(maxHealth + hp, 0);

        public virtual float GetHealth() => _health;

        public void SetHealth(float hp) => _health = Utils.Distance(hp, 0, maxHealth);

        public void Damage(float hp)
        {
            if (hp < 0) throw new ArgumentException("Cannot Input Negative Number");
            _health = Math.Max(_health - hp, 0);
            if(IsDeath()) Kill();
        }

        public void Heal(float hp)
        {
            if (hp < 0) throw new ArgumentException("Cannot Input Negative Number");
            _health = Math.Min(_health + hp, maxHealth);
        }

        public virtual float GetDamage() => damage;

        public void SetDamage(float dmg) 
        {
            if (dmg < 0) throw new ArgumentException("Cannot Input Negative Number");
            damage = Math.Max(dmg, 0);
        }

        public virtual float GetSpeed() => speed;

        public void SetSpeed(float spd)
        {
            if (spd < 0) throw new ArgumentException("Cannot Input Negative Number");
            speed = Math.Max(spd, 0);
        }

        public void AddSpeed(float spd) => speed = Math.Max(speed + spd, 0);

        public void ResetSpeed() => speed = _defaultSpeed;

        public float GetDefaultSpeed() => _defaultSpeed;

        public virtual bool IsDeath() => _health <= 0;

        public virtual void Kill() => Destroy(gameObject);
    }
}