using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed = 10.0f;
    [SerializeField] private float rotationSpeed = 600.0f;
    [SerializeField] public float maxHealth = 200;
    [SerializeField] private float curHealth;
    [SerializeField] private Transform gunSpawnPos;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] public float bonusdame = 0;
    [SerializeField] public Slider healthBar;

    public Weapon firstWeapon;
    public Buff firtBuff;
    //abc
    private Rigidbody2D rb2d;
    private Camera mainCamera;
    [SerializeField] private Weapon curWeapon;
    [SerializeField] private Buff curBuff;

    private Vector2 movementInput;
    private Vector2 movementInputSmooth;
    private Vector2 velocityInputSmooth;


    private void Awake()
    {
        GameManager.instance.player = this;
        healthBar.maxValue = maxHealth;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        curWeapon = Instantiate(firstWeapon, gunSpawnPos.position, gunSpawnPos.rotation);
        curWeapon.transform.SetParent(this.transform);
    }
    private void Update()
    {
        healthBar.value = curHealth;
    }

    private void LateUpdate()
    {
        //set camera follow
        if (this != null)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.x = transform.position.x;
            cameraPos.y = transform.position.y;
            mainCamera.transform.position = cameraPos;
        }
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb2d;
    }
    public Vector2 GetMovementInputSmooth()
    {
        return movementInputSmooth;
    }
    public Buff GetCurBuff()
    {
        return curBuff;
    }
    public float GetCurHealth()
    {
        return curHealth;
    }
    public void SetCurHealth(float healthUndead)
    {
        curHealth = healthUndead;
    }

    private void FixedUpdate()
    {
        SetVelocityOfInput();
        SetRotationInDirectinOfInput();
        Dead();
    }


    private void SetVelocityOfInput()
    {
        movementInput = new Vector2(joystick.Horizontal, joystick.Vertical)
            + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movementInputSmooth = Vector2.SmoothDamp(movementInputSmooth,
            movementInput, ref velocityInputSmooth, 0.1f);

        rb2d.velocity = movementInputSmooth * speed;
    }



    private void SetRotationInDirectinOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotaion =
                Quaternion.LookRotation(transform.forward, movementInputSmooth);

            Quaternion rotation = Quaternion.RotateTowards
                (transform.rotation, targetRotaion, rotationSpeed * Time.deltaTime);

            rb2d.MoveRotation(rotation);
        }

    }

    public void Shoot()
    {
        curWeapon.Shoot();
    }
    public void UltiShoot()
    {
        curWeapon.UltiShoot();
    }
    public void BuffSkill()
    {
        if (curBuff != null)
        {
            switch (curBuff.buffskill)
            {
                case BuffSkillStyle.boomSkill:
                    Boom();
                    break;
                case BuffSkillStyle.dashSkill:
                    Dash();
                    break;
                case BuffSkillStyle.immortalSkill:
                    Dead();
                    break;
            }
        }
    }
    public void BuffUpdate()
    {
        if (curBuff != null)
        {
            switch (curBuff.style)
            {
                case BuffStyle.health:
                    curHealth += curBuff.quantity;
                    break;
                case BuffStyle.speed:
                    speed += curBuff.quantity;
                    break;
                case BuffStyle.strong:
                    bonusdame += curBuff.quantity;
                    break;
            }
        }
    }
    private void Boom()
    {

    }
    private void Dash()
    {
        Debug.Log("dash");
        Vector2 force = 1000 * transform.up;
        Debug.Log(force);
        rb2d.AddForce(force);
    }


    float undeadDuration = 5.0f;
    bool isUndead = true;
    private void Undead()
    {
        StartCoroutine(Undead(undeadDuration, this));
    }


    public void ChangeWeapon(Weapon newWeapon)
    {
        Debug.Log("change");
        if (curWeapon.style != newWeapon.style)
        {
            Debug.Log("change");
            Destroy(curWeapon.gameObject);
            curWeapon = Instantiate(newWeapon, gunSpawnPos.position, gunSpawnPos.rotation);
            curWeapon.transform.SetParent(this.transform);
        }
    }

    public void ChangeBuffSkill(Buff newBuff)
    {
        if (curBuff == null || (curBuff.style != newBuff.style))
        {
            Debug.Log("change");
            foreach (Buff buff in GameManager.instance.Buffs)
            {
                if (buff.style == newBuff.style)
                {
                    curBuff = buff;
                    break;
                }
            }
        }
    }

    public void TakeDamge(float damage)
    {
        curHealth -= damage;
    }

    public void Dead()
    {
        if (curHealth <= 0)
        {
            GameManager.instance.EndGame();
        }
    }

    private IEnumerator Undead(float timeDuration, Player player)
    {
        isUndead = true;
        float timeCount = timeDuration;
        while (timeCount > 0)
        {
            Debug.Log(isUndead);
            timeCount -= Time.deltaTime;
            if (isUndead)
            {
                if (player.curHealth <= 160) player.curHealth = 160;
            }
            yield return null;
        }
        isUndead = false;
    }

}
