using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Events;
using System;

public class TimeAPI : MonoBehaviour
{
    public TimeSpan Time;
    public UnityEvent<TimeSpan> TimeReceived=new();
    private TimeResponse TimeResponse { get; set; }
    private string responseJson;
    private readonly string[] url = new string[]   {
                                                    //"http://worldtimeapi.org/api/timezone/Europe/Moscow",
                                                    "https://timeapi.io/api/Time/current/zone?timeZone=Europe/Moscow"
                                                   };

    public void GetTime()
    {
        StartCoroutine(LoadJsonTime(url[UnityEngine.Random.Range(0, 1)]));
    }

    private IEnumerator LoadJsonTime(string url)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                yield break;
            }
            else
            {
                responseJson = request.downloadHandler.text;

                TimeResponse = JsonUtility.FromJson<TimeResponse>(responseJson);

                if (TimeResponse.datetime != null)
                    TimeResponse.time = TimeResponse.datetime;
                else if (TimeResponse.dateTime != null)
                    TimeResponse.time = TimeResponse.dateTime;

                DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(TimeResponse.time);
                Time = new TimeSpan(dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second);
                TimeReceived.Invoke(Time);
            }
        }
    }
}
