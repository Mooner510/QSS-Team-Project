using UnityEngine;

public class Player : Entity.Entity
{
    [SerializeField] private GameObject capsule;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x - speed * Time.deltaTime, -2.4f, 2.4f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x + speed * Time.deltaTime, -2.4f, 2.4f);
            transform.position = pos;
        }
    }
}
