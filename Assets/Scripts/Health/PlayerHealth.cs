using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [Header("Health Vars")]
    public float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float minHealth = 0f;

    [Header("Oxygen Vars")]
    public float maxOxygen;
    public float currentOxygen;
    [SerializeField] private float oxygenDecayRate;

    [Header("Water Vars")]
    public float maxWater;
    public float currentWater;
    [SerializeField] private float waterDecayRate;
    [SerializeField] private float waterDamageRate;

    [Header("Food Vars")]
    public float maxFood;
    public float currentFood;
    [SerializeField] private float foodDecayRate;
    [SerializeField] private float foodDamageRate;

    [SerializeField] private Subject subject;

    public List<Subject> _subjects;

    [SerializeField] private UIManager Uimanager;
    private void Awake()
    {
        StartGame(maxHealth, maxOxygen, maxWater, maxFood);
        _subjects = new List<Subject>();
    }

    public void StartGame(float maxHealth, float maxOxygen, float maxWater, float maxFood)
    {
        currentHealth = maxHealth;
        currentOxygen = maxOxygen;
        currentWater = maxWater;
        currentFood = maxFood;
    }
    /// <summary>
    /// Does damage to this.gameobject
    /// </summary>
    /// <param name="Damage"></param>
    /// <returns></returns>
    public float DoDamage(float l_damage)
    {
        float l_Health = currentHealth - l_damage;
        return l_Health;
    }
    /// <summary>
    /// Updates the Health
    /// </summary>
    public void SetHealth(float l_bulletDamage)
    {
        //Checks if enemy doesn't have negative health
        if (currentHealth > minHealth)
        {
            currentHealth = DoDamage(l_bulletDamage);
        }
        else if (currentHealth < minHealth)
        {
            Destroy(gameObject);
        }
    }

    public void OnNotify(int Dmg, string Tag)
    {
        SetHealth(Dmg);
    }

    private void Update()
    {
        Decaying();
        Uimanager.SetOxygenBar(currentOxygen);
        Uimanager.SetFoodBar(currentFood);
        Uimanager.SetWaterBar(currentWater);

        CheckForDeath();
    }

    private void Decaying()
    {
        //Checks if player has enough oxygen
        if (currentOxygen > 0f)
        {
            currentOxygen -= oxygenDecayRate * Time.deltaTime;
        }
        else if (currentOxygen < 0f)
        {
            currentOxygen = 0f;
            GetComponent<PlayerMovement>().speed = GetComponent<PlayerMovement>().speed / 2;
        }

        //Checks if player has enough water
        if (currentWater > 0f)
        {
            currentWater -= waterDecayRate * Time.deltaTime;
        }
        else if (currentWater < 0f)
        {
            currentWater = 0f;
            currentHealth -= waterDamageRate * Time.deltaTime;
        }

        //Checks if player has enough food
        if (currentFood > 0f)
        {
            currentFood -= foodDecayRate * Time.deltaTime;
        }
        else if (currentFood < 0f)
        {
            currentFood = 0f;
            currentHealth -= foodDamageRate * Time.deltaTime;
        }
    }

    private void CheckForDeath()
    {
        if (currentHealth <= minHealth)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("reloaded scene");
        }
    }
}
