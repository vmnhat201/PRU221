using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class Boss1Enemy : Enemies

{
    [SerializeField] public Animator animator;
    public GameObject player;
    private float screenWidth;
    private float screenHeight;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dashspeed = 13f;
    [SerializeField] float dashduration = 2f;
    [SerializeField] float cooldown = 5f;
    bool isDashing;
    bool canDash = true;
    private bool flip;

    void Start()
    {

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        SetUp();
        currentHealth = maxHealth;
        isAlive = true;
        isHunt = false;
        endPoint = Gennerate();
    }

    public void SetUp()
    {
        maxHealth = 1000;
        damage = 30;
        movementSpeed = 6;
        dashspeed = 17f;
        dashduration = 2f;
        cooldown = 5f;
    }
    void Update()
    {

        if (GameSave.instance.isIntro)
        {
            damage = 1;
        }
        else
        {
            SetUp();
        }

        Hunt(GameManager.instance.player.transform.position, movementSpeed);

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;
            GameManager.instance.isBossAlive = false;

            Instantiate<GameObject>(explosivePrefabs, transform.position, Quaternion.identity);
            //ScoreController.instance.Addpoint(4);
            if (!GameManager.instance.isUpgrade)
            {
                GameManager.instance.UpgradeAttribute();
                GameManager.instance.isUpgrade = true;
            }
            if (GameSave.instance.isIntro != true)
            {
                //ScoreController.instance.Addpoint(4);
                GameManager.instance.AddCoin(10);
                GameManager.instance.Addpoint(4);
            }
            DestroyEnemies();
        }
    }

    public void Hunt(Vector3 player, float MovementSpeed)
    {
        Vector3 po1 = transform.position;
        if (GameManager.instance.player.isVisible == false)
        {
            if (Vector3.Distance(po1, player) < 10f && canDash)
            {
                StartCoroutine(Dash(player));

            }
            else
            {
                transform.position = Vector3.MoveTowards(po1,
                            player, MovementSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, MovementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.001f)
            {
                endPoint = Gennerate();
            }
        }

        ////}
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            AttackPlayer();

        }

    }
    public void AttackPlayer()
    {
        GameManager.instance.player.TakeDamge(damage);

    }
    private IEnumerator Dash(Vector3 player)
    {

        movementSpeed = dashspeed;
        Debug.Log(movementSpeed);
        Debug.Log("Dashing");
        canDash = false;
        isDashing = true;
        animator.SetBool("RunAnim", isDashing);
        //transform.position = Vector3.MoveTowards(transform.position,
        //                            player, movementSpeed * Time.deltaTime);

        yield return new WaitForSeconds(dashduration);
        isDashing = false;
        animator.SetBool("RunAnim", isDashing);
        movementSpeed = 2;
        Debug.Log("Stop Dashing");
        yield return new WaitForSeconds(cooldown);
        canDash = true;

    }



    public Vector3 Gennerate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld = Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        float screenLeft = lowerLeftCornerWorld.x;
        float screenRight = upperRightCornerWorld.x;
        float screenTop = upperRightCornerWorld.y;
        float screenBottom = lowerLeftCornerWorld.y;
        return new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop), -1);
    }


    public void DestroyEnemies()
    {
        SoundController.instance.PlayEnemyDead();
        SpawnManager.instance.BuffSpawn(this.transform);
        SpawnManager.instance.SpawnWeapon(this.transform);
        GameManager.instance.CurEnemies.Remove(this);
        Destroy(this.gameObject);
    }

    public static Enemies ToEnemies(EnemyData enemyData)
    {
        if (enemyData.prefabName == null)
            return null;
        GameObject enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(enemyData.prefabName);
        GameObject enemyObject = Instantiate(enemyPrefab);
        Enemies enemies = enemyObject.GetComponent<Enemies>();
        enemies.transform.position = enemyData.position.Vector3();
        enemies.transform.rotation = enemyData.rotation.Quaternion();
        enemies.intro = AssetDatabase.LoadAssetAtPath<Sprite>(enemyData.introSpriteName);
        if (enemyData.explosivePrefabs != null)
            enemies.explosivePrefabs = AssetDatabase.LoadAssetAtPath<GameObject>(enemyData.explosivePrefabs);
        if (enemyData.rangedBulletPrefabs != null)
            enemies.rangedBulletPrefabs = enemyData.rangedBulletPrefabs.BulletEnemies();
        enemies.enemyType = enemyData.enemyType;
        enemies.currentHealth = enemyData.currentHealth;
        enemies.maxHealth = enemyData.maxHealth;
        enemies.damage = enemyData.damage;
        enemies.movementSpeed = enemyData.movementSpeed;
        enemies.attackSpeed = enemyData.attackSpeed;
        enemies.isAlive = enemyData.isAlive;
        enemies.isHunt = enemyData.isHunt;
        enemies.endPoint = enemyData.endPoint.Vector3();
        enemies.popular = enemyData.popular;
        return enemies;
    }

}
