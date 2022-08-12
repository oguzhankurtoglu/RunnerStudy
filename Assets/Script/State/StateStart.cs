using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Script.State
{
    public class StateStart : State
    {
        public StateStart(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            ReturnDefaultCamera();
            UIManager.Instance.LevelCompleted.SetActive(false);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            GameManager.SetState(new StateRunning(GameManager));
        }

        private void ReturnDefaultCamera()
        {
            var cameraAngle = GameManager.mainCamera.transform.parent.transform.localEulerAngles;
            DOTween.To(() => cameraAngle.y, y => cameraAngle.y = y, 0, 1).OnUpdate(() =>
            {
                GameManager.mainCamera.transform.parent.transform.localEulerAngles =
                    new Vector3(cameraAngle.x, cameraAngle.y, cameraAngle.z);
            }).OnComplete(() => { GameManager.mainCamera.GetComponent<CinemachineBrain>().enabled = true; });
        }
    }
}