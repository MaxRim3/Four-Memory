using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_Controller : MonoBehaviour
{

    public bool iPadTest;

    public double screenRatio;

    public GameObject camera;

    public GameObject canvas;



    void Start()
    {

        screenRatio = (1.0 * Screen.height) / (1.0 * Screen.width);


        if (SystemInfo.deviceModel.Contains("iPad") || iPadTest)
        {
            camera.GetComponent<Camera>().orthographicSize = 5.3f;
        }

        else if (screenRatio > 1 && screenRatio < 1.4f) //3:2 Iphones - models 4 and earlier
        {
            camera.GetComponent<Camera>().orthographicSize = 5;
        }

        else if (screenRatio > 1.4 && screenRatio < 1.6f) //3:2 Iphones - models 4 and earlier
        {
            camera.GetComponent<Camera>().orthographicSize = 6;
        }

        else if (screenRatio > 1.7 && screenRatio < 1.8) // 16:9 Iphones - models 5, SE, up to 8+
        {
            camera.GetComponent<Camera>().orthographicSize = 7;
        }

        else if (screenRatio > 2.1 && screenRatio < 2.2) //19.5:9 Iphones - models X, Xs, Xr, Xsmax
        {
            camera.GetComponent<Camera>().orthographicSize = 8.6f;
        }
        else if (screenRatio < 0.9f) //19.5:9 Iphones - models X, Xs, Xr, Xsmax
        {
            //camera.GetComponent<Camera>().orthographicSize = 8.6f;
            canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }
        else if (screenRatio < 2.1f && screenRatio > 1.9f) //xiamoi mix2 1080x2160
        {
            camera.GetComponent<Camera>().orthographicSize = 8f;
            //canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}

