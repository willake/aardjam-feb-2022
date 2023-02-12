using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace Game.Gameplay
{
    public class VillagerFactory : MonoBehaviour
    {
        //Maybe seperate some other time
        private float _normalSpeed = 3f;
        private float _lazySpeed = 1.5f;
        private float _fastSpeed = 4.5f;
        private readonly string[] Adjectives = { "Annoying", "Accurate", "Angry", "Awesome", "Big", "Busy", "Blue", "Charming", "Calm", "Cheerful", "Cute", "Dirty", "Foolish", "Funny", "Juicy", "Little", "Macho", "Spicy"};
        private readonly string[] Names = { "Aero", "Windy", "Velvette", "Starlet", "Snowdrop", "Wiatt", "Tenysi", "Brayan", "Brudo", "Jim", "Abrielle", "Domini", "Elvira", "Eula", "Fox", "Hui En", "Jesse", "Thijs" };

        private int _increment = 0;
        public Villager GenerateVillager(Transform parent = null)
        {
            GameObject go = Instantiate(
                ResourceManager.instance.GameplayResources.Village.Villager, parent);
            Villager villager = go.GetComponent<Villager>();
            villager.Init(_increment, PickRandomSpeed(), PickRandomName());

            _increment++;

            return villager;
        }

        public void RemoveVillager(Villager villager)
        {
            _increment--;
            GameObject.Destroy(villager.gameObject);
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

        private string PickRandomName()
        {
            return Adjectives[UnityRandom.Range(0, Adjectives.Length)] + " " + Names[UnityRandom.Range(0, Names.Length)];
        }
    }
}