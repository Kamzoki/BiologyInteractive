using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{

    //private:
    RaycastHit info;
    Ray cameraRay;

    //public:
    public static LabManager LM;
    [HideInInspector]
    public GameObject currentSelectedObject;

    private void Start()
    {
        LM = this;
    }
    // Update is called once per frame
    /*   void Update()
       {
           CheckMouseClick();
       }
       */
    public void CheckMouseClick()
    {
        //This function checks if the raycast fired from the mouse hit an object tagged tool or not.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out info))
            {
                if (info.collider.gameObject.tag == "Tool")
                {
                    currentSelectedObject = info.collider.gameObject;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            currentSelectedObject = null;
        }
    }
}
