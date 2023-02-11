using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    public class VillagerSystem : MonoBehaviour
    {
        [Header("References")]
        public VillagerFactory factory;
        public Transform houseHolder;
        public Transform villagerHolder;
        public Transform towerTarget;

        private Dictionary<int, Vector2> _villagersRegisteredDict =
            new Dictionary<int, Vector2>();
        private List<Villager> _villagers = new List<Villager>();
        public int VillagerAmount { get => _villagers.Count; }

        private float _towerXOffset = 2;
        private float _edgeXOffset = 1;

        public Villager AddNewVillager()
        {
            Villager newVillager = factory.GenerateVillager(villagerHolder);
            _villagers.Add(newVillager);
            Vector2 home = PickRandomPointAsHome();
            _villagersRegisteredDict.Add(newVillager.ID, home);
            newVillager.transform.position = home;
            Debug.Log("Home: " + home);
            return newVillager;
        }

        private Vector2 PickRandomPointAsHome()
        {
            Vector2 worldSpaceMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            float randomXScreenSpace = UnityRandom.Range(_towerXOffset, 4.5f - _edgeXOffset);
            Vector2 spawnPoint =
                UnityRandom.value >= 0.5f
                    ? new Vector2(randomXScreenSpace, towerTarget.position.y)
                    : new Vector2(-randomXScreenSpace, towerTarget.position.y);
            return spawnPoint;
        }

        public async UniTask MoveVillagersToTower()
        {
            await UniTask
                .WhenAll(_villagers.Select(x => x.MoveTo(towerTarget.position)));
        }

        public async UniTask StartTowerWork()
        {
            await UniTask
                .WhenAll(_villagers.Select(x => x.AnimateWork()));
        }

        //Maybe eventually play an animation for this too, would add to immersion!
        public async UniTask MoveVillagersHome()
        {
            await UniTask
                .WhenAll(_villagers.Select(x =>
                {
                    if (_villagersRegisteredDict.TryGetValue(x.ID, out Vector2 home))
                    {
                        return x.MoveTo(home);
                    }
                    else
                    {
                        return x.MoveTo(houseHolder.position);
                    }
                }));
        }
    }
}