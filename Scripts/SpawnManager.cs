using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{

    public static int totalEnemies = 10;
<<<<<<< Updated upstream
   
    public List<Weapon> weaponsPrefab;
    public GameObject[] gunPrefabs; 
=======
>>>>>>> Stashed changes

    // Start is called before the first frame update
    public void BuffSpawn(Transform tf)
    {

        int r = Random.Range(0, 10);
        if (r < 2)
        {
            Debug.Log("abc");
            Instantiate(GameManager.instance.Buffs[Random.Range(0, 2)], tf.position,Quaternion.identity);
            Debug.Log("ABC");

        }
        else if (r < 3)
        {
            Debug.Log("123");
            Instantiate(GameManager.instance.Buffs[Random.Range(3, 5)], tf.position, Quaternion.identity);
        }
    }
    void Start()
    {
<<<<<<< Updated upstream
        InvokeRepeating("SpawnEnemies", 0f, 10f);
        InvokeRepeating("SpawnBoss", 50f, 50f);
=======
        InvokeRepeating("SpawnEnemies", 0f, 1000000f);
        InvokeRepeating("SpawnBoss", 50f, 50f);       
>>>>>>> Stashed changes
    }
    void Update()
    {
    }

    public void SpawnEnemies()
    {
        if (GameManager.instance.isBossAlive == false)
        {
            foreach (var item in GameManager.instance.Enemies)
            {
                if (item.enemyType == EnemyType.Bee)
                {
                    for (int i = 0; i < GameManager.instance.totalEnemies * 0.1; i++)
                    {
                        Instantiate(item.gameObject, Gennerate(), Quaternion.identity);
                    }
                }

                if (item.enemyType == EnemyType.Ant)
                {
                    for (int i = 0; i < GameManager.instance.totalEnemies * 0.7; i++)
                    {
                        Instantiate(item.gameObject, Gennerate(), Quaternion.identity);
                    }
                }

                if (item.enemyType == EnemyType.Ranged)
                {
                    for (int i = 0; i < GameManager.instance.totalEnemies * 0.2; i++)
                    {
                        Instantiate(item.gameObject, Gennerate(), Quaternion.identity);
                    }
                }
            }
        }
    }

    public void SpawnBoss()
    {
        foreach (var item in GameManager.instance.Enemies)
        {
            if (item.enemyType == EnemyType.Boss && GameManager.instance.isBossAlive == false)
            {
                GameManager.instance.isBossAlive = true;
                Instantiate(item.gameObject, Gennerate(), Quaternion.identity);
            }
        }
    }
    public void SpawnWeapon(Transform tf)
    {
        int r = Random.Range(0, 10);
        if (r <= 2)
        {
            Instantiate(GameManager.instance.Weapons[Random.Range(0, 2)], tf.position, Quaternion.identity);
        }
        else if (r <= 3)
        {
            Instantiate(GameManager.instance.Weapons[Random.Range(0, 3)], tf.position, Quaternion.identity);
        }

    }
    public Vector3 Gennerate()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // save screen edges in world coordinates
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
}