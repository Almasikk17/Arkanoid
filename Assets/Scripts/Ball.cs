using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector3 _reflectetDirection;
    private Vector3 _ballPosition;
    private Vector3 _platformPosition;
    private bool _ballOnPlatform = true;

    public float JumpForce;
    public Platform Platform;
    public Block Block;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ballPosition = transform.position;
        _platformPosition = Platform.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPoint = collision.contacts[0].normal;
        _rigidbody.velocity = Vector3.Reflect(_reflectetDirection, hitPoint).normalized * JumpForce;
        Debug.Log(_reflectetDirection.magnitude);

        if(collision.gameObject.CompareTag("BlockTag"))
        {
            Block.SetDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = _ballPosition;
        Platform.transform.position = _platformPosition;
        _rigidbody.velocity = Vector3.zero;
        _ballOnPlatform = true;
    }

    private void Update()
    {
        _reflectetDirection = _rigidbody.velocity.normalized;
       

        if (_ballOnPlatform)
        {
            transform.position = new Vector2(Platform.transform.position.x, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _ballOnPlatform)
        {
            _rigidbody.velocity = Vector3.right + Vector3.up * JumpForce;
            _ballOnPlatform = false;
        }
    }
}