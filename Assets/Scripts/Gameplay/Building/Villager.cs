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

        private float _towerXOffset = 2;
        private float _edgeXOffset = 1;
        private float _randomTargetOffset = 1f;
        private float _YSpawn = -2.5f;

        public event System.Action onVillagerReachedTower = delegate { };
        private Vector2 _targetPos;
        private bool _movingToTower = false;
        private bool _reachedTower = false;
        private float _speed;

        //Maybe seperate some other time
        private float _normalSpeed = 3f;
        private float _lazySpeed = 1.5f;
        private float _fastSpeed = 4.5f;

        public void Init(Transform homePointHolder, Transform target)
        {
            Type = PickRandomType();
            HomePoint = Instantiate(homePointPrefab, homePointHolder);
            HomePoint.transform.position = PickRandomSpawnLoc();
            transform.position = HomePoint.transform.position;
            _targetPos = target.position;
        }

        private VillagerType PickRandomType()
        {
            float randomType = Random.Range(0f, 1f);
            if (randomType <= 0.33f)
            {
                _speed = _normalSpeed;
                return VillagerType.Normal;
            }
            else if (randomType >= 0.66f)
            {
                _speed = _lazySpeed;
                return VillagerType.Lazy;
            }
            else
            {
                _speed = _fastSpeed;
                return VillagerType.Fast;
            }
        }

        private Vector2 PickRandomSpawnLoc()
        {
            Vector2 worldSpaceMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            float randomXScreenSpace = Random.Range(_towerXOffset, worldSpaceMax.x - _edgeXOffset);
            Vector2 spawnPoint = Random.value >= 0.5f ? new Vector2(randomXScreenSpace, _YSpawn) : new Vector2(-randomXScreenSpace, _YSpawn);
            return spawnPoint;
        }

        private void Update()
        {
            if (_movingToTower && !_reachedTower)
            {
                if (_targetPos == null || !HomePoint) return;

                float step = _speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, step);
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == Consts.TOWERTAG)
            {
                if (!_reachedTower)
                {
                    ReachedTower();
                }
            }
        }

        public void StartMoveTowardTower()
        {
            transform.position = HomePoint.transform.position;
            _reachedTower = false;
            _movingToTower = true;
            AnimateWalk();
        }

        public void ReachedTower()
        {
            DOTween.KillAll();
            _reachedTower = true;
            onVillagerReachedTower.Invoke();
        }

        public void ReturnHome()
        {
            transform.position = HomePoint.transform.position;
            _reachedTower = false;
            _movingToTower = false;
            DOTween.KillAll();
        }

        public void AnimateWalk()
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(_YSpawn + 0.1f, 0.1f))
                .Append(transform.DOMoveY(_YSpawn, 0.05f))
                .SetLoops(-1);
        }

        public void AnimateWork()
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(_YSpawn + Random.Range(0.5f, 1.5f), Random.Range(0.1f, 0.3f)))
                .Append(transform.DOMoveY(_YSpawn, 0.1f))
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