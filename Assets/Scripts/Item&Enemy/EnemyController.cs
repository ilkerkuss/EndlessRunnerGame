using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 _snowmanDir;

    private Rigidbody _enemyRb;
    private bool _enemyIsGrounded;

    [SerializeField] private LayerMask _groundLayerMask;
    private float _checkRadius;


    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _checkRadius = 1.2f;

        _snowmanDir = Vector3.right;

        


    }

    // Update is called once per frame
    void Update()
    {
        // Enemy Penguin IsGrounded
        _enemyIsGrounded = Physics.CheckSphere(transform.position, _checkRadius, _groundLayerMask);


        if (transform.gameObject.name == "Snowman")
        {
            
            MoveSnowman();
        }
        else if (transform.gameObject.name== "Penguin")
        {
            MovePenguin();
        }
        
    }

    private void MoveSnowman()
    {
        transform.Translate(_snowmanDir * 4 * Time.deltaTime,Space.World);  //Snowman objeleri rotate yapýldýðýndan translate uzayý belirtildi


        if (transform.position.x <= -4)
        {

            _snowmanDir = Vector3.right;

        }
        else if (transform.position.x >= 4)
        {
            _snowmanDir = Vector3.left;
        }
    }



    private void MovePenguin()
    {
        if (_enemyIsGrounded)
        {
            _enemyRb.velocity = Vector3.up * 5f;  //If penguin grounded jump penguin :)

        }

        
    }
}
