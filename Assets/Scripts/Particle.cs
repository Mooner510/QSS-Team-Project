using UnityEngine;

public class Particle : MonoBehaviour
{
    private Vector3 _beforeScale;

    private void Start() => _beforeScale = transform.localScale;

    private void FixedUpdate() => transform.localScale -= _beforeScale * Time.fixedDeltaTime / 4;

    private void OnBecameInvisible() => Destroy(gameObject);
}