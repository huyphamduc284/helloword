using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private Dictionary<string, float> activePowers;
    private Powerups pu;
    Dictionary<string, float> maxPowerDurations = new Dictionary<string, float>()
    {
        { "InstantKill", 10f },
    };

    private void Start()
    {
        pu = this;
        activePowers = new Dictionary<string, float>();
    }

    public void ActivatePowerup(string powerName, float duration)
    {
        if (activePowers.ContainsKey(powerName))
        {
            activePowers[powerName] = Mathf.Min(activePowers[powerName] + duration, maxPowerDurations[powerName]);
            //Debug.Log(activePowers[powerName]);
        }
        else
        {
            activePowers.Add(powerName, Mathf.Min(duration, maxPowerDurations[powerName]));
        }
    }

    private void Update()
    {
        List<string> keys = new List<string>(activePowers.Keys);
        foreach (string key in keys)
        {
            activePowers[key] -= Time.deltaTime;
            if (activePowers[key] <= 0)
            {
                DeactivatePowerup(key);
            }
        }
    }

    public bool IsPowerActive(string powerName)
    {
        return activePowers.ContainsKey(powerName);
    }

    private void DeactivatePowerup(string buffName)
    {
        activePowers.Remove(buffName);
    }
}
