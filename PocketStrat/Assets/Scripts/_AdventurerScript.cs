using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _AdventurerScript : MonoBehaviour
{

    public int _Health, _Damage,_AtkSpd;
    private NavMeshAgent _Agent;
    [SerializeField] private GameObject _AdventurerSprite;
    [SerializeField] private Sprite[] _Sprites;
    [SerializeField] private List<GameObject> _Enemies;
    [SerializeField] private GameObject _EndObj;
    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _Agent.SetDestination(_EndObj.transform.position);
        _AdventurerSprite = gameObject.transform.Find("AdventurerSprite").gameObject;
        _AdventurerSprite.GetComponent<SpriteRenderer>().sprite = _Sprites[Random.Range(0, _Sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {

        if (_Enemies.Count > 0)
        {

            for (var i = _Enemies.Count - 1; i > -1; i--)
            {
                if (_Enemies[i] == null)
                    _Enemies.RemoveAt(i);
            }
            if (_Enemies.Count > 0)  // bro why does it break if i dont have this LOL 
            {                           // WHY DO I HAVE TO HAVE TWO. i dont care why i know why i just dont care to fix XX
                _Agent.SetDestination(_Enemies[0].transform.position);
            }


        }
        else
        {

            _Agent.SetDestination(_EndObj.transform.position);
        }
    }


    private IEnumerator Attack()
    {
        if (_Enemies.Count > 0)
        {

            for (var i = _Enemies.Count - 1; i > -1; i--)
            {
                if (_Enemies[i] == null)        // just checking xx
                    _Enemies.RemoveAt(i);       // I'd rather it be less efficient than it break
            }
            if (Vector3.Distance(_Enemies[0].transform.position, transform.position) < 2)
            {
                _Enemies[0].GetComponent<_EnemyScript>().TakeDamage(_Damage);
            }
        }
        yield return new WaitForSeconds(_AtkSpd);
        if (_Enemies.Count > 0)
        {
            StartCoroutine(Attack());
        }
    }

    public void TakeDamage(int damage)
    {
        _Health -= damage;
        if(_Health <=0)
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            _Enemies.Add(other.gameObject);
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _Enemies.Remove(other.gameObject);
        }
    }
}
