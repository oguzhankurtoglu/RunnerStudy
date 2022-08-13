using Cinemachine;
using DG.Tweening;
using Script.State;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Manager
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
        #region fields

        public GameState gameState = GameState.Start;
        public Camera mainCamera;
        public Character player;
        private State.State _currentState;

        #endregion


        #region UnityLyfeCycle

        private void Awake()
        {
            player = FindObjectOfType<Character>();
            mainCamera = Camera.main;
            SetState(new StateStart(this));
        }

        #endregion


        #region Methods

        public void LevelStart()
        {
            UIManager.Instance.LevelCompleted.SetActive(false);
            var cameraAngle = mainCamera.transform.parent.transform.localEulerAngles;
            DOTween.To(() => cameraAngle.y, y => cameraAngle.y = y, -6.186f, 1).OnUpdate(() =>
            {
                mainCamera.transform.parent.transform.localEulerAngles =
                    new Vector3(cameraAngle.x, cameraAngle.y, cameraAngle.z);
            }).OnComplete(() =>
            {
                gameState = GameState.Start;
                mainCamera.GetComponent<CinemachineBrain>().enabled = true;
            });
        }


        public void SetState(State.State state)
        {
            _currentState = state;
            StartCoroutine(_currentState.Start());
        }

        #endregion
    }
}