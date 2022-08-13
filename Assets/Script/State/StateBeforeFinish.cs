using System.Collections;
using Script.Manager;
using UnityEngine;

namespace Script.State
{
    public class StateBeforeFinish : State
    {
        public StateBeforeFinish(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            if (GameManager.player.transform.position.z >= LevelManager.Instance.FinisPosition-3)
            {
                GameManager.gameState = GameState.Success;
                GameManager.SetState(new StateSuccess(GameManager));
            }
            else
            {
                yield return new WaitUntil(() => GameManager.player.IsFalling);
                GameManager.SetState(new StateFail(GameManager));
            }
        }
    }
}