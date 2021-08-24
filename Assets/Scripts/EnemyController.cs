using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = Vector3.right;

    }

    // Update is called once per frame
    void Update()
    {
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
        transform.Translate(dir * 4 * Time.deltaTime);

        
        if (transform.position.x <= -4)
        {

            dir = Vector3.right;

        }
        else if (transform.position.x >= 4)
        {

            dir = Vector3.left;
        }
    }

    private void MovePenguin()
    {
        transform.Translate(Vector3.up*3);
    }
}
