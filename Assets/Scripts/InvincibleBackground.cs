using System;
using System.Collections;
using UnityEngine;

public class InvincibleBackground : MonoBehaviour
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
        _preColor = _player.IsInvincible() ? Color.cyan : Color.white;
        _preColor.a = _player.IsInvincible() ? 0.6f : 0;
        var color = _renderer.color;
        color += (_preColor - color) * Time.deltaTime * 1.35f;
        _renderer.color = color;
    }
}