using System;
using UnityEngine;
[CreateAssetMenu(fileName = "StringEventControl", menuName = "Event/StringEventControl")]
public class StringEventControl : ScriptableObject
{
    public event Action<string> OnEvenRaised;

    public void Raise(string value)
    {
        OnEvenRaised?.Invoke(value);
    }

    public void Subscribe(Action<string> onEvenRaised)
    {
        OnEvenRaised += onEvenRaised;
    }

    public void Unsubscribe(Action<string> onEvenRaised)
    {
        OnEvenRaised -= onEvenRaised;
    }
    
    
}