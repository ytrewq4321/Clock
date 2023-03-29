using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private Button analogClock;
    [SerializeField] private Button digitClock;
    [SerializeField] private Button onButton;
    [SerializeField] private Button offButton;
    [SerializeField] private Button showButton;

    [SerializeField] private GameObject digitalAlarmTime;
    [SerializeField] private GameObject digitalTime;

    void Start()
    {
        showButton.onClick.AddListener(ShowTime);
    }

    private void ShowTime()
    {
        digitalAlarmTime.SetActive(!digitalAlarmTime.activeSelf);
        digitalTime.SetActive(!digitalTime.activeSelf);

        if(digitalAlarmTime.activeSelf)
        {
            showButton.GetComponentInChildren<TextMeshProUGUI>().text = "Show Time";
        }
        else
        {
            showButton.GetComponentInChildren<TextMeshProUGUI>().text = "Show Alarm Time";
        }
    }
}
