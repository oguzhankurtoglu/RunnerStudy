using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace Script
{
    public class PlatformManager : MonoBehaviour
    {
        [field: SerializeField] public PlatformItem CurrentCube { get; set; }
        [field: SerializeField] public PlatformItem LastCube { get; set; }

        #region fields

        [SerializeField] public Transform spawnPoint;
        [SerializeField] private int forwardOffset = 3;
        [SerializeField] private GameObject referencePrefab;
        private Transform _defaultTransform;

        #endregion

        #region UnityLifeCycle

        private void OnEnable()
        {
            _defaultTransform = referencePrefab.transform;
        }

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Running)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Stop();
                    forwardOffset += 3;
                    SpawnCube();
                }
            }
        }

        #endregion

        #region Methods

        private void SpawnCube()
        {
            var cube = CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = spawnPoint.transform.position + (Vector3.forward * forwardOffset);
            cube.transform.localScale = LastCube.transform.localScale;
            LastCube = CurrentCube;
            CurrentCube = cube.GetComponent<PlatformItem>();
        }

        private void Stop()
        {
            LastCube.speed = 0;
            float distance = CurrentCube.transform.position.x - LastCube.transform.position.x;
            float direction = distance > 0 ? 1f : -1f;
            if (Mathf.Abs(distance) >= LastCube.transform.localScale.x)
            {
                this.AddComponent<Rigidbody>();
            }

            SlicePlatform(distance, direction);
        }

        private void SlicePlatform(float distance, float direction)
        {
            float sizeX = LastCube.transform.localScale.x - Mathf.Abs(distance);
            float fallingSideSize = CurrentCube.transform.localScale.x - sizeX;
            float posX = LastCube.transform.position.x + (distance / 2);

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.localScale = new Vector3(sizeX, CurrentCube.transform.localScale.y,
                CurrentCube.transform.localScale.z);
            cube.transform.position =
                new Vector3(posX, CurrentCube.transform.position.y, CurrentCube.transform.position.z);

            float cubeEdge = CurrentCube.transform.position.x + (sizeX / 2 * direction);
            float fallingSidePosition = cubeEdge + fallingSideSize / 2 * direction;

            SpawnFallItem(fallingSidePosition, fallingSideSize);
        }

        private void SpawnFallItem(float fallingSidePosition, float fallingSideSize)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(fallingSidePosition, CurrentCube.transform.position.y,
                CurrentCube.transform.position.z);
            cube.transform.localScale = new Vector3(fallingSideSize, CurrentCube.transform.localScale.y,
                CurrentCube.transform.localScale.z);
            cube.AddComponent<Rigidbody>().useGravity = true;
            gameObject.SetActive(false);
        }

        #endregion
    }
}