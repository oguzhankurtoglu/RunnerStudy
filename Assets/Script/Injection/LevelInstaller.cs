using Script.Manager;
using UnityEngine;
using Zenject;

namespace Script.Injection
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private PlatformManager platformManager;
        //[SerializeField] private UIManager UIManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private FeedBackManager feedBackManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(platformManager);
            Container.BindInstance(gameManager);
            Container.BindInstance(feedBackManager);    
            //Container.BindInstance(UIManager);
        }
    }
}