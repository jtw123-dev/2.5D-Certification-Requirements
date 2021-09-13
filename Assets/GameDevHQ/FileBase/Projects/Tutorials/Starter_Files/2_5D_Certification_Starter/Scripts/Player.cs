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
    [SerializeField]private float _rollDistance;
    private bool _flip;
    private bool _roll;
    private Vector3 _velocity;
    private Animator _anim;
    public bool _jumping = false;
    [SerializeField] private Transform _ledge;
    private Vector3 _upwardCheck;
    public bool _hitObject;
    private int _score;
 
    // Start is called before the first frame update
    void Start()
    {
        _upwardCheck = transform.TransformDirection(Vector3.up);
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
               
                if (_horizontalInput<0 &&_flip==false)
                {
                    _flip = true;
                }
                else if (_horizontalInput>0 &&_flip ==true)
                {
                    _flip = false;
                }
            }
            if (_jumping==true)
            {
                _jumping = false;
                _anim.SetBool("IdleJumping", _jumping);
                _anim.SetBool("Jumping", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space)&&_roll==false)
            {
                _yVelocity = _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping",_jumping);
            }     
            if (Input.GetKeyDown(KeyCode.Space)&&_anim.GetFloat("Speed")<0.1f&&_roll==false)
            {
                _yVelocity = _jumpHeight;
                _anim.SetBool("IdleJumping", _jumping);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            { 
                _controller.height = 0.72f;
                _controller.center = new Vector3(0, 0.52f, 0);
                _roll = true;
                _anim.SetBool("Roll", true);
            }   
            if (_roll==true)
            {
                if (_flip == false)
                {
                    _velocity.z += _rollDistance;
                }
                else
                {
                    _velocity.z -= _rollDistance;
                }
            }
        }
        else
        {
            _yVelocity -= _gravity ;                             
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
    public void DoneRolling()
    {                
        if (_hitObject==true)
        {
            Debug.Log("hit object");
            return;
        }
        else
        {
            _anim.SetBool("Roll", false);
            _roll = false;
            _controller.height = 1.86f;
            _controller.center = new Vector3(0, 0.87f, 0);
        }
           
    }

    public void Score()
    {
        _score++;
        UIManager._Instance.ScoreUpdate(_score);
    }
    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, 6 )&&_roll==true)
        {
            _hitObject = true;
            _controller.height = 0.72f;
            _controller.center = new Vector3(0, 0.52f, 0);
        }
        else
        {
            _hitObject = false;
        }
    }
}
