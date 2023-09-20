using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagablePawn : MonoBehaviour
{
    public enum PawnState { alive, dead}
    protected PawnState state;
    protected AnimationController animController;

    [SerializeField]
    protected int MaxHealth = 20;
    protected int currentHealth = 0;
    [SerializeField]
    GameObject healthBarObj;
    Slider healthBar;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }
    protected void UpdateHealthUI()
    {
        if(healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        healthBar = healthBarObj.GetComponent<Slider>();
        healthBar.maxValue = MaxHealth;
        state = PawnState.alive;
        currentHealth = MaxHealth;
        healthBar.value = currentHealth;
    }

    protected virtual void Die()
    {
        state = PawnState.dead;
    }
}
