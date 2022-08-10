using System;
using UnityEngine;

namespace Script
{
    public class PlatformItem : MonoBehaviour
    {
        public float speed;

        private void OnEnable()
        {
            EventManager.OnClickPressed.AddListener(Stop);
        }

        private void OnDisable()
        {
            EventManager.OnClickPressed.RemoveListener(Stop);
        }

        public void Stop()
        {
            speed = 0;
        }
        public void Move()
        {
            speed = 3.5f;
        }

        private void Update()
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        
    }
}