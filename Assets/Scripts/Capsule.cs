using System;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Enemy")) return;
        ScoreBoard.AddScore(4);
            
        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}
