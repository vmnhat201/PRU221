//using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum EnemyType
{
    Ant, 
    Ranged, 
    Bee, 
    Boss
}
public class Enemies : MonoBehaviour
{
    //Attribute and Property
    [SerializeField] public GameObject explosivePrefabs;
    public BulletEnemies rangedBulletPrefabs;
    public EnemyType enemyType;
    public float currentHealth;
    public float maxHealth;
    public int damage;
    public float movementSpeed;
    public float attackSpeed = 0;
    public bool isAlive;
    public bool isHunt;
    Vector3 endPoint;

    Timer timer;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        SetUp();
        currentHealth = maxHealth;
        isAlive = true;
        isHunt = false;
        endPoint = Gennerate();
        timer.Duarion = 3;
        timer.Run();
    }

    public void SetUp()
    {
        switch(enemyType)
        {
            case EnemyType.Ant:
                maxHealth = 50;
                damage = 5;
                movementSpeed= GameManager.instance.player.speed * 0.3f;
                break;
            case EnemyType.Ranged:
                maxHealth = 40;
                damage = 15;
                movementSpeed = 5;
                break;
            case EnemyType.Bee:
                maxHealth = 20;
                damage = 20;
                movementSpeed = 25;
                attackSpeed= 50;
                break;
            case EnemyType.Boss:
                maxHealth = 50;
                damage = 15;
                movementSpeed = 2;
                break;
        }
    }
    void Update()
    {
        if (enemyType == EnemyType.Ant)
        {
            Hunt(GameManager.instance.player.transform.position, movementSpeed);
        }

        if (enemyType == EnemyType.Ranged)
        {
            Patrol();
        } 

        if (enemyType == EnemyType.Bee)
        {
            Patrol();
        }

        if (enemyType == EnemyType.Boss)
        {
            Hunt(GameManager.instance.player.transform.position, movementSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isAlive = false;

            if (enemyType == EnemyType.Boss)
            {
                GameManager.instance.isBossAlive = false;
                UpgradeAttribute();
            }
            DestroyEnemies();
        }
    }

    public void Hunt(Vector3 player, float MovementSpeed)
    {
        if (enemyType == EnemyType.Ant)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                        player, MovementSpeed * Time.deltaTime);
        }
        //if (enemyType == EnemyType.Ranged)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position,
        //                player, MovementSpeed * Time.deltaTime);
        //}
        Vector3 po = player;
        if (enemyType== EnemyType.Bee)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                       po, MovementSpeed * Time.deltaTime);
        }

        Vector3 po1 = transform.position;
        
        if (enemyType == EnemyType.Boss)
        {
            if (Vector3.Distance(po1, player) < 10f)
            {
                transform.position = po1;
                
            } else
            {
                transform.position = Vector3.MoveTowards(po1,
                            player, MovementSpeed * Time.deltaTime);
            }
        }
    }


    public void AttackPlayer()
    {
        switch (enemyType)
        {
            case EnemyType.Ant:
                GameManager.instance.player.TakeDamge(damage);
                break;
            case EnemyType.Ranged:
                    BulletEnemies bur = Instantiate(rangedBulletPrefabs, transform.position, Quaternion.identity);
                    Vector3 dir = GameManager.instance.player.transform.position - transform.position;
                    bur.Project(dir);
                break;
            case EnemyType.Bee:
                GameManager.instance.player.TakeDamge(damage);
                break;
        }
    }
    public void Patrol()
    {
        if (enemyType == EnemyType.Bee)
        {
            if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) < 10f)
            {
                Hunt(GameManager.instance.player.transform.position, movementSpeed);
            }
            else
            {
                Vector2 dir = endPoint - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = rotation;

                transform.position = Vector3.MoveTowards(transform.position, endPoint, 10 * Time.deltaTime);
                if (Vector3.Distance(transform.position, endPoint) < 0.001f)
                {
                    endPoint = Gennerate();
                }
            }
        }
        Vector3 po = transform.position;
        if (enemyType == EnemyType.Ranged)
        {
            if (Vector3.Distance(po, GameManager.instance.player.transform.position) < 10f)
            {
                transform.position = po;
                Vector2 dir = endPoint - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = rotation;
                if (timer.Finished)
                {
                    AttackPlayer();
                    timer.Duarion = 3;
                    timer.Run();
                }

            }
            else
            {
                Vector2 dir = endPoint - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = rotation;

                transform.position = Vector3.MoveTowards(transform.position, endPoint, 10 * Time.deltaTime);
                if (Vector3.Distance(transform.position, endPoint) < 0.001f)
                {
                    endPoint = Gennerate();
                }
            }
        }

    }

    public void UpgradeAttribute()
    {
        GameManager.instance.totalEnemies = (int)Mathf.Round(GameManager.instance.totalEnemies * 1.3f);
        if (enemyType == EnemyType.Ant)
        {
            maxHealth += maxHealth * 0.1f;
            damage += (int)Mathf.Round(damage * 0.1f);
            movementSpeed += movementSpeed * 0.1f;
        }

        if (enemyType == EnemyType.Bee)
        {
            maxHealth += maxHealth * 0.1f;
            damage += (int)Mathf.Round(damage * 0.1f);
            movementSpeed += movementSpeed * 0.1f;
        }

        if (enemyType == EnemyType.Ranged)
        {
            maxHealth += maxHealth * 0.1f;
            damage += (int)Mathf.Round(damage * 0.1f);
            movementSpeed += movementSpeed * 0.1f;
        }

        if (enemyType == EnemyType.Boss)
        {
            maxHealth += maxHealth * 0.2f;
            damage += (int)Mathf.Round(damage * 0.2f);
            movementSpeed += movementSpeed * 0.1f;
        }
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

    //Check event takeDamage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p!=null) 
        {
            if(enemyType == EnemyType.Bee)
            {
                DestroyEnemies();
                AttackPlayer();    
            }

            if (enemyType == EnemyType.Ant)
            {
                AttackPlayer();
            }
        }
        
    }

    public void DestroyEnemies()
    {
        SpawnManager.instance.BuffSpawn(this.transform);
        SpawnManager.instance.SpawnWeapon(this.transform);
        Destroy(this.gameObject);
    }
 }
