using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Player _player;
    private Color _preColor;
    private SpriteRenderer _renderer;
    
    private static readonly Color RedColor = new Color(0.45f, 0f, 0f, 1f);
    private static readonly Color GreenColor = new Color(0f, 0.45f, 0f, 1f);
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = Color.white;
        _preColor = Color.black;
    }

    private void Update()
    {
        _preColor = RedColor * (1 - _player.GetHealth() / _player.GetMaxHealth()) + GreenColor * (_player.GetPain() / 100f);
        _preColor.a = Math.Min(Math.Max(1 - _player.GetHealth() / _player.GetMaxHealth(), _player.GetPain() / 100f) * 1.2f, 1f);
        var color = _renderer.color;
        color += (_preColor - color) * 0.4f * Time.deltaTime;
        _renderer.color = color;
    }
}