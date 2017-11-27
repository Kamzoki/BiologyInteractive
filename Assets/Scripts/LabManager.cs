using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public Animator m_MainCameraAnimator;
    public GameObject m_PrepareButton;
    public GameObject m_BackButton;
    public GameObject m_UseButton;
    public GameObject m_EmptyButton;
    public Text m_ToolText;
    public Text[] m_MissionsText;
    public Text[] m_RequiredToolsText;

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
        infoPanel = new GameObject();
        if (ApplicationManager.AM != null)
        {
            AssignTrays();
            AssignMissionsToText();
            if (m_PrepareButton == null || m_BackButton == null)
            {
                Debug.Log("Assign UI buttons to LabManager");
            }
        }
    }

    private void AssignMissionsToText()
    {
        if (m_MissionsText != null)
        {
            for (int i = 0; i < ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_Missions.Length; i++)
            {
                if (m_MissionsText[i+1].text == "")
                {
                    m_MissionsText[i+1].text = ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_Missions[i].m_MissionDescription;
                }
            }
        }

        if (m_RequiredToolsText != null)
        {
            for (int i = 0;  i< ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_RequiredTools.Length; i++)
            {
                if (m_RequiredToolsText[i].text == "")
                {
                    switch (ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].m_RequiredTools[i])
                    {
                        case ToolType.Bunsen_Burner:
                            m_RequiredToolsText[i].text = "ﺪﻗﻮﻣ";
                            break;
                        case ToolType.Dropper:
                            m_RequiredToolsText[i].text = "ﺓﺭﺎﻄﻗ";
                            break;
                        case ToolType.Container_Sample:
                            m_RequiredToolsText[i].text = "ﺔﻨﻴﻋ ﺀﺎﻋﻭ";
                            break;
                        case ToolType.MircoScope_GlassSection:
                            m_RequiredToolsText[i].text = "ﺔﻴﺟﺎﺟﺯ ﺔﺤﻳﺮﺷ";
                            break;
                        case ToolType.MircoScope:
                            m_RequiredToolsText[i].text = "ﺏﻮﻜﺳﻭﺮﻜﻴﻣ";
                            break;
                        case ToolType.Mortar_Pestle:
                            m_RequiredToolsText[i].text = "ﻥﻮﻫ";
                            break;
                        case ToolType.Scalple:
                            m_RequiredToolsText[i].text = "ﻁﺮﺸﻣ";
                            break;
                        case ToolType.TestingTubes_Rack:
                            m_RequiredToolsText[i].text = "ﺮﺒﺘﺨﻤﻟﺍ ﺐﻴﺑﺎﻧﺃ ﻞﻣﺎﺣ";
                            break;
                        case ToolType.Thermometer:
                            m_RequiredToolsText[i].text = "ﺮﺘﻣﻮﻣﺮﺗ";
                            break;
                        case ToolType.Tongs:
                            m_RequiredToolsText[i].text = "ﺮﺒﺘﺨﻤﻟﺍ ﺐﻴﺑﺎﻧﺃ ﻚﺳﺎﻣ";
                            break;
                        case ToolType.Iodine_Solution:
                            m_RequiredToolsText[i].text = "ﺩﻮﻴﻟﺍ ﻝﻮﻠﺤﻣ";
                            break;
                        case ToolType.Glucose_Solution:
                            m_RequiredToolsText[i].text = "ﺯﻮﻛﻮﻠﺠﻟﺍ ﻝﻮﻠﺤﻣ";
                            break;
                        case ToolType.Starch_Solution:
                            m_RequiredToolsText[i].text = "ﺎﺸﻨﻟﺍ ﻝﻮﻠﺤﻣ";
                            break;
                        case ToolType.Egg_Yolk:
                            m_RequiredToolsText[i].text = "ﺾﻴﺒﻟﺍ ﻝﻻﺯ";
                            break;
                        case ToolType.Distilled_Water:
                            m_RequiredToolsText[i].text = "ﺮﻄﻘﻣ ﺀﺎﻣ";
                            break;
                        case ToolType.Benedict_Reagent:
                            m_RequiredToolsText[i].text = "ﺖﻛﺪﻨﺑ ﻒﺷﺎﻛ";
                            break;
                        case ToolType.Peas_Seeds_Container:
                            m_RequiredToolsText[i].text = "ﻻﺯﺎﺒﻟﺍ ﺏﻮﺒﺣ ﺀﺎﻋﻭ";
                            break;
                        case ToolType.Tomatoes_Container:
                            m_RequiredToolsText[i].text = "ﻢﻃﺎﻤﻄﻟﺍ ﻑﺎﺼﻧﺃ ﺀﺎﻋﻭ";
                            break;
                        case ToolType.Wheat_Seeds_Container:
                            m_RequiredToolsText[i].text = "ﺢﻤﻘﻟﺍ ﺏﻮﺒﺣ ﺀﺎﻋﻭ";
                            break;
                        case ToolType.Bread_Pieces_Container:
                            m_RequiredToolsText[i].text = "ﺰﺒﺨﻟﺍ ﻊﻄﻗ ﺀﺎﻋﻭ";
                            break;
                        default:
                            break;
                    }
                }
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
                    infoPanel = GameObject.FindWithTag("Sugar Panel"); infoPanel.SetActive(true);
                    break;
                case "Detecting Starch":
                    preToolTray = GameObject.Find("CarbPreTray");
                    readyToolTray = GameObject.Find("CarbReadyTray");
                    infoPanel = GameObject.FindWithTag("Starch Panel"); infoPanel.SetActive(true);
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
        m_ToolText.text = "";
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
                m_MainCameraAnimator.SetBool("isLabReady", true);
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
        m_ToolText.text = m_CurrentSelectedTool.GetComponent<Tool>().m_ToolName;
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

    public void fn_UpdateMissionText(int index)
    {
        Color newColor = new Color(0, 255, 0);
        m_MissionsText[index].color = newColor;
    }

    public GameObject fn_GetInfoPanel()
    {
        return infoPanel;
    }
}

public enum LabState { UsingItem, EmptyingItem, UsingMicrosope, Idle};