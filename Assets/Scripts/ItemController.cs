using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public static ItemController Instance;

    private GameObject[] _coins;



    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        
    }

 
    void Update()
    {
        _coins = GameObject.FindGameObjectsWithTag("Coin");
        RotateItem(_coins);
    }






    public void TakeShoe() //ayakkab� al�n�rsa speed ve jumpheight artacak 5 sn
    {
        Debug.Log("Ayakkab� al�nd�.");
    }

    public IEnumerator TakeStar() //if y�ld�z true 5 sn score *5 eklenecek
    {
        
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Y�ld�z al�nd�.");
    }



    private void RotateItem(GameObject[] coinList)
    {
        Vector3 rotationVector = new Vector3(0, 0, -20 * Time.deltaTime);

        foreach (var item in coinList)
        {
            item.transform.Rotate(rotationVector);
        }
        
    }
}
