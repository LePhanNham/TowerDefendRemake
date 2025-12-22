
using System;
using UnityEngine;
[CreateAssetMenu(fileName = "IntEventControl", menuName = "Event/IntEventControl")]
public class IntEventControl : ScriptableObject
{
    public event Action<int> OnEvenRaised;

    public void Raise(int value)
    {
        OnEvenRaised?.Invoke(value);
    }

    public void Subscribe(Action<int> onEvenRaised)
    {
        OnEvenRaised += onEvenRaised;
    }

    public void Unsubscribe(Action<int> onEvenRaised)
    {
        OnEvenRaised -= onEvenRaised;
    }
    
    
}
