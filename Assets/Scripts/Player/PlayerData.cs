using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
    public Quaternion rotation;
    public float speed;
    public float rotationSpeed;
    public float maxHealth;
    public float curHealth;
    public Transform gunSpawnPos;
    public FixedJoystick joystick;
    public float bonusdame;
    public Slider healthBar;

    public Weapon firstWeapon;
    public Buff firtBuff;
    public BuffSkill firtBuffSkill;

    public bool isVisible;
    public bool isUndead;
    public Rigidbody2D rb2d;
    public Camera mainCamera;
    public Weapon curWeapon;
    public BuffSkill curBuffSkill;

    private Vector2 movementInput;
    private Vector2 movementInputSmooth;
    private Vector2 velocityInputSmooth;
    public float unDeahHeath;

    public void setCurWeapon(Weapon weapon)
    {
        this.curWeapon = new Weapon();
        this.curWeapon.transform.position = weapon.transform.position;
        this.curWeapon.transform.rotation = weapon.transform.rotation;
        this.curWeapon.style = weapon.style;
        this.curWeapon.quantity = weapon.quantity;
        this.curWeapon.bulletForce = weapon.bulletForce;
        this.curWeapon.norCd = weapon.norCd;
        this.curWeapon.ultCd = weapon.ultCd;
        this.curWeapon.norReady = weapon.norReady;
        this.curWeapon.ultReady = weapon.ultReady;

        this.curWeapon.hittf.rotation = weapon.hittf.rotation;
        this.curWeapon.hittf.position = weapon.hittf.position;

        this.curWeapon.explosivePrefabs = weapon.explosivePrefabs;
        this.curWeapon.damage = weapon.damage;
    }

}

