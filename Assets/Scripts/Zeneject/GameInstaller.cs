using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] GameObject ButtonCubePrefab;
    public override void InstallBindings()
    {
        
        Container.BindInterfacesAndSelfTo<ObjectSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIListOfObjectsManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIAnimation>().FromComponentInHierarchy().AsSingle();
        Container.BindFactory<ItemData, Transform, ButtonCube, ButtonCube.Factory>().FromComponentInNewPrefab(ButtonCubePrefab);
    }
}