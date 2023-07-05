using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelWeapon : MonoBehaviour
{
    public Text levelFastGun;
    public Text levelStrongGun;
    public Text levelBomml;

    public TextMeshProUGUI costUpdateFastGun;
    public TextMeshProUGUI costUpdateStrongGun;
    public TextMeshProUGUI costUpdateBom;


    private ScoreController scoreController;
    private Weapon weapon;

    private void Start()
    {
        scoreController = FindObjectOfType<ScoreController>();
        weapon = FindObjectOfType<Weapon>();

    }
    void Update()
    {
        Debug.Log(weapon.costUpdateLevelFastGun);
        levelFastGun.text = "Level: " + weapon.levelSkillFastGun;
        levelStrongGun.text = "Level: " + weapon.levelSkillStrongGun;
        levelBomml.text = "level: " + weapon.levelSkillBom;

        costUpdateFastGun.text = weapon.costUpdateLevelFastGun + " Gold";
        costUpdateStrongGun.text = weapon.costUpdateLevelStrongGun + " Gold";
        costUpdateBom.text = weapon.costUpdateBom + " Gold";
    }

    public void UpdateLevelSkillFastGun()
    {
        if (weapon.levelSkillFastGun < 3 || scoreController.coins > weapon.costUpdateLevelFastGun)
        {

            weapon.levelSkillFastGun += 1;
            weapon.costUpdateLevelFastGun += 100;
            scoreController.coins -= weapon.costUpdateLevelFastGun;
        }
    }
    public void UpdateLevelSkillStrongGun()
    {
        if ( weapon.levelSkillStrongGun < 3 || scoreController.coins > weapon.costUpdateLevelStrongGun)
        {
            weapon.levelSkillStrongGun += 1;
            weapon.costUpdateLevelStrongGun += 100;
            scoreController.coins -= weapon.costUpdateLevelStrongGun;
        }
    }
    public void UpdateLevelSkillBoom()
    {
        if ( weapon.levelSkillBom < 3 || scoreController.coins > weapon.costUpdateBom)
        {
            weapon.levelSkillBom += 1;
            weapon.costUpdateBom += 100;
            scoreController.coins -= weapon.costUpdateBom;
        }
    }
}
