using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, _target.transform.position.z+_offset.z);
        transform.position = Vector3.Lerp(transform.position,newPosition,0.5f);

        //transform.position = newPosition;
        //Debug.Log(_offset);


    }
}
