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
        [Inject] public PlatformManager platformManager;
        
        public float playerSpeed;
        private Animator _animator;
        public ParticleSystem confettie;
        
        private float _playerXAxis;
        public Animator Animator => _animator ? _animator : GetComponentInChildren<Animator>();
        public bool IsFalling => transform.position.y < 0;

        private void Update()
        {
            playerSpeed = _gameManager.gameState is GameState.Running or GameState.BeforeFinish ? 2 : 0;
            if (platformManager.LastCube.correctTimeClicked)
            {
                _playerXAxis = Mathf.Lerp(_playerXAxis, platformManager.LastCube.transform.position.x, Time.deltaTime);
                transform.position = new Vector3(_playerXAxis, transform.position.y, transform.position.z);
            }

            Animator.SetFloat("Running", playerSpeed);
            Animator.SetFloat("Fall", transform.position.y);
            transform.Translate(Vector3.forward * (Time.deltaTime * playerSpeed));
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
          // transform.DOMoveZ(LevelManager.Instance.CurrentBase.position.z, 1f);
          LevelManager.Instance.stageIndex++;
        }
    }
}