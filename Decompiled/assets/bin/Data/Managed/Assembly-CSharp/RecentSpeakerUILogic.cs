using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class RecentSpeakerUILogic : MonoBehaviour
{
	// Token: 0x06000B93 RID: 2963 RVA: 0x000544E8 File Offset: 0x000526E8
	public void Reset(List<RecentSpeaker> speakerList)
	{
		this.mRecentSpeakerList = speakerList;
		int num = speakerList.Count - this.mSpeakerBtnList.Count;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = Object.Instantiate(this.SpeakerBtnPrefab.gameObject) as GameObject;
			gameObject.transform.parent = this.SpeakerBtnPrefab.transform.parent;
			gameObject.transform.localScale = Vector3.one;
			this.mSpeakerBtnList.Add(gameObject.GetComponent<RecentSpeakerBtnLogic>());
		}
		int num2 = 20;
		int num3 = 0;
		for (int j = 0; j < this.mSpeakerBtnList.Count; j++)
		{
			if (j < speakerList.Count)
			{
				this.mSpeakerBtnList[j].Reset(this.mRecentSpeakerList[this.mRecentSpeakerList.Count - 1 - j]);
				this.mSpeakerBtnList[j].transform.localPosition = new Vector3((float)(num3 + 5 + this.mSpeakerBtnList[j].BottomSprite.width / 2), (float)num2, 0f);
				num3 += this.mSpeakerBtnList[j].BottomSprite.width + 5;
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.mSpeakerBtnList[j].gameObject, false);
			}
		}
		UnityVersionUtil.SetActiveRecursive(this.SpeakerBtnPrefab.gameObject, false);
		if (speakerList.Count == 1)
		{
			this.OnClickBtn(speakerList[0]);
		}
		else
		{
			this.OnClickBtn(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.RecentSpeakers.GetLastSpeaker());
		}
		vp_Timer.In(0.1f, delegate()
		{
			this.mPanel.SetDirty();
		}, null);
		this.ScrollView.ResetPosition();
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x000546C0 File Offset: 0x000528C0
	public void OnClickBtn(RecentSpeaker speakerInfo)
	{
		if (speakerInfo == null)
		{
			UnityVersionUtil.SetActiveRecursive(this.ChoosedPic.gameObject, false);
			return;
		}
		UnityVersionUtil.SetActiveRecursive(this.ChoosedPic.gameObject, true);
		for (int i = 0; i < this.mSpeakerBtnList.Count; i++)
		{
			if (speakerInfo == this.mSpeakerBtnList[i].SpeakerInfo)
			{
				this.ChoosedPic.transform.parent = this.mSpeakerBtnList[i].transform;
				this.ChoosedPic.transform.transform.localPosition = Vector3.zero;
				this.ChoosedPic.width = this.mSpeakerBtnList[i].BottomSprite.width;
				this.ChoosedPic.height = this.mSpeakerBtnList[i].BottomSprite.height;
				UnityVersionUtil.SetActiveRecursive(this.mSpeakerBtnList[i].TipPic.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.mSpeakerBtnList[i].SelfTipPic.gameObject, false);
				break;
			}
		}
	}

	// Token: 0x04000A5E RID: 2654
	private List<RecentSpeaker> mRecentSpeakerList;

	// Token: 0x04000A5F RID: 2655
	public List<RecentSpeakerBtnLogic> mSpeakerBtnList;

	// Token: 0x04000A60 RID: 2656
	public RecentSpeakerBtnLogic SpeakerBtnPrefab;

	// Token: 0x04000A61 RID: 2657
	public UISprite ChoosedPic;

	// Token: 0x04000A62 RID: 2658
	public UIScrollView ScrollView;

	// Token: 0x04000A63 RID: 2659
	public UIPanel mPanel;
}
