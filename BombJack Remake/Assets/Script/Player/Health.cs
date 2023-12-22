using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameManager gameManager;
    public int numOfHearts;
    public int playerHealth = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHearts;

    private void Start()
    {
        UpdateHealth();
    }
    public void Update()
    {
        if (playerHealth == 0)
        {
            gameManager.EndGame();
        }
    }

    public void UpdateHealth()
    {
        if(playerHealth > numOfHearts)
        {
            playerHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }
        }
    }
}
