using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class PlatformItem : MonoBehaviour
    {
        public float speed;
        public static PlatformItem CurrentCube { get; private set; }
        public static PlatformItem LastCube { get; private set; }

        private void OnEnable()
        {
            EventManager.OnClickPressed.AddListener(Stop);
            if (LastCube == null)
            {
                LastCube = GameObject.FindGameObjectWithTag("Base").GetComponent<PlatformItem>();
            }
            else
            {
                transform.localScale = LastCube.transform.localScale;
                CurrentCube = this;
            }
        }

        private void OnDisable()
        {
            EventManager.OnClickPressed.RemoveListener(Stop);
        }

        private void Stop()
        {
            speed = 0;
            float distance = transform.position.x - LastCube.transform.position.x;
            float direction = distance > 0 ? 1f : -1f;
            SlicePlatform(distance, direction);
            LastCube = this;
        }

        private void SlicePlatform(float distance, float direction)
        {
            float sizeX = LastCube.transform.localScale.x - Mathf.Abs(distance);
            float fallingSideSize = transform.localScale.x - sizeX;
            float posX = LastCube.transform.position.x + (distance / 2);

            transform.localScale = new Vector3(sizeX, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);

            float cubeEdge = transform.position.x + (sizeX / 2 * direction);
            float fallingSidePosition = cubeEdge + fallingSideSize / 2 *direction;

            SpawnFallItem(fallingSidePosition, fallingSideSize);
        }

        private void SpawnFallItem(float fallingSidePosition, float fallingSideSize)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(fallingSidePosition, transform.position.y, transform.position.z);
            cube.transform.localScale = new Vector3(fallingSideSize, transform.localScale.y, transform.localScale.z);
            cube.AddComponent<Rigidbody>().useGravity = true;
        }

        private void Update()
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}