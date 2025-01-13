using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void SetDamage()
    {
        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
