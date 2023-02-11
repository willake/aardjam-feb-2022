using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace Game.Gameplay
{
    public class VillagerFactory : MonoBehaviour
    {
        [Header("References")]
        public GameObject villagerPrefab;

        //Maybe seperate some other time
        private float _normalSpeed = 3f;
        private float _lazySpeed = 1.5f;
        private float _fastSpeed = 4.5f;

        private int _increment = 0;
        public Villager GenerateVillager(Transform parent = null)
        {
            GameObject go = Instantiate(villagerPrefab, parent);
            Villager villager = go.GetComponent<Villager>();
            villager.Init(_increment, PickRandomSpeed());

            _increment++;

            return villager;
        }

        private float PickRandomSpeed()
        {
            float randomType = UnityRandom.Range(0f, 1f);
            if (randomType <= 0.33f)
            {
                return _normalSpeed;
            }
            else if (randomType >= 0.66f)
            {
                return _lazySpeed;
            }
            else
            {
                return _fastSpeed;
            }
        }
    }
}