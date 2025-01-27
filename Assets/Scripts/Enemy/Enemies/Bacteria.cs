﻿using UnityEngine;

namespace Enemy.Enemies
{
    public class Bacteria : Enemy
    {
        [SerializeField] private float attackDelay;

        public override EnemyType GetEnemyType() => EnemyType.Bacteria;

        protected override float GetAttackDelay() => attackDelay;
        
        protected override Vector3 Movement() => Vector3.down;

        protected override void Attack()
        {
            var o = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
            o.GetComponent<Toxic>().SetDamage(GetDamage());
            StartCoroutine(StartAttackDelay());
        }
    }
}