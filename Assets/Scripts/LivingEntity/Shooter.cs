using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LivingEntity
{
    public class Shooter : MonoBehaviour, IShooter
    {
        [SerializeField] private GameObject capsule;
        [SerializeField] private Sprite[] sprites;
        // [SerializeField] private float arrowDamage;
        [SerializeField] private float defaultShootDelay;
        private Player _player;
        private float _shootDelay;
        private float _shoot;

        private void Start()
        {
            _shootDelay = defaultShootDelay;
            _player = GameObject.Find("Player").GetComponent<Player>();
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
            switch (_player.bulletLevel)
            {
                case 0:
                case 1:
                    Launch(Quaternion.identity);
                    break;
                case 2:
                case 3:
                    Launch(Quaternion.identity);
                    StartCoroutine(Launch(0.15f, Quaternion.identity));
                    break;
                case 4:
                    Launch(Quaternion.identity);
                    StartCoroutine(Launch(0.15f, Quaternion.identity));
                    Launch(Random.value < 0.5f
                        ? Quaternion.Euler(0, 0, Random.Range(-3f, -1f))
                        : Quaternion.Euler(0, 0, Random.Range(1f, 3f)));
                    break;
                case 5:
                    Launch(Quaternion.identity);
                    StartCoroutine(Launch(0.15f, Quaternion.identity));
                    Launch(Quaternion.Euler(0, 0, Random.Range(-3f, -1f)));
                    Launch(Quaternion.Euler(0, 0, Random.Range(1f, 3f)));
                    break;
                case 6:
                case 7:
                    Launch(Quaternion.identity);
                    StartCoroutine(Launch(0.15f, Quaternion.identity));
                    Launch(Quaternion.Euler(0, 0, Random.Range(-3f, -1f)));
                    Launch(Quaternion.Euler(0, 0, Random.Range(1f, 3f)));
                    StartCoroutine(Launch(0.15f, Random.value < 0.5f
                        ? Quaternion.Euler(0, 0, Random.Range(-3f, -1f))
                        : Quaternion.Euler(0, 0, Random.Range(1f, 3f))));
                    break;
                case 8:
                    Launch(Quaternion.identity);
                    StartCoroutine(Launch(0.15f, Quaternion.identity));
                    Launch(Quaternion.Euler(0, 0, Random.Range(-3f, -1f)));
                    StartCoroutine(Launch(0.15f, Quaternion.Euler(0, 0, Random.Range(-3f, -1f))));
                    Launch(Quaternion.Euler(0, 0, Random.Range(1f, 3f)));
                    StartCoroutine(Launch(0.15f, Quaternion.Euler(0, 0, Random.Range(1f, 3f))));
                    break;
            }
        }

        private void Launch(Quaternion direction)
        {
            Instantiate(capsule, transform.position, direction).GetComponent<SpriteRenderer>().sprite = _player.bulletLevel switch
            {
                0 => sprites[0],
                1 => sprites[1],
                2 => sprites[1],
                3 => sprites[2],
                4 => sprites[2],
                5 => sprites[2],
                6 => sprites[2],
                7 => sprites[3],
                8 => sprites[3],
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IEnumerator Launch(float delay, Quaternion direction)
        {
            yield return new WaitForSeconds(delay);
            Launch(direction);
        }
    }
}