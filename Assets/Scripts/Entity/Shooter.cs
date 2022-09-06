using UnityEngine;

namespace Entity
{
    public class Shooter : MonoBehaviour, IShooter
    {
        [SerializeField] private float arrowDamage;
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) Shoot();
        }

        public float GetArrowDamage() => arrowDamage;

        public void Shoot()
        {
            //TODO: We Have to Add Shooting Code
        }
    }
}