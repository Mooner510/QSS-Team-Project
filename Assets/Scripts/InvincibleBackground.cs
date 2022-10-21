using UnityEngine;

public class InvincibleBackground : MonoBehaviour
{
    private Player _player;
    private Color _preColor;
    private SpriteRenderer _renderer;

    private static readonly Color AquaColor = new Color(0, 1f, 1f, 0.3f);
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = Color.white;
        _preColor = Color.black;
    }

    private void Update()
    {
        _preColor = _player.IsInvincible() ? AquaColor : Color.clear;
        var color = _renderer.color;
        color += (_preColor - color) * Time.deltaTime * 1.35f;
        _renderer.color = color;
    }
}