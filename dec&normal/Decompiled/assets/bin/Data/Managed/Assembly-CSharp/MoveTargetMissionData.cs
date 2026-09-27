using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class MoveTargetMissionData
{
	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00064720 File Offset: 0x00062920
	public List<Vector3> TargetPointList
	{
		get
		{
			if (this.mTargetPointList == null)
			{
				this.mTargetPointList = new List<Vector3>();
				string[] array = this.TargetPoint.Split(new char[]
				{
					'|'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'#'
					});
					int num = int.Parse(array2[0]);
					int num2 = int.Parse(array2[1]);
					Vector3 vector;
					vector..ctor((float)num / 100f, 0f, (float)num2 / 100f);
					this.mTargetPointList.Add(vector);
				}
			}
			return this.mTargetPointList;
		}
	}

	// Token: 0x0400112C RID: 4396
	public string ID = string.Empty;

	// Token: 0x0400112D RID: 4397
	public string MapId = string.Empty;

	// Token: 0x0400112E RID: 4398
	public int TargetNum;

	// Token: 0x0400112F RID: 4399
	public string TargetPoint = string.Empty;

	// Token: 0x04001130 RID: 4400
	public int LimitTime = 120;

	// Token: 0x04001131 RID: 4401
	private List<Vector3> mTargetPointList;
}
