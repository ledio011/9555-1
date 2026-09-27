using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000019 RID: 25
[AddComponentMenu("NGUI/Examples/Equip Random Item")]
public class EquipRandomItem : MonoBehaviour
{
	// Token: 0x06000070 RID: 112 RVA: 0x00004194 File Offset: 0x00002394
	private void OnClick()
	{
		if (this.equipment == null)
		{
			return;
		}
		List<InvBaseItem> items = InvDatabase.list[0].items;
		if (items.Count == 0)
		{
			return;
		}
		int num = 12;
		int num2 = Random.Range(0, items.Count);
		InvBaseItem invBaseItem = items[num2];
		InvGameItem invGameItem = new InvGameItem(num2, invBaseItem);
		invGameItem.quality = (InvGameItem.Quality)Random.Range(0, num);
		invGameItem.itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel);
		this.equipment.Equip(invGameItem);
	}

	// Token: 0x04000066 RID: 102
	public InvEquipment equipment;
}
