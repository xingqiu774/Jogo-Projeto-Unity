using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SUBJECT : MonoBehaviour
{
    
    private List<IOBSERVER> _observers = new List<IOBSERVER>();

    public void AddObserver(IOBSERVER observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IOBSERVER observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers()
    {
        _observers.ForEach( (_observers) => { _observers.OnNotify(); } );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
