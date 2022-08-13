using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using Zenject;


namespace Script
{
    public class PlatformManager : MonoBehaviour
    {
        #region fields

        [Inject] private GameManager _gameManager;
        
        [SerializeField] public int forwardOffset = 3;
        [SerializeField] public Transform finishLine;
        [SerializeField] public Transform defaultTransform;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] public float tolerance;


        [SerializeField] private Material[] materials;
        [SerializeField] private List<GameObject> platformList;
        [SerializeField] private readonly Queue<PlatformItem> _platformPool = new();
        private readonly WaitForSeconds _returnPoolDuration = new(10f);

        [field: SerializeField] public PlatformItem CurrentCube { get; set; }
        [field: SerializeField] public PlatformItem LastCube { get; set; }
        public bool CanSpawn => CurrentCube.transform.position.z < LevelManager.Instance.FinisPosition - 3;
        private bool _isFinish;

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
            if (_gameManager.gameState is GameState.Running)
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
            cube.transform.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
            cube.gameObject.SetActive(true);
            cube.Move();

            Debug.Log(cube.name + " " + LastCube.name);

            LastCube = CurrentCube;
            CurrentCube = cube;
            StartCoroutine(ReturnPool(cube));
        }

        public void Starter()
        {
            Debug.Log("starter");
            var cube = _platformPool.Dequeue();
            cube.transform.position = defaultTransform.transform.position + Vector3.forward * forwardOffset;
            cube.transform.localScale = defaultTransform.transform.localScale;
            cube.gameObject.SetActive(true);
            cube.Move();
            
            Debug.Log("current: "+cube.name + " " +"Last Cube: " + LastCube.name);

            CurrentCube = cube;
            forwardOffset += 3;
            StartCoroutine(ReturnPool(cube));
        }

        private IEnumerator ReturnPool(PlatformItem platformItem)
        {
            yield return _returnPoolDuration;
            platformItem.gameObject.SetActive(false);
            platformItem.visualEffect.SetActive(false);
            _platformPool.Enqueue(platformItem);
        }

        #endregion
    }
}