using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private float _changePlayerSpeed = 0;
    [SerializeField] private float _changeAttackCooldown = 0;
    [SerializeField] private float _changeProjectileSize = 0;
    [SerializeField] private float _changeProjectileDamage = 0;

    private PlayerController PlayerC;

    private void Start()
    {
        PlayerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void AssignValues()
    {
        PlayerC._playerSpeed *= _changePlayerSpeed;
        PlayerC._attackCooldown *= _changeAttackCooldown;
        PlayerC._projectileSize *= _changeProjectileSize;
        PlayerC._projectileDamage *= _changeProjectileDamage;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).position = new Vector3(0, 1000000000, 0);
            Destroy(transform.parent.GetChild(i).gameObject, .1f);
        }

        PlayerC.UpdateDebugScreen();
    }
}
