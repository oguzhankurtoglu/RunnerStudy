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

        public override IEnumerator Start()
        {
            GameManager.platformManager.LastCube =
                LevelManager.Instance.CurrentBase.GetComponent<PlatformItem>();

            GameManager.platformManager.forwardOffset =
                (int) GameManager.platformManager.LastCube.transform.position.z + 3;
            GameManager.player.MoveBase();
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            GameManager.platformManager.Starter();
            GameManager.SetState(new StateRunning(GameManager));
        }
    }
}