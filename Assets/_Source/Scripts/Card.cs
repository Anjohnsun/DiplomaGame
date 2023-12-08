using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private float _changePlayerSpeed = 0;
    [SerializeField] private float _changePlayerHealth = 0;
    [SerializeField] private float _changeAttackCooldown = 0;
    [SerializeField] private float _changeProjectileSize = 0;
    [SerializeField] private float _changeProjectileDamage = 0;
    [SerializeField] private int _changeProjectileLeft = 0;

    private PlayerController PlayerC;

    private void Start()
    {
        PlayerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void AssignValues()
    {
        PlayerC._playerSpeed *= CheckForZero(_changePlayerSpeed);
        PlayerC._playerHealth *= CheckForZero(_changePlayerHealth);
        PlayerC._attackCooldown *= CheckForZero(_changeAttackCooldown);
        PlayerC._projectileSize *= CheckForZero(_changeProjectileSize);
        PlayerC._projectileDamage *= CheckForZero(_changeProjectileDamage);
        PlayerC._projectileLeft += _changeProjectileLeft;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).position = new Vector3(0, 1000000000, 0);
            Destroy(transform.parent.GetChild(i).gameObject, .1f);
        }

        PlayerC.UpdateDebugScreen();
    }

    // ѕровер€ет нужно ли мен€ть число и возращает обратно
    private float CheckForZero(float value)
    {
        if (value == 0) return 1;
        return value;
    }
}
