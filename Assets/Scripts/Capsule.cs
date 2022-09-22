using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update() => transform.Translate(transform.up * Time.deltaTime * speed);

    private void OnBecameInvisible() => Destroy(gameObject);
}
