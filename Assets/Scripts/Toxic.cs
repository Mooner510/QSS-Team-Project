using UnityEngine;

public class Toxic : MonoBehaviour
{
    [SerializeField] private float speed;
    private float _damage;

    private void Update() => transform.Translate(new Vector3(transform.forward.x, transform.forward.z) * Time.deltaTime * speed);

    private void OnBecameInvisible() => Destroy(gameObject);

    public void SetDamage(float damage) => _damage = damage;

    public float GetDamage() => _damage;
}
