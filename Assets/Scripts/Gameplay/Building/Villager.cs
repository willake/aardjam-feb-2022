using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class Villager : MonoBehaviour
    {
        public VillagerType Type { get; private set; }
        public GameObject HomePoint { get; private set; }

        [Header("Settings")]
        public GameObject homePointPrefab;

        private float towerXOffset = 2;
        private float edgeXOffset = 1;
        private float randomTargetOffset = 1f;
        private float YSpawn = -2.5f;

        public event System.Action onVillagerReachedTower = delegate { };
        private Vector2 targetPos;
        private bool movingToTower = false;
        private bool reachedTower = false;
        private float speed;

        //Maybe seperate some other time
        private float normalSpeed = 3f;
        private float lazySpeed = 1.5f;
        private float fastSpeed  = 4.5f;

        public void Init(Transform _homePointHolder, Transform _target)
        {
            Type = PickRandomType();
            HomePoint = Instantiate(homePointPrefab, _homePointHolder);
            HomePoint.transform.position = PickRandomSpawnLoc();
            transform.position = HomePoint.transform.position;
            targetPos = _target.position;
        }

        private VillagerType PickRandomType()
        {
            float randomType = Random.Range(0f, 1f);
            if (randomType <= 0.33f)
            {
                speed = normalSpeed;
                return VillagerType.Normal;
            }
            else if (randomType >= 0.66f)
            {
                speed = lazySpeed;
                return VillagerType.Lazy;
            }
            else
            {
                speed = fastSpeed;
                return VillagerType.Fast;
            }
        }

        private Vector2 PickRandomSpawnLoc()
        {
            Vector2 worldSpaceMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            float randomXScreenSpace = Random.Range(towerXOffset, worldSpaceMax.x - edgeXOffset);
            Vector2 spawnPoint = Random.value >= 0.5f ? new Vector2(randomXScreenSpace, YSpawn) : new Vector2(-randomXScreenSpace, YSpawn);
            return spawnPoint;
        }

        private void Update()
        {
            if (movingToTower && !reachedTower)
            {
                if (targetPos == null || !HomePoint) return;

                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == Consts.TOWERTAG)
            {
                if (!reachedTower)
                {
                    ReachedTower();
                }
            }
        }

        public void StartMoveTowardTower()
        {
            transform.position = HomePoint.transform.position;
            reachedTower = false;
            movingToTower = true;
            AnimateWalk();
        }

        public void ReachedTower()
        {
            DOTween.KillAll();
            reachedTower = true;
            onVillagerReachedTower.Invoke();
        }

        public void ReturnHome()
        {
            transform.position = HomePoint.transform.position;
            reachedTower = false;
            movingToTower = false;
            DOTween.KillAll();
        }

        public void AnimateWalk()
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(YSpawn + 0.1f, 0.1f))
                .Append(transform.DOMoveY(YSpawn, 0.05f))
                .SetLoops(-1);
        }

        public void AnimateWork()
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(YSpawn + Random.Range(0.5f, 1.5f), Random.Range(0.1f, 0.3f)))
                .Append(transform.DOMoveY(YSpawn, 0.1f))
                .SetLoops(-1);
        }

        public enum VillagerType
        {
            Normal,
            Lazy,
            Fast
        }
    }
}