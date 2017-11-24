using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabManager : MonoBehaviour
{

    //private:
    RaycastHit info;
    Ray cameraRay;
    private GameObject preToolTray;
    private GameObject readyToolTray;
    private GameObject infoPanel;
    //public:
    public static LabManager LM;
    public LabState m_LabState = LabState.Idle;

    public GameObject m_SelectedEffect;

    //Lab UI Dynamic Elements.
    public GameObject m_PrepareButton;
    public GameObject m_BackButton;
    public GameObject m_UseButton;
    public GameObject m_EmptyButton;

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
            if (m_PrepareButton == null || m_BackButton == null)
            {
                Debug.Log("Assign UI buttons to LabManager");
            }
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
                case "Detecting Sugar":
                    preToolTray = GameObject.Find("CarbPreTray");
                    readyToolTray = GameObject.Find("CarbReadyTray");
                    infoPanel = GameObject.Find("Sugar Panel"); infoPanel.SetActive(true);
                    break;
                case "Detecting Starch":
                    preToolTray = GameObject.Find("CarbPreTray");
                    readyToolTray = GameObject.Find("CarbReadyTray");
                    infoPanel = GameObject.Find("Starch Panel"); infoPanel.SetActive(true);
                    break;

                    break;

                default: Debug.Log("NothingFound"); break;
            }
        }
    }

    private void CheckMouseClick()
    {
        //This function checks if the raycast fired from the mouse hit an object tagged tool or not.
        if (Input.GetKey(KeyCode.Mouse0))
        {
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(cameraRay.origin, cameraRay.direction * 10, Color.red);
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                if (Physics.Raycast(cameraRay, 10f) == false)
                {
                    fn_ResetCurrentSelectedTool();
                }
            }
        }
    }

    private void SetPrepareBtns(bool isPrepared)
    {
        if (isPrepared == true)
        {
            m_BackButton.SetActive(true);
            m_PrepareButton.SetActive(false);
        }
        else
        {
            m_PrepareButton.SetActive(true);
            m_BackButton.SetActive(false);
        }
    }

    public void fn_ResetCurrentSelectedTool()
    {
        m_CurrentSelectedTool = null;
        m_PrepareButton.SetActive(true);
        m_BackButton.SetActive(false);
        if (m_SelectedEffect != null)
        {
            m_SelectedEffect.SetActive(false);
        }
        else
        {
            Debug.Log("No Selected Effect found");
        }
    }

    public bool fn_CheckReadyTools()
    {

        int successCounter = 0;
        if (m_ReadyTools.Count == ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_RequiredTools.Length)
        {
            Debug.Log("CheckTools working");
            for (int i = 0; i < m_ReadyTools.Count; i++)
            {
                for (int j = 0; j < m_ReadyTools.Count; j++)
                {
                    if (m_ReadyTools[i].GetComponent<Tool>().m_ToolType == ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_RequiredTools[j])
                    {
                        successCounter++;
                        break;
                    }
                }
            }

            if (successCounter == m_ReadyTools.Count)
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

    public void fn_SelectTool(GameObject tool, bool isPrepared)
    {
        fn_ResetCurrentSelectedTool();
        m_CurrentSelectedTool = tool;
        SetPrepareBtns(isPrepared);
        if (m_SelectedEffect != null)
        {
            m_SelectedEffect.transform.parent = m_CurrentSelectedTool.transform;
            m_SelectedEffect.transform.position = new Vector3(0, 0, 0);
            m_SelectedEffect.SetActive(true);
        }
        else
        {
            Debug.Log("Please Assign selected effect");
        }
    }
}

public enum LabState { UsingItem, EmptyingItem, UsingMicrosope, Idle};