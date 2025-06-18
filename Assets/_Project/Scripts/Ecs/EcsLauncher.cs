using _Project.Scripts.Data.Business;
using _Project.Scripts.Data.Localization;
using _Project.Scripts.Ecs.Systems;
using _Project.Scripts.Ecs.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Ecs
{
    public class EcsLauncher : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
    
        [SerializeField] private AllBusinessData _allBusinessData;
        [SerializeField] private LocalizationData _localizationData;
        [SerializeField] private MainView _mainView;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
        
            var saveData = LoadSavedData();
            
            _systems
                .Add(new InitBusinessSystem(_world, _allBusinessData, _localizationData, _mainView, saveData))
                .Add(new BusinessProgressSystem(_allBusinessData))
                .Add(new LevelUpSystem(_allBusinessData))
                .Add(new UpgradeSystem(_allBusinessData))
                .Add(new BusinessViewSystem(_allBusinessData, _localizationData))
                .Add(new BalanceViewSystem(_mainView.BalanceText))
                .Add(new SaveSystem())
                .Init();
        }

        private void Update() => 
            _systems?.Run();

        private void OnDestroy ()
        {
            if (_systems != null)
            {
                _systems.Destroy ();
                _systems = null;
            }
            
            if (_world != null)
            {
                _world.Destroy ();
                _world = null;
            }
        }
    
        private static SaveData LoadSavedData()
        {
            if (!PlayerPrefs.HasKey("BusinessGameSave")) 
                return null;
            
            var json = PlayerPrefs.GetString("BusinessGameSave");
            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}