using System;
using UnityEngine;

public abstract class Clock : MonoBehaviour
{
    [SerializeField] private TimeAPI timeAPI;
    internal TimeSpan time;
    public static TimeSpan AlarmTime;

    void Awake()
    {
        timeAPI.TimeReceived.AddListener(SetTime);
        InvokeRepeating("SyncTime", 0,3600);
    }

    private void SetTime(TimeSpan timeSpan)
    {
        time = timeSpan;
    }

    private void SyncTime()
    {
        timeAPI.GetTime();
    }
}
