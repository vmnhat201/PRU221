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

    public int costUpdateLevelFastGun = 100;
    public int costUpdateLevelStrongGun = 100;
    public int costUpdateBom = 100;

    public Text levelFastGun;
    public Text levelStrongGun;
    public Text levelBomml;

    public TextMeshProUGUI costUpdateFastGun;
    public TextMeshProUGUI costUpdateStrongGun;
    public TextMeshProUGUI costUpdateBomGun;


    private ScoreController scoreController; 

    private void Start()
    {
        scoreController = FindObjectOfType<ScoreController>();        

    }
    void Update()
    {
        Debug.Log(levelSkillFastGun);
        levelFastGun.text = "Level: " + levelSkillFastGun;
        levelStrongGun.text = "Level: " + levelSkillStrongGun;
        levelBomml.text = "level: " + levelSkillBom;

        costUpdateFastGun.text = costUpdateLevelFastGun + " Gold";
        costUpdateStrongGun.text = costUpdateLevelStrongGun + " Gold";
        costUpdateBomGun.text = costUpdateBom + " Gold";
    }

    public void UpdateLevelSkillFastGun()
    {
        if (levelSkillFastGun < 3 && scoreController.coins > costUpdateLevelFastGun)
        {

            levelSkillFastGun += 1;
            scoreController.coins -= costUpdateLevelFastGun;
            costUpdateLevelFastGun += 100;           
        }
    }
    public void UpdateLevelSkillStrongGun()
    {
        if ( levelSkillStrongGun < 3 && scoreController.coins > costUpdateLevelStrongGun)
        {
            levelSkillStrongGun += 1;
            scoreController.coins -= costUpdateLevelStrongGun;
            costUpdateLevelStrongGun += 100;           
        }
    }
    public void UpdateLevelSkillBoom()
    {
        if (levelSkillBom < 3 && scoreController.coins > costUpdateBom)
        {
            levelSkillBom += 1;
            scoreController.coins -= costUpdateBom;
            costUpdateBom += 100;
            
        }
    }
}
