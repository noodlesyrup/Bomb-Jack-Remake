using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<EPatrol3>().EnemySlow();
            FindObjectOfType<AudioManager>().Play("PowerUp");
            Destroy(gameObject);
        }
    }
}
