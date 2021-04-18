using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _timer;
    private int _direction = 1;
    private bool _broken = true;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _timer = changeTime;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_broken) return;

        _timer -= Time.deltaTime;

        if (_timer >= 0) return;
        _direction = -_direction;
        _timer = changeTime;
    }

    private void FixedUpdate()
    {
        if (!_broken) return;
        
        var position = _rigidbody2D.position;

        if (vertical)
        {
            _animator.SetFloat("Move X", 0);
            _animator.SetFloat("Move Y", _direction);
            position.y += Time.deltaTime * speed * _direction;
        }
        else
        {
            _animator.SetFloat("Move X", _direction);
            _animator.SetFloat("Move Y", 0);
            position.x += Time.deltaTime * speed * _direction;
        }

        _rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<RubyController>();

        if (player == null) return;
        player.ChangeHealth(-1);
    }

    public void Fix()
    {
        _broken = false;
        _rigidbody2D.simulated = false;
        _animator.SetTrigger("Fixed");
    }
}