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






    public IEnumerator TakeShoe() //ayakkab� al�n�rsa speed ve jumpheight iki kat�na ��kar 10 sn
    {
        Debug.Log("Ayakkab� al�nd�.");
        //speed ve jump force *2 ayarlama
        float tempSpeed = PlayerController.Instance.GetPlayerSpeed();
        PlayerController.Instance.SetPlayerSpeed(tempSpeed * 2);

        float tempJumpForce = PlayerController.Instance.GetPlayerJumpForce();
        PlayerController.Instance.SetPlayerJumpForce(tempJumpForce*2);

        yield return new WaitForSeconds(10f);

        //speed ve jumpforce eski de�erine d�nd�rme
        PlayerController.Instance.SetPlayerSpeed(tempSpeed);
        PlayerController.Instance.SetPlayerSpeed(tempJumpForce);
    }

    public IEnumerator TakeStar() //if y�ld�z true 5 sn score *5 eklenecek
    {
        Debug.Log("Y�ld�z al�nd�.");

        int tempCoinValue = PlayerController.Instance.GetCoinValue();
        PlayerController.Instance.SetCoinValue(tempCoinValue * 5); // coin value *5 ayarlama
        yield return new WaitForSeconds(5f);

        PlayerController.Instance.SetCoinValue(tempCoinValue); //eski de�erine d�nd�rme

        Debug.Log("y�ld�z bitti");
        
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
