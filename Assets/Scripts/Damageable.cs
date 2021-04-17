using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        var rubyController = other.GetComponent<RubyController>();

        if (rubyController == null) return;
        rubyController.ChangeHealth(-1);
    }
}
