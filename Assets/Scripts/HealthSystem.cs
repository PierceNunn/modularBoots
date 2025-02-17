using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    private float currentHealth;
    private CanDie entityController;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        entityController = GetComponent<CanDie>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(name + " took " + damage + " damage.");
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) 
        {
            AudioManager.Instance.PlaySFX("Enemy Death");
            entityController.Die();
        }
        //Debug.Log("Health: " + CurrentHealth + " Max Health: " + MaxHealth);
    }

    public void ReceiveHealing(float healing)
    {
        CurrentHealth += healing;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    
}
