using System;
using UnityEngine;

namespace Entity
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] protected float health;
        [SerializeField] protected float speed;
        [SerializeField] protected float damage;
        private float _defaultSpeed;
        
        void Start()
        {
            _defaultSpeed = speed;
        }

        public float GetHealth() => health;

        public void SetHealth(float hp) => health = Math.Min(Math.Max(hp, 0), 100);

        public void Damage(float hp)
        {
            if (hp < 0) throw new ArgumentException("Cannot Input Negative Number");
            health = Math.Max(health - hp, 0);
            if(IsDeath()) Kill();
        }

        public void Heal(float hp)
        {
            if (hp < 0) throw new ArgumentException("Cannot Input Negative Number");
            health = Math.Min(health + hp, 100);
        }

        public float GetSpeed() => speed;

        public float GetDamage() => damage;

        public void SetDamage(float dmg) 
        {
            if (dmg < 0) throw new ArgumentException("Cannot Input Negative Number");
            damage = dmg;
        }

        public void AddSpeed(float spd) => speed = Math.Min(Math.Max(health + spd, 0), 100);

        public void SetSpeed(float spd)
        {
            if (spd < 0) throw new ArgumentException("Cannot Input Negative Number");
            speed = Math.Min(Math.Max(spd, 0), 100);
        }

        public float GetDefaultSpeed() => _defaultSpeed;

        public bool IsDeath() => health <= 0;

        public void Kill() => Destroy(gameObject);
    }
}