using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 8;
    private float _yVelocity;
    private Vector3 _direction;
    [SerializeField]private float _gravity;
    [SerializeField] private float _jumpHeight;
    private CharacterController _controller;
    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (_controller.isGrounded==true)
        {
            float _horizontalInput = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, _horizontalInput)*_speed;
           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
            }
        }
        _controller.Move(_direction * Time.deltaTime);
        _direction.y -= _gravity *Time.deltaTime;
        
        
    }
}
