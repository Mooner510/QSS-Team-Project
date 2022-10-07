using System;
using System.Collections;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Player _player;
    private Color _preColor;
    private SpriteRenderer _renderer;
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = Color.white;
        _preColor = Color.black;
    }

    private void Update()
    {
        _preColor = (Color.red * (1 - _player.GetHealth() / _player.GetMaxHealth()) + Color.green * (_player.GetPain() / 100f)) * 0.4f;
        _preColor.a = Math.Min(Math.Max(1 - _player.GetHealth() / _player.GetMaxHealth(), _player.GetPain() / 100f) * 1.2f, 1f);
        var color = _renderer.color;
        color += (_preColor - color) * 0.4f * Time.deltaTime;
        _renderer.color = color;
    }
}