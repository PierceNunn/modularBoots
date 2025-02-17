using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    private float currentHealth;
    private CanDie entityController;
    

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<EnemyBehavior>() != null)
        {
            maxHealth = GetComponent<EnemyBehavior>().EnemyStats.health;
        }
        currentHealth = maxHealth;
        entityController = GetComponent<CanDie>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(name + " took " + damage + " damage.");
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            AudioManager.Instance.PlaySFX("Enemy Death");
            entityController.Die();
        }
    }

    public void ReceiveHealing(float healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    
}
