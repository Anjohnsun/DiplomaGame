using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordProjectile : MonoBehaviour
{
    private float dmg;

    private void Start()
    {
        Destroy(gameObject, .1f);
    }

    public void TakeStats(float inDmg)
    {
        dmg = inDmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyBasic>().TakeDmg(dmg);
        }
    }
}
