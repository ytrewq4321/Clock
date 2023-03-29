using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DigitalClock : Clock
{
    [SerializeField] private TextMeshProUGUI hoursText;
    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private TMP_InputField hoursInput;
    [SerializeField] private TMP_InputField minutesInput;
    [SerializeField] private Button onAlarmButton;
    [SerializeField] private Button offAlarmButton;
    private bool isAlarmEnabled;
    private float temp;

    private void Start()
    {
        onAlarmButton.onClick.AddListener(() => isAlarmEnabled = true);
        onAlarmButton.onClick.AddListener(SetAlarm);
        offAlarmButton.onClick.AddListener(() => isAlarmEnabled = false);
        hoursInput.onValueChanged.AddListener(OnHoursChanged);
        minutesInput.onValueChanged.AddListener(OnMinutesChanged);
    }
    private  void SetAlarm()
    {
        var hours = int.Parse(hoursInput.text);
        var minutes = int.Parse(minutesInput.text);
        AlarmTime = new TimeSpan(hours, minutes, 0);
    }


    private void Update()
    {
        temp += Time.deltaTime;
        if (temp >= 1f)
        {
            time += TimeSpan.FromSeconds(1);
            hoursText.text = time.Hours.ToString();
            minutesText.text = time.Minutes.ToString();
            secondsText.text = time.Seconds.ToString();
            temp = 0;
        }

        if(isAlarmEnabled)
        {
            if(AlarmTime.Hours==time.Hours && AlarmTime.Minutes==time.Minutes)
            {
                Debug.Log("ALARM!!!!");
            }
        }
    }

    private void OnHoursChanged(string value)
    {
        if (!Regex.IsMatch(value, "^([0-9]|0[0-9]|1[0-9]|2[0-3])$"))
        {
            hoursInput.text = "00";
        }
    }

    private void OnMinutesChanged(string value)
    {
        if (!Regex.IsMatch(value, "^([0-6]|[0-5][0-9])$"))
        {
            minutesInput.text = "00";
        }
    }
}
