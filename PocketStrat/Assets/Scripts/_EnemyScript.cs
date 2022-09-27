using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _EnemyScript : MonoBehaviour
{
    private NavMeshAgent _Agent;
    [SerializeField] private GameObject _EnemySprite;
    [SerializeField] private int _Damage, _AtkSpd,_Health;
    private Transform _OriginalPos;
    [SerializeField] private List<GameObject> _Adventurers;
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _OriginalPos = transform;
        _EnemySprite = gameObject.transform.Find("EnemySprite").gameObject;
    }

    
    void Update()
    {
        if(_Adventurers.Count > 0)
        {

            for (var i = _Adventurers.Count - 1; i > -1; i--)
            {
                if (_Adventurers[i] == null)
                    _Adventurers.RemoveAt(i);
            }
            if (_Adventurers.Count > 0)  // bro why does it break if i dont have this LOL 
            {                           // WHY DO I HAVE TO HAVE TWO. i dont care why i know why i just dont care to fix XX
                _Agent.SetDestination(_Adventurers[0].transform.position);
            }
           

        }
        else 
        {
            if(_Agent.remainingDistance > 1)
            {
                return;
            }
            _Agent.SetDestination(_OriginalPos.position);
        }
            
    }

    private IEnumerator Attack()
    {
        if (_Adventurers.Count > 0)
        {

            for (var i = _Adventurers.Count - 1; i > -1; i--)
            {
                if (_Adventurers[i] == null)        // just checking xx
                    _Adventurers.RemoveAt(i);       // I'd rather it be less efficient than it break
            }
            if (Vector3.Distance(_Adventurers[0].transform.position, transform.position) < 2)
            {
                _Adventurers[0].GetComponent<_AdventurerScript>().TakeDamage(_Damage);
            }
        }
        yield return new WaitForSeconds(_AtkSpd);
        if (_Adventurers.Count > 0)
        {

            StartCoroutine(Attack());
        }
    }


    public void TakeDamage(int damage)
    {
        _Health -= damage;
        if (_Health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Adventurer"))
        {
            _Adventurers.Add(other.gameObject);
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Adventurer"))
        {
            _Adventurers.Remove(other.gameObject);
        }
    }
}
