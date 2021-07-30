using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Controller : MonoBehaviour
{
    public float posX, posY, posZ;
    public GameObject player;

    private float playerDistance;
    private Image myImage;

    public delegate void ClickAction(float x, float y, float z);
    public static event ClickAction OnClicked;

    private void Start()
    {
        myImage = this.GetComponent<Image>();
    }
    private void Update()
    {
        playerDistance = Vector3.Distance(this.transform.position, player.transform.position);
        //Debug.Log("Distancia: " + playerDistance);
        if (playerDistance <= 3.0f)
            myImage.enabled = false;
        else
            myImage.enabled = true;

        Vector3 relativePos = player.transform.position - this.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        this.transform.rotation = rotation;
        this.transform.Rotate(Vector3.up, 180.0f);
    }

    public void QuieroIr()
    {
        if (OnClicked != null)
            OnClicked(posX, posY, posZ);
    }
}
