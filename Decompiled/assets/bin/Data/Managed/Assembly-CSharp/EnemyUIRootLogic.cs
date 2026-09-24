using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x020009A5 RID: 2469
public class EnemyUIRootLogic : SingletonUnity<EnemyUIRootLogic>
{
	// Token: 0x06004612 RID: 17938 RVA: 0x00162334 File Offset: 0x00160534
	protected override void Awake()
	{
		base.Awake();
		this.uiWrapContent.enabled = false;
		UIWrapContentNew uiwrapContentNew = this.uiWrapContent;
		uiwrapContentNew.onInitializeItem = (UIWrapContentNew.OnInitializeItem)Delegate.Combine(uiwrapContentNew.onInitializeItem, new UIWrapContentNew.OnInitializeItem(this.OnInitializeItem));
	}

	// Token: 0x06004613 RID: 17939 RVA: 0x0016237C File Offset: 0x0016057C
	public void EnableReset()
	{
		for (int i = 0; i < this.EnemyItems.Count; i++)
		{
			NGUITools.SetActive(this.EnemyItems[i].gameObject, false);
		}
	}

	// Token: 0x06004614 RID: 17940 RVA: 0x001623BC File Offset: 0x001605BC
	public void UpdateEnemyList()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		List<friend_info> list = new List<friend_info>(playerData.FriendInfo.MainPlayerEnemyDic.Values);
		list = FriendInfo.SortEnemyList(list);
		int num = Mathf.Min(list.Count, this.LineCount) - this.EnemyItems.Count;
		int count = this.EnemyItems.Count;
		this.curListEnemyInfo = list;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.EnemyItems[0].gameObject) as GameObject;
				gameObject.name = string.Format("huaDongTiao_{0}", count + i);
				EnemyItemLogic component = gameObject.GetComponent<EnemyItemLogic>();
				if (component != null)
				{
					component.transform.parent = this.EnemyItems[0].transform.parent;
					component.transform.localScale = Vector3.one;
					component.transform.localPosition = Vector3.zero;
					this.EnemyItems.Add(component);
				}
			}
		}
		for (int j = 0; j < this.EnemyItems.Count; j++)
		{
			NGUITools.SetActive(this.EnemyItems[j].gameObject, j < list.Count);
			if (j < list.Count)
			{
				this.ResetItemLine(this.EnemyItems[j], j);
			}
		}
		this.uiWrapContent.minIndex = 1 - list.Count;
		this.uiWrapContent.maxIndex = 0;
		this.WrapContentBottomWidget.height = list.Count * this.uiWrapContent.itemSize;
		this.uiWrapContent.SortBasedOnScrollMovement();
		this.uiScrollView.ResetPosition();
		this.uiWrapContent.enabled = true;
		this.NumLabel.text = string.Format("{0}:{1}/{2}", StrDictionary.GetDictionaryString("#{100220}", new object[0]), playerData.FriendInfo.EnemyCount, GameDefine.MAX_ENEMY_COUNT);
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x001625E0 File Offset: 0x001607E0
	private void OnInitializeItem(GameObject obj, int index, int realIndex)
	{
		EnemyItemLogic itemLogic = this.EnemyItems[index];
		this.ResetItemLine(itemLogic, Mathf.Abs(realIndex));
	}

	// Token: 0x06004616 RID: 17942 RVA: 0x00162608 File Offset: 0x00160808
	private void ResetItemLine(EnemyItemLogic itemLogic, int idx)
	{
		if (idx < this.curListEnemyInfo.Count)
		{
			itemLogic.UpdateFriendInfo(this.curListEnemyInfo[idx]);
		}
	}

	// Token: 0x0400331D RID: 13085
	public int LineCount = 6;

	// Token: 0x0400331E RID: 13086
	public List<EnemyItemLogic> EnemyItems = new List<EnemyItemLogic>();

	// Token: 0x0400331F RID: 13087
	public UIWrapContentNew uiWrapContent;

	// Token: 0x04003320 RID: 13088
	public UIScrollView uiScrollView;

	// Token: 0x04003321 RID: 13089
	public UIWidget WrapContentBottomWidget;

	// Token: 0x04003322 RID: 13090
	private List<friend_info> curListEnemyInfo = new List<friend_info>();

	// Token: 0x04003323 RID: 13091
	public UILabel NumLabel;
}
