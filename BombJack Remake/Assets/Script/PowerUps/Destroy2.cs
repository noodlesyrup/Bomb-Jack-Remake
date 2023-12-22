using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Epatrol2>().Destroy();
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Destroy(gameObject);
        }
    }
}
