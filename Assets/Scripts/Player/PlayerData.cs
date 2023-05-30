using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerData
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;
    public float speed;
    public float rotationSpeed;
    public float maxHealth;
    public float curHealth;
    public TransformData gunSpawnPos;
    public FixedJoystickData joystickData;
    public float bonusdame;
    public SliderData healthBar;

    //public WeaponData firstWeapon;
    public BuffData firstBuff;
    public BuffSkillData firtBuffSkill;

    public bool isVisible;
    public bool isUndead;
    //public Rigidbody2D rb2d;
    //public Camera mainCamera;
    //public WeaponData curWeapon;
    public BuffSkillData curBuffSkill;

    public SerializableVector2 movementInput;
    public SerializableVector2 movementInputSmooth;
    public SerializableVector2 velocityInputSmooth;
    public float unDeahHeath;

}







