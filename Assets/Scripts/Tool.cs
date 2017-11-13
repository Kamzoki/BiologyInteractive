using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    //private:
    float originalZ;
    //public:
    public ToolType m_ToolType = ToolType.Beaker;

    //public GameObject m_ToolCanvas;

    private void Start()
    {
        originalZ = gameObject.transform.position.z;
    }

    public void fn_SwitchToolParent(bool isReadyTool)
    {
        gameObject.transform.parent = LabManager.LM.fn_GetTray(isReadyTool).transform;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, originalZ);
    }

    private void OnMouseUp()
    {
        if (LabManager.LM != null)
        {
            LabManager.LM.fn_SelectTool(gameObject);
        }
    }
}
