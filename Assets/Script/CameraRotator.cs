using UnityEngine;

namespace Script
{
    public class CameraRotator : MonoBehaviour
    {
        public int rotateSpeed;

        public void RotateCamera()
        {
            transform.localEulerAngles -= Vector3.up * Time.deltaTime * rotateSpeed;
        }

        public void StopCamera()
        {
            rotateSpeed = 0;
        }
    }
}