using UniRx;
using System;
using Cysharp.Threading.Tasks;
using Game.Events;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using TMPro;
using Game.Gameplay.Weathers;

namespace Game.UI
{
    public class VillagerRiddle_UI : MonoBehaviour
    {
        [Title("References")]
        public TextMeshProUGUI villagerName;
        public TextMeshProUGUI villagerStatement;
        public GameObject sweatDrops;

        public void SetValues(string _villagerName, string _villagerStatement, bool isLying)
        {
            villagerName.text = _villagerName;
            villagerStatement.text = _villagerStatement;
            if (isLying) sweatDrops.SetActive(true);
        }
    }
}