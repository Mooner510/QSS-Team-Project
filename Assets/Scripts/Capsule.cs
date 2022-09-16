using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     if(!col.gameObject.CompareTag("Enemy") && !col.gameObject.CompareTag("Toxic")) return;
    //
    //     Destroy(gameObject);
    // }
}
