using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player Instance;

    public static float energy;

    public static float maxEnergy;

    public static float hp;
    public static float maxHp;

    [HideInInspector]
    public static Transform player;

    public static Rigidbody2D rb;

    [SerializeField]
    private float _speed;

    [SerializeField]
    public static bool isGrounded;

    [SerializeField]
    private float _jumpForce;

    private static int jumpCounter;

    private static SpriteRenderer _armSprite;

    private static SpriteRenderer _bodySprite;

    private static Animator animator;

    private static float _moveDirection;

    public static float rotationZ;
    static float  currentSpeed;

    public static int direction;

    [SerializeField]
    private Image _currentHpSprite;

    [SerializeField]
    private Image _currentEnergySprite;

    private void Initialization() {
        Instance = this;
        hp = 500;
        maxHp = hp;

        energy = 1000;
        maxEnergy = energy;
        player = GetComponent<Transform>();
        _bodySprite = GetComponent<SpriteRenderer>();
        _armSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = _speed;
    }

    void Start() {
        Initialization();

        SpellSelection.currentSpell = new Spell();

        if (transform.position.x > 0) {
            direction = -1;
        }
        else {
            direction = 1;
        }

    }

    private void HpBarControl() {
        _currentHpSprite.fillAmount = hp / maxHp;
    }

    private void EnergyBarControl() {
        _currentEnergySprite.fillAmount = energy / maxEnergy ;
    }

    private void Update() {
        CalculateDirection();
        Jump();
        SpriteBodyRotate();
        SpriteArmRotate();
        HpBarControl();
        EnergyBarControl();


        if (MenuControl.isSelectionClosed) SpellSelection.currentSpell.UseSpell();
        if (Input.GetKey(KeyCode.Mouse0)) {
            _speed = 9f;
        }
        else {
            _speed = currentSpeed;
        }
    }
    void FixedUpdate() {
        Move();
    }

    void CalculateDirection() {
        if (transform.position.x > 0) {
            direction = 1;
        }
        else {
            direction = -1;
        }
    }

    void Jump() {
        if (!isGrounded) return;
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            jumpCounter++;
        }
        isGrounded = !(jumpCounter >= 2);
    }

    void Land(Collision2D collision) {
        if (collision.collider.CompareTag("Ground")) {
            jumpCounter = 0;
            isGrounded = true;
        }
    }

    private void Move() {
        _moveDirection = Input.GetAxisRaw("Horizontal");
        float moveBy = _moveDirection * _speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        animator.SetBool("IsWalking", _moveDirection != 0);
    }

    private void SpriteArmRotate() {

        Vector3 mousePos = MousePosition.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePos - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimDirection);

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        rotationZ = angle;     
        _armSprite.transform.eulerAngles = new Vector3(0, 0, angle + 90);
    }

    private void SpriteBodyRotate() {
        if (Input.GetKeyDown(KeyCode.A)) {
            _bodySprite.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            _bodySprite.flipX = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        Land(collision);
    }

    public static void TakeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            OnDeath();
        }
    }

    public static void OnDeath() {
        GameLogic.GameOver();
    }


}

