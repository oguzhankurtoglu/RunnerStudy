using System;
using UnityEngine;

namespace Script
{
    public class CameraRotator : MonoBehaviour
    {
        public int rotateSpeed=0;

        public void RotateCamera()
        {
            rotateSpeed = 10;
        }

        public void StopCamera()
        {
            rotateSpeed = 0;
        }

        private void Update()
        {
            transform.localEulerAngles -= Vector3.up * Time.deltaTime * rotateSpeed;
        }
    }
}