using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public GameObject projectilePrefab;
    
    private int _currentHealth;
    public int health => _currentHealth;

    private bool _isInvincible;
    private float _invincibleTimer;
    
    private Rigidbody2D _rigidbody2d;
    private float _horizontal;
    private float _vertical;
    private Vector2 _move;

    private Animator _animator;
    private Vector2 _lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        _move = new Vector2(_horizontal, _vertical);

        if (!Mathf.Approximately(_move.x, 0.0f) || !Mathf.Approximately(_move.y, 0.0f))
        {
            _lookDirection.Set(_move.x, _move.y);
            _lookDirection.Normalize();
        }
        
        _animator.SetFloat("Look X", _lookDirection.x);
        _animator.SetFloat("Look Y", _lookDirection.y);
        _animator.SetFloat("Speed", _move.magnitude);
        
        if(Input.GetKeyDown(KeyCode.C)) 
            Launch();

        // invincibility check should come last for each update

        if (!_isInvincible) return;
        
        _invincibleTimer -= Time.deltaTime;
        if (_invincibleTimer < 0)
            _isInvincible = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position += _move * (speed * Time.deltaTime);

        _rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (_isInvincible) return;
            
            _animator.SetTrigger("Hit");

            _isInvincible = true;
            _invincibleTimer = timeInvincible;
        }
        
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
        Debug.Log(_currentHealth + "/" + maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject =
            Instantiate(projectilePrefab, _rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(_lookDirection, 300);
        
        _animator.SetTrigger("Launch");
    }
}