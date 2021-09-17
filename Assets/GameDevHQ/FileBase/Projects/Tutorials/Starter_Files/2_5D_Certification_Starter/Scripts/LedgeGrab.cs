using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    [SerializeField] Transform _ledgeTransform;
    [SerializeField] Vector3 standPos;
    public Transform _finalPos;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GameObject.Find("Model").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="LedgeGrabChecker")
        {                    
            _animator.SetBool("LedgeGrab",true);
            _animator.SetFloat("Speed", 0);
            _animator.SetBool("Jumping", false);
            other.GetComponentInParent<CharacterController>().enabled = false;
            _player.transform.position = _ledgeTransform.position;                    
        }
    }
}
