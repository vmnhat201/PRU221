using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelWeapon : MonoBehaviour
{

    public int levelSkillFastGun = 1;
    public int levelSkillStrongGun = 1;
    public int levelSkillBom = 1;
    public int levelBomb = 1;

    public int costUpdateLevelFastGun = 100;
    public int costUpdateLevelStrongGun = 100;
    public int costUpdateBom = 100;
    public int costUpdateBomb = 100;
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


    private ScoreController scoreController;

    private void Start()
    {
        scoreController = FindObjectOfType<ScoreController>();
        message = "";

    }
    void Update()
    {
        Debug.Log(levelSkillFastGun);
        levelFastGun.text = "Level: " + levelSkillFastGun;
        levelStrongGun.text = "Level: " + levelSkillStrongGun;
        levelBommlGun.text = "level: " + levelSkillBom;
        levelBombb.text = "level: " + levelBomb;

        costUpdateFastGun.text = costUpdateLevelFastGun + " Gold";
        costUpdateStrongGun.text = costUpdateLevelStrongGun + " Gold";
        costUpdateBomGun.text = costUpdateBom + " Gold";
        costUpdatebomb.text = costUpdateBomb + " Gold";

        Message.text = message;
    }

    public void UpdateLevelSkillFastGun()
    {
        if (levelSkillFastGun < 3)
        {
            if (scoreController.coins > costUpdateLevelFastGun)
            {
                message = "Update successfully";
                levelSkillFastGun += 1;
                scoreController.coins -= costUpdateLevelFastGun;
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
        if (levelSkillStrongGun < 3)
        {
            if (scoreController.coins > costUpdateLevelStrongGun)
            {
                message = "Update successfully";
                levelSkillStrongGun += 1;
                scoreController.coins -= costUpdateLevelStrongGun;
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
        if (levelSkillBom < 3)
        {
            if (scoreController.coins > costUpdateBom)
            {
                message = "Update successfully";
                levelSkillBom += 1;
                scoreController.coins -= costUpdateBom;
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
        if(levelBomb < 3)
        {
            if(scoreController.coins > costUpdateBomb)
            {
                message = "Update successfully";
                levelBomb += 1;
                scoreController.coins -= costUpdateBomb;
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
