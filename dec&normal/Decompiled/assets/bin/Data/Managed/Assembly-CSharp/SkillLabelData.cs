using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class SkillLabelData
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001022 RID: 4130 RVA: 0x000660B4 File Offset: 0x000642B4
	public List<string> ValueList
	{
		get
		{
			if (this.mValues == null)
			{
				this.mValues = new List<string>();
				string[] array = this.value1.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length; i++)
				{
					this.mValues.Add(array[i]);
				}
			}
			return this.mValues;
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001023 RID: 4131 RVA: 0x00066118 File Offset: 0x00064318
	public string[] ValueArray
	{
		get
		{
			if (this.mValueArray == null)
			{
				this.mValueArray = this.value1.Split(new char[]
				{
					'#'
				});
			}
			return this.mValueArray;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001024 RID: 4132 RVA: 0x00066148 File Offset: 0x00064348
	public Color LabelColor
	{
		get
		{
			if (this.labelCol == null)
			{
				this.labelCol = new List<int>();
				string[] array = this.color.Split(new char[]
				{
					'#'
				});
				for (int i = 0; i < array.Length; i++)
				{
					this.labelCol.Add(int.Parse(array[i]));
				}
			}
			return new Color((float)this.labelCol[0] / 255f, (float)this.labelCol[1] / 255f, (float)this.labelCol[2] / 255f, 1f);
		}
	}

	// Token: 0x040012C9 RID: 4809
	public string ID = string.Empty;

	// Token: 0x040012CA RID: 4810
	public string localization = string.Empty;

	// Token: 0x040012CB RID: 4811
	public string value1 = string.Empty;

	// Token: 0x040012CC RID: 4812
	private List<string> mValues;

	// Token: 0x040012CD RID: 4813
	private string[] mValueArray;

	// Token: 0x040012CE RID: 4814
	public string color = string.Empty;

	// Token: 0x040012CF RID: 4815
	public int score;

	// Token: 0x040012D0 RID: 4816
	private List<int> labelCol;
}
