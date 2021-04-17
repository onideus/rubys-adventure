using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    
    private int _currentHealth;
    public int health => _currentHealth;

    private bool _isInvincible;
    private float invincibleTimer;
    
    private Rigidbody2D _rigidbody2d;
    private float _horizontal;
    private float _vertical;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();

        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        if (!_isInvincible) return;
        
        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer < 0)
            _isInvincible = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * _horizontal * Time.deltaTime;
        position.y = position.y + speed * _vertical * Time.deltaTime;

        _rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible) return;

            _isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
    }
}