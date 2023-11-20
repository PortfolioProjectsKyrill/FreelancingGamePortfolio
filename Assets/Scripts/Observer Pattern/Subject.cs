using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    [SerializeField] private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObservers(int Dmg)
    {
        observers.ForEach((observer) => { observer.OnNotify(Dmg); });
    }

    private void Start()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            print(observers[i].ToString());
        }
    }
}
