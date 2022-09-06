using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Translate(Vector3.up * speed);
    }
}
