using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float _speed = 5;
    private Vector3 _direction;
    private Vector3 _directionUp;
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
    public bool _ladder;
    private float _verticalInput;
    private LedgeGrab _grab;
  
    // Start is called before the first frame update
    void Start()
    {
        
        _upwardCheck = transform.TransformDirection(Vector3.up);
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _grab = GameObject.Find("Ledge_Checker").GetComponent<LedgeGrab>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_controller.isGrounded);
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

            if (_ladder == true)
            {
                float _verticalInput = Input.GetAxisRaw("Vertical");
                _yVelocity = _verticalInput;
                _directionUp = new Vector3(0, _verticalInput, _horizontalInput);
                Vector3  _velocityUp = _directionUp * _speed;
                _controller.Move(_velocityUp * Time.deltaTime);
                _anim.SetFloat("Speed", Mathf.Abs(_verticalInput));
            }
        }
        else
        {
           if (_ladder==true)
            {
                float _verticalInput = Input.GetAxisRaw("Vertical");
                _yVelocity = _verticalInput;
                _directionUp = new Vector3(0, _verticalInput, _horizontalInput);
                Vector3 _velocityUp = _directionUp * _speed;
                _controller.Move(_velocityUp * Time.deltaTime);
                _anim.SetFloat("Speed", Mathf.Abs(_verticalInput));
            }
            _yVelocity -= _gravity ;                             
        }
        _velocity.y = _yVelocity;   
        _controller.Move(_velocity * Time.deltaTime);        
    }
    
    public void DoneClimbing()
    {
        transform.position = _grab._finalPos.position;
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
        //Ray rayLadder = new Ray(transform.position, transform.right);

       // if (Physics.Raycast(rayLadder,1)&&_ladder==true)
       // {
        //    Debug.Log(_controller.isGrounded);
       // }

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
    public void ClimbUpLadder()
    {      
        if (_flip ==false)
        {
            _ladder = true;
            _jumping = false;
            _anim.SetBool("ClimbLadder", true);
        }      
    }
    public void StopClimbingLadder()
    {
        _ladder = false;
        _jumping = true;
        _anim.SetBool("ClimbLadder", false);
    }
}
