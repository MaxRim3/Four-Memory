using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject objectToDeactivate;

    public void deactivateObject()
    {
        objectToDeactivate.SetActive(false);
    }

}
