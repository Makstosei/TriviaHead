using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveUIController : MonoBehaviour
{
    public GameObject health1, health2, health3;
    public Sprite Hearthon, Hearthoff;
    private void OnEnable()
    {
        EventManager.onHealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        EventManager.onHealthChanged -= HealthChanged;
    }

    void HealthChanged(int CurrentHealth)
    {

        switch (CurrentHealth)
        {
            case 3:
                health1.GetComponent<Image>().sprite = Hearthon;
                health2.GetComponent<Image>().sprite = Hearthon;
                health3.GetComponent<Image>().sprite = Hearthon;
                break;
            case 2:
                health1.GetComponent<Image>().sprite = Hearthon;
                health2.GetComponent<Image>().sprite = Hearthon;
                health3.GetComponent<Image>().sprite = Hearthoff;
                break;
            case 1:
                health1.GetComponent<Image>().sprite = Hearthon;
                health2.GetComponent<Image>().sprite = Hearthoff;
                health3.GetComponent<Image>().sprite = Hearthoff;
                break;
            case 0:
                health1.GetComponent<Image>().sprite = Hearthoff;
                health2.GetComponent<Image>().sprite = Hearthoff;
                health3.GetComponent<Image>().sprite = Hearthoff;
                break;
            default:
                break;
        }

    }

}
