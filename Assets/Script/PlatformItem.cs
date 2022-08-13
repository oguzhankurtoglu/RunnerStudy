using System;
using DG.Tweening;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class PlatformItem : MonoBehaviour
    {
        private PlatformManager _platformManager;
        public bool correctTimeClicked;
        public GameObject visualEffect;

        public void SetUp(PlatformManager platformManager)
        {
            _platformManager = platformManager;
        }

        private void Awake()
        {
            if (_platformManager == null)
            {
                _platformManager = FindObjectOfType<PlatformManager>();
            }

            Move();
        }

        public void Move()
        {
            if (_platformManager.LastCube != this)
            {
                transform.DOLocalMoveX(
                        -transform.position.x,
                        2f)
                    .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            }
        }

        public void Stop()
        {
            DOTween.KillAll();
            float distance = transform.position.x - _platformManager.LastCube.transform.position.x;
            if (Mathf.Abs(distance) > _platformManager.LastCube.transform.localScale.x)
            {
                transform.AddComponent<Rigidbody>();
                GameManager.Instance.gameState = GameState.BeforeFinish;
                correctTimeClicked = false;
                _platformManager.LastCube = this;
            }
            else
            {
                if (Mathf.Abs(distance) < _platformManager.tolerance)
                {
                    transform.position = new Vector3(_platformManager.LastCube.transform.position.x,
                        transform.position.y, transform.position.z);
                    if (_platformManager.CanSpawn)
                    {
                        _platformManager.LastCube.visualEffect.SetActive(false);
                        _platformManager.LastCube.visualEffect.SetActive(true);
                        visualEffect.SetActive(true);
                        correctTimeClicked = true;

                        _platformManager.LastCube = this;
                        FeedBackManager.Instance.PlaySound(Mathf.Abs(distance) < _platformManager.tolerance);
                    }
                }
                else
                {
                    correctTimeClicked = true;
                    float direction = distance > 0 ? 1f : -1f;
                    var material = transform.GetComponent<Renderer>().material;
                    SlicePlatform(distance, direction, material);
                    _platformManager.LastCube = this;
                    FeedBackManager.Instance.PlaySound(Mathf.Abs(distance) < _platformManager.tolerance);
                }
            }
        }

        private void SlicePlatform(float distance, float direction, Material material)
        {
            float sizeX = _platformManager.LastCube.transform.localScale.x - Mathf.Abs(distance);
            float fallingSideSize = transform.localScale.x - sizeX;
            float posX = _platformManager.LastCube.transform.position.x + (distance / 2);

            transform.localScale = new Vector3(sizeX, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);

            float cubeEdge = transform.position.x + (sizeX / 2 * direction);
            float fallingSidePosition = cubeEdge + fallingSideSize / 2 * direction;

            SpawnFallItem(fallingSidePosition, fallingSideSize, material);
        }

        private void SpawnFallItem(float fallingSidePosition, float fallingSideSize, Material material)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Material mat = new Material(material);
            cube.GetComponent<Renderer>().material = mat;
            cube.transform.position = new Vector3(fallingSidePosition, transform.position.y, transform.position.z);
            cube.transform.localScale = new Vector3(fallingSideSize, transform.localScale.y, transform.localScale.z);
            cube.AddComponent<Rigidbody>().useGravity = true;
        }
    }
}