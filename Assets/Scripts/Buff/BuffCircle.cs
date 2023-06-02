using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCircle : MonoBehaviour
{
    public float duration = 45f;
    public Vector3 startScale = new Vector3(5f, 5f, 1f);

    private float scaleSpeed;
    private Vector3 initialScale;
    private bool isScaling = false;
    private void Start()
    {
        initialScale = startScale;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ButtonControl.instance.isBuffCircle = true;
        }
        if (collision.CompareTag("Player") && !isScaling)
        {
            Debug.Log("ða vao");
            CountdownTime();
            isScaling = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ButtonControl.instance.isBuffCircle = false;
        }
    }
    private void CountdownTime()
    {
        scaleSpeed = startScale.x / duration;
        transform.localScale = initialScale;

        StartCoroutine(DecreaseSize());
    }

    private System.Collections.IEnumerator DecreaseSize()
    {
        while (transform.localScale.x > 0f)
        {
            float scaleFactor = scaleSpeed * Time.deltaTime;
            transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0f);

            yield return null;
        }
        Destroy(gameObject);
    }

}
