using System;
using UnityEngine;

// Token: 0x02000A2F RID: 2607
public class NewMiniMapActivityObj : MonoBehaviour
{
	// Token: 0x06004C06 RID: 19462 RVA: 0x0019AF28 File Offset: 0x00199128
	public void Reset(string IconName, string missionStatePicName, bool isMission)
	{
		this.cacheTrans = base.transform;
		this.parentTrans = this.cacheTrans.parent;
		this.IconPic.spriteName = IconName;
		this.IconPic.MakePixelPerfect();
		this.IconPic.width = (int)((float)this.IconPic.width * 0.6f);
		this.IconPic.height = (int)((float)this.IconPic.height * 0.6f);
		if (string.IsNullOrEmpty(missionStatePicName))
		{
			this.MissionStatePic.enabled = false;
		}
		else
		{
			this.MissionStatePic.enabled = true;
			this.MissionStatePic.spriteName = missionStatePicName;
			this.MissionStatePic.MakePixelPerfect();
			this.MissionStatePic.width /= 3;
			this.MissionStatePic.height /= 3;
		}
		if (isMission)
		{
			this.MissionStatePic.pivot = UIWidget.Pivot.Left;
			this.MissionStatePic.transform.localPosition = new Vector3(6f, 0f, 0f);
			this.MissionStatePic.color = Color.white;
		}
		else
		{
			this.MissionStatePic.pivot = UIWidget.Pivot.Center;
			this.MissionStatePic.transform.localPosition = Vector3.zero;
			this.MissionStatePic.color = Color.red;
		}
	}

	// Token: 0x06004C07 RID: 19463 RVA: 0x0019B088 File Offset: 0x00199288
	private void Update()
	{
		if (this.parentTrans != null)
		{
			this.cacheTrans.localEulerAngles = new Vector3(0f, 0f, -this.parentTrans.localEulerAngles.z);
		}
	}

	// Token: 0x040039B7 RID: 14775
	public UISprite IconPic;

	// Token: 0x040039B8 RID: 14776
	public UISprite MissionStatePic;

	// Token: 0x040039B9 RID: 14777
	private Transform parentTrans;

	// Token: 0x040039BA RID: 14778
	private Transform cacheTrans;
}
