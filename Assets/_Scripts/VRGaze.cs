using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour
{
    public Image imgGaze;

    public float totalTime;
    bool gvrStatus;
    float gvrTimer;

    public int distanceOfRay = 10;
    // Start is called before the first frame update
    void Start()
    {
        GVROff();
    }

    // Update is called once per frame
    void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        { 
            if (imgGaze.fillAmount == 1 && hit.transform.CompareTag("Teleport") && Button_Controller.iCanClick)
                hit.transform.gameObject.GetComponent<Button_Controller>().QuieroIr();
            else if (imgGaze.fillAmount == 1 && hit.transform.CompareTag("Credits"))
                hit.transform.gameObject.GetComponent<MainMenu_Controller>().OpenCreditsPanel();
            else if (imgGaze.fillAmount == 1 && hit.transform.CompareTag("Exit"))
                hit.transform.gameObject.GetComponent<MainMenu_Controller>().CloseApp();
            else if (imgGaze.fillAmount == 1 && hit.transform.CompareTag("Back"))
            {
                hit.transform.gameObject.GetComponent<MainMenu_Controller>().CloseCreditsPanel();
                GVROff();
            }
                
            
        }
    }

    public void GVROn()
    {
        gvrStatus = true;
    }
    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }
}
