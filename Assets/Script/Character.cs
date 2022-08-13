using Cinemachine;
using DG.Tweening;
using Script.Manager;
using UnityEngine;
using Zenject;

namespace Script
{
    public class Character : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        [Inject] private PlatformManager _platformManager;
        [Inject] public CharacterAnimator characterAnimator;
        public float playerSpeed;
        public ParticleSystem confetti;

        private float _playerXAxis;

        public bool IsFalling => transform.position.y < 0;

        private void Update()
        {
            PlayerMovement();
            SetAnimation();
        }

        private void PlayerMovement()
        {
            playerSpeed = _gameManager.gameState is GameState.Running or GameState.BeforeFinish ? 2 : 0;
            if (_platformManager.LastCube.correctTimeClicked)
            {
                _playerXAxis = Mathf.Lerp(_playerXAxis, _platformManager.LastCube.transform.position.x, Time.deltaTime);
                transform.position = new Vector3(_playerXAxis, transform.position.y, transform.position.z);
            }

            transform.Translate(Vector3.forward * (Time.deltaTime * playerSpeed));
        }

        private void SetAnimation()
        {
            characterAnimator.SetFloat("Running", playerSpeed);
            characterAnimator.SetFloat("Fall", transform.position.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                other.gameObject.SetActive(false);
                _gameManager.gameState = GameState.BeforeFinish;
            }
        }

        public void MoveBase()
        {
            LevelManager.Instance.stageIndex++;
        }
    }
}