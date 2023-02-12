using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class VillagerRiddle : RiddleElement
    {
        public RiddleElement villagerRiddle;
        public StatementCredibility villagerRiddleBelief;

        public VillagerRiddle(RiddleElement _villagerRiddle, Villager _toldBy, StatementType _statementType, StatementCredibility _statementCredibility = StatementCredibility.Random) : base(_toldBy, _statementType, _statementCredibility)
        {
            villagerRiddle = _villagerRiddle;

            bool tellsTruth = statementCredibility == StatementCredibility.Truth ? true : false;

            //If both predictions are true OR both riddles are lies, we believe the said statement to be true. On the contrary, if we are lying and the first one is the truth OR if we are telling the truth and the first one is lying, we believe the statement to be a lie.
            if ((tellsTruth && villagerRiddle.statementCredibility == StatementCredibility.Truth) ||
                (!tellsTruth && villagerRiddle.statementCredibility == StatementCredibility.Lie))
                villagerRiddleBelief = StatementCredibility.Truth;
            else 
                villagerRiddleBelief = StatementCredibility.Lie;
        }
    }
}