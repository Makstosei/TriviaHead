using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public Material Skybox;
    float rotationvalue;


    void Update()
    {
        if (rotationvalue >= 360)
        {
            rotationvalue = 0;
        }
        else
        {
            rotationvalue = rotationvalue + 1 * Time.deltaTime;
        }
     
        Skybox.SetFloat("_Rotation",rotationvalue);
    }
}

