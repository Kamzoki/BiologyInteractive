using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour {

    //public:
    public static ApplicationManager AM;
//    public List<Lab> m_Labs;
    public SceneData[] m_Scenes;

    [HideInInspector]
    public string m_PreviousScene = "";
    [HideInInspector]
    public string m_CurrentScene= "";
    [HideInInspector]
    public int m_CurrentScenesIndex = 0;
    [HideInInspector]
    public int m_SceneToLoadIndex = -1;

    // Use this for initialization
    void Awake () {
        if (AM == null)
        {
            DontDestroyOnLoad(gameObject);
            AM = this;
        }
        else if (AM != this)
        {
            Destroy(gameObject);
        }

        if (m_Scenes != null)
        {
            m_CurrentScene = m_Scenes[0].m_SceneName;
        }
	}

    public void fn_TransferSceneData(string newScene)
    {
        //Tested
        for (int i = 0; i < m_Scenes.Length; i++)
        {
            if (m_Scenes[i].m_SceneName == newScene)
            {
                m_PreviousScene = m_CurrentScene;
                m_CurrentScene = newScene;
                m_CurrentScenesIndex = i;
                m_SceneToLoadIndex =  m_Scenes[i].m_SceneIndex;
            }
        }
    }
}

[System.Serializable]
public struct SceneData
{
    public string m_SceneName;
    public int m_SceneIndex;
    public bool isLabScene;
    public ToolType[] m_RequiredTools;
    public Missions[] m_Missions;
}

[System.Serializable]
public struct Missions
{
    //TODO populate missions of the two lab scenes needed as Nehal's note. Implement a global index to missions, with each click on a lab tool, check if the lab tool type matches the next lab tool accroding to the global index.
    public string m_MissionName;
    public string m_MissionDescription;
    public ToolType[] m_NextNeededTool;
    public bool isDone;

}

public enum ToolType {Beaker, Bunsen_Burner, Dropper, Container_Sample, MircoScope_GlassSection, MircoScope, Mortar_Pestle, Scalple, TestingTubes_Rack, TestingTube,
                      Thermometer, Tongs, Iodine_Solution, Glucose_Solution, Starch_Solution, Egg_Yolk, Distilled_Water, Benedict_Reagent, Peas_Seeds_Container, Tomatoes_Container,
                      Wheat_Seeds_Container, Bread_Pieces_Container}