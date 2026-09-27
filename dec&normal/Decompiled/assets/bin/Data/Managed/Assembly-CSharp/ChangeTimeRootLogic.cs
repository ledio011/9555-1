using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000926 RID: 2342
public class ChangeTimeRootLogic : SingletonUnity<ChangeTimeRootLogic>
{
	// Token: 0x0600410B RID: 16651 RVA: 0x001345D4 File Offset: 0x001327D4
	public void ResetGuildDance(long[] timelist, int select, string name, int duringtime)
	{
		this.ProSelect = select;
		this.CurTimes = timelist;
		this.CurSelect = select;
		this.DuringTime = duringtime;
		this.infoLabel.text = StrDictionary.GetDictionaryString("#{105099}", new object[]
		{
			name
		});
		if (timelist != null)
		{
			int num = timelist.Length - this.TimeList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.TimeList[0].gameObject) as GameObject;
					gameObject.transform.parent = this.ParentGrid.transform;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.name = string.Format("{0:d2}", this.TimeList.Count);
					this.TimeList.Add(gameObject.GetComponent<TimeItem>());
				}
			}
		}
		for (int j = 0; j < this.TimeList.Count; j++)
		{
			if (j < this.CurTimes.Length)
			{
				NGUITools.SetActive(this.TimeList[j].gameObject, true);
				this.TimeList[j].Reset(this.CurTimes[j], j, this.ProSelect, new DelegateDefine.OneIntParamDelegate(this.OnClickItem));
			}
			else
			{
				NGUITools.SetActive(this.TimeList[j].gameObject, false);
			}
		}
		this.panelView.ResetPosition();
		this.ParentGrid.Reposition();
	}

	// Token: 0x0600410C RID: 16652 RVA: 0x00134774 File Offset: 0x00132974
	public void OnClickConfirmBtn()
	{
		if (this.CurSelect != this.ProSelect)
		{
			if (TimeTools.IsTimeRange(this.CurTimes[this.CurSelect], this.CurTimes[this.CurSelect] + (long)this.DuringTime))
			{
				NoticeLogic.AddNotifyData("#{105101}", true, false);
			}
			else
			{
				update_guild_dance_time.request request = new update_guild_dance_time.request();
				request.index = (long)(this.CurSelect + 1);
				NetLogic.GetInstance().Send<Protocol.update_guild_dance_time>(request, null);
				SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChangeTimeRoot);
			}
		}
		else
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChangeTimeRoot);
		}
	}

	// Token: 0x0600410D RID: 16653 RVA: 0x00134814 File Offset: 0x00132A14
	public void OnClickCancelBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.ChangeTimeRoot);
	}

	// Token: 0x0600410E RID: 16654 RVA: 0x00134828 File Offset: 0x00132A28
	public void OnClickItem(int select)
	{
		if (this.CurSelect == select)
		{
			return;
		}
		this.CurSelect = select;
		for (int i = 0; i < this.TimeList.Count; i++)
		{
			this.TimeList[i].UpdateSelect(select);
		}
	}

	// Token: 0x04002CBA RID: 11450
	public List<TimeItem> TimeList;

	// Token: 0x04002CBB RID: 11451
	public UIGrid ParentGrid;

	// Token: 0x04002CBC RID: 11452
	public UIScrollView panelView;

	// Token: 0x04002CBD RID: 11453
	private int ProSelect;

	// Token: 0x04002CBE RID: 11454
	private long[] CurTimes;

	// Token: 0x04002CBF RID: 11455
	private int CurSelect;

	// Token: 0x04002CC0 RID: 11456
	public UILabel infoLabel;

	// Token: 0x04002CC1 RID: 11457
	private int DuringTime;
}
