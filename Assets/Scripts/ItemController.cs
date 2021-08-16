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






    public void TakeShoe() //ayakkabý alýnýrsa speed ve jumpheight artacak 5 sn
    {
        Debug.Log("Ayakkabý alýndý.");
    }

    public IEnumerator TakeStar() //if yýldýz true 5 sn score *5 eklenecek
    {
        
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Yýldýz alýndý.");
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
