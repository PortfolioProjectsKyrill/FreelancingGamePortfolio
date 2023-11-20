using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] upgrades;
    public List<GameObject> colliders;
    public bool mapExplored = false;

    [SerializeField] private MissionCheckLists missionCheckLists;

    public static GameManager instance;

    [Header("EndGame")]
    [SerializeField] private GameObject gameMenu;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        upgrades = GameObject.FindGameObjectsWithTag("Upgrade");

        gameMenu.SetActive(false);
    }

    private void Update()
    {
        CheckEnemiesDead();
        CheckUpgradesCollected();
        CheckMapExplored();

        EndGame();
    }

    private bool CheckEnemiesDead()
    {
        int count = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                count++;
            }
        }

        if (count == enemies.Length)
        {
            // If all enemies are dead, perform some action (e.g. open the exit door)
            missionCheckLists.MarkObjective1Completed();
            return true;
        }
        else { return false; }
    }

    private bool CheckUpgradesCollected()
    {
        int count = 0;
        for (int i = 0; i < upgrades.Length; i++)
        {
            if (upgrades[i] == null)
            {
                count++;
            }
        }

        if (count == upgrades.Length)
        {
            // If all upgrades are collected, perform some action (e.g. open a secret room)
            missionCheckLists.MarkObjective2Completed();
            return true;
        }
        else { return false; }
    }

    private bool CheckMapExplored()
    {
        if (colliders.Count == 0)
        {
            mapExplored = true;
        }

        if (mapExplored)
        {
            // If all parts of the map have been explored
            missionCheckLists.MarkObjective3Completed();
        }
        return true;
    }

    private void EndGame()
    {
        bool one = CheckEnemiesDead();
        bool two = CheckUpgradesCollected();
        bool three = CheckMapExplored();

        if (one && two && three)
        {
            gameMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
