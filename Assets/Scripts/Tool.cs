using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    //private:
    Vector3 originalPosition;
    //public:
    public ToolType m_ToolType = ToolType.Beaker;


    private void Start()
    {
        originalPosition = gameObject.transform.position;
    }

    public void fn_SwitchToolParent(bool isReadyTool)
    {
        gameObject.transform.parent = LabManager.LM.fn_GetTray(isReadyTool).transform;
        if (isReadyTool == true)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z - LabManager.LM.fn_GetTray(!isReadyTool).transform.localScale.z) + (LabManager.LM.fn_GetTray(!isReadyTool).transform.position.z + 0.5f) );
            LabManager.LM.m_ReadyTools.Add(gameObject);
        }
        else
        {
            gameObject.transform.position = originalPosition;
            LabManager.LM.m_ReadyTools.Remove(gameObject);
        }
        Debug.Log(LabManager.LM.m_ReadyTools.Count);
    }

    private void OnMouseUp()
    {
        if (LabManager.LM != null)
        {
            LabManager.LM.fn_SelectTool(gameObject);
        }
    }
}
