using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200084A RID: 2122
public class CreateGangRootLogic : MonoBehaviour
{
	// Token: 0x060036A0 RID: 13984 RVA: 0x000E0F50 File Offset: 0x000DF150
	private void Start()
	{
		this.CenterOnChild.RegisterCenterOnEvent(new UICenterOnChild.CenterOnChildDelegate(this.OnCenterOnIcon));
	}

	// Token: 0x060036A1 RID: 13985 RVA: 0x000E0F6C File Offset: 0x000DF16C
	private void OnEnable()
	{
		this.CenterOnChild.CenterOn(this.IconList[0]);
	}

	// Token: 0x060036A2 RID: 13986 RVA: 0x000E0F88 File Offset: 0x000DF188
	private void OnCenterOnIcon(Transform target)
	{
		for (int i = 0; i < this.IconList.Count; i++)
		{
			if (this.IconList[i] == target)
			{
				this.mCurIconIndex = i;
				Debug.Log("mCurIconIndex :: " + this.mCurIconIndex);
				return;
			}
		}
	}

	// Token: 0x060036A3 RID: 13987 RVA: 0x000E0FEC File Offset: 0x000DF1EC
	public void OnClickCreateBtn()
	{
	}

	// Token: 0x060036A4 RID: 13988 RVA: 0x000E0FF0 File Offset: 0x000DF1F0
	public void OnClickLeftBtn()
	{
		this.mCurIconIndex = (this.mCurIconIndex + this.IconList.Count - 1) % this.IconList.Count;
		this.CenterOnChild.CenterOn(this.IconList[this.mCurIconIndex]);
	}

	// Token: 0x060036A5 RID: 13989 RVA: 0x000E1040 File Offset: 0x000DF240
	public void OnClickRightBtn()
	{
		this.mCurIconIndex = (this.mCurIconIndex + 1) % this.IconList.Count;
		this.CenterOnChild.CenterOn(this.IconList[this.mCurIconIndex]);
	}

	// Token: 0x040023FF RID: 9215
	public List<Transform> IconList;

	// Token: 0x04002400 RID: 9216
	public UICenterOnChild CenterOnChild;

	// Token: 0x04002401 RID: 9217
	private int mCurIconIndex;

	// Token: 0x04002402 RID: 9218
	public UIInput InputGangName;

	// Token: 0x04002403 RID: 9219
	private int MinNameCharNum = 5;

	// Token: 0x04002404 RID: 9220
	private int MaxNameCharNum = 36;
}
