using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    //private:
    /*float newXPosition;
    float newYPosition;
    float cameraDistance;
    */

    //public:
    public ToolType m_ToolType = ToolType.Beaker;

    private void Start()
    {
       // cameraDistance = (Camera.main.transform.position - gameObject.transform.position).magnitude;
    }

    public void fn_SwitchToolParent(bool isReadyTool)
    {
        float originalZ = gameObject.transform.position.z;
        gameObject.transform.parent = LabManager.LM.fn_GetTray(isReadyTool).transform;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, originalZ);
    }
    
    //TODO Establish button for switching tools between trays, test the functionality of tools switching and checktools.
    
    
    
    
    
    
    
    
    
    
    /*private void OnMouseDrag()
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
    }*/
}
