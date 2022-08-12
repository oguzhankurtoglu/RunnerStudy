using System.Collections;

namespace Script.State
{
    public class StateFail : State
    {
        public StateFail(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.gameState = GameState.Fail;
            GameManager.StartCoroutine(UIManager.Instance.LevelFailed());
            yield return GameManager.gameState == GameState.Start;
            GameManager.SetState(new StateStart(GameManager));
        }
    }
}