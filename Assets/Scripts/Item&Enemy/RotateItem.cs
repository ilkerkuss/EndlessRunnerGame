using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTheItem();
    }


    private void RotateTheItem()
    {
        Vector3 rotationVector = new Vector3(0, 0, 40 * Time.deltaTime);

        transform.Rotate(rotationVector);

    }
}
