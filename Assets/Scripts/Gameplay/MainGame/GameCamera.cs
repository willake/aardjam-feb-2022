using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Gameplay
{
    public class GameCamera : MonoBehaviour
    {
        [Header("Settings")]
        public float moveDuration = 0.4f;
        public Ease moveEase = Ease.Linear;
        public float zoomDuration = 0.4f;
        public Ease zoomEase = Ease.Linear;
        private Camera _camera;
        private Camera GetCamera()
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>();
            }

            return _camera;
        }

        public void LookAt(Vector2 target)
        {
            Vector3 pos = GetCamera().transform.position;

            pos.x = target.x;
            pos.y = target.y;

            GetCamera().transform.position = pos;
        }
        public Tween LookAtAsync(Vector2 target)
        {
            Vector3 pos = GetCamera().transform.position;

            pos.x = target.x;
            pos.y = target.y;

            return GetCamera().transform
                .DOMove(pos, moveDuration).SetEase(moveEase);
        }
        public void Zoom(float size)
        {
            GetCamera().orthographicSize = size;
        }

        public Tween ZoomAsync(float size)
        {
            return GetCamera()
                .DOOrthoSize(size, zoomDuration).SetEase(zoomEase);
        }
    }
}