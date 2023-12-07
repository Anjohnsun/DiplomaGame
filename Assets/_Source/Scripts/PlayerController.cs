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
    [SerializeField] private GameObject _enemyPrefab;

    // Стата игрока
    public float _health = 100;
    public float _playerSpeed = 5;
    public float _attackCooldown = 2;
    public float _projectileSize = 1;
    public float _projectileDamage = 1;
    public int _projectileLeft = 1;
    private bool AbleToFire = true;

    // Уровень
    private int Level = 0;
    private int LevelExperience = 0;

    private Vector2 MovementInput;
    private VirtualMouseInput virtualMouseInput;



    private void Start()
    {
        virtualMouseInput = GameObject.Find("VirtualMouseUI").GetComponent<VirtualMouseInput>();
        UpdateDebugScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = 10f;
            Instantiate(_enemyPrefab, Camera.main.ScreenToWorldPoint(mousePos), new Quaternion());
        }
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
            LevelExperience -= 10;
            for (int i = 0; i < 3; i++)
            {
                Instantiate(_powerupCards[Random.Range(0, _powerupCards.Length)], _pickPowerupUI.transform);
            }
        }

        UpdateDebugScreen();
    }

    // Меняет циферки слева сверху
    public void UpdateDebugScreen()
    {
        _debugSreen.GetComponentsInChildren<TMP_Text>()[0].text = "_playerSpeed = " + _playerSpeed;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[1].text = "_attackCooldown = " + _attackCooldown;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[2].text = "_projectileSize = " + _projectileSize;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[3].text = "_projectileDamage = " + _projectileDamage;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[4].text = "Level = " + Level;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[5].text = "LevelExperience = " + LevelExperience;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[6].text = "AbleToFire? = " + AbleToFire;
        _debugSreen.GetComponentsInChildren<TMP_Text>()[7].text = "_projectileLeft = " + _projectileLeft;
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

    // Управление
    private void OnMove(InputValue inputValue)
    {
        MovementInput = inputValue.Get<Vector2>();
    }

    private void OnFire()
    {
        if (!AbleToFire) return;
        AbleToFire = false;

        // Получение точки в пространстве от мыши
        Vector3 mousePos = new Vector3(0, 0, 0);
        if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            mousePos = Mouse.current.position.ReadValue();
        }
        else if (_playerInput.currentControlScheme == "Gamepad")
        {
            mousePos = virtualMouseInput.cursorTransform.position;
        }
        mousePos.z = 10f;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

        GameObject proj = Instantiate(_swordProjectile, transform);
        proj.GetComponent<SwordProjectile>().TakeInfo(this, (worldPoint - transform.position).normalized, _projectileDamage, _projectileLeft, true);

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
