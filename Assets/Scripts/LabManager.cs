using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{

    //private:
    RaycastHit info;
    Ray cameraRay;
    private GameObject preToolTray;
    private GameObject readyToolTray;
    
    //public:
    public static LabManager LM;

    public GameObject SelectedEffect;
    [HideInInspector]
    public bool isBeganPractice = false;
    [HideInInspector]
    public List<GameObject> m_ReadyTools;
    [HideInInspector]
    public GameObject m_CurrentSelectedTool;

    private void Awake()
    {
        LM = this;
        m_ReadyTools = new List<GameObject>();
        if (ApplicationManager.AM != null)
        {
            AssignTrays();
        }
    }

    private void Update()
    {
        if (m_CurrentSelectedTool != null)
        {
            CheckMouseClick();
        }
    }

    private void AssignTrays()
    {
        //Tested
        if (ApplicationManager.AM.m_CurrentScene != "")
        {
            switch (ApplicationManager.AM.m_CurrentScene)
            {
                case "Carbohydrates": preToolTray = GameObject.Find("CarbPreTray"); readyToolTray = GameObject.Find("CarbReadyTray"); break;

                default: Debug.Log("NothingFound"); break;
            }
        }
    }

    private void ResetCurrentSelectedTool()
    {
        m_CurrentSelectedTool = null;
        Debug.Log("Null");
        if (SelectedEffect != null)
        {
            SelectedEffect.SetActive(false);
        }
        else
        {
            Debug.Log("Please Assign selected effect");
        }
    }

    private void CheckMouseClick()
    {
        //This function checks if the raycast fired from the mouse hit an object tagged tool or not.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("ChecMouseClick");
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);            
            if (Physics.Raycast(cameraRay, out info))
            {
                Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
                if (info.collider.gameObject.tag != "Tool")
                {
                    Debug.Log("No tool found");
                    ResetCurrentSelectedTool();
                }
            }
        }
    }

    public bool fn_CheckReadyTools()
    {
        int successCounter = 0;
        if (m_ReadyTools.Capacity == ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_RequiredTools.Length)
        {
            for (int i = 0; i < m_ReadyTools.Capacity; i++)
            {
                for (int j = 0; j < m_ReadyTools.Capacity; j++)
                {
                    if (m_ReadyTools[i].GetComponent<Tool>().m_ToolType == ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_RequiredTools[j])
                    {
                        successCounter++;
                        break;
                    }
                }
            }

            if (successCounter == m_ReadyTools.Capacity)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    public GameObject fn_GetTray(bool isReady)
    {
        if (isReady == true)
        {
            return readyToolTray;
        }
        else
        {
            return preToolTray;
        }
    }

    public void fn_SelectTool(GameObject tool)
    {
        m_CurrentSelectedTool = tool;
        if (SelectedEffect != null)
        {
            SelectedEffect.transform.parent = m_CurrentSelectedTool.transform;
            SelectedEffect.transform.position = new Vector3(0, 0, 0);
            SelectedEffect.SetActive(true);
        }
        else
        {
            Debug.Log("Please Assign selected effect");
        }
    }
   
    
    
    
    
    
    
    
    
    
    
    
    
    /*
    private void CopyRequiredTools()
    {
        //This function reformate the requried tools for each lab and assign the correct requried tools from the local database in ApplicationManager.
        m_RequiredTools = new string[ApplicationManager.AM.m_Labs[ApplicationManager.AM.m_CurrentLabIndex].m_RequiredTools.Length];
        for (int i = 0; i < m_RequiredTools.Length; i++)
        {
            m_RequiredTools[i] = ApplicationManager.AM.m_Labs[ApplicationManager.AM.m_CurrentLabIndex].m_RequiredTools[i];
        }
    }*/
}
