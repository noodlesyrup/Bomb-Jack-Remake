using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Epatrol2>().EnemySlow();
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Destroy(gameObject);
        }
    }

}
