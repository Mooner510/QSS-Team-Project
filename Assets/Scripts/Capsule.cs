using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float speed;
    private float _velocity;

    private void Start() => _velocity = 0.075f;

    private void FixedUpdate() => transform.Translate(transform.up * speed * (_velocity *= 0.98f));

    private void OnBecameInvisible() => Destroy(gameObject);
}
