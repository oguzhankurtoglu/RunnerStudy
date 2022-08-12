using System.Collections;
using Script.Manager;

namespace Script.State
{
    public abstract class State
    {
        protected readonly GameManager GameManager;

        protected State(GameManager gameManager)
        {
            GameManager = gameManager;
        }

        public abstract IEnumerator Start();
    }
}