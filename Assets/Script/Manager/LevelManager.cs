using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] public GameObject[] stages;
        public bool IsLevelFinished => stageIndex == stages.Length - 1;
        public int stageIndex;
        public float FinisPosition => stages[stageIndex].transform.position.z;
        public Transform CurrentBase => stages[stageIndex].transform.GetChild(0).transform;

        public void RestartLevel()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}