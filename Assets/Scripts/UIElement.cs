using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElement : MonoBehaviour {
    //This class is attached to any button or interactable UI element that does something. This class provides all UI functionalities and properties.

    //public:
    public string m_SceneToLoad;

    public GameObject[] m_ActivationObjects;
    public GameObject[] m_DisableObjects;

    public Animation m_AnimationComponent;
    public string m_AnotherAnimation;
    public bool playMultiAnimations;

    public enum Functions { LoadLabScene, LoadObject, StartAnimation};
    public Functions m_FunctionToCall = Functions.LoadObject;
    public bool isTrueParameter;
    public GameObject[] OtherScript;

    //private:
    private bool isPressedBefore = false;
    private bool isCalledExternally = false;
    private UIElement externalScript;

    public void fn_LoadLabScene(bool isRestarting)
    {
        //tested
        if (isCalledExternally == false)
        {
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
        else
        {
            externalScript.fn_LoadLabScene(isRestarting);
        }
    }
    public void fn_LoadObject(bool disableObject)
    {
        if (isCalledExternally == false)
        {
            if (m_ActivationObjects != null)
            {
                for (int i = 0; i < m_ActivationObjects.Length; i++)
                {
                    m_ActivationObjects[i].SetActive(true);
                }
            }
            if (disableObject == true)
            {
                if (m_DisableObjects != null)
                {
                    for (int i = 0; i < m_DisableObjects.Length; i++)
                    {
                        m_DisableObjects[i].SetActive(false);
                    }
                }

            }
        }
        else
        {
            externalScript.fn_LoadObject(disableObject);
        }
    }

    public void fn_StartAnimation(string AnimationName)
    {
        if (isCalledExternally == false)
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
        else
        {
            externalScript.fn_StartAnimation(AnimationName);
        }
    }

    public void fn_CheckTools()
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

    public void fn_SwitchToolParent(bool isReadyTool)
    {
        if (LabManager.LM != null)
        {
            if (LabManager.LM.m_CurrentSelectedTool != null)
            {
                
                LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().fn_SwitchToolParent(isReadyTool);
                m_ActivationObjects[0].SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void fn_CallOtherUIFunction (int OtherScriptIndex) {

        isCalledExternally = true;
        externalScript = OtherScript[OtherScriptIndex].GetComponent<UIElement>();

        switch (m_FunctionToCall)
        {
            case Functions.LoadLabScene: fn_LoadLabScene(isTrueParameter);
                break;
            case Functions.LoadObject: fn_LoadObject(isTrueParameter);
                break;
            case Functions.StartAnimation: fn_StartAnimation(m_AnotherAnimation);
                break;
            default: Debug.Log("Error enum");
                break;
        }
    }

    public void fn_UseItem_EmptyItem(bool isUseItem)
    {
        if (LabManager.LM != null)
        {
            if (isUseItem == true)
            {
                LabManager.LM.m_LabState = LabState.UsingItem;
            }
            else
            {
                LabManager.LM.m_LabState = LabState.EmptyingItem;
            }
        }
        else
        {
            Debug.Log("LabManager is null");
        }
    }
}
