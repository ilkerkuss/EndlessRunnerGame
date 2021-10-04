using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
    private Vector3 _snowmanDir;

    void Start()
    {
        _snowmanDir = Vector3.right;

    }

    void Update()
    {
        if (LevelManager.Instance.GameState == LevelManager.GameStates.IsGamePlaying)
        {
            MoveSnowman();
        }
    }

    private void MoveSnowman()
    {
        transform.Translate(_snowmanDir * 4 * Time.deltaTime, Space.World);  //Snowman objeleri rotate yapýldýðýndan translate uzayý belirtildi


        if (transform.position.x <= -4)
        {
            _snowmanDir = Vector3.right;

        }
        else if (transform.position.x >= 4)
        {
            _snowmanDir = Vector3.left;
        }
    }
}
