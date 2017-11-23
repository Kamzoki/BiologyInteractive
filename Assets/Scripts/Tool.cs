using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    //private:
    Vector3 originalPosition;
    bool isFull = false;

    //public:
    public ToolType m_ToolType = ToolType.Beaker;

    public GameObject m_Content;
    public Transform m_ContentPosition;

    public GameObject[] m_ChildTools;
    public float m_BunsenBurner_Timer = 10;

    [HideInInspector]
    public bool isPrepared = false;
    [HideInInspector]
    public float m_ToolTempreture = 0;

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
            isPrepared = true;
        }
        else
        {
            gameObject.transform.position = originalPosition;
            LabManager.LM.m_ReadyTools.Remove(gameObject);
            isPrepared = false;
        }
    }

    private void OnMouseUp()
    {
        if (LabManager.LM != null)
        {
            if (LabManager.LM.m_LabState == LabState.Idle)
            {
                LabManager.LM.fn_SelectTool(gameObject, isPrepared);
                Debug.Log(m_ToolType);
            }
            else
            {
                Mission thisMission = ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].
                    m_Missions[ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].LastMissionIndex];

                if (LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().m_ToolType == thisMission.m_CurrentNeededTool
                    && m_ToolType == thisMission.m_NextNeededTool 
                    && LabManager.LM.m_LabState == thisMission.m_ExpectedAction)
                {
                    Debug.Log("Applied Action");
                    switch (m_ToolType)
                    {
                        case ToolType.Beaker:
                            break;
                        case ToolType.Bunsen_Burner:
                            break;
                        case ToolType.Dropper:
                            break;
                        case ToolType.Container_Sample:
                            break;
                        case ToolType.MircoScope_GlassSection:
                            break;
                        case ToolType.MircoScope:
                            break;
                        case ToolType.Mortar_Pestle:
                            break;
                        case ToolType.Scalple:
                            break;
                        case ToolType.TestingTubes_Rack:
                            break;
                        case ToolType.TestingTube:
                            break;
                        case ToolType.Thermometer:
                            break;
                        case ToolType.Tongs:
                            break;
                        case ToolType.Iodine_Solution:
                            break;
                        case ToolType.Glucose_Solution:
                            break;
                        case ToolType.Starch_Solution:
                            break;
                        case ToolType.Egg_Yolk:
                            break;
                        case ToolType.Distilled_Water:
                            break;
                        case ToolType.Benedict_Reagent:
                            break;
                        case ToolType.Peas_Seeds_Container:
                            break;
                        case ToolType.Tomatoes_Container:
                            break;
                        case ToolType.Wheat_Seeds_Container:
                            break;
                        case ToolType.Bread_Pieces_Container:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Debug.Log("Wrong step");
                }
            }
        }
    }

    public void fn_ClearContent()
    {
        GameObject.Destroy(m_Content.gameObject);
        m_Content = null;
        isFull = false;
    }
    public IEnumerator fn_Bunsen_Burner(LabState LS)
    {
        if (LS == LabState.UsingItem)
        {
            m_Content.SetActive(true);
            LabManager.LM.m_LabState = LabState.Idle;
            LabManager.LM.fn_ResetCurrentSelectedTool();

            yield  return new WaitForSeconds (m_BunsenBurner_Timer);
            if (m_ChildTools != null)
            {
                m_ChildTools[0].GetComponent<Tool>().m_ToolTempreture = 10;
            }
            else
            {
                Debug.Log("Beaker not a child to bunsen burner"); 
            }
        }
        else
        {
            Debug.Log("Action Can't be applied to this tool"); // TODO Change to dynamic on screen arabic text
        }
    }

    public void fn_Dropper(LabState LS, Tool OtherTool)
    {
        if (LS == LabState.UsingItem)
        {
            if (isFull == false)
            {
                if (OtherTool.m_ToolType == ToolType.Distilled_Water || OtherTool.m_ToolType == ToolType.Egg_Yolk || OtherTool.m_ToolType == ToolType.Glucose_Solution ||
                OtherTool.m_ToolType == ToolType.Iodine_Solution || OtherTool.m_ToolType == ToolType.Starch_Solution || OtherTool.m_ToolType == ToolType.Benedict_Reagent)
                {
                    GameObject newContent = Instantiate(OtherTool.m_Content, m_ContentPosition.position, transform.rotation, transform);
                    m_Content = newContent;
                    isFull = true;
                    LabManager.LM.m_LabState = LabState.Idle;
                    LabManager.LM.fn_ResetCurrentSelectedTool();
                }
                else
                {
                    Debug.Log("Can't Use dropper with this tool"); // TODO Change to dynamic on screen arabic text
                }
            }
            else
            {
                Debug.Log("Dropper is full, empty it first"); // TODO Change to dynamic on screen arabic text
            }
        }

        else if (LS == LabState.EmptyingItem)
        {
            if (isFull == true)
            {
                if (OtherTool.m_ToolType == ToolType.Container_Sample || OtherTool.m_ToolType == ToolType.MircoScope_GlassSection || OtherTool.m_ToolType == ToolType.Mortar_Pestle
                    || OtherTool.m_ToolType == ToolType.TestingTube)
                {
                    GameObject DropedContent = Instantiate(m_Content, OtherTool.m_ContentPosition.position, OtherTool.gameObject.transform.rotation, OtherTool.gameObject.transform);
                    switch (OtherTool.m_Content.tag)
                    {
                        case "Glucose Solution":
                            if (m_Content.tag == "Bendict Reagent")
                            {
                                // TODO CHANGE COLOR FROM BLUE TO ORANGE
                            }
                            break;

                        case "Bread":
                            if (m_Content.tag == "Iodine Solution")
                            {
                                // TODO CHANGE COLOR FROM ORANGE TO DARK BLUE
                            }
                            break;

                        case "Wheat Seeds":
                            if (m_Content.tag == "Iodine Solution")
                            {
                                // TODO CHANGE COLOR FROM ORANGE TO DARK BLUE
                            }
                            break;

                        case "Peas Seeds":
                            if (m_Content.tag == "Iodine Solution")
                            {
                                // TODO CHANGE COLOR FROM ORANGE TO LIGHT BLUE
                            }
                            break;

                        default: Debug.Log("No specified behavior");
                            break;
                    }

                    fn_ClearContent();
                    LabManager.LM.m_LabState = LabState.Idle;
                    LabManager.LM.fn_ResetCurrentSelectedTool();
                }
                else
                {
                    Debug.Log("Can't Empty Dropper in this tool"); // TODO Change to dynamic on screen arabic text
                }
            }
            else
            {
                Debug.Log("Dropper has no content"); // TODO Change to dynamic on screen arabic text
            }
        }
    }

}
