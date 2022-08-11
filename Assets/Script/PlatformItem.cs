using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class PlatformItem : MonoBehaviour
    {
        public float speed;
        
        private void Update()
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}