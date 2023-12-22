using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField]
    private int enemyDamage;

    [SerializeField]
    private Health _health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Damage();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<PlayerController>();
        if(player != null)
        {
            player.Die();
            FindObjectOfType<AudioManager>().Play("Damage");
        }
    }

    void Damage()
    {
        _health.playerHealth = _health.playerHealth - enemyDamage;
        _health.UpdateHealth();
    }
}
