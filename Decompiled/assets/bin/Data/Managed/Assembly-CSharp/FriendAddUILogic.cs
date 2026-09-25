using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SprotoType;
using UnityEngine;

// Token: 0x020009A7 RID: 2471
public class FriendAddUILogic : SingletonUnity<FriendAddUILogic>
{
	// Token: 0x0600461B RID: 17947 RVA: 0x00162824 File Offset: 0x00160A24
	protected override void Awake()
	{
		base.Awake();
		this.InitList();
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x00162834 File Offset: 0x00160A34
	public void Reset()
	{
		this.InitList();
	}

	// Token: 0x0600461D RID: 17949 RVA: 0x0016283C File Offset: 0x00160A3C
	private void OnEnable()
	{
		this.curOpenType = FriendAddUILogic.OPEN_TYPE.RANDOM_TYPE;
		this.InitList();
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x0016284C File Offset: 0x00160A4C
	private void InitList()
	{
		for (int i = 0; i < this.FriendItems.Count; i++)
		{
			NGUITools.SetActive(this.FriendItems[i].gameObject, false);
		}
	}

	// Token: 0x0600461F RID: 17951 RVA: 0x0016288C File Offset: 0x00160A8C
	public void SetOpenType(FriendAddUILogic.OPEN_TYPE type)
	{
		this.curOpenType = type;
	}

	// Token: 0x06004620 RID: 17952 RVA: 0x00162898 File Offset: 0x00160A98
	public void UpdateFriendList()
	{
		NGUITools.SetActive(this.refreshObj, this.curOpenType == FriendAddUILogic.OPEN_TYPE.RANDOM_TYPE);
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		List<friend_info> list;
		if (this.curOpenType == FriendAddUILogic.OPEN_TYPE.RANDOM_TYPE)
		{
			list = new List<friend_info>(friendInfo.MainPlayerRandomFriendDic.Values);
		}
		else
		{
			list = new List<friend_info>(friendInfo.MainPlayerSearchFriendDic.Values);
		}
		int num = list.Count - this.FriendItems.Count;
		int count = this.FriendItems.Count;
		this.curListFriendInfo = list;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.FriendItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0}", count + i);
				FriendAddItemLogic component = gameObject.GetComponent<FriendAddItemLogic>();
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
		this.grid.Reposition();
	}

	// Token: 0x06004621 RID: 17953 RVA: 0x00162A4C File Offset: 0x00160C4C
	private void ResetItemLine(FriendAddItemLogic itemLogic, int idx)
	{
		if (idx < this.curListFriendInfo.Count)
		{
			itemLogic.UpdateFriendInfo(this.curListFriendInfo[idx]);
		}
	}

	// Token: 0x06004622 RID: 17954 RVA: 0x00162A74 File Offset: 0x00160C74
	public void OnClickRefresh()
	{
		NetLogic.GetInstance().Send<Protocol.req_random_online_character_list>(null, null);
	}

	// Token: 0x06004623 RID: 17955 RVA: 0x00162A84 File Offset: 0x00160C84
	public void OnClickSearch()
	{
		string text = this.uiInput.value;
		text = text.Trim();
		if (string.IsNullOrEmpty(text))
		{
			NoticeLogic.AddNotifyData("#{100274}", true, false);
			return;
		}
		if (!this.isRightName(text))
		{
			NoticeLogic.AddNotifyData("#{100222}", true, false);
			return;
		}
		search_online_character_by_name.request request = new search_online_character_by_name.request();
		request.name = text;
		NetLogic.GetInstance().Send<Protocol.search_online_character_by_name>(request, null);
		this.curOpenType = FriendAddUILogic.OPEN_TYPE.SEARCH_TYPE;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Friend", "search_times");
	}

	// Token: 0x06004624 RID: 17956 RVA: 0x00162B10 File Offset: 0x00160D10
	private bool isRightName(string namestr)
	{
		string text = "^[a-zA-Z0-9]{1}([a-zA-Z0-9]|[ _]){4,15}$";
		return Regex.IsMatch(namestr, text);
	}

	// Token: 0x06004625 RID: 17957 RVA: 0x00162B34 File Offset: 0x00160D34
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.FriendAddUILogic);
	}

	// Token: 0x0400332C RID: 13100
	public List<FriendAddItemLogic> FriendItems = new List<FriendAddItemLogic>();

	// Token: 0x0400332D RID: 13101
	private List<friend_info> curListFriendInfo = new List<friend_info>();

	// Token: 0x0400332E RID: 13102
	public UIGrid grid;

	// Token: 0x0400332F RID: 13103
	public UIInput uiInput;

	// Token: 0x04003330 RID: 13104
	public GameObject refreshObj;

	// Token: 0x04003331 RID: 13105
	private FriendAddUILogic.OPEN_TYPE curOpenType;

	// Token: 0x020009A8 RID: 2472
	public enum OPEN_TYPE
	{
		// Token: 0x04003333 RID: 13107
		RANDOM_TYPE,
		// Token: 0x04003334 RID: 13108
		SEARCH_TYPE
	}
}
