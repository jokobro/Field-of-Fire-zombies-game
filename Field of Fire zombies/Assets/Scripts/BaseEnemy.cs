using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickups; 
    private GameManager gameManager;
    public int scoreAmount;
    public int health;


    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void HandleDamage(int damage)
    {
        health -= damage;

        if (health <= 0) 
        { 
            Destroy(this.gameObject);
        }
    }
}
