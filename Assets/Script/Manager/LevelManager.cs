using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] public GameObject[] stages;
        public int stageIndex;
        public float FinisPosition => stages[stageIndex].transform.position.z;

        public void RestartLevel()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}