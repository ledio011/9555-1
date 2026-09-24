using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
[AddComponentMenu("NGUI/Examples/Equip Items")]
public class EquipItems : MonoBehaviour
{
	// Token: 0x0600006E RID: 110 RVA: 0x000040B0 File Offset: 0x000022B0
	private void Start()
	{
		if (this.itemIDs != null && this.itemIDs.Length > 0)
		{
			InvEquipment invEquipment = base.GetComponent<InvEquipment>();
			if (invEquipment == null)
			{
				invEquipment = base.gameObject.AddComponent<InvEquipment>();
			}
			int num = 12;
			int i = 0;
			int num2 = this.itemIDs.Length;
			while (i < num2)
			{
				int num3 = this.itemIDs[i];
				InvBaseItem invBaseItem = InvDatabase.FindByID(num3);
				if (invBaseItem != null)
				{
					invEquipment.Equip(new InvGameItem(num3, invBaseItem)
					{
						quality = (InvGameItem.Quality)Random.Range(0, num),
						itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel)
					});
				}
				else
				{
					Debug.LogWarning("Can't resolve the item ID of " + num3);
				}
				i++;
			}
		}
		Object.Destroy(this);
	}

	// Token: 0x04000065 RID: 101
	public int[] itemIDs;
}
