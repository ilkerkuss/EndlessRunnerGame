using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    [SerializeField]private GameObject[] _tileList;
    private float _nextSpawn = 0f;
    private float _tileLength=99f;
    private int NumberOfTiles = 5;

    private Transform _playerTransform;
    private List<GameObject> _activeTiles;

    [SerializeField] private GameObject _parentObject;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
 
    }

    // Update is called once per frame
    void Update()
    {
 
            if (_playerTransform.position.z - 120 >= _nextSpawn - (_tileLength * NumberOfTiles))
            {
                SpawnTile(Random.Range(0, NumberOfTiles));
                DeleteTile(); // yeni tile oluþtuðunda ilk oluþturulan tile silinir
            }

     


    }


    private void SpawnTile(int tileIndex)
    {

        GameObject TileGO=Instantiate(_tileList[tileIndex], transform.forward * _nextSpawn, transform.rotation);
        TileGO.transform.parent = _parentObject.transform;
        //_activeTiles.Add(TileGO);

        _nextSpawn += _tileLength;


    }

    private void DeleteTile()
    {
        Destroy(_parentObject.transform.GetChild(0).gameObject);
        //_activeTiles.RemoveAt(0);
    }

    private void Init()
    {
        Debug.Log("TileManager INIT");
        //_activeTiles = new List<GameObject>();
        _nextSpawn = 0;

        // Generate beginning parkour
        for (int i = 0; i < NumberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, NumberOfTiles));
            }

        }

        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ResetTiles() //activetile sýfýrla baþlangýç fonk çaðýr
    {
        //Debug.Log("activeTile"+_activeTiles.Count);
        foreach (Transform child in _parentObject.transform)
        {
            Destroy(child.gameObject);
        }

        Init();
    }

    
}
