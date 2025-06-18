using System;
using System.Collections.Generic;
using _Project.Scripts.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace _Project.Scripts.Ecs.Systems
{
    public class SaveSystem : IEcsDestroySystem
    {
        public void Destroy(IEcsSystems systems)
        {
            SaveGame(systems.GetWorld());
        }

        private static void SaveGame(EcsWorld world)
        {
            var saveData = new SaveData();
        
            var walletFilter = world.Filter<Wallet>().End();
            foreach (var entity in walletFilter)
            {
                saveData.Balance = world.GetPool<Wallet>().Get(entity).Balance;
            }
        
            saveData.Businesses = new List<BusinessSave>();
            var businessFilter = world.Filter<Business>().End();
            foreach (var entity in businessFilter)
            {
                ref var business = ref world.GetPool<Business>().Get(entity);
                saveData.Businesses.Add(new BusinessSave
                {
                    ID = business.ID,
                    Level = business.Level,
                    Progress = business.Progress,
                    Upgrade1Purchased = business.Upgrade1Purchased,
                    Upgrade2Purchased = business.Upgrade2Purchased
                });
            }
        
            var json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString("BusinessGameSave", json);
            PlayerPrefs.Save();
        }
    }
    
    [Serializable]
    public class SaveData
    {
        public float Balance;
        public List<BusinessSave> Businesses;
    }

    [Serializable]
    public class BusinessSave
    {
        public int ID;
        public int Level;
        public float Progress;
        public bool Upgrade1Purchased;
        public bool Upgrade2Purchased;
    }
}