using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000832 RID: 2098
public class BombSustainedRangeObj : SustainedRangeObj
{
	// Token: 0x06003567 RID: 13671 RVA: 0x000D9584 File Offset: 0x000D7784
	public void Reset(Vector3 startPos, Vector3 targetPos, ObjCharacter skillSender, List<ObjCharacter> targetList, EffInfoData effInfoData, float damageInterval = 1f)
	{
		this.mTargetPos = targetPos;
		this.mDuartion = 1f;
		this.mUseTime = 0f;
		this.mStartPos = startPos;
		base.transform.position = startPos;
		base.ResetSustainedRange(skillSender, targetList, effInfoData, damageInterval);
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x000D95D0 File Offset: 0x000D77D0
	private void Update()
	{
		this.mUseTime += Time.deltaTime;
		this.percent = this.mUseTime / this.mDuartion;
		if (this.percent <= 1f)
		{
			float num = 3f - 12f * (this.percent - 0.5f) * (this.percent - 0.5f) + 0.1f;
			base.transform.position = Vector3.Lerp(this.mStartPos, this.mTargetPos, this.percent) + Vector3.up * num;
		}
		else
		{
			if (UnityVersionUtil.IsActive(this.MeshObj.gameObject))
			{
				this.MeshObj.active = false;
				base.transform.position = this.mTargetPos + Vector3.up * 0.1f;
			}
			base.StartDamage();
			base.UpdateDamage();
		}
	}

	// Token: 0x06003569 RID: 13673 RVA: 0x000D96C8 File Offset: 0x000D78C8
	public override void OnRecycle()
	{
		UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
		Singleton<ObjManager>.Instance.RecycleBombSustainedRangeObj(this);
	}

	// Token: 0x040022D7 RID: 8919
	public GameObject MeshObj;

	// Token: 0x040022D8 RID: 8920
	private Vector3 mStartPos;

	// Token: 0x040022D9 RID: 8921
	private float mDuartion;

	// Token: 0x040022DA RID: 8922
	private float mUseTime;

	// Token: 0x040022DB RID: 8923
	private Vector3 mTargetPos;

	// Token: 0x040022DC RID: 8924
	private float percent;
}
