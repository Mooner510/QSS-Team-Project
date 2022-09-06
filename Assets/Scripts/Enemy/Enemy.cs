using UnityEngine;

namespace Enemy
{
    public abstract class Enemy : Entity.Entity
    {

        public void DamagePlayer()
        {
            GameObject.Find("Player").GetComponent<Player>().Damage(damage / 2);
        }

        protected abstract void Attack();
    }
}