using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int exp;

    private void Start()
    {
        _rb.velocity = new Vector2(Random.Range(-10, 10), (Random.Range(-10, 10))).normalized;
    }

    public void TakeExp(int value)
    {
        exp = value;
    }

    public int ReadExp()
    {
        return exp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().ChangeExp(exp);
            Destroy(gameObject);
        }
    }
}
