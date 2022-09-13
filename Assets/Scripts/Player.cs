using LivingEntity;
using UnityEngine;

public class Player : Entity
{
    private void Update()
    {
        Movement();
    }
    
    private void Movement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x - GetSpeed() * Time.deltaTime, -2.4f, 2.4f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x + GetSpeed() * Time.deltaTime, -2.4f, 2.4f);
            transform.position = pos;
        }
    }
}
