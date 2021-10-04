using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    [SerializeField]private GameObject[] _tileList;
    private float _nextSpawn = 0f;
    private float _tileLength=99f;
    private int _numberOfTiles = 5;

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

    void Start()
    {
        Init();
 
    }


    void Update()
    {
 
            if (_playerTransform.position.z - 120 >= _nextSpawn - (_tileLength * _numberOfTiles))
            {
                SpawnTile(Random.Range(0, _numberOfTiles));
                DeleteTile(); // yeni tile oluþtuðunda ilk oluþturulan tile silinir
            }

    }


    private void SpawnTile(int tileIndex)
    {

        GameObject TileGO=Instantiate(_tileList[tileIndex], transform.forward * _nextSpawn, transform.rotation);
        TileGO.transform.parent = _parentObject.transform;

        _nextSpawn += _tileLength;


    }

    private void DeleteTile()
    {
        Destroy(_parentObject.transform.GetChild(0).gameObject);
    }

    private void Init()
    {

        _nextSpawn = 0;

        // Generate beginning parkour
        for (int i = 0; i < _numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, _numberOfTiles));
            }

        }

        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ResetTiles() //activetile sýfýrla baþlangýç fonk çaðýr
    {

        foreach (Transform child in _parentObject.transform)
        {
            Destroy(child.gameObject);
        }

        Init();
    }

    
}
