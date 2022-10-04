using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float speed;
    private float _velocity;

    private void Start() => _velocity = 4f;

    private void Update() => transform.Translate(transform.up * Time.deltaTime * speed * (_velocity *= 0.98f));

    private void OnBecameInvisible() => Destroy(gameObject);
}
