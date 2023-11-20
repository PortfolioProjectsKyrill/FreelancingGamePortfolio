using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour, IHealth, IObserver
{
    [Header("Enemy Health Vars")]
    public float currentEnemyHealth;
    [SerializeField] private float maxEnemyHealth;
    [SerializeField] private float minEnemyHealth = 0f;

    [SerializeField] private GameObject TMPObject;

    public List<Subject> _subjects;
    private void Start()
    {
        StartGame(maxEnemyHealth);
        _subjects = new List<Subject>();
    }
    public void StartGame(float l_maxEnemyHealth, float one = 0, float two = 0, float three = 0)
    {
        currentEnemyHealth = l_maxEnemyHealth;
        StartCoroutine(AddObserver());
    }
    public float DoDamage(float l_EnemyDamage)
    {
        float l_Health = currentEnemyHealth -= l_EnemyDamage;
        return l_Health;
    }
    public void SetHealth(float l_bulletDamage)
    {
        //Checks enemy for negative health
        if (currentEnemyHealth > minEnemyHealth)
        {
            //instantiate text
            GameObject TMP = Instantiate(TMPObject, transform);

            //do the actual damage
            currentEnemyHealth = DoDamage(l_bulletDamage);
        }
        else if (currentEnemyHealth <= minEnemyHealth)
            Death();
    }

    public void OnNotify(int Dmg, string Tag)
    {
        SetHealth(Dmg);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        //adds itself to observer list
        foreach (Subject subject in _subjects)
        {
            subject.RemoveObserver(this);
        }
    }

    private IEnumerator AddObserver()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Subject subject in _subjects)
        {
            subject.AddObserver(this);
        }
    }
}
