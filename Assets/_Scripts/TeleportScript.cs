using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    private Vector3 posInicial, posFinal;
    private float iniTime;
    private bool enableLerp;

    public float totalTime;

    private void OnEnable()
    {
        Button_Controller.OnClicked += Teleport;
    }
    private void OnDisable()
    {
        Button_Controller.OnClicked -= Teleport;
    }

    void Teleport(float x, float y, float z)
    {
        posInicial = this.transform.position;
        posFinal = new Vector3(x, y, z);
        iniTime = Time.time;
        enableLerp = true;
    }

    private void Start()
    {
        enableLerp = false;
    }
    private void Update()
    {
        if (enableLerp)
            this.transform.position = Lerp(posInicial, posFinal, iniTime, totalTime);
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timerStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timerStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        if (percentageComplete >= 1.0f)
        {
            Button_Controller.iCanClick = true;
            enableLerp = false;
        }
            
        var result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }
}
