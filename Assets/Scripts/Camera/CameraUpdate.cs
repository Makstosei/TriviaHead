using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraUpdate : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera FollowCamera, BadGuyCamera, BossCamera, BossDeathCamera, ScoreBoardCam, PlayerCryCam, SelectCam;

    private Cinemachine.CinemachineTransposer FollowCamTransposerRef;
    private Cinemachine.CinemachineComposer ComposerRef;

    public void OnEnable()
    {
        EventManager.onGameStart += GameStartedEvent;
    }
    public void OnDisable()
    {
        EventManager.onGameStart -= GameStartedEvent;

    }

    void GameStartedEvent()
    {
        FollowCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = -1.7f;
        //for angle
        // FollowCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine.CinemachineComposer>();
    }
}