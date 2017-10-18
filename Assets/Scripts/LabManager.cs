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
    public GameObject m_CurrentSelectedObject;
    [HideInInspector]
    public string[] m_RequiredTools;

    private void Start()
    {
        LM = this;
        if (ApplicationManager.AM.m_Labs != null)
        {
            CopyRequiredTools();
        }
    }

    public void fn_CheckMouseClick()
    {
        //This function checks if the raycast fired from the mouse hit an object tagged tool or not.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out info))
            {
                if (info.collider.gameObject.tag == "Tool")
                {
                    m_CurrentSelectedObject = info.collider.gameObject;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_CurrentSelectedObject = null;
        }
    }

    private void CopyRequiredTools()
    {
        //TODO test the function.
        //This function reformate the requried tools for each lab and assign the correct requried tools from the local database in ApplicationManager.
        m_RequiredTools = new string[ApplicationManager.AM.m_Labs[ApplicationManager.AM.m_CurrentLabIndex].m_RequiredTools.Length];
        for (int i = 0; i < m_RequiredTools.Length; i++)
        {
            m_RequiredTools[i] = ApplicationManager.AM.m_Labs[ApplicationManager.AM.m_CurrentLabIndex].m_RequiredTools[i];
        }
    }
}
