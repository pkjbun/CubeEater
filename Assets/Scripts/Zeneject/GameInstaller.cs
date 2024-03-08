using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CubeBehaviour>().AsTransient();
        Container.BindInterfacesAndSelfTo<CubeEater>().AsTransient();
        Container.BindInterfacesAndSelfTo<ObjectSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIListOfObjectsManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIAnimation>().FromComponentInHierarchy().AsSingle();
    }
}