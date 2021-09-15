using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player _player;
    [SerializeField] Transform _ladderTop;
    [SerializeField] Transform _ladderBottom;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("this was called");
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            Vector3 ladderClamp = _player.transform.position;
            ladderClamp.y = Mathf.Clamp(ladderClamp.y, 69.05f, 84.73f);
            _player.transform.position = ladderClamp;

            _player.ClimbUpLadder();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Player")
        {
            _player.StopClimbingLadder();
        }
    }
}
