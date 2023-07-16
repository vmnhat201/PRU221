using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //[SerializeField]
    //public GameObject prefabCoin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            Debug.Log("Đã vô coin đây!!!");
            GameManager.instance.AddCoin(1);
            Destroy(this.gameObject);
        }
    } 
}
