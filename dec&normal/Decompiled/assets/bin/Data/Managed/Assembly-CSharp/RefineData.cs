using System;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class RefineData
{
	// Token: 0x06000FE2 RID: 4066 RVA: 0x00065070 File Offset: 0x00063270
	public int GetCombatValue()
	{
		float num = 0f;
		num += (float)this.Value1 * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(this.Stat1);
		if (this.Stat2 != 0)
		{
			num += (float)this.Value2 * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(this.Stat2);
		}
		if (this.Stat3 != 0)
		{
			num += (float)this.Value3 * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(this.Stat3);
		}
		if (this.Stat4 != 0)
		{
			num += (float)this.Value4 * GameDefine.GET_ATTRIBUTE_COMBAT_VAL(this.Stat4);
		}
		return Mathf.FloorToInt(num);
	}

	// Token: 0x040011D1 RID: 4561
	public string ID = string.Empty;

	// Token: 0x040011D2 RID: 4562
	public int Job = -1;

	// Token: 0x040011D3 RID: 4563
	public int Part = -1;

	// Token: 0x040011D4 RID: 4564
	public int Lv;

	// Token: 0x040011D5 RID: 4565
	public int Stat1;

	// Token: 0x040011D6 RID: 4566
	public int Value1;

	// Token: 0x040011D7 RID: 4567
	public int Stat2;

	// Token: 0x040011D8 RID: 4568
	public int Value2;

	// Token: 0x040011D9 RID: 4569
	public int Stat3;

	// Token: 0x040011DA RID: 4570
	public int Value3;

	// Token: 0x040011DB RID: 4571
	public int Stat4;

	// Token: 0x040011DC RID: 4572
	public int Value4;

	// Token: 0x040011DD RID: 4573
	public string CostId1 = string.Empty;

	// Token: 0x040011DE RID: 4574
	public int Cost1;

	// Token: 0x040011DF RID: 4575
	public string CostId2 = string.Empty;

	// Token: 0x040011E0 RID: 4576
	public int Cost2;

	// Token: 0x040011E1 RID: 4577
	public int MoneyType;

	// Token: 0x040011E2 RID: 4578
	public int MoneyCost;

	// Token: 0x040011E3 RID: 4579
	public string SaftyId = string.Empty;

	// Token: 0x040011E4 RID: 4580
	public int SaftyCost;

	// Token: 0x040011E5 RID: 4581
	public int Chance;

	// Token: 0x040011E6 RID: 4582
	public string ICON = string.Empty;
}
