using System;
using UnityEngine;

[Serializable]
public class Cooldown
{
    public event Action OnReset;

    public float Value;

    private float _timesUp;

    public void Reset()
    {
        OnReset?.Invoke();

        _timesUp = Time.time + Value;
    }
    public float RemainingTime => Mathf.Max(_timesUp - Time.time, 0);
    public bool IsReady => _timesUp <= Time.time;
}
