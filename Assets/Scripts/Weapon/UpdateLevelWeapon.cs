using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelWeapon : MonoBehaviour
{

    //public int levelSkillFastGun = 1;
    //public int levelSkillStrongGun = 1;
    //public int levelSkillBom = 1;
    //public int levelBomb = 1;

    public int costUpdateLevelFastGun = 5;
    public int costUpdateLevelStrongGun = 10;
    public int costUpdateBom = 10;
    public int costUpdateBomb = 10;
    public string message = "";

    public Text levelFastGun;
    public Text levelStrongGun;
    public Text levelBommlGun;
    public Text levelBombb;
    public Text Message;

    public TextMeshProUGUI costUpdateFastGun;
    public TextMeshProUGUI costUpdateStrongGun;
    public TextMeshProUGUI costUpdateBomGun;
    public TextMeshProUGUI costUpdatebomb;


    //private ScoreController scoreController;

    private void Start()
    {
        //scoreController = FindObjectOfType<ScoreController>();
        message = "";

    }
    void Update()
    {
        levelFastGun.text = "Level: " + GameManager.instance.levelSkillFastGun;
        levelStrongGun.text = "Level: " + GameManager.instance.levelSkillStrongGun;
        levelBommlGun.text = "level: " + GameManager.instance.levelSkillBom;
        levelBombb.text = "level: " + GameManager.instance.levelBomb;

        costUpdateFastGun.text = costUpdateLevelFastGun + " Gold";
        costUpdateStrongGun.text = costUpdateLevelStrongGun + " Gold";
        costUpdateBomGun.text = costUpdateBom + " Gold";
        costUpdatebomb.text = costUpdateBomb + " Gold";

        Message.text = message;
    }

    public void UpdateLevelSkillFastGun()
    {
        Debug.Log("CointCurrent: " + GameManager.instance.totalCoins);
        Debug.Log("Coin: " + costUpdateLevelFastGun);
        if (GameManager.instance.levelSkillFastGun < 3)
        {
            if (GameManager.instance.totalCoins > costUpdateLevelFastGun)
            {
                
                message = "Update successfully";
                GameManager.instance.levelSkillFastGun += 1;
                GameManager.instance.totalCoins -= costUpdateLevelFastGun;
                costUpdateLevelFastGun += 100;
            }
            else
            {
                message = "Don't enoght gold to update level";
            }

        }
        else
        {
            message = "The level of the weapon you have maxed";
        }
    }
    public void UpdateLevelSkillStrongGun()
    {
        Debug.Log("CointCurrent: " + GameManager.instance.totalCoins);
        //Debug.Log("CoinSt: " + costUpdateLevelFastGun);
        if (GameManager.instance.levelSkillStrongGun < 3)
        {
            //if (scoreController.coins > costUpdateLevelStrongGun)
            if (GameManager.instance.totalCoins > costUpdateLevelStrongGun)
            {
                message = "Update successfully";
                GameManager.instance.levelSkillStrongGun += 1;
                GameManager.instance.totalCoins -= costUpdateLevelStrongGun;
                costUpdateLevelStrongGun += 100;
            }
            else
            {
                message = "Don't enoght gold to update level";
            }
        }
        else
        {
            message = "The level of the weapon you have maxed";
        }
    }
    public void UpdateLevelSkillBoom()
    {
        Debug.Log("CointCurrent: " + GameManager.instance.totalCoins);
        if (GameManager.instance.levelSkillBom < 3)
        {
            //if (scoreController.coins > costUpdateBom)
            if (GameManager.instance.totalCoins > costUpdateBom)
            {
                message = "Update successfully";
                GameManager.instance.levelSkillBom += 1;
                GameManager.instance.totalCoins -= costUpdateBom;
                costUpdateBom += 100;
            }
            else
            {
                message = "Don't enoght gold to update level";
            }

        }
        else
        {
            message = "The level of the weapon you have maxed";
        }
    }
    public void UpdateLevelBomb()
    {
        Debug.Log("CointCurrent: " + GameManager.instance.totalCoins);
        if (GameManager.instance.levelBomb < 3)
        {
            if (GameManager.instance.totalCoins > costUpdateBomb)
            {
                message = "Update successfully";
                GameManager.instance.levelBomb += 1;
                GameManager.instance.totalCoins -= costUpdateBomb;
                costUpdateBomb += 100;
            }
            else
            {
                message = "Don't enoght gold to update level";
            }
        }
        else
        {
            message = "The level of the weapon you have maxed";
        }
    }
}
