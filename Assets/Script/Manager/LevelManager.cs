using UnityEngine.SceneManagement;

namespace Script
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public void RestartLevel()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}