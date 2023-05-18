using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForBoss : MonoBehaviour
{
    private float moveSpeed;
    private Vector2 moveDirection;

    private void OnEnable()
    {
        Invoke("Destroy", 10f);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            GameManager.instance.player.TakeDamge(20);
        }
    }
}
