using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009CC RID: 2508
public class TianTiResultRootLogic : SingletonUnity<TianTiResultRootLogic>
{
	// Token: 0x06004766 RID: 18278 RVA: 0x0016C0D0 File Offset: 0x0016A2D0
	public void Reset(tiantti_result.request request)
	{
		if (request.win)
		{
			if (request.HasBestRankPos)
			{
				this.titleLabel.text = StrDictionary.GetDictionaryString("#{101009}", new object[0]);
				NGUITools.SetActive(this.RecordnameLabel.gameObject, true);
				this.RecordnameLabel.text = StrDictionary.GetDictionaryString("#{101009}", new object[0]);
				this.bestLabel.text = string.Format("{0}", request.bestRankPos);
			}
			else
			{
				NGUITools.SetActive(this.RecordnameLabel.gameObject, false);
				this.titleLabel.text = StrDictionary.GetDictionaryString("#{101012}", new object[0]);
			}
			NGUITools.SetActive(this.ranknameLabel.gameObject, true);
			this.ranknameLabel.text = StrDictionary.GetDictionaryString("#{101010}", new object[0]);
			this.rankLabel.text = string.Format("{0}", request.rankPos2);
			this.deltaLabel.text = string.Format("(   {0})", request.rankPos1 - request.rankPos2);
			NGUITools.SetActive(this.deltaLabel.gameObject, request.rankPos1 - request.rankPos2 > 0L);
			this.midline.enabled = true;
			this.rewardOffset.localPosition = Vector3.zero;
		}
		else
		{
			this.titleLabel.text = StrDictionary.GetDictionaryString("#{101013}", new object[0]);
			NGUITools.SetActive(this.RecordnameLabel.gameObject, false);
			NGUITools.SetActive(this.ranknameLabel.gameObject, false);
			this.midline.enabled = false;
			this.rewardOffset.localPosition = new Vector3(0f, 36f, 0f);
		}
		if (request.HasItems)
		{
			this.showrewarditem.ShowRewards(request.items);
		}
	}

	// Token: 0x06004767 RID: 18279 RVA: 0x0016C2C4 File Offset: 0x0016A4C4
	public void OnClickExitBtn()
	{
		NetLogic.GetInstance().Send<Protocol.leave_copy_scene>(null, null);
	}

	// Token: 0x0400349C RID: 13468
	public UILabel titleLabel;

	// Token: 0x0400349D RID: 13469
	public UILabel RecordnameLabel;

	// Token: 0x0400349E RID: 13470
	public UILabel bestLabel;

	// Token: 0x0400349F RID: 13471
	public UILabel ranknameLabel;

	// Token: 0x040034A0 RID: 13472
	public UILabel rankLabel;

	// Token: 0x040034A1 RID: 13473
	public UILabel deltaLabel;

	// Token: 0x040034A2 RID: 13474
	public Transform rewardOffset;

	// Token: 0x040034A3 RID: 13475
	public UISprite midline;

	// Token: 0x040034A4 RID: 13476
	public ShowRewardItems showrewarditem;
}
