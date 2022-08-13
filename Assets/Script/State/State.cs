using System.Collections;
using Script.Manager;
using Zenject;

namespace Script.State
{
    public abstract class State
    {
        //[Inject] protected UIManager UIManager;
        protected readonly GameManager GameManager;

        protected State(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        public abstract IEnumerator Start();
    }
}