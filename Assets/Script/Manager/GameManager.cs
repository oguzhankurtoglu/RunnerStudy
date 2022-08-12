using Script.State;
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
            gameState = GameState.Start;
        }

        public void SetState(State.State state)
        {
            _currentState = state;
            StartCoroutine(_currentState.Start());
        }

        #endregion
    }
}