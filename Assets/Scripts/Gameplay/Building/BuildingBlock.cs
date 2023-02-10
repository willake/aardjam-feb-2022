using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BuildingBlock : MonoBehaviour
    {
        public enum State
        {
            Building,
            Completed
        }
        [Header("Settings")]
        public Sprite spriteBuilding;
        public Sprite spriteCompleted;

        private SpriteRenderer _renderer;

        private SpriteRenderer GetSpriteRenderer()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<SpriteRenderer>();
            }

            return _renderer;
        }

        public void SetState(State state)
        {
            switch (state)
            {
                case State.Building:
                    GetSpriteRenderer().sprite = spriteBuilding;
                    break;
                case State.Completed:
                    GetSpriteRenderer().sprite = spriteCompleted;
                    break;
            }
        }
    }
}