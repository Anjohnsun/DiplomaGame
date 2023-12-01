using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _pickPowerupUI;
    [SerializeField] private GameObject[] _powerupCards;
    [SerializeField] private Transform _debugSreen;
    [SerializeField] private GameObject _swordProjectile;
    public float _playerSpeed = 5;
    public float _attackCooldown = 2;
    public float _projectileSize = 1;
    public float _projectileDamage = 1;

    private int Level = 0;
    private int LevelExperience = 0;
    private Vector2 MovementInput;
    private VirtualMouseInput virtualMouseInput;
    private bool AbleToFire = true;



    private void Start()
    {
        virtualMouseInput = GameObject.Find("VirtualMouseUI").GetComponent<VirtualMouseInput>();
        UpdateDebugScreen();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = MovementInput * _playerSpeed;
    }

    private void LateUpdate()
    {
        Vector2 virtualMousePos = virtualMouseInput.virtualMouse.position.value;
        virtualMousePos.x = Mathf.Clamp(virtualMousePos.x, 0, Screen.width);
        virtualMousePos.y = Mathf.Clamp(virtualMousePos.y, 0, Screen.height);
        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePos);

        if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Cursor.visible = true;
            virtualMouseInput.cursorGraphic.enabled = false;
        }
        else if (_playerInput.currentControlScheme == "Gamepad")
        {
            Cursor.visible = false;
            virtualMouseInput.cursorGraphic.enabled = true;
        }
    }
    /*
    public void LevelUp()
    {
        Level++;
    }*/

    public void ChangeExp(int value)
    {
        LevelExperience += value;

        if (LevelExperience >= 10)
        {
            Level++;
            LevelExperience = 0;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(_powerupCards[Random.Range(0, _powerupCards.Length)], _pickPowerupUI.transform);
            }
        }

        UpdateDebugScreen();
    }

    // ������ ������� ����� ������
    public void UpdateDebugScreen()
    {
        _debugSreen.GetComponentsInChildren<TMP_Text>()[0].text = "_playerSpeed = " + _playerSpeed;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[1].text = "_attackCooldown = " + _attackCooldown;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[2].text = "_projectileSize = " + _projectileSize;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[3].text = "_projectileDamage = " + _projectileDamage;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[4].text = "Level = " + Level;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[5].text = "LevelExperience = " + LevelExperience;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[6].text = "AbleToFire? = " + AbleToFire;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exp")
        {
            ChangeExp(collision.gameObject.GetComponent<Experience>().ReadExp());
            Destroy(collision.gameObject);
        }
    }*/

    // ����������
    private void OnMove(InputValue inputValue)
    {
        MovementInput = inputValue.Get<Vector2>();
    }

    private void OnFire()
    {
        if (!AbleToFire) return;
        AbleToFire = false;

        GameObject proj = Instantiate(_swordProjectile, transform);
        proj.transform.position += new Vector3(1.2f, 0, 0);
        proj.GetComponent<swordProjectile>().TakeStats(_projectileDamage);

        StartCoroutine(AttackCooldownReset());
        UpdateDebugScreen();
    }

    IEnumerator AttackCooldownReset()
    {
        yield return new WaitForSeconds(_attackCooldown);
        AbleToFire = true;
        UpdateDebugScreen();
    }
}
