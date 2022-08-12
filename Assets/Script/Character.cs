using UnityEngine;

namespace Script
{
    public class Character : MonoBehaviour
    {
        public float playerSpeed;
        private Animator _animator;
        public ParticleSystem confettie;
        private PlatformManager _platformManager;
        private float _playerXAxis;
        public Animator Animator => _animator ? _animator : GetComponentInChildren<Animator>();
        public bool IsFalling => transform.position.y < 0;

        private void Awake()
        {
            _platformManager = FindObjectOfType<PlatformManager>();
        }

        private void Update()
        {
            playerSpeed = GameManager.Instance.gameState is GameState.Running or GameState.BeforeFinish ? 2 : 0;
            _playerXAxis = Mathf.Lerp(_playerXAxis, _platformManager.LastCube.transform.position.x, Time.deltaTime);
            transform.position = new Vector3(_playerXAxis, transform.position.y, transform.position.z);
            Animator.SetFloat("Running", playerSpeed);
            Animator.SetFloat("Fall", transform.position.y);
            transform.Translate(Vector3.forward * (Time.deltaTime * playerSpeed));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                other.gameObject.SetActive(false);
                GameManager.Instance.gameState = GameState.BeforeFinish;
            }
        }
    }
}