using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBoarHeadAttack : MonoBehaviour
{
    private EnemyAIController enemyAIController;

    private void Awake()
    {
        enemyAIController = transform.root.GetComponent<EnemyAIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(enemyAIController.damageAmount);
        }
    }
}
