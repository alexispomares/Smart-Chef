using UnityEngine;
using System.Collections;
using System;

public class Sensor : MonoBehaviour
{
    private string url = "http://192.168.43.183:3000/data";
    public FoodManager foodManager;

    [Serializable]
    public class SensorData
    {
        public int temperature;
        public float carboneDioxide;
        public string lastMovement;

        public SensorData()
        {
            temperature = 0;
            carboneDioxide = 0;
            lastMovement = "";
        }
    }

    public SensorData data = new SensorData();

    void Start()
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {

        yield return www;

        // check for errors
        if (www.error == null)
        {
            data = JsonUtility.FromJson<SensorData>(www.text);
            //Debug.Log(JsonUtility.ToJson(data));
            foodManager.temperature = data.temperature;
            foodManager.co2 = data.carboneDioxide;
            string[] s = data.lastMovement.Split(':');
            foodManager.timeStir = 60 * float.Parse(s[1]) + float.Parse(s[2]);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    private int count = 0;
    void Update()
    {
        if (++count >= 200)
        {
            count = 0;
            WWW www = new WWW(url);
            StartCoroutine(WaitForRequest(www));
        }
    }
}