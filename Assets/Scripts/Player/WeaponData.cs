using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponData
{
    public WeaponStyle style;
    public int quantity;
    public GunBulletData normalBulletData;
    public GunBulletData ultiBulletData;
    public string introSpriteName;
    public string ultiSoundClipName;
    public string norSoundClipName;

    public float bulletForce;
    public float norCd;
    public float ultCd;
    public bool norReady;
    public bool ultReady;
    public SerializableVector3 hitTransformPosition;
    public string explosivePrefabName;
    public float damage;

    public WeaponData(Weapon weapon)
    {
        if (weapon == null)
        {
            Debug.Log("Weapon is null");
            return;
        }
        this.style = weapon.style;
        this.quantity = weapon.quantity;
        if (weapon.normalBullet == null)
        {
            Debug.Log("Normal Bullet is null");
        }
        else
        {
            this.normalBulletData = new GunBulletData(weapon.normalBullet);
        }
        if (weapon.ultiBullet == null)
        {
            Debug.Log("Ulti Bullet is null");
        }
        else
        {
            this.ultiBulletData = new GunBulletData(weapon.ultiBullet);
        }
        this.introSpriteName = weapon.intro.name;
        this.ultiSoundClipName = weapon.ultiSound.name;
        this.norSoundClipName = weapon.norSound.name;
        this.bulletForce = weapon.bulletForce;
        this.norCd = weapon.norCd;
        this.ultCd = weapon.ultCd;
        this.norReady = weapon.norReady;
        this.ultReady = weapon.ultReady;
        this.damage = weapon.damage;
        if (weapon.hittf == null)
        {
            Debug.Log("Hit Transform is null");
        }
        else
        {
            this.hitTransformPosition = new SerializableVector3(weapon.hittf.position);
        }
        if (weapon.explosivePrefabs == null)
        {
            Debug.Log("Explosive Prefab is null");
        }
        else
        {
            this.explosivePrefabName = weapon.explosivePrefabs.name;
        }
    }
}


