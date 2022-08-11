using System;
using UnityEngine;

namespace Script
{
    public class Character : MonoBehaviour
    {
        public float playerSpeed;
        private Animator _animator;

        private Animator Animator => _animator ? _animator : GetComponentInChildren<Animator>();


        private void Update()
        {
            playerSpeed = GameManager.Instance.gameState is GameState.Running or GameState.BeforeFinish ? 2 : 0;
            
            Animator.SetFloat("Running", playerSpeed);
            Animator.SetFloat("Fall", transform.position.y);
            transform.Translate(Vector3.forward * (Time.deltaTime * playerSpeed));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                GameManager.Instance.gameState = GameState.Success;
                GetComponentInChildren<Animator>().SetTrigger("Dance");
            }
        }
    }
}