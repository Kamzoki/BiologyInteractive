﻿using System.Collections;
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
    public bool resetAnimations = false;

    //private:
    private bool isPressedBefore = false;


    private void OnEnable()
    {
        if (resetAnimations == true)
        {
            m_AnimationComponent.Play("Carbs module Reseting animation");
           /* m_AnimationComponent[""].enabled = false;
            m_AnimationComponent[""].enabled = false;*/
        }
    }
    public void fn_LoadLabScene(bool isRestarting)
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

            if (m_ActivationObjects != null)
            {
                m_ActivationObjects[0].SetActive(true);
            }
    }
    public void fn_LoadObject(bool disableObject)
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

    public void fn_StartAnimation(string AnimationName)
    {
        {
            if (m_AnimationComponent != null)
            {
                m_AnimationComponent.enabled = true;
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
            else
            {
                Debug.Log("wrong prepration");
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

    public void fn_UseItem_EmptyItem(bool isUseItem)
    {
        if (LabManager.LM != null)
        {
            if (LabManager.LM.isBeganPractice == true)
            {
                if (isUseItem == true)
                {
                    LabManager.LM.m_LabState = LabState.UsingItem;
                    if (LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().m_ToolType == ToolType.Mortar_Pestle)
                    {
                        LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().fn_Mortar_Pestle(LabManager.LM.m_LabState, null);
                    }
                    else if (LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().m_ToolType == ToolType.Bunsen_Burner)
                    {
                        LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().fn_Bunsen_Burner(LabManager.LM.m_LabState);
                    }
                }
                else
                {
                    LabManager.LM.m_LabState = LabState.EmptyingItem;
                }
            }
            else
            {
                Debug.Log("Wrong preparing");
            }
        }
        else
        {
            Debug.Log("LabManager is null");
        }
    }
}
