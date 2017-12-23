using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSceneManager : MonoBehaviour {

    public static HumanSceneManager HSM;
    public HumanSubObjects[] m_HumanSubObjects;

    private void Awake()
    {
        HSM = this;
    }

}

[System.Serializable]
public class HumanSubObjects
{
    public string name;
    public GameObject[] m_GroupSubObjects;
    public Material [] m_MainMaterial;
    public Material [] m_FadeMaterial;

    private bool isFaded = false;

    public void fn_MainToFade()
    {
        if (isFaded == false)
        {
            for (int i = 0; i < m_GroupSubObjects.Length; i++)
            {
                if (m_GroupSubObjects[i].name == "Body3:body2")
                {
                    Debug.Log("Here");
                    m_GroupSubObjects[i].SetActive(false);
                }
                else
                {
                    Debug.Log("shit" + m_GroupSubObjects[i].name);
                    for (int j = 0; j < m_GroupSubObjects[i].GetComponent<MeshRenderer>().materials.Length; j++)
                    {
                        m_GroupSubObjects[i].GetComponent<MeshRenderer>().material = m_FadeMaterial[j];
                    }
                }
            }
        }
    }

    public void fn_FadeToMain()
    {
        if (isFaded == true)
        {
            for (int i = 0; i < m_GroupSubObjects.Length; i++)
            {
                if (m_GroupSubObjects[i].name == "Body3:body2")
                {
                    m_GroupSubObjects[i].SetActive(true);
                }
                else
                {
                    for (int j = 0; j < m_GroupSubObjects[i].GetComponent<MeshRenderer>().materials.Length; j++)
                    {
                        m_GroupSubObjects[i].GetComponent<MeshRenderer>().material = m_MainMaterial[j];
                    }
                }
            }
        }
    }

    public void fn_SetisFaded(bool isFaded)
    {
        this.isFaded = isFaded;
    }
}