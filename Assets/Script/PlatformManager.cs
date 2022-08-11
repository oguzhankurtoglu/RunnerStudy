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

        private Vector3 _defaultScale;
        [SerializeField] private GameObject movingCubePrefab;

        #endregion

        #region UnityLifeCycle

        private void Update()
        {
            if (GameManager.Instance.gameState == GameState.Running)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    EventManager.OnClickPressed.Invoke();
                }
            }
        }

        #endregion

        #region Methods

   

        #endregion
    }
}