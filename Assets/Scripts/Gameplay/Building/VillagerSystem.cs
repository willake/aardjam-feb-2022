using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class VillagerSystem : MonoBehaviour
    {
        [Header("References")]
        public Transform villagerHolder;
        public Transform towerTarget;

        [Header("Settings")]
        public Villager villagerPrefab;

        private List<Villager> _villagers = new List<Villager>();
        public List<Villager> villagers { get { return _villagers; } }
        public int villagerAmount { get; private set; }
        private int currentVillagerReachedTowerAmount = 0;
        public event Action onAllVillagersReachedTower = delegate { };

        public void Init()
        {
            villagerAmount = 0;
        }

        public Villager AddNewVillager()
        {
            Villager newVillager = Instantiate(villagerPrefab, villagerHolder);
            newVillager.Init(villagerHolder, towerTarget);
            newVillager.onVillagerReachedTower += IncrementVillagerReachedTower;
            _villagers.Add(newVillager);
            villagerAmount++;
            return newVillager;
        }

        public void MoveVillagersToTower()
        {
            _villagers.ForEach(villager =>
            {
                villager.StartMoveTowardTower();
            });
        }

        public void StartTowerWork()
        {
            _villagers.ForEach(villager =>
            {
                villager.AnimateWork();
            });
        }

        //Maybe eventually play an animation for this too, would add to immersion!
        public void MoveVillagersHome()
        {
            _villagers.ForEach(villager =>
            {
                villager.ReturnHome();
            });
        }

        public void IncrementVillagerReachedTower()
        {
            currentVillagerReachedTowerAmount++;

            if (currentVillagerReachedTowerAmount >= villagerAmount)
            {
                onAllVillagersReachedTower.Invoke();
                currentVillagerReachedTowerAmount = 0;
            }
        }
    }
}