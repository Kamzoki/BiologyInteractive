using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

    //private:
    Vector3 originalPosition;
    //public:
    public ToolType m_ToolType = ToolType.Beaker;
    [HideInInspector]
    public bool isPrepared = false;

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
                Debug.Log(m_ToolType);
                LabManager.LM.fn_SelectTool(gameObject, isPrepared);
            }
            else
            {
                Mission thisMission = ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].
                    m_Missions[ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].LastMissionIndex];

                if (LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().m_ToolType == thisMission.m_CurrentNeededTool
                    && m_ToolType == thisMission.m_NextNeededTool 
                    && LabManager.LM.m_LabState == thisMission.m_ExpectedAction)
                {
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
}
