using LivingEntity;
using UnityEngine;

public class Player : Entity
{
    private float _pain;

    protected override void Start()
    {
        base.Start();
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
    
    
}
