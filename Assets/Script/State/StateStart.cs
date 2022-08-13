using System.Collections;
using Script.Manager;
using UnityEngine;

namespace Script.State
{
    public class StateStart : State
    {
        public StateStart(GameManager gameManager) : base(gameManager)
        {
        }

        private bool _isSecond = false;

        public override IEnumerator Start()
        {
            GameManager.player.platformManager.LastCube =
                LevelManager.Instance.CurrentBase.GetComponent<PlatformItem>();

            GameManager.player.platformManager.forwardOffset = (int) LevelManager.Instance.FinisPosition + 3;
            GameManager.player.MoveBase();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            GameManager.player.platformManager.Starter();
            

            GameManager.SetState(new StateRunning(GameManager));
        }
    }
}