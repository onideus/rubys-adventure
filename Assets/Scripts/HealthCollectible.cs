using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var rubyController = other.GetComponent<RubyController>();

        if (rubyController == null) return;
        if (rubyController.health >= rubyController.maxHealth) return;
        rubyController.ChangeHealth(1);
        Destroy(gameObject);
    }
}