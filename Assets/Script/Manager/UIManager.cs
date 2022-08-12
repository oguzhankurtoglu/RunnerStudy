using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public GameObject LevelCompleted;
        public GameObject GameStart;
        public GameObject InGame;
        public GameObject LevelFail;
        
        public IEnumerator<WaitForSeconds> LevelSuccess()
        {
            yield return new WaitForSeconds(1f);
            InGame.SetActive(false);
            LevelCompleted.SetActive(true);
        }
        public IEnumerator<WaitForSeconds> LevelFailed()
        {
            yield return new WaitForSeconds(1f);

            InGame.SetActive(false);
            LevelFail.SetActive(true);
        }
        
    }
}