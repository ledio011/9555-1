using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A36 RID: 2614
public class NewMessageUIRootLogic : SingletonUnity<NewMessageUIRootLogic>
{
	// Token: 0x06004C3C RID: 19516 RVA: 0x0019D4B0 File Offset: 0x0019B6B0
	public void ResetInfo(List<GameDefine.ACTIVITY_TYPE> messagelist)
	{
		NewMessageUIRootLogic.CurActList = messagelist;
		NewMessageUIRootLogic.MissionList = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.MissionTimeOutList;
		int num = messagelist.Count + NewMessageUIRootLogic.teamInfoList.Count + NewMessageUIRootLogic.MissionList.Count - this.newsitemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.newsitemList[0].gameObject) as GameObject;
				NewMessageItemLogic component = gameObject.GetComponent<NewMessageItemLogic>();
				gameObject.name = string.Format("message{0:D2}", this.newsitemList.Count);
				gameObject.transform.parent = this.ParentGrid.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				this.newsitemList.Add(component);
			}
		}
		for (int j = 0; j < this.newsitemList.Count; j++)
		{
			if (j < messagelist.Count)
			{
				NGUITools.SetActive(this.newsitemList[j].gameObject, true);
				this.newsitemList[j].ResetInfo(messagelist[j]);
			}
			else if (j < messagelist.Count + NewMessageUIRootLogic.teamInfoList.Count)
			{
				NGUITools.SetActive(this.newsitemList[j].gameObject, true);
				this.newsitemList[j].ResetInfo(NewMessageUIRootLogic.teamInfoList[j - messagelist.Count]);
			}
			else if (j < messagelist.Count + NewMessageUIRootLogic.teamInfoList.Count + NewMessageUIRootLogic.MissionList.Count)
			{
				NGUITools.SetActive(this.newsitemList[j].gameObject, true);
				this.newsitemList[j].ResetInfo(NewMessageUIRootLogic.MissionList[j - messagelist.Count - NewMessageUIRootLogic.teamInfoList.Count]);
			}
			if (j >= messagelist.Count + NewMessageUIRootLogic.teamInfoList.Count + NewMessageUIRootLogic.MissionList.Count)
			{
				NGUITools.SetActive(this.newsitemList[j].gameObject, false);
			}
		}
		this.ParentGrid.Reposition();
	}

	// Token: 0x06004C3D RID: 19517 RVA: 0x0019D714 File Offset: 0x0019B914
	public static void AddteamInfo(InviteTeamInfo newteaminfo)
	{
		if (newteaminfo.IsUrgeFlag)
		{
			for (int i = NewMessageUIRootLogic.teamInfoList.Count - 1; i >= 0; i--)
			{
				if (NewMessageUIRootLogic.teamInfoList[i].characterId == newteaminfo.characterId)
				{
					NewMessageUIRootLogic.teamInfoList.RemoveAt(i);
				}
			}
		}
		else
		{
			for (int j = NewMessageUIRootLogic.teamInfoList.Count - 1; j >= 0; j--)
			{
				if (NewMessageUIRootLogic.teamInfoList[j].characterId == newteaminfo.characterId && NewMessageUIRootLogic.teamInfoList[j].teamId == newteaminfo.teamId)
				{
					NewMessageUIRootLogic.teamInfoList.RemoveAt(j);
				}
			}
		}
		if (NewMessageUIRootLogic.teamInfoList.Count >= 10)
		{
			NewMessageUIRootLogic.teamInfoList.RemoveAt(NewMessageUIRootLogic.teamInfoList.Count - 1);
		}
		NewMessageUIRootLogic.teamInfoList.Insert(0, newteaminfo);
		NewMessageUIRootLogic.UpdateFunctionBtn();
	}

	// Token: 0x06004C3E RID: 19518 RVA: 0x0019D80C File Offset: 0x0019BA0C
	public static void ClearAllteamMessage()
	{
		if (SingletonUnity<NewMessageUIRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<NewMessageUIRootLogic>.Instance.gameObject))
		{
			SingletonUnity<NewMessageUIRootLogic>.Instance.OnClickClearBtn();
		}
		else
		{
			NewMessageUIRootLogic.teamInfoList.Clear();
			NewMessageUIRootLogic.UpdateFunctionBtn();
		}
	}

	// Token: 0x06004C3F RID: 19519 RVA: 0x0019D858 File Offset: 0x0019BA58
	public static bool IsHaveTeamMessageInfo()
	{
		return NewMessageUIRootLogic.teamInfoList != null && NewMessageUIRootLogic.teamInfoList.Count > 0;
	}

	// Token: 0x06004C40 RID: 19520 RVA: 0x0019D878 File Offset: 0x0019BA78
	public void DecMissionLine(string missionid)
	{
		for (int i = NewMessageUIRootLogic.MissionList.Count - 1; i >= 0; i--)
		{
			if (NewMessageUIRootLogic.MissionList[i].Equals(missionid))
			{
				NewMessageUIRootLogic.MissionList.RemoveAt(i);
			}
		}
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.DecMissionLine(missionid);
		NewMessageUIRootLogic.UpdateFunctionBtn();
		this.ResetInfo(NewMessageUIRootLogic.CurActList);
	}

	// Token: 0x06004C41 RID: 19521 RVA: 0x0019D8E8 File Offset: 0x0019BAE8
	public void OnClickClearBtn()
	{
		for (int i = 0; i < this.newsitemList.Count; i++)
		{
			if (i >= NewMessageUIRootLogic.CurActList.Count)
			{
				NGUITools.SetActive(this.newsitemList[i].gameObject, false);
			}
		}
		NewMessageUIRootLogic.MissionList.Clear();
		SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.ActivityData.MissionTimeOutList.Clear();
		this.ParentGrid.Reposition();
		NewMessageUIRootLogic.teamInfoList.Clear();
		NewMessageUIRootLogic.UpdateFunctionBtn();
	}

	// Token: 0x06004C42 RID: 19522 RVA: 0x0019D978 File Offset: 0x0019BB78
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.NewMessageUIRoot);
	}

	// Token: 0x06004C43 RID: 19523 RVA: 0x0019D98C File Offset: 0x0019BB8C
	public static void UpdateFunctionBtn()
	{
		if (SingletonUnity<FunctionBtnRootLogic>.Exists)
		{
			SingletonUnity<FunctionBtnRootLogic>.Instance.UpdateMessageTips();
		}
	}

	// Token: 0x040039F7 RID: 14839
	public UIGrid ParentGrid;

	// Token: 0x040039F8 RID: 14840
	public List<NewMessageItemLogic> newsitemList;

	// Token: 0x040039F9 RID: 14841
	public static List<InviteTeamInfo> teamInfoList = new List<InviteTeamInfo>();

	// Token: 0x040039FA RID: 14842
	public static List<GameDefine.ACTIVITY_TYPE> CurActList = new List<GameDefine.ACTIVITY_TYPE>();

	// Token: 0x040039FB RID: 14843
	public static List<string> MissionList = new List<string>();
}
