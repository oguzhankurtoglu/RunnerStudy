using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Script
{
    public enum GameState
    {
        Start,
        Running,
        Fail,
        BeforeFinish,
        Success
    }

    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState gameState = GameState.Start;
        public Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && gameState == GameState.Start)
            {
                gameState = GameState.Running;
            }

            if (gameState == GameState.Success)
            {
                mainCamera.GetComponent<CinemachineBrain>().enabled = false;
                mainCamera.transform.GetComponentInParent<CameraRotator>().RotateCamera();
            }
        }

        public void ReturnDefaultCamera()
        {
            
            gameState = GameState.Start;
           var cameraAngle = mainCamera.transform.parent.transform.localEulerAngles;
            DOTween.To(() => cameraAngle.y, y => cameraAngle.y = y, 0, 1).OnUpdate(() =>
            {
                mainCamera.transform.parent.transform.localEulerAngles =
                    new Vector3(cameraAngle.x, cameraAngle.y, cameraAngle.z);
            }).OnComplete(() => { mainCamera.GetComponent<CinemachineBrain>().enabled = true; });
        }
    }
}