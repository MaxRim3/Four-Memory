using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    public GameObject objectToSetActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActiveObject()
    {
        objectToSetActive.SetActive(true);
    }
}
