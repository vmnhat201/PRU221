using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public enum BuffStyle
{
    health, strong, speed
}
public enum BuffSkillStyle
{
    dashSkill, immortalSkill, boomSkill
}
public class Buff : MonoBehaviour
{

    public BuffStyle style;
    public BuffSkillStyle buffskill;
    public float quantity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upGrade(int amount)
    {
    }
    public void BuffEffect()
    {
        switch (style)
        {
            case BuffStyle.health:
                quantity = 20;
                break;
            case BuffStyle.strong:
                quantity = 2;
                break;
            case BuffStyle.speed:
                quantity = 5;
                break;
        }
    }


    public void UpHealth(Player player)
    {
        player.maxHealth += quantity;
    }
    public void UpSpeed(Player player)
    {
        player.speed += quantity;
    }
    public void upDame(Player player)
    {
        player.bonusdame += quantity;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Player p = collision.gameObject.GetComponent<Player>();

        if (p != null)
        {
            p.ChangeBuffSkill(this);
<<<<<<< Updated upstream
            Destroy(this.gameObject);

        }
        GameManager.instance.skillButton.ChangeAvatar();
=======
            GameManager.instance.skillButton.ChangeAvatar();
            Destroy(this.gameObject);
        }        
>>>>>>> Stashed changes
    }


   
}
