using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public GameObject player;
    public GameObject myTv1, myTv2, myTv3;
    private float playerDistance;
    
    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance(myTv1);
        CheckPlayerDistance(myTv2);
        CheckPlayerDistance(myTv3);
    }
    void CheckPlayerDistance(GameObject pantalla)
    {
        Debug.Log("Soy la pantalla: " + pantalla.transform.position);
        playerDistance = Vector3.Distance(pantalla.transform.position, player.transform.position);
        var vp = pantalla.GetComponentInChildren<VideoPlayer>();
        if (playerDistance <= 10.0f)
            vp.Play();
        else
            vp.Pause();
    }
}
