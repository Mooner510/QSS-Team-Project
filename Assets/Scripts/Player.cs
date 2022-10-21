using System;
using System.Collections;
using LivingEntity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity {
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer _plrSprite;
    private bool _invincible;
    private bool _invincibleByDamage;
    private float _pain;
    public int bulletLevel;
    private ScoreBoard _scoreBoard;

    protected override void Start() {
        base.Start();
        _invincible = false;
        _invincibleByDamage = false;
        _plrSprite = gameObject.GetComponent<SpriteRenderer>();
        _scoreBoard = GameObject.Find("ScoreBoard").GetComponent<ScoreBoard>();
        bulletLevel = 0;
        _pain = 0;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x - GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -2.5f,
                2.5f);
            transform.position = pos;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            var pos = transform.position;
            pos.x = Utils.Distance(pos.x + GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -2.5f,
                2.5f);
            transform.position = pos;
        } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            var pos = transform.position;
            pos.y = Utils.Distance(pos.y + GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -4.5f,
                4.5f);
            transform.position = pos;
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            var pos = transform.position;
            pos.y = Utils.Distance(pos.y - GetSpeed() * Time.deltaTime * _scoreBoard.GetSpeedBoostForPlayer(), -4.5f,
                4.5f);
            transform.position = pos;
        }
    }

    public float GetPain() => _pain;

    public void SetPain(float pain) => _pain = Utils.Distance(pain, 0, 100);

    public void AddPain(float pain) {
        _pain = Utils.Distance(_pain + pain, 0, 100);
        if (_pain >= 100)
            Kill();
    }

    public override float GetDamage() {
        return bulletLevel switch {
            1 => 1.25f,
            2 => 0.75f,
            3 => 1f,
            4 => 0.9375f,
            5 => 0.875f,
            6 => 0.8125f,
            7 => 1.125f,
            8 => 1.25f,
            _ => 1
        };
        // float additive = 0;
        // if (bulletLevel >= 7)
        //     additive += 1f;
        // else if (bulletLevel >= 4)
        //     additive += 0.5f;
        // else if (bulletLevel >= 1)
        //     additive += 0.25f;
        // return base.GetDamage() + additive;
    }

    public override bool IsDeath() => base.IsDeath() || GetPain() >= 100;

    public override void Kill() => SceneManager.LoadScene("Scenes/Restart");

    public void OnTriggerEnter2D(Collider2D col) {
        if (IsDeath())
            return;
        switch (col.tag) {
            case "Item":
                switch (col.GetComponent<Item>().GetItemType()) {
                    case ItemType.ScoreUp:
                        ScoreBoard.AddScore(200);
                        break;
                    case ItemType.PainDown:
                        AddPain(-10);
                        break;
                    case ItemType.Heal:
                        Heal(15);
                        break;
                    case ItemType.Invincibility:
                        StopCoroutine(Invincible());
                        StartCoroutine(Invincible());
                        break;
                    case ItemType.BulletUpgrade:
                        bulletLevel = Math.Min(bulletLevel + 1, 8);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Destroy(col.gameObject);
                break;
            case "Toxic":
                if (!_invincible && !_invincibleByDamage) {
                    Damage(col.gameObject.GetComponent<Toxic>().GetDamage());
                    Destroy(col.gameObject);
                }

                break;
            case "Enemy":
                if (!_invincible && !_invincibleByDamage) {
                    Damage(col.gameObject.GetComponent<Enemy.Enemy>().GetDamage());
                    Destroy(col.gameObject);
                }

                break;
        }
    }

    public override void Damage(float hp) {
        StopCoroutine(InvincibleByDamage());
        StartCoroutine(InvincibleByDamage());
        base.Damage(hp);
    }

    private IEnumerator InvincibleByDamage() {
        _invincibleByDamage = true;
        for (var j = 0; j < 2; j++)
        for (var i = 0f; i <= 1; i += Time.deltaTime) {
            _plrSprite.color = new Color(1, 1, 1, i < 0.5 ? 0.25f + i : 0.75f - (i - 1));
            yield return null;
        }

        _invincibleByDamage = false;
        _plrSprite.color = new Color(1, 1, 1, 1f);
    }

    public bool IsInvincible() => _invincible;

    private IEnumerator Invincible() {
        _invincible = true;
        GetComponent<SpriteRenderer>().sprite = sprites[1];

        yield return new WaitForSeconds(5f);

        for (var j = 0; j < 2; j++)
            for (var i = 0f; i <= 1; i += Time.deltaTime) {
                _plrSprite.color = new Color(1, 1, 1, i < 0.5 ? 0.25f + i : 0.75f - (i - 1));
                yield return null;
            }

        _invincible = false;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}