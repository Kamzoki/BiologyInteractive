using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    //private:
    float newXPosition;
    float newYPosition;
    float cameraDistance;

    private void Start()
    {
        cameraDistance = (Camera.main.transform.position - gameObject.transform.position).magnitude;
    }

    private void OnMouseDrag()
    {
        LabManager.LM.fn_CheckMouseClick();
        if (gameObject.name == LabManager.LM.m_CurrentSelectedObject.name)
        {
            newXPosition = Input.mousePosition.x;
            newYPosition = Input.mousePosition.y;
            Vector3 mousePosition = new Vector3(newXPosition, newYPosition, cameraDistance);
            Vector3 newObjPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            gameObject.transform.position = newObjPosition;
        }
    }
}
