using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script.Manager;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace Script
{
    public class PlatformManager : MonoBehaviour
    {
        #region fields

        [SerializeField] public int forwardOffset = 3;
        [SerializeField] public Transform finishLine;
        [SerializeField] public Transform defaultTransform;
        [SerializeField] private GameObject platformPrefab;


        [SerializeField] private Material[] materials;
        [SerializeField] private List<GameObject> platformList;
        [SerializeField] private readonly Queue<PlatformItem> _platformPool = new();
        private readonly WaitForSeconds _returnPoolDuration = new(10f);

        [field: SerializeField] public PlatformItem CurrentCube { get; set; }
        [field: SerializeField] public PlatformItem LastCube { get; set; }
        private bool CanSpawn => CurrentCube.transform.position.z < LevelManager.Instance.FinisPosition - 3;

        #endregion

        #region UnityLifeCycle

        private void Awake()
        {
            defaultTransform = platformPrefab.transform;
            foreach (var platform in platformList)
            {
                _platformPool.Enqueue(platform.GetComponent<PlatformItem>());
            }
        }

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Running ||
                GameManager.Instance.gameState == GameState.Start)
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

        public void Spawner()
        {
            var cube = _platformPool.Dequeue();
            cube.transform.position = defaultTransform.transform.position + Vector3.forward * forwardOffset;
            cube.transform.localScale = LastCube.transform.localScale;
            cube.transform.GetComponent<Renderer>().material = materials[forwardOffset / 3];
            cube.gameObject.SetActive(true);
            cube.Move();
            cube.SetUp(this);

            LastCube = CurrentCube;
            CurrentCube = cube;
            StartCoroutine(ReturnPool(cube));
        }


        private IEnumerator ReturnPool(PlatformItem platformItem)
        {
            yield return _returnPoolDuration;
            platformItem.gameObject.SetActive(false);
            _platformPool.Enqueue(platformItem);
        }

        #endregion
    }
}