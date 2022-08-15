using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;


public static class NiceVibrationsManager
{
    private static void Test()
    {
        Handheld.Vibrate();
    }
    public static void Selection()
    {
        MMVibrationManager.Haptic(HapticTypes.Selection);
    }
    public static void LightImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
    }
    public static void MediumImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }
    public static void HeavyImpact()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }
}

