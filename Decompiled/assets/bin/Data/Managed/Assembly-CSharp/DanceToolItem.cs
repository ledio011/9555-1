using System;
using UnityEngine;

// Token: 0x020008AE RID: 2222
public class DanceToolItem : MonoBehaviour
{
	// Token: 0x06003BE1 RID: 15329 RVA: 0x00105508 File Offset: 0x00103708
	public void Init(string itemid, int num)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(itemid);
		if (itemDataByID != null)
		{
			NGUITools.SetActive(base.gameObject, true);
			this.IconSprite.spriteName = GameDefine.DanceToolName[itemid];
			this.NumLabel.text = num.ToString();
		}
		else
		{
			NGUITools.SetActive(base.gameObject, false);
		}
		this.CDSprite.fillAmount = 0f;
		this.ReamainTime = -1L;
	}

	// Token: 0x06003BE2 RID: 15330 RVA: 0x00105580 File Offset: 0x00103780
	public void SetCDTime(long time, long alltime)
	{
		this.ReamainTime = time - SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
		this.allTime = alltime;
		if (this.ReamainTime > 0L)
		{
			this.CDSprite.fillAmount = (float)this.ReamainTime / (float)this.allTime;
		}
		else
		{
			this.CDSprite.fillAmount = 0f;
		}
	}

	// Token: 0x06003BE3 RID: 15331 RVA: 0x001055E8 File Offset: 0x001037E8
	private void Update()
	{
		if (this.ReamainTime > 0L)
		{
			this.tempCountTime += Time.deltaTime;
			if (this.tempCountTime >= 1f)
			{
				this.ReamainTime -= 1L;
				this.tempCountTime -= 1f;
				this.CDSprite.fillAmount = (float)this.ReamainTime / (float)this.allTime;
			}
			if (this.ReamainTime <= 0L)
			{
				this.tempCountTime = 0f;
				this.CDSprite.fillAmount = 0f;
			}
		}
	}

	// Token: 0x04002726 RID: 10022
	public UISprite IconSprite;

	// Token: 0x04002727 RID: 10023
	public UILabel NumLabel;

	// Token: 0x04002728 RID: 10024
	public UISprite CDSprite;

	// Token: 0x04002729 RID: 10025
	public long ReamainTime;

	// Token: 0x0400272A RID: 10026
	private long allTime = 1L;

	// Token: 0x0400272B RID: 10027
	private float tempCountTime;
}
