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
    public BuffSkill firtBuffSkill;
   
    public bool isVisible;
    public bool isUndead;
    //abc
    private Rigidbody2D rb2d;
    private Camera mainCamera;
    [SerializeField] public Weapon curWeapon;
    [SerializeField] private BuffSkill curBuffSkill;
  


    private Vector2 movementInput;
    private Vector2 movementInputSmooth;
    private Vector2 velocityInputSmooth;
    private float unDeahHeath;
    public delegate void WeaponChangedHandler();
    public static event WeaponChangedHandler OnWeaponChanged;
    
    public delegate void BuffSkillChangedHandler();
    public static event BuffSkillChangedHandler OnBuffSkillChanged;
   
    private void Awake()
    {
        isVisible = false;
        isUndead = false;
        GameManager.instance.player = this;
        healthBar.maxValue = 1;
        mainCamera = Camera.main;
        rb2d = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        curWeapon = Instantiate(firstWeapon, gunSpawnPos.position, gunSpawnPos.rotation);
        curWeapon.transform.SetParent(this.transform);
    }

    private void Update()
    {
        healthBar.value = curHealth/maxHealth;

        if (curWeapon.quantity <= 0)
        {
            ChangeWeapon(firstWeapon);
        }

        if (isUndead)
        {
            if (curHealth <= unDeahHeath)curHealth = unDeahHeath;
        }

        if(curHealth > maxHealth)curHealth = maxHealth;
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
    
    public BuffSkill GetCurBuffSkill()
    {
        return curBuffSkill;
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
        curWeapon.Shoot(transform.up);
    }

    public void UltiShoot()
    {
        curWeapon.UltiShoot(transform.up);
    }

    public void BuffSkill()
    {
        if (curBuffSkill.buffReady)
        {
            switch (curBuffSkill.buffskill)
            {
                case BuffSkillStyle.boomSkill:
                    Invisible();
                    break;
                case BuffSkillStyle.dashSkill:
                    Dash();
                    break;
                case BuffSkillStyle.immortalSkill:
                    Undead();
                    break;
            }
                curBuffSkill.buffReady = false;
                StartCoroutine(CountDownBuff(curBuffSkill.cdBuff));
        }
       
    }

    IEnumerator CountDownBuff(float time)
    {
        yield return new WaitForSeconds(time);
        curBuffSkill.buffReady = true;
    }
    public void BuffUpdate(Buff b)
    {

            switch (b.style)
            {
                case BuffStyle.health:
                    curHealth += b.quantity;
                    break;
                case BuffStyle.speed:
                    speed += b.quantity;
                    break;
                case BuffStyle.strong:
                   maxHealth += b.quantity;
                   curHealth += b.quantity;
                     break;
            }
       
    }
    
    private IEnumerator IEInvisible(float timeDuration)
    {

        isVisible = true;
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        //this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(timeDuration);
        isVisible = false;
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Invisible()
    {
        StartCoroutine(IEInvisible(curBuffSkill.timeEffect));
    }
    
    private void Dash()
    {
        Vector2 force = 10000 * transform.up;
        rb2d.AddForce(force);
    }



    public void ChangeWeapon(Weapon newWeapon)
    {
        Destroy(curWeapon.gameObject);
        curWeapon = Instantiate(newWeapon, gunSpawnPos.position, gunSpawnPos.rotation);
        curWeapon.transform.SetParent(this.transform);
        OnWeaponChanged?.Invoke();

    }

    public void ChangeBuffSkill(BuffSkill newBuff)
    {
            foreach (BuffSkill buff in GameManager.instance.BuffSkill)
            {
                if (buff.buffskill == newBuff.buffskill)
                {
                    curBuffSkill = buff;
                    break;
                }
            }
        OnBuffSkillChanged?.Invoke();
        
    }

   

    public void TakeDamge(float damage)
    {
        curHealth -= damage;
    }

    public void Dead()
    {
        if (curHealth <= 0)
        {
            SoundController.instance.PlayGameOver();
            ButtonControl.instance.GameOver();
        }
    }

    private IEnumerator IEUndead(float timeDuration)
    {
        isUndead = true;
        this.gameObject.GetComponent<Renderer>().material.color = Color.black;
        unDeahHeath = curHealth;
        yield return new WaitForSeconds(timeDuration);
        isUndead = false;
        this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
    private void Undead()
    {

        StartCoroutine(IEUndead(curBuffSkill.timeEffect));
    }
}
