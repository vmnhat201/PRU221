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
    public string explosivePrefabName;
    public GunBulletData(GunBullet bullet)
    {
        this.style = bullet.style;
        this.bulletLifeTime = bullet.bulletLifeTime;
        this.explosivePrefabName = bullet.explosivePrefabs.name;
    }
}