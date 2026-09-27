using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
[Serializable]
public class InvGameItem
{
	// Token: 0x060000A2 RID: 162 RVA: 0x000051AC File Offset: 0x000033AC
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000051CC File Offset: 0x000033CC
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000A4 RID: 164 RVA: 0x000051FC File Offset: 0x000033FC
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005204 File Offset: 0x00003404
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005234 File Offset: 0x00003434
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005274 File Offset: 0x00003474
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005370 File Offset: 0x00003570
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result..ctor(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result..ctor(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result..ctor(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result..ctor(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return result;
		}
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x000054B0 File Offset: 0x000036B0
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x040000A1 RID: 161
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x040000A2 RID: 162
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x040000A3 RID: 163
	public int itemLevel = 1;

	// Token: 0x040000A4 RID: 164
	private InvBaseItem mBaseItem;

	// Token: 0x02000025 RID: 37
	public enum Quality
	{
		// Token: 0x040000A6 RID: 166
		Broken,
		// Token: 0x040000A7 RID: 167
		Cursed,
		// Token: 0x040000A8 RID: 168
		Damaged,
		// Token: 0x040000A9 RID: 169
		Worn,
		// Token: 0x040000AA RID: 170
		Sturdy,
		// Token: 0x040000AB RID: 171
		Polished,
		// Token: 0x040000AC RID: 172
		Improved,
		// Token: 0x040000AD RID: 173
		Crafted,
		// Token: 0x040000AE RID: 174
		Superior,
		// Token: 0x040000AF RID: 175
		Enchanted,
		// Token: 0x040000B0 RID: 176
		Epic,
		// Token: 0x040000B1 RID: 177
		Legendary,
		// Token: 0x040000B2 RID: 178
		_LastDoNotUse
	}
}
