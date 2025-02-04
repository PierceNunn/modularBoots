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
