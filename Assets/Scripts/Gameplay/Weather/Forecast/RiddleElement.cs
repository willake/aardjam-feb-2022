using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class RiddleElement
    {
        public Villager toldBy;
        public StatementType statementType;
        public StatementCredibility statementCredibility;

        public RiddleElement(Villager _toldBy, StatementType _statementType, StatementCredibility _statementCredibility = StatementCredibility.Random)
        {
            toldBy = _toldBy;
            statementType = _statementType;

            if (_statementCredibility == StatementCredibility.Random)
            {
                statementCredibility = Random.value >= 0.5f ?
                    StatementCredibility.Truth :
                    StatementCredibility.Lie;
            }
            else
                statementCredibility = _statementCredibility;
        }

        public enum StatementType
        {
            AboutWeather,
            AboutVillager,
            AboutVillagers
        }

        public enum StatementCredibility
        {
            Random,
            Truth,
            Lie
        }
    }
}