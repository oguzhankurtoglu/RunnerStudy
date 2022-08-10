using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    public class PlatformManager : MonoBehaviour
    {
        #region fields

        [SerializeField] private int forwardOffset = 3;
        [SerializeField] public List<GameObject> platformPool;
        private Queue<GameObject> _platformQueue = new();
        [SerializeField] private List<GameObject> slicedItemPool;
        [SerializeField] private SlicerLeft slicerLeft;
        [SerializeField] private SlicerRight slicerRight;
        private GameObject _currentPlatform;


        private Vector3 _defaultScale;

        #endregion

        #region UnityLifeCycle

        private void OnEnable()
        {
            EventManager.OnClickPressed.AddListener(SetSlicersTransform);
            EventManager.OnClickPressed.AddListener(TriggerPlatformItem);
            PlatformProperty();
        }

        private void Start()
        {
            foreach (var platform in platformPool)
            {
                SetPoolItem(platform);
            }
        }

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Start ||
                GameManager.Instance.gameState == GameState.Running)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    EventManager.OnClickPressed.Invoke();
                    forwardOffset += 3;
                }
            }
        }

        private void OnDisable()
        {
            EventManager.OnClickPressed.RemoveListener(SetSlicersTransform);
            EventManager.OnClickPressed.RemoveListener(TriggerPlatformItem);
        }

        #endregion

        #region Methods

        private void PlatformProperty()
        {
            var currentPlatform = platformPool[0];
            _defaultScale = currentPlatform.transform.localScale;
        }

        private void SetSlicersTransform()
        {
            if (GameManager.Instance.gameState != GameState.Running) return;
            slicerLeft.transform.SetParent(_currentPlatform.transform);
            slicerLeft.transform.position =
                new Vector3(_currentPlatform.transform.position.x - _currentPlatform.transform.localScale.x / 2,
                    _currentPlatform.transform.position.y, forwardOffset);
            
            slicerRight.transform.SetParent(_currentPlatform.transform);
            slicerRight.transform.position =
                new Vector3(_currentPlatform.transform.position.x + _currentPlatform.transform.localScale.x / 2,
                    _currentPlatform.transform.position.y, forwardOffset);
        }

        private void TriggerPlatformItem()
        {
            var currentItem = GetPoolItem();
            _currentPlatform = currentItem.gameObject;
            currentItem.transform.position += Vector3.forward * forwardOffset;
            currentItem.Move();
            StartCoroutine(ReturnPool(currentItem.gameObject));
        }

        private readonly WaitForSeconds _duration = new(6f);

        private IEnumerator ReturnPool(GameObject returner)
        {
            yield return _duration;
            SetPoolItem(returner);
        }

        private PlatformItem GetPoolItem()
        {
            var platformItem = _platformQueue.Dequeue();
            platformItem.SetActive(true);
            return platformItem.GetComponent<PlatformItem>();
        }

        private void SetPoolItem(GameObject poolItem)
        {
            ReturnDefault(poolItem);
            _platformQueue.Enqueue(poolItem);
        }

        private void ReturnDefault(GameObject platformItem)
        {
            platformItem.transform.localScale = _defaultScale;
            platformItem.transform.position = Vector3.right * -6;
            platformItem.SetActive(false);
        }

        #endregion
    }
}