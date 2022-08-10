using System;
using UnityEngine;

namespace Script
{
    public class Character : MonoBehaviour
    {
        public float playerSpeed;

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Running || GameManager.Instance.gameState==GameState.BeforeFinish)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * playerSpeed));
            }
        }
    }
}