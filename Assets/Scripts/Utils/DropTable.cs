using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropTable<T>
{
	public T item;

	public float probabilityWeight;

	[HideInInspector]
	public float probabilityRangeFrom;
	[HideInInspector]
	public float probabilityRangeTo;

	public T ReturnLootFromTable<T>(List<DropTable<T>> dropTableProbabilities)
	{
		float currentProbabilityWeightMaximum = 0f;

		foreach (DropTable<T> lootDropItem in dropTableProbabilities)
		{
			lootDropItem.probabilityRangeFrom = currentProbabilityWeightMaximum;
			currentProbabilityWeightMaximum += lootDropItem.probabilityWeight;
			lootDropItem.probabilityRangeTo = currentProbabilityWeightMaximum;
		}

		float pickedNumber = Random.Range(0, currentProbabilityWeightMaximum);

		foreach (DropTable<T> lootDropItem in dropTableProbabilities)
		{
			if (pickedNumber > lootDropItem.probabilityRangeFrom && pickedNumber < lootDropItem.probabilityRangeTo)
			{
				return lootDropItem.item;
			}
		}

		return dropTableProbabilities[0].item;
	}
}