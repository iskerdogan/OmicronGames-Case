using Game.Managers;
using UnityEngine;
using Zenject;

namespace Injection
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private TrajectoryManager trajectoryManager;
        [SerializeField] private SlingshotManager slingshotManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().FromInstance(gameManager).AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().FromInstance(inputManager).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelManager>().FromInstance(levelManager).AsSingle();
            Container.BindInterfacesAndSelfTo<TrajectoryManager>().FromInstance(trajectoryManager).AsSingle();
            Container.BindInterfacesAndSelfTo<SlingshotManager>().FromInstance(slingshotManager).AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().FromInstance(uiManager).AsSingle();
        }
    }
}
