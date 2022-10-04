using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Player _player;
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        var color = (Color.red * (1 - _player.GetHealth() / _player.GetMaxHealth()) + Color.green * (_player.GetPain() / 100f)) * 0.4f;
        color.a = Math.Max(1 - _player.GetHealth() / _player.GetMaxHealth(), _player.GetPain() / 100f);
        GetComponent<SpriteRenderer>().color = color;
    }
}