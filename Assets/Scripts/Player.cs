using System;
using System.Collections;
using LivingEntity;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private bool isInvincible;
    private SpriteRenderer _plrSprite;
    private float _pain;
    private int _bulletLevel;
    private ScoreBoard _scoreBoard;

    protected override void Start()
    {
        base.Start();
        _plrSprite = gameObject.GetComponent<SpriteRenderer>();
        _scoreBoard = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        _bulletLevel = 0;
        _pain = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x - GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -2.4f, 2.4f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x + GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -2.4f, 2.4f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            var pos = transform.position;
            pos.y = Utils.Distance(pos.y + GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -4.5f, 4.5f);
            transform.position = pos;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            var pos = transform.position;
            pos.y = Utils.Distance(pos.y - GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -4.5f, 4.5f);
            transform.position = pos;
        }
    }

    public float GetPain() => _pain;

    public void SetPain(float pain) => _pain = Utils.Distance(pain, 0, 100);

    public void AddPain(float pain) => _pain = Utils.Distance(_pain + pain, 0, 100);

    public override float GetDamage() => base.GetDamage() + _bulletLevel;

    public override bool IsDeath() => base.IsDeath() && GetPain() <= 0;

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "ScoreUp":
                 ScoreBoard.AddScore(200);
                break;
            case "PainDown":
                AddPain(-15);
                break;
            case "Heal":
                Heal(15);
                break;
            case "Invincibility":
                StopCoroutine(Invincible());
                StartCoroutine(Invincible());
                break;
            case "BulletUpgrade":
                _bulletLevel = Math.Min(_bulletLevel + 1, 5);
                break;
            case "Toxic":
            case "Enemy":
                if (!isInvincible) Damage(col.gameObject.GetComponent<Toxic>().GetDamage());
                Destroy(col.gameObject);
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
