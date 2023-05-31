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
    public TransformData hittf;
    public PrefabData explosivePrefab;
    public float damage;

    public WeaponData(Weapon weapon)
    {
        if (weapon == null)
            return;
        this.style = weapon.style;
        this.quantity = weapon.quantity;
        this.normalBulletData = new GunBulletData(weapon.normalBullet);
        this.ultiBulletData = new GunBulletData(weapon.ultiBullet);
        this.introSpriteName = weapon.intro.name;
        this.ultiSoundClipName = weapon.ultiSound.name;
        this.norSoundClipName = weapon.norSound.name;
        this.bulletForce = weapon.bulletForce;
        this.norCd = weapon.norCd;
        this.ultCd = weapon.ultCd;
        this.norReady = weapon.norReady;
        this.ultReady = weapon.ultReady;
        this.damage = weapon.damage;
        this.hittf = new TransformData(weapon.hittf);
        this.explosivePrefab = new PrefabData(weapon.explosivePrefabs);
    }

    public void Weapon(Weapon weapon)
    {
        weapon.style = this.style;
        weapon.quantity = this.quantity;
        if (this.normalBulletData != null)
        {
            this.normalBulletData.GunBullet(weapon.normalBullet);
        }
        if (this.ultiBulletData != null)
        {
            this.ultiBulletData.GunBullet(weapon.ultiBullet);
        }
        weapon.intro = Resources.Load<Sprite>("Sprites/Weapon/" + this.introSpriteName);
        weapon.ultiSound = Resources.Load<AudioClip>("Sounds/Weapon/" + this.ultiSoundClipName);
        weapon.norSound = Resources.Load<AudioClip>("Sounds/Weapon/" + this.norSoundClipName);
        weapon.bulletForce = this.bulletForce;
        weapon.norCd = this.norCd;
        weapon.ultCd = this.ultCd;
        weapon.norReady = this.norReady;
        weapon.ultReady = this.ultReady;
        weapon.damage = this.damage;
        if (this.hittf != null)
        {
            this.hittf.Transform(weapon.hittf);
        }
        if (this.explosivePrefab != null)
        {
            weapon.explosivePrefabs = this.explosivePrefab.ToGameObject();
        }

    }
}
