using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElement : MonoBehaviour {
    //This class is attached to any button or interactable UI element that does something. This class provides all UI functionalities and properties.

    //public:
    public string m_SceneToLoad;

    public GameObject m_ActivationObject;
    public GameObject m_DisableObject;

    public Animation m_AnimationComponent;
    public string m_AnotherAnimation;
    public bool playMultiAnimations;

    //private:
    private bool isPressedBefore = false;

    public void fn_LoadScene(bool isRestarting)
    {
        //tested
        if (isRestarting == false)
        {
            ApplicationManager.AM.fn_TransferSceneData(m_SceneToLoad);
            if (ApplicationManager.AM.m_SceneToLoadIndex > -1)
            {
                SceneManager.LoadSceneAsync(ApplicationManager.AM.m_SceneToLoadIndex);
            }
            else
            {
                Debug.Log("Transition Error");
            }
        }
        else
        {
            SceneManager.LoadSceneAsync(ApplicationManager.AM.m_SceneToLoadIndex);
        }
    }
    public void fn_LoadObject(bool disableObject)
    {
        m_ActivationObject.SetActive(true);
        if (disableObject == true)
        {
            if (m_DisableObject == null)
            {
                m_DisableObject = gameObject;
            }
            m_DisableObject.SetActive(false);
        }
    }

    public void fn_StartAnimation(string AnimationName)
    {
        if (m_AnimationComponent != null)
        {
            if (playMultiAnimations == true)
            {
                if (isPressedBefore == false)
                {
                    m_AnimationComponent.Play(AnimationName);
                }
                else
                {
                    m_AnimationComponent.Play(m_AnotherAnimation);
                }
                isPressedBefore = !isPressedBefore;
            }
            else
            {
                m_AnimationComponent.Play(AnimationName);
            }
        }
    }

    public void CheckTools()
    {
        if (LabManager.LM != null)
        {
            if (LabManager.LM.fn_CheckReadyTools() == true)
            {
                LabManager.LM.isBeganPractice = true;
                Debug.Log("Let's Rock");
            }
        }
    }

    public void SwitchToolParent(bool isReadyTool)
    {
        GetComponentInParent<Tool>().fn_SwitchToolParent(isReadyTool);
    }
}
