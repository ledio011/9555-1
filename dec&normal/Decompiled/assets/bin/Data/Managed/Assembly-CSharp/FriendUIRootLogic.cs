using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009AB RID: 2475
public class FriendUIRootLogic : SingletonUnity<FriendUIRootLogic>
{
	// Token: 0x06004636 RID: 17974 RVA: 0x00163254 File Offset: 0x00161454
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06004637 RID: 17975 RVA: 0x0016329C File Offset: 0x0016149C
	public void EnableReset()
	{
		for (int i = 0; i < this.FriendItems.Count; i++)
		{
			NGUITools.SetActive(this.FriendItems[i].gameObject, false);
		}
	}

	// Token: 0x06004638 RID: 17976 RVA: 0x001632DC File Offset: 0x001614DC
	public void UpdateFriendList()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<friend_info> list = new List<friend_info>(playerData.FriendInfo.MainPlayerFriendDic.Values);
		list = FriendInfo.SortFrientList(list);
		int num = Mathf.Min(list.Count, this.LineCount) - this.FriendItems.Count;
		int count = this.FriendItems.Count;
		this.curListFriendInfo = list;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.FriendItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0}", count + i);
				FriendItemLogic component = gameObject.GetComponent<FriendItemLogic>();
				if (component != null)
				{
					component.transform.parent = this.FriendItems[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.FriendItems.Add(component);
				}
			}
		}
		for (int j = 0; j < this.FriendItems.Count; j++)
		{
			NGUITools.SetActive(this.FriendItems[j].gameObject, j < list.Count);
			if (j < list.Count)
			{
				this.ResetItemLine(this.FriendItems[j], j);
			}
		}
		this.uiWrapContent.minIndex = 1 - list.Count;
		this.uiWrapContent.maxIndex = 0;
		this.WrapContentBottomWidget.height = list.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		this.Times.text = string.Format("{0}:{1}", StrDictionary.GetDictionaryString("#{100220}", new object[0]), playerData.FriendInfo.FriendCount);
	}

	// Token: 0x06004639 RID: 17977 RVA: 0x001634F4 File Offset: 0x001616F4
	public void OnClikcTips()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ExcInfoRoot, delegate
		{
			SingletonUnity<ExcInfoRootLogic>.Instance.Reset("#{101533}", "#{100269}", null, new object[0]);
		}, null);
	}

	// Token: 0x0600463A RID: 17978 RVA: 0x00163524 File Offset: 0x00161724
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		FriendItemLogic itemLogic = this.FriendItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x0600463B RID: 17979 RVA: 0x0016354C File Offset: 0x0016174C
	private void ResetItemLine(FriendItemLogic itemLogic, int idx)
	{
		if (idx < this.curListFriendInfo.Count)
		{
			itemLogic.UpdateFriendInfo(this.curListFriendInfo[idx]);
		}
	}

	// Token: 0x0600463C RID: 17980 RVA: 0x00163574 File Offset: 0x00161774
	public void OnClickAdd()
	{
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (friendInfo.IsCanAddFriend())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.FriendAddUILogic, delegate(bool bSuccess, object param)
			{
				SingletonUnity<FriendAddUILogic>.Instance.Reset();
				NetLogic.GetInstance().Send<Protocol.req_random_online_character_list>(null, null);
			}, null);
		}
		else
		{
			NoticeLogic.AddNotifyData("#{100227}", true, false);
		}
	}

	// Token: 0x0400333F RID: 13119
	public int LineCount = 6;

	// Token: 0x04003340 RID: 13120
	public List<FriendItemLogic> FriendItems = new List<FriendItemLogic>();

	// Token: 0x04003341 RID: 13121
	public UILabel Times;

	// Token: 0x04003342 RID: 13122
	public UIWrapContentNew uiWrapContent;

	// Token: 0x04003343 RID: 13123
	public UIScrollView uiScrollView;

	// Token: 0x04003344 RID: 13124
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04003345 RID: 13125
	private List<friend_info> curListFriendInfo = new List<friend_info>();
}
