using System.Collections;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.State
{
    public class StateRunning : State
    {
        public StateRunning(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.gameState = GameState.Running;
            yield return new WaitUntil(() =>
                GameManager.gameState == GameState.BeforeFinish);
            GameManager.SetState(new StateBeforeFinish(GameManager));
        }
    }
}