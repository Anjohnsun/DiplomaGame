using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] private float _health = 5;
    [SerializeField] private GameObject _expPrefab;

    [SerializeField] private Transform _player;
    [SerializeField] private float enemySpeed = 2;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDmg(float dmg)
    {
        _health -= dmg;

        Color color = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(TemporaryRed(color));

        if (_health <= 0)
        {
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                GameObject expGO = Instantiate(_expPrefab, transform.position, new Quaternion());
                expGO.GetComponent<Experience>().TakeExp(Random.Range(1, 5));
            }

            Destroy(gameObject);
        }
        Debug.Log("Damaged!");
    }

    IEnumerator TemporaryRed(Color color)
    {
        yield return new WaitForSeconds(.2f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = color;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) < 5)
        {
            Vector3 dir = (_player.position - transform.position) * Time.deltaTime * enemySpeed;
            transform.Translate(dir.x, dir.y, dir.z);
        }
    }
}
