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
        levelFastGun.text = "Level: " + weapon.leveSkillFastGun;
        levelStrongGun.text = "Level: " + weapon.leveSkillStrongGun;
        levelBomml.text = "level: " + weapon.leverSkillBom;

        costUpdateFastGun.text = weapon.costUpdateLevelFastGun + " Gold";
        costUpdateStrongGun.text = weapon.costUpdateLevelStrongGun + " Gold";
        costUpdateBom.text = weapon.costUpdateBom + " Gold";
    }

    public void UpdateLevelSkillFastGun()
    {
        if (weapon.leveSkillFastGun < 3 || scoreController.coins < weapon.costUpdateLevelFastGun)
        {

            weapon.leveSkillFastGun += 1;
            weapon.costUpdateLevelFastGun += 100;
            scoreController.coins -= weapon.costUpdateLevelFastGun;
        }
    }
    public void UpdateLevelSkillStrongGun()
    {
        if ( weapon.leveSkillStrongGun < 3 || scoreController.coins > weapon.costUpdateLevelStrongGun)
        {
            weapon.leveSkillStrongGun += 1;
            weapon.costUpdateLevelStrongGun += 100;
            scoreController.coins -= weapon.costUpdateLevelStrongGun;
        }
    }
    public void UpdateLevelSkillBoom()
    {
        if ( weapon.leverSkillBom < 3 || scoreController.coins > weapon.costUpdateBom)
        {
            weapon.leverSkillBom += 1;
            weapon.costUpdateBom += 100;
            scoreController.coins -= weapon.costUpdateBom;
        }
    }
}
