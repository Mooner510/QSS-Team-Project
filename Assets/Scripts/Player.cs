using System.Collections;
using LivingEntity;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private bool isInvincible;
    private SpriteRenderer _plrSprite;
    private float _pain;

    protected override void Start()
    {
        base.Start();
        _plrSprite = gameObject.GetComponent<SpriteRenderer>();
        _pain = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x - GetSpeed() * Time.deltaTime * GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GetSpeedBoostForPlayer(), -2.4f, 2.4f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x + GetSpeed() * Time.deltaTime * GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GetSpeedBoostForPlayer(), -2.4f, 2.4f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            var pos = transform.position;
            pos.y = Utils.Distance(pos.y + GetSpeed() * Time.deltaTime * GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GetSpeedBoostForPlayer(), -4.5f, 4.5f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            var pos = transform.position;
            pos.y = Utils.Distance(pos.y - GetSpeed() * Time.deltaTime * GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>().GetSpeedBoostForPlayer(), -4.5f, 4.5f);
            transform.position = pos;
        }
    }

    public float GetPain() => _pain;

    public void SetPain(float pain) => _pain = pain;

    public void AddPain(float pain) => _pain += pain;

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "ScoreUp":
                 ScoreBoard.AddScore(500);
                break;
            case "PainDown":
                AddPain(-15);
                 if(GetPain() < 0)
                    SetPain(0);
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

    private IEnumerator Invincible()
    {
        isInvincible = true;
        _plrSprite.color = new Color(1, 1, 1, 0.5f);
        
        yield return new WaitForSeconds(2.5f);
        
        _plrSprite.color = new Color(1, 1, 1, 0.75f);
        
        yield return new WaitForSeconds(0.5f);
        
        isInvincible = false;
        _plrSprite.color = new Color(1, 1, 1, 1f);
    }
}
