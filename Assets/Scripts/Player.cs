using System.Collections;
using LivingEntity;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private bool isInvincible;
    private SpriteRenderer plrSprite;

    void Start()
    {
        plrSprite = gameObject.GetComponent<SpriteRenderer>();
    }
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
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "ScoreUp":
                 ScoreBoard.AddScore(500);
                break;
            case "PainDown":
                ScoreBoard.AddPain(-15);
                 if(ScoreBoard.GetPain() < 0)
                    ScoreBoard.SetPain(0);
                break;
            case "heal":
                Heal(15);
                break;
            case "invincibility":
                StopCoroutine(Invincible());
                StartCoroutine(Invincible());
                break;
            case "BulletUpgrade":
                /*
                 bulletLevel +=1;
                 if(bulletLevel > 5) 
                 bulletLevel = 5;
                 */
                break;
            case "Toxic":
                if (isInvincible) Destroy(col.gameObject);
                else
                    Damage(col.gameObject.GetComponent<Toxic>().GetDamage());

                break;
            case "Enemy":
                if (isInvincible) Destroy(col.gameObject);
                else
                    Damage(col.gameObject.GetComponent<Entity>().GetDamage());

                break;
        }
    }

    public IEnumerator Invincible()
    {
        isInvincible = true;
        plrSprite.color = new Color(1, 1, 1, 0.5f);
        
        yield return new WaitForSeconds(2.5f);
        
        plrSprite.color = new Color(1, 1, 1, 0.75f);
        
        yield return new WaitForSeconds(0.5f);
        
        isInvincible = false;
        plrSprite.color = new Color(1, 1, 1, 1f);
    }
}
