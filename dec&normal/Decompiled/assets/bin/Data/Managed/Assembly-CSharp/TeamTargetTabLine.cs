using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009C6 RID: 2502
public class TeamTargetTabLine : MonoBehaviour
{
	// Token: 0x06004737 RID: 18231 RVA: 0x0016B114 File Offset: 0x00169314
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004738 RID: 18232 RVA: 0x0016B120 File Offset: 0x00169320
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x17000FB3 RID: 4019
	// (get) Token: 0x06004739 RID: 18233 RVA: 0x0016B140 File Offset: 0x00169340
	public string Key
	{
		get
		{
			return this.mKey;
		}
	}

	// Token: 0x17000FB4 RID: 4020
	// (get) Token: 0x0600473A RID: 18234 RVA: 0x0016B148 File Offset: 0x00169348
	public bool HasSubLine
	{
		get
		{
			return this.mHasSub;
		}
	}

	// Token: 0x0600473B RID: 18235 RVA: 0x0016B150 File Offset: 0x00169350
	public void Reset(string title, List<string> subTitle, string key, List<string> subKey, DelegateDefine.StringGameObjectDelegate clickFunc)
	{
		this.TitleLabel.text = title;
		if (!string.IsNullOrEmpty(key))
		{
			this.mKey = key;
			this.onClickTab = clickFunc;
		}
		if (subTitle != null && subTitle.Count > 0)
		{
			this.mHasSub = true;
			UnityVersionUtil.SetActiveRecursive(this.ArrowPicTw.gameObject, true);
			this.ArrowPicTw.ResetToBeginning();
			UnityVersionUtil.SetActiveRecursive(this.SubTabRootTw.gameObject, true);
			int num = subTitle.Count - this.SubLineList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.SubLineList[0].gameObject) as GameObject;
					gameObject.transform.parent = this.SubTabGride.transform;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					TeamTargetTabSubLine component = gameObject.GetComponent<TeamTargetTabSubLine>();
					this.SubLineList.Add(component);
				}
				this.SubTabGride.Reposition();
			}
			for (int j = 0; j < this.SubLineList.Count; j++)
			{
				if (j < subTitle.Count)
				{
					UnityVersionUtil.SetActiveRecursive(this.SubLineList[j].gameObject, true);
					this.SubLineList[j].Reset(subTitle[j], subKey[j], clickFunc);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.SubLineList[j].gameObject, false);
				}
			}
		}
		else
		{
			this.mHasSub = false;
			UnityVersionUtil.SetActiveRecursive(this.ArrowPicTw.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.SubTabRootTw.gameObject, false);
		}
		this.SubTabRootTw.ResetToBeginning();
		this.IsClose = true;
	}

	// Token: 0x0600473C RID: 18236 RVA: 0x0016B32C File Offset: 0x0016952C
	public void OnClickTab()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.CREATE_TEAM_CHOOSE_COPY)
		{
			this.CheckTutorialEvent();
		}
		if (!this.mHasSub)
		{
			if (this.onClickTab != null)
			{
				this.onClickTab(this.mKey, base.gameObject);
			}
		}
		else if (this.IsClose)
		{
			this.IsClose = false;
			this.ArrowPicTw.PlayForward();
			this.SubTabRootTw.PlayForward();
		}
		else
		{
			this.IsClose = true;
			this.ArrowPicTw.PlayReverse();
			this.SubTabRootTw.PlayReverse();
		}
	}

	// Token: 0x04003462 RID: 13410
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003463 RID: 13411
	public UILabel TitleLabel;

	// Token: 0x04003464 RID: 13412
	public TweenRotation ArrowPicTw;

	// Token: 0x04003465 RID: 13413
	public TweenScale SubTabRootTw;

	// Token: 0x04003466 RID: 13414
	public UIGrid SubTabGride;

	// Token: 0x04003467 RID: 13415
	public UISprite BottomPic;

	// Token: 0x04003468 RID: 13416
	public List<TeamTargetTabSubLine> SubLineList;

	// Token: 0x04003469 RID: 13417
	private DelegateDefine.StringGameObjectDelegate onClickTab;

	// Token: 0x0400346A RID: 13418
	private string mKey;

	// Token: 0x0400346B RID: 13419
	private bool IsClose;

	// Token: 0x0400346C RID: 13420
	private bool mHasSub;
}
