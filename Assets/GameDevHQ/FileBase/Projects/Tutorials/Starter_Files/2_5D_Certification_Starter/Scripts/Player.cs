using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float _speed = 5;
    private Vector3 _direction;
    [SerializeField]private float _gravity;
    [SerializeField] private float _jumpHeight;
    private CharacterController _controller;
    private float _yVelocity;
    private Vector3 _velocity;
    private Animator _anim;
    public bool _jumping = false;
    [SerializeField] private Transform _ledge;
    private LedgeGrab _activeLedge;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& _anim.GetBool("LedgeGrab")==true)
        {
            _anim.SetBool("ClimbUp",true);
                      
        }
        float _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (_controller.isGrounded==true)
        {                      
            _direction = new Vector3(0, 0, _horizontalInput);
            _velocity = _direction * _speed;                                                      
            _anim.SetFloat("Speed",Mathf.Abs( _horizontalInput));

            if (_horizontalInput!=0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }
            if (_jumping==true)
            {
                _jumping = false;
                _anim.SetBool("IdleJumping", _jumping);
                _anim.SetBool("Jumping", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping",_jumping);
            }     
            if (Input.GetKeyDown(KeyCode.Space)&&_anim.GetFloat("Speed")<0.1f)
            {
                _yVelocity = _jumpHeight;
                _anim.SetBool("IdleJumping", _jumping);
            }
        }
        else
        {
            _yVelocity -= _gravity;                             
        }
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }
    public void DoneClimbing()
    {
        transform.position = new Vector3 (0.39f,74.36f,125.13f);
        _anim.SetBool("LedgeGrab", false);
         _controller.enabled = true;
        _anim.SetBool("ClimbUp", false);
    } 
}
