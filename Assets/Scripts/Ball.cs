using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class Ball : MonoBehaviour
{
    //public event Action OnBallDestroyed;
    public float JumpForce;
    public Platform Platform;
    public float YPosition;
    public GameManager GameManager;
    public bool _ballOnPlatform;

    private Rigidbody2D _rigidbody;
    private Vector3 _reflectetDirection;
    private Vector3 _ballPosition;
    private Vector3 _platformPosition;
    private Transform _transform;
    private Vector3 _hitPoint;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ballPosition = transform.position;
        _platformPosition = Platform.transform.position;
        _transform = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _hitPoint = collision.contacts[0].normal;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.velocity = Vector3.Reflect(_reflectetDirection, _hitPoint).normalized * JumpForce;
        FixBallDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int live = 1;
        GameManager.BallLivesHandler(live);
        transform.position = _ballPosition;
        Platform.transform.position = _platformPosition;
        _rigidbody.velocity = Vector3.zero;
        _ballOnPlatform = true;
    }

    private void FixedUpdate()
    {
    }

    public void Update()
    { 
        _reflectetDirection = _rigidbody.velocity;
        if (_ballOnPlatform)
        {
            transform.position = new Vector2(Platform.transform.position.x, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _ballOnPlatform && GameManager.GameIsOn)
        {
            _rigidbody.velocity = Vector3.right + Vector3.up * JumpForce;
            _ballOnPlatform = false;
        }     
    }

    private void FixBallDirection()
    {
        float minXVelocity = 1f;
        float minYVelocity = 1f;
        Vector2 ballVelocity = _rigidbody.velocity;
        if(Mathf.Abs(ballVelocity.x) < minXVelocity)
        {
            ballVelocity.x = ballVelocity.x > 0 ? minXVelocity : -minXVelocity;
            _rigidbody.velocity = ballVelocity.normalized * JumpForce;
        }
        if(Mathf.Abs(ballVelocity.y) < minYVelocity)
        {
            ballVelocity.y = ballVelocity.y > 0 ? minYVelocity : - minYVelocity;
            _rigidbody.velocity = ballVelocity.normalized * JumpForce;
        }
    }
}