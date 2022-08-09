using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class PlatformController : MonoBehaviour
    {
        public List<GameObject> platformList;
        public int currentPlatformIndex;
        public GameObject cutterx;
        public GameObject cutter1;
        private void Update()
        {
            if (GameManager.Instance.gameState==GameState.Running)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentPlatformIndex++;
                    var currentPlatform = platformList[currentPlatformIndex];
                    if (currentPlatform.TryGetComponent(out Cutter cutter))
                    {
                        cutter.speed = 0;
                    }

                    
                    platformList[currentPlatformIndex].SetActive(true);
                    cutterx.transform.SetParent(platformList[currentPlatformIndex].transform);
                    cutter1.transform.SetParent(platformList[currentPlatformIndex].transform);
                    var x = platformList[currentPlatformIndex].transform.position.x -
                            platformList[currentPlatformIndex].transform.localScale.x / 2;
                    cutterx.transform.position = new Vector3(x, platformList[currentPlatformIndex].transform.position.y,
                        platformList[currentPlatformIndex].transform.position.z);

                }
            }
        }
    }
}
