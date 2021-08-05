using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _chrController;
    private Vector3 _direction;

    private float _forwardSpeed = 5f;
    private float _laneDistance = 4f;

    private int _movementLane = 1; //0:left 1:middle 2:right

    private float _jumpForce = 5f;
    private float _gravityForce = -9.81f;

    //[SerializeField]private GameObject _startPoint;

    private void Start()
    {
        /*
        Debug.Log(_startPoint.transform.position);
        transform.position = _startPoint.transform.position;
        Debug.Log(transform.position);
        */
        Debug.Log(transform.position);
        _chrController = GetComponent<CharacterController>();
        _direction.z = _forwardSpeed;
    }
    private void Update()
    {
        _chrController.Move(_direction * Time.deltaTime);
        Debug.Log("pleyer pos:"+ transform.position);
        
        if (_chrController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jump();
            }
        }
        else
        {
            _direction.y += _gravityForce * Time.deltaTime;
        }
        

        if (Input.GetKeyDown(KeyCode.A))
        {
            _movementLane -= 1;
            transform.position += Vector3.left * _laneDistance;

            if (_movementLane < 0)
            {
                _movementLane = 0;

            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _movementLane += 1;
            transform.position += Vector3.right * _laneDistance;

            if (_movementLane > 2)
            {
                _movementLane = 2;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;


        if (_movementLane == 0)
        {
            targetPosition += Vector3.left * _laneDistance;
        }
        else if (_movementLane == 2)
        {
            targetPosition += Vector3.right * _laneDistance;
        }
     
        transform.position = targetPosition;

        //Debug.Log(transform.position);
    }

    private void _jump()
    {
        _direction.y = _jumpForce;
    }
}
