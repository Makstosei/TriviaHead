using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMaterialController : MonoBehaviour
{
    AnswerManager answerManager;
    public Material Abackground, Bbackground, Cbackground,bodyMaterial;

    private void OnEnable()
    {
        EventManager.onAnswerChanged += AnswerChangedEvent;
    }
    private void OnDisable()
    {
        EventManager.onAnswerChanged -= AnswerChangedEvent;
    }
    private void Start()
    {
        answerManager = FindObjectOfType<AnswerManager>();
        AnswerChangedEvent();
    }

    

    void AnswerChangedEvent()
    {
        switch (answerManager.currentAnswerid)
        {
            case 0:
                bodyMaterial.CopyPropertiesFromMaterial(Abackground);
                bodyMaterial.shader = Abackground.shader;
                break;
            case 1:
                bodyMaterial.CopyPropertiesFromMaterial(Bbackground);
                bodyMaterial.shader = Bbackground.shader;
                break;
            case 2:
                bodyMaterial.CopyPropertiesFromMaterial(Cbackground);
                bodyMaterial.shader = Cbackground.shader;
                break;

        }
    }
    

}

