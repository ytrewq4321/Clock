using UnityEngine;
using System;
using UnityEngine.UI;

public class AnalogClock : Clock
{
    [SerializeField] private Transform hoursHand;
    [SerializeField] private Transform minutesHand;
    [SerializeField] private Transform secondsHand;
    [SerializeField] private Transform alarmHand;
    [SerializeField] private Button onAlarmButton;
    [SerializeField] private Button offAlarmButton;

    private bool isAlarmEnabled;
    private float hoursDegrees = -30f;
    private float minitesDegrees = -6f;
    private float secondDegrees = -6f;
    private float temp;
    private float previousAngle;
    private float startRotation;

    private void Start()
    {
        startRotation = alarmHand.localRotation.eulerAngles.z;
        onAlarmButton.onClick.AddListener(() => isAlarmEnabled = true);
        onAlarmButton.onClick.AddListener(SetAlarm);
        offAlarmButton.onClick.AddListener(() => isAlarmEnabled = false);
    }

    private void Update()
    {
        temp += Time.deltaTime;
        if (temp >= 1f)
        {
            time += TimeSpan.FromSeconds(1);
            hoursHand.localRotation = Quaternion.Euler(0, 0, hoursDegrees * (float)time.TotalHours);
            minutesHand.localRotation = Quaternion.Euler(0, 0, minitesDegrees * (float)time.TotalMinutes);
            secondsHand.localRotation = Quaternion.Euler(0, 0, secondDegrees * (float)time.TotalSeconds);
            temp = 0;
        }

        if(Input.touchCount > 0 && !isAlarmEnabled)
        {
            RotateAlarmHand();
        }

        if(isAlarmEnabled)
        {
            if ((time.Hours == AlarmTime.Hours || time.Hours == AlarmTime.Hours + 12) && time.Minutes == AlarmTime.Minutes)
            {
                Debug.Log("ALARM!!!");
            }
        }
    }

    private void RotateAlarmHand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == transform.gameObject)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(alarmHand.position);
            Vector3 delta = touchPosition - objectPosition;
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg - startRotation - 90f;
            float angleDelta = angle - previousAngle;
            alarmHand.transform.Rotate(0f, 0f, angleDelta);
            previousAngle = angle;
        }
    }

    private void SetAlarm()
    {
        float alarmRotation = alarmHand.localRotation.eulerAngles.z;

        if (alarmRotation >= 180 && alarmRotation <= 360)
        {
            alarmRotation -= 360f;
        }
        else if (alarmRotation >0  && alarmRotation < 180)
        {
            alarmRotation = alarmRotation - 360;
        }

        float totalHours = alarmRotation /-30f;
        int hours = Mathf.FloorToInt( totalHours);
        int minutes = Mathf.FloorToInt(totalHours * 60 % 60f);
        AlarmTime = new TimeSpan(hours, minutes,0);
    }
}
