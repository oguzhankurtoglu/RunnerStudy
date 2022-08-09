using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    public class PlatformManager : MonoBehaviour
    {
        #region fields

        [SerializeField] private float forwardOffset = 3;
        [SerializeField] public List<GameObject> platformPool;
        private Queue<GameObject> _platformQueue = new();
        [SerializeField] private List<GameObject> slicedItemPool;

        private Vector3 _defaultScale;

        #endregion

        #region UnityLifeCycle

        private void OnEnable()
        {
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
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.OnClickPressed.Invoke();
                forwardOffset += 3;
            }
        }

        private void OnDisable()
        {
            EventManager.OnClickPressed.RemoveListener(TriggerPlatformItem);
        }

        #endregion

        #region Methods

        private void PlatformProperty()
        {
            var currentPlatform = platformPool[0];
            _defaultScale = currentPlatform.transform.localScale;
        }

        private void TriggerPlatformItem()
        {
            var currentItem = GetPoolItem();
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