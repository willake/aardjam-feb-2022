using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BuildingBlock : MonoBehaviour
    {
        [Header("Settings")]
        public Sprite spriteBuilding;
        public Sprite spriteCompleted;

        [SerializeField]
        private BuildingBlockType _type;
        public BuildingBlockType Type { get => _type; }

        private SpriteRenderer _renderer;

        private SpriteRenderer GetSpriteRenderer()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<SpriteRenderer>();
            }

            return _renderer;
        }

        // public void SetState(State state)
        // {
        //     switch (state)
        //     {
        //         case State.Building:
        //             GetSpriteRenderer().sprite = spriteBuilding;
        //             break;
        //         case State.Completed:
        //             GetSpriteRenderer().sprite = spriteCompleted;
        //             break;
        //     }
        // }

        public enum State
        {
            Building,
            Completed
        }
    }
}