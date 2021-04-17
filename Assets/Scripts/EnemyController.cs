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
    private float _timer;
    private int _direction = 1;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer >= 0) return;
        _direction = -_direction;
        _timer = changeTime;
    }

    private void FixedUpdate()
    {
        var position = _rigidbody2D.position;

        if (vertical)
        {
            position.y += Time.deltaTime * speed * _direction;
        }
        else
        {
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
}