using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Transform _transform;
    public float XborderPosition;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _transform.position += Vector3.right * Speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _transform.position += Vector3.left * Speed * Time.deltaTime;
        }

        float clampX = Mathf.Clamp(_transform.position.x,-XborderPosition,XborderPosition);
        _transform.position = new Vector3(clampX, _transform.position.y, _transform.position.z);

        float horizontalX = Input.GetAxis("Horizontal");
        if (horizontalX > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (horizontalX < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
