using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spino_Controller : MonoBehaviour
{
    private Animator anim_spino;
    private float cronometro;
    private Vector3 finalPosSipno;
    private float timeLerpStart, timeLerpStartAcender;
    private Quaternion inicialSpinoRot;
    private Quaternion preLerpSpinoRot;
    private float rotationTime;
    private bool ready4reload, acendiendo, nadando, decendiendo;

    public GameObject spino;
    public float speed = 20.0f;
    public float altura;
    public float lerpTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim_spino = spino.GetComponent<Animator>();
        Restart();    
    }
    void Restart()
    {
        Debug.Log("Iniciamos");
        cronometro = 0.0f;
        SetAnim2Nadar();
        inicialSpinoRot = spino.transform.rotation;
        rotationTime = 0;
        ready4reload = false;
        acendiendo = false;
        decendiendo = false;
        nadando = true;
    }

    // Update is called once per frame
    void Update()
    {
        cronometro += 1 * Time.deltaTime;
        //Debug.Log("Tiempo: " + cronometro);
        Controlador();
        

    }

    // Determina que fase del movimiento del spinosaurio es la siguiente
    void Controlador()
    {
        if (nadando)
            SpinoNadar(); 
        if(decendiendo)
            SpinoDecender();

        if (anim_spino.GetCurrentAnimatorStateInfo(0).IsName("Roar_Despegue"))
        {
            decendiendo = false;
            ready4reload = true;
        }
            
        if (ready4reload && anim_spino.GetCurrentAnimatorStateInfo(0).IsTag("Inicio_Nadar"))
        {
            timeLerpStartAcender = Time.time;
            acendiendo = true;
            ready4reload = false;
            SetAnim2Nadar();
            spino.transform.position = new Vector3(20.0f, 0.0f, 0.0f);
        }

        if (acendiendo)
        {
            SpinoAcender();
        }
    }

    void SpinoDecender()
    {
        SetAnim2Decender();
        spino.transform.position = Lerp(finalPosSipno, new Vector3(20.0f, 0.0f, 0.0f), timeLerpStart, lerpTime);
        spino.transform.rotation = Quaternion.Lerp(preLerpSpinoRot, inicialSpinoRot, rotationTime);
        rotationTime += Time.deltaTime / 5;
    }
    void SpinoAcender()
    {

        if (spino.transform.position != new Vector3(20.0f, altura, 0.0f))
            spino.transform.position = Lerp(new Vector3(20.0f, 0.0f, 0.0f), new Vector3(20.0f, altura, 0.0f), timeLerpStartAcender, 2.0f);
        else
            Restart();
    }

    void SpinoNadar()
    {
        spino.transform.RotateAround(Vector3.zero, spino.transform.up, speed * Time.deltaTime);
        spino.transform.position = new Vector3(spino.transform.position.x, altura, spino.transform.position.z);
        if (cronometro >= 15.0f)
        {
            finalPosSipno = spino.transform.position;
            timeLerpStart = Time.time;
            preLerpSpinoRot = spino.transform.rotation;
            nadando = false;
            decendiendo = true;
        }
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timerStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timerStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }

    void SetAnim2Nadar()
    {
        anim_spino.SetBool("nadar", true);
    }
    void SetAnim2Decender()
    {
        anim_spino.SetBool("nadar", false);
    }
}
