using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField]
    private int healValue;

    [SerializeField]
    private Health _health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Heal();
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }
    void Heal()
    {
        _health.playerHealth = _health.playerHealth + healValue;
        _health.UpdateHealth();
    }
}
