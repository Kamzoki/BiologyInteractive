﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    
    public enum Functions { fn_LoadLabScene, fn_LoadObject, fn_StartAnimation, fn_CheckTools, fn_SwitchToolParent, fn_UseItem_EmptyItem, fn_ExitApplication, fn_ToggelActivationObject, ResetScrollRect, CallOutterUIFunction};
    [System.Serializable]
    public struct FunctionsEntity
    {
        public bool boolParameter;
        public string stringParameter;
        public GameObject gameObjectParameter;
        public Functions outterFunctionName;
        public Functions functionName;
    }

    public FunctionsEntity[] FunctionsToCall;
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

    private void ResetScrollRect(GameObject ScrollRect)
    {
        ScrollRect.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }

    private void CallOutterUIFunction (Functions function, GameObject outerObject, bool boolParameter, string stringParameter)
    {
        switch (function)
        {
            case Functions.fn_LoadLabScene: outerObject.GetComponent<UIElement>().fn_LoadLabScene(boolParameter);
                break;
            case Functions.fn_LoadObject:
                outerObject.GetComponent<UIElement>().fn_LoadObject(boolParameter);
                break;
            case Functions.fn_StartAnimation:
                outerObject.GetComponent<UIElement>().fn_StartAnimation(stringParameter);
                break;
            default: Debug.Log("No function Specified");
                break;
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

    public void fn_ToggelActivationObject(bool toggelisPressedBefore)
    {
        if (toggelisPressedBefore == true)
        {
            isPressedBefore = !isPressedBefore;
        }

        if (isPressedBefore == true)
        {
            for (int i = 0; i < m_ActivationObjects.Length; i++)
            {
                m_ActivationObjects[i].SetActive(true);
            }
        }

        else
        {
            for (int i = 0; i < m_ActivationObjects.Length; i++)
            {
                m_ActivationObjects[i].SetActive(false);
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
                LabManager.LM.m_ToolText.text = "ﺔﺑﺮﺠﺘﻟﺍ ﻲﻓ ﺀﺪﺒﻟﺍ ﻚﻨﻜﻤﻳ ﻥﻷﺍ !ﺖﻨﺴﺣﺃ";
            }
            else
            {
                LabManager.LM.m_ToolText.text = "ﺊﻃﺎﺧ ﺮﻴﻀﺤﺗ";
            }
        }
    }

    public void fn_SwitchToolParent(bool isReadyTool)
    {
        if (LabManager.LM != null)
        {
            if (LabManager.LM.m_CurrentSelectedTool != null && LabManager.LM.isBeganPractice == false)
            {
                if (LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().m_ToolType != ToolType.Beaker)
                {
                    LabManager.LM.m_ToolText.text = "";
                    LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().fn_SwitchToolParent(isReadyTool);
                    m_ActivationObjects[0].SetActive(true);
                    gameObject.SetActive(false);
                }
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
                            if (ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].
                                m_Missions[ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].LastMissionIndex].m_CurrentNeededTool == ToolType.Mortar_Pestle)
                            {
                            LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().fn_Mortar_Pestle(LabManager.LM.m_LabState, null);
                            }
                        else
                        {
                            LabManager.LM.m_ToolText.text = "ﺔﺌﻃﺎﺧ ﺓﻮﻄﺧ";
                        }
                        }
                        else if (LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().m_ToolType == ToolType.Bunsen_Burner)
                        {
                            if (ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].
                                m_Missions[ApplicationManager.AM.m_Scenes[ApplicationManager.AM.m_CurrentScenesIndex].LastMissionIndex].m_CurrentNeededTool == ToolType.Bunsen_Burner)
                        {
                            StartCoroutine(LabManager.LM.m_CurrentSelectedTool.GetComponent<Tool>().fn_Bunsen_Burner(LabManager.LM.m_LabState));
                        }
                        else
                        {
                            LabManager.LM.m_ToolText.text = "ﺔﺌﻃﺎﺧ ﺓﻮﻄﺧ";
                        }
                            
                        }
                    }
                    else
                    {
                        LabManager.LM.m_LabState = LabState.EmptyingItem;
                    }
            }
            else
            {
                LabManager.LM.m_ToolText.text = "ﻻﻭﺃ ﺮﻴﻀﺤﺘﻟﺍ ﺔﻠﺣﺮﻣ ﻦﻣ ﺀﺎﻬﺘﻧﻻﺍ ﻚﻴﻠﻋ";
            }
        }
        else
        {
            Debug.Log("LabManager is null");
        }
    }

    public void fn_ExitApplication()
    {
        Application.Quit();
    }

    public void fn_CallMultipleFuncitons()
    {
        if (FunctionsToCall != null)
        {
            for (int i = 0; i < FunctionsToCall.Length; i++)
            {
                switch (FunctionsToCall[i].functionName)
                {
                    case Functions.fn_LoadLabScene: fn_LoadLabScene(FunctionsToCall[i].boolParameter);
                        break;
                    case Functions.fn_LoadObject: fn_LoadObject(FunctionsToCall[i].boolParameter);
                        break;
                    case Functions.fn_StartAnimation: fn_StartAnimation(FunctionsToCall[i].stringParameter);
                        break;
                    case Functions.fn_CheckTools: fn_CheckTools();
                        break;
                    case Functions.fn_SwitchToolParent: fn_SwitchToolParent(FunctionsToCall[i].boolParameter);
                        break;
                    case Functions.fn_UseItem_EmptyItem: fn_UseItem_EmptyItem(FunctionsToCall[i].boolParameter);
                        break;
                    case Functions.fn_ExitApplication:fn_ExitApplication();
                        break;
                    case Functions.fn_ToggelActivationObject: fn_ToggelActivationObject(FunctionsToCall[i].boolParameter);
                        break;
                    case Functions.ResetScrollRect: ResetScrollRect(FunctionsToCall[i].gameObjectParameter);
                        break;
                    case Functions.CallOutterUIFunction: CallOutterUIFunction(FunctionsToCall[i].outterFunctionName, FunctionsToCall[i].gameObjectParameter, FunctionsToCall[i].boolParameter, FunctionsToCall[i].stringParameter);
                        break;
                    default: Debug.Log("No function with this name");
                        break;
                }
            }
        }
    }



}
