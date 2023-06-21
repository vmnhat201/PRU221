using Assets.Scripts.SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntEnemy : Enemies
{
    ////New Commit
    //public Sprite intro;
    ////New Commit
    ////Attribute and Property
    //[SerializeField] public GameObject explosivePrefabs;
    //public BulletEnemies rangedBulletPrefabs;
    //public EnemyType enemyType;
    //public float currentHealth;
    //public float maxHealth;
    //public int damage;
    //public float movementSpeed;
    //public float attackSpeed = 0;
    //public bool isAlive;
    //public bool isHunt;
    //public Vector3 endPoint;
    //public float popular;

    //Timer timer;
    //Timer timer1;

    void Start()
    {
        //timer = gameObject.AddComponent<Timer>();
        //timer1 = gameObject.AddComponent<Timer>();
        SetUp();
        currentHealth = maxHealth;
        isAlive = true;
        isHunt = false;
        endPoint = Gennerate();
        //timer.Duarion = 2;
        //timer.Run();
        //timer1.Duarion = 3;
        //timer1.Run();
    }

    public void SetUp()
    {
        popular = 0.7f;
        maxHealth = 50;
        damage = 2;
        movementSpeed = GameManager.instance.player.speed * 0.1f;
    }
    void Update()
    {
        Hunt(GameManager.instance.player.transform.position, movementSpeed);
        if (GameSave.instance.isIntro)
        {
            damage = 1;
        }
        else
        {
            SetUp();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;

            //if (enemyType == EnemyType.Ant)
            //{
                GameManager.instance.isAntAliveIntro = false;

                if (GameSave.instance.isIntro != true)
                {
                    ScoreController.instance.Addpoint(1);
                }
            //}
            DestroyEnemies();
        }
    }

    public void Hunt(Vector3 player, float MovementSpeed)
    {
        if (GameManager.instance.player.isVisible == false)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                    player, MovementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, MovementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, endPoint) < 0.001f)
            {
                endPoint = Gennerate();
            }
        }

    }


    public void AttackPlayer()
    {
        GameManager.instance.player.TakeDamge(damage);

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            AttackPlayer();
        }

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
