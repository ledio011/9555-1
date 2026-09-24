using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008AB RID: 2219
public class AnimationFit : MonoBehaviour
{
	// Token: 0x06003BCF RID: 15311 RVA: 0x00104C8C File Offset: 0x00102E8C
	private void Awake()
	{
		for (int i = 0; i < this.AnimNameList.Count; i++)
		{
			this.AnimNameDict.Add(this.AnimNameList[i], 1);
		}
		this.AnimNameDict.Add("die", 1);
	}

	// Token: 0x06003BD0 RID: 15312 RVA: 0x00104CE0 File Offset: 0x00102EE0
	public void UpdateAnim(string name, float crossTime)
	{
		if (this.AnimNameDict.ContainsKey(name))
		{
			this.targetPos = Vector3.zero;
		}
		else
		{
			this.targetPos = Vector3.up * this.defaultHight;
		}
		this.startPos = base.transform.localPosition;
		this.mCrossTime = crossTime;
		this.mCountTime = 0f;
	}

	// Token: 0x06003BD1 RID: 15313 RVA: 0x00104D48 File Offset: 0x00102F48
	private void Update()
	{
		if (this.mCrossTime > 0f)
		{
			this.mCountTime += Time.deltaTime;
			base.transform.localPosition = Vector3.Lerp(this.startPos, this.targetPos, this.mCountTime / this.mCrossTime);
			if (this.mCountTime >= this.mCrossTime)
			{
				this.mCrossTime = -1f;
			}
		}
	}

	// Token: 0x0400271E RID: 10014
	public bool setFlag = true;

	// Token: 0x0400271F RID: 10015
	public float defaultHight;

	// Token: 0x04002720 RID: 10016
	public List<string> AnimNameList = new List<string>();

	// Token: 0x04002721 RID: 10017
	private Dictionary<string, int> AnimNameDict = new Dictionary<string, int>();

	// Token: 0x04002722 RID: 10018
	private float mCrossTime;

	// Token: 0x04002723 RID: 10019
	private float mCountTime;

	// Token: 0x04002724 RID: 10020
	private Vector3 targetPos;

	// Token: 0x04002725 RID: 10021
	private Vector3 startPos;
}
