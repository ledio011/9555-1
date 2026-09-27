using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009BE RID: 2494
public class TeamApplyListLogic : SingletonUnity<TeamApplyListLogic>
{
	// Token: 0x060046FE RID: 18174 RVA: 0x00169158 File Offset: 0x00167358
	private void Start()
	{
		this.UpdataApplyList(new List<teammember>(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.TeamInfo.ApplyMemberDic.Values));
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x0016918C File Offset: 0x0016738C
	public void UpdataApplyList(List<teammember> list)
	{
		if (!UnityVersionUtil.IsActive(base.gameObject))
		{
			return;
		}
		int num = list.Count - this.ApplyList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.ApplyList[0].gameObject) as GameObject;
				this.ApplyGrid.AddChild(gameObject.transform);
				gameObject.transform.localScale = Vector3.one;
				this.ApplyList.Add(gameObject.GetComponent<ApplyLineLogic>());
			}
		}
		for (int j = 0; j < this.ApplyList.Count; j++)
		{
			NGUITools.SetActive(this.ApplyList[j].gameObject, j < list.Count);
		}
		this.ApplyGrid.Reposition();
		int num2 = 0;
		for (int k = 0; k < list.Count; k++)
		{
			this.ApplyList[num2++].InitApplyListItem(list[k]);
		}
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x001692A8 File Offset: 0x001674A8
	public void AllAgree()
	{
		for (int i = this.ApplyList.Count - 1; i >= 0; i--)
		{
			this.ApplyList[i].OnClickAcceptBtn();
		}
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x001692E4 File Offset: 0x001674E4
	public void DelAll()
	{
		for (int i = this.ApplyList.Count - 1; i >= 0; i--)
		{
			this.ApplyList[i].OnClickRejectBtn();
		}
	}

	// Token: 0x06004702 RID: 18178 RVA: 0x00169320 File Offset: 0x00167520
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.TeamApplyListRoot);
	}

	// Token: 0x04003417 RID: 13335
	public List<ApplyLineLogic> ApplyList = new List<ApplyLineLogic>();

	// Token: 0x04003418 RID: 13336
	public UIGrid ApplyGrid;
}
