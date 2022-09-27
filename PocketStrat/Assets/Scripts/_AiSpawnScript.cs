using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AiSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _SpawnPoints;
    [SerializeField] private GameObject _Adventurer;        // At the start of the dungeon phase parse the fame into this script
                                                            // it will then spawn that many adventurers;
    public int _Fame, _Level;                // also parse their level;
    void Start()
    {
        StartCoroutine(SpawnAdventurer(_Level));
    }

  

    public IEnumerator SpawnAdventurer(int Level)
    {



    
        yield return new WaitForSeconds(0.5f);
        if (_Fame > 0)
        {
            _Fame -= 1;
            var  _GOBJ = Instantiate(_Adventurer, _SpawnPoints[Random.Range(0,_SpawnPoints.Length)].transform.position, Quaternion.identity);
            _GOBJ.gameObject.GetComponent<_AdventurerScript>()._Health = _Level *2;
            _GOBJ.gameObject.GetComponent<_AdventurerScript>()._Damage = _Level;
            StartCoroutine(SpawnAdventurer(_Level));
        }
        else
        {
            yield return null;
        }
    }
}
