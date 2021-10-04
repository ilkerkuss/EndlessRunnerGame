using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinController : MonoBehaviour
{
    private Rigidbody _penguinRb;
    private bool _enemyIsGrounded;

    void Start()
    {
        _penguinRb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {

        if ( _enemyIsGrounded && LevelManager.Instance.GameState == LevelManager.GameStates.IsGamePlaying)
        {
            MovePenguin();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _enemyIsGrounded = true;
        }
    }

    private void MovePenguin()
    {
       _penguinRb.velocity = Vector3.up * 5f;  //If penguin grounded jump penguin :)
       _enemyIsGrounded = false;
        

    }

}
