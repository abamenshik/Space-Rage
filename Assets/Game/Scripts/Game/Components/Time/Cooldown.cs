using UnityEngine;

[System.Serializable]
public class Cooldown
{
    public float Value;

    private float _timesUp;

    public void Reset()
    {
        _timesUp = Time.time + Value;
    }
    public float RemainingTime => Mathf.Max(_timesUp - Time.time, 0);
    public bool IsReady => _timesUp <= Time.time;
}
