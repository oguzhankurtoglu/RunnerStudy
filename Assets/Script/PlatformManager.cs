using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace Script
{
    public class PlatformManager : MonoBehaviour
    {
        #region fields

        [SerializeField] private int forwardOffset = 3;
        [SerializeField] public List<GameObject> platformPool;
        private readonly Queue<GameObject> _platformQueue = new();
        [SerializeField] private Material[] materials;

        [SerializeField] private GameObject platformPrefab;
        public Transform defaultTransform;
        [field: SerializeField] public PlatformItem CurrentCube { get; set; }
        [field: SerializeField] public PlatformItem LastCube { get; set; }
        public Transform finishline;
        public bool CanSpawn => CurrentCube.transform.position.z < finishline.transform.position.z - 3;

        #endregion

        #region UnityLifeCycle

        private void Awake()
        {
            defaultTransform = platformPrefab.transform;
        }

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Running)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CurrentCube.Stop();
                    if (CanSpawn)
                    {
                        Spawner();
                        forwardOffset += 3;
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void Spawner()
        {
            var cube = Instantiate(platformPrefab).GetComponent<PlatformItem>();
            cube.SetUp(this);
            cube.transform.position = defaultTransform.transform.position + Vector3.forward * forwardOffset;
            cube.transform.localScale = LastCube.transform.localScale;
            cube.transform.GetComponent<Renderer>().material = materials[forwardOffset / 3];

            LastCube = CurrentCube;
            CurrentCube = cube;
        }

        #endregion
    }
}