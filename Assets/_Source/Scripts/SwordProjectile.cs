using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _swordProjectilePrefab;
    private PlayerController PlayerC;
    private Vector3 ProjectileDirection;
    private float Dmg;
    private int ProjLeft;
    private bool IsSizeChanged;

    private void Start()
    {
        Destroy(gameObject, .1f);
        
        if (IsSizeChanged)
        {
            transform.GetChild(0).localScale *= PlayerC._projectileSize;
            IsSizeChanged = false;
        }
        
        transform.GetChild(0).position = transform.position + new Vector3(1.2f + transform.GetChild(0).localScale.x / 2, 0, 0);
        transform.rotation = Quaternion.LookRotation(ProjectileDirection, Vector3.forward);

        ProjLeft--;
    }

    public void TakeInfo(PlayerController pc, Vector3 direction, float inDmg, int inProjLeft, bool size)
    {
        PlayerC = pc;
        Dmg = inDmg;
        ProjLeft = inProjLeft;
        IsSizeChanged = size;


        ProjectileDirection = direction;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile: Triggered");
        if (collision.tag == "Enemy")
        {
            Debug.Log("Projectile: Enemy detected!");
            collision.GetComponent<EnemyBasic>().TakeDmg(Dmg);
        }
    }

    private void OnDestroy()
    {
        if (ProjLeft < 1) return;
        GameObject proj = Instantiate(_swordProjectilePrefab, transform.GetChild(0).position, new Quaternion(-90, 0, -90, 0));
        proj.GetComponent<SwordProjectile>().TakeInfo(PlayerC, ProjectileDirection, Dmg, ProjLeft, IsSizeChanged);
        proj.SetActive(true);
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("<color=RED>TEST");
    }*/
}
