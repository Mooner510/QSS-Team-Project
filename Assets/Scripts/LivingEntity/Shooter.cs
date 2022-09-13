using UnityEngine;

namespace LivingEntity
{
    public class Shooter : MonoBehaviour, IShooter
    {
        [SerializeField] private GameObject capsule;
        // [SerializeField] private float arrowDamage;
        [SerializeField] private float defaultShootDelay;
        private float _shootDelay;
        private float _shoot;

        private void Start()
        {
            _shootDelay = defaultShootDelay;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) Shoot();
        }

        // public float GetArrowDamage() => arrowDamage;

        public void Shoot()
        {
            if (_shoot + _shootDelay >= Time.realtimeSinceStartup) return;
            _shoot = Time.realtimeSinceStartup;
            Instantiate(capsule, transform.position, transform.rotation);
        }
    }
}