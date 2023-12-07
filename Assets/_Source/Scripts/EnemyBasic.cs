using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] private float _health = 1;
    [SerializeField] private GameObject _expPrefab;



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
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) { TakeDmg(1); }
    }*/
}
