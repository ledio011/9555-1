using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class TargetCarMissionData
{
	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06001035 RID: 4149 RVA: 0x000663E8 File Offset: 0x000645E8
	public Vector3 Pos
	{
		get
		{
			float num = (float)this.PosX / 100f;
			float num2 = (float)this.PosZ / 100f;
			return new Vector3(num, SceneManager.GetHitHeight(num, num2), num2);
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06001036 RID: 4150 RVA: 0x00066420 File Offset: 0x00064620
	public Vector3 GetRot
	{
		get
		{
			if (this.mRot == null)
			{
				string[] array = this.Rot.Split(new char[]
				{
					'#'
				});
				this.mRot = new float[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.mRot[i] = (float)int.Parse(array[i]) / 100f;
				}
			}
			return new Vector3(this.mRot[0], this.mRot[1], this.mRot[2]);
		}
	}

	// Token: 0x04001340 RID: 4928
	public string ID;

	// Token: 0x04001341 RID: 4929
	public string SceneID;

	// Token: 0x04001342 RID: 4930
	public int PosX;

	// Token: 0x04001343 RID: 4931
	public int PosZ;

	// Token: 0x04001344 RID: 4932
	public string Rot;

	// Token: 0x04001345 RID: 4933
	public string Mountid;

	// Token: 0x04001346 RID: 4934
	public int RequireNum;

	// Token: 0x04001347 RID: 4935
	private float[] mRot;
}
