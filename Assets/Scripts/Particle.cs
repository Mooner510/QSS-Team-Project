using UnityEngine;

public class Particle : MonoBehaviour
{
    private Vector3 _beforeScale;

    private void Start() => _beforeScale = transform.localScale;

    private void Update() => transform.localScale -= _beforeScale * Time.deltaTime / 4;

    private void OnBecameInvisible() => Destroy(gameObject);
}