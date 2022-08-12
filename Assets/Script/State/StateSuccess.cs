using System.Collections;
using Cinemachine;
using Script.Manager;
using UnityEngine;

namespace Script.State
{
    public class StateSuccess : State
    {
        public StateSuccess(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.player.Animator.SetTrigger("Dance");
            GameManager.player.confettie.Play();
            GameManager.mainCamera.GetComponent<CinemachineBrain>().enabled = false;
            GameManager.mainCamera.transform.GetComponentInParent<CameraRotator>().RotateCamera();
            UIManager.Instance.StartCoroutine(UIManager.Instance.LevelSuccess());
            yield return new WaitUntil(() => GameManager.gameState == GameState.Start);
            GameManager.SetState(new StateStart(GameManager));
        }
    }
}