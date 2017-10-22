using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour {

    //public:
    public static ApplicationManager AM;
    //TODO populate this list.
    public List<Lab> m_Labs;
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public struct Lab
{
    public string m_LabTitle;
    public string[] m_RequiredTools;
}