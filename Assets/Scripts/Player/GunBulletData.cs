using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class GunBulletData
{
    public BulletStyle style;
    public float bulletLifeTime;
    public GunBulletData(GunBullet bullet)
    {
        if (bullet == null)
            return;
        this.style = bullet.style;
        this.bulletLifeTime = bullet.bulletLifeTime;
    }

    public void GunBullet(GunBullet gunBullet)
    {
        gunBullet.style = this.style;
        gunBullet.bulletLifeTime = this.bulletLifeTime;

    }
}