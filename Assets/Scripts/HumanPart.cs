using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPart : MonoBehaviour {

    public GameObject[] m_ActiavtionObjects;
    public Animation m_AnimationComponent;
    public string m_AnimationName;

    private void ActivateObjects()
    {
        for (int i = 0; i < m_ActiavtionObjects.Length; i++)
        {
            m_ActiavtionObjects[i].SetActive(true);
        }
    }

    private void PlayAnimation()
    {
        m_AnimationComponent.Play(m_AnimationName);
    }
    private void OnMouseUp()
    {
        ActivateObjects();
        PlayAnimation();
    }
}
