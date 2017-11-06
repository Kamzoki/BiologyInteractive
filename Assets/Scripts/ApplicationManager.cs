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

    public int fn_TransferSceneData(string newScene)
    {
        //Tested
        for (int i = 0; i < m_Scenes.Length; i++)
        {
            if (m_Scenes[i].m_SceneName == newScene)
            {
                m_PreviousScene = m_CurrentScene;
                m_CurrentScene = newScene;
                m_CurrentScenesIndex = i;
                return m_Scenes[i].m_SceneIndex;
            }
        }
        return -1;
    }
}

/*[System.Serializable]
public struct Lab
{
    public string m_LabTitle;
    public string[] m_RequiredTools;
}*/

[System.Serializable]
public struct SceneData
{
    public string m_SceneName;
    public int m_SceneIndex;
    public bool isLabScene;
    public ToolType[] m_RequiredTools;
}

public enum ToolType {Beaker, Bunsen_Burner, Dropper, Container_Jar, Container_Sample, Container_Solution, MircoScope_GlassSection, MircoScope, Mortar_Pestle, Scalple, TestingTubes_Rack, TestingTube,
                      Thermometer, Tongs}