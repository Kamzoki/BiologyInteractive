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

    //TODO Test undocumented functions
    public void fn_LoadScene()
    {
       int index = ApplicationManager.AM.fn_TransferSceneData(m_SceneToLoad);
        if (index > -1)
        {
            SceneManager.LoadSceneAsync(index);
        }
        else 
        {
            Debug.Log("Transition Error");
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
                isPressedBefore = !isPressedBefore;
                if (isPressedBefore == false)
                {
                    m_AnimationComponent.Play(AnimationName);
                }
                else
                {
                    m_AnimationComponent.Play(m_AnotherAnimation);
                }
            }
            else
            {
                m_AnimationComponent.Play(AnimationName);
            }
        }
    }
}
