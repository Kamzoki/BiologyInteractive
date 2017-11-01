using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour {

    //public:
    public static ApplicationManager AM;
    //TODO populate this list.
    public List<Lab> m_Labs;
    public SceneData[] m_Scenes;

    [HideInInspector]
    public string m_PreviousScene;
    [HideInInspector]
    public string m_CurrentScene;

    [HideInInspector]
    public int m_CurrentLabIndex = 0;

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
        for (int i = 0; i < m_Scenes.Length; i++)
        {
            if (m_Scenes[i].m_SceneName == newScene)
            {
                m_PreviousScene = m_CurrentScene;
                m_CurrentScene = newScene;
                return m_Scenes[i].m_SceneIndex;
            }
        }
        return -1;
    }
}

[System.Serializable]
public struct Lab
{
    public string m_LabTitle;
    public string[] m_RequiredTools;
}

[System.Serializable]
public struct SceneData
{
    public string m_SceneName;
    public int m_SceneIndex;
}