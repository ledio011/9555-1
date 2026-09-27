using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008EE RID: 2286
public class CopyDrugUseUIRoot : SingletonUnity<CopyDrugUseUIRoot>
{
	// Token: 0x06003DDE RID: 15838 RVA: 0x00117964 File Offset: 0x00115B64
	protected override void Awake()
	{
		base.Awake();
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
	}

	// Token: 0x06003DDF RID: 15839 RVA: 0x0011797C File Offset: 0x00115B7C
	private void OnEnable()
	{
		this.isFirtst = true;
		this.mList = null;
		UIUpdateEvent.SyncBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.SyncBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
		this.Reset();
	}

	// Token: 0x06003DE0 RID: 15840 RVA: 0x001179E0 File Offset: 0x00115BE0
	private void OnDisable()
	{
		UIUpdateEvent.SyncBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.SyncBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
	}

	// Token: 0x06003DE1 RID: 15841 RVA: 0x00117A30 File Offset: 0x00115C30
	public void Reset()
	{
		if (this.playerData == null)
		{
			this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		List<GameItem> targetTypeItem = ItemContainerTool.GetTargetTypeItem(this.playerData.ItemBackPack, false, GameDefine.ITEM_TYPE.BUFF, false, PROFESSION_TYPE.INVALID);
		if (this.mList == null)
		{
			this.mList = new List<GameItem>();
			for (int i = 0; i < targetTypeItem.Count; i++)
			{
				this.mList.Add(targetTypeItem[i].ShallowCopy());
			}
		}
		else
		{
			for (int j = 0; j < this.mList.Count; j++)
			{
				this.mList[j].StackNum = 0;
			}
			for (int k = 0; k < targetTypeItem.Count; k++)
			{
				for (int l = 0; l < this.mList.Count; l++)
				{
					if (this.mList[l].ItemId == targetTypeItem[k].ItemId)
					{
						this.mList[l].StackNum = targetTypeItem[k].StackNum;
					}
				}
			}
		}
		for (int m = 0; m < this.DrugItemList.Count; m++)
		{
			NGUITools.SetActive(this.DrugItemList[m].gameObject, m < this.mList.Count);
		}
		for (int n = 0; n < this.mList.Count; n++)
		{
			if (n < this.DrugItemList.Count)
			{
				if (!this.mTimeDict.ContainsKey(this.mList[n].IndexId))
				{
					this.mTimeDict[this.mList[n].IndexId] = -1f;
				}
				float time = this.mTimeDict[this.mList[n].IndexId];
				this.DrugItemList[n].Reset(this.mList[n], time);
			}
		}
		this.Grid.Reposition();
	}

	// Token: 0x06003DE2 RID: 15842 RVA: 0x00117C6C File Offset: 0x00115E6C
	public void OnResetPosition(GameItem item)
	{
		if (item.StackNum <= 0)
		{
			this.mList.Remove(item);
		}
		this.Grid.Reposition();
	}

	// Token: 0x06003DE3 RID: 15843 RVA: 0x00117CA0 File Offset: 0x00115EA0
	public void UseItem(CopyDrugItemLogic logic, GameItem item)
	{
		ItemData itemData = item.ItemData;
		EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(itemData.Function.ToString());
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (effInfoDataById != null && mainPlayer != null)
		{
			if (!this.mTimeDict.ContainsKey(item.IndexId))
			{
				this.mTimeDict[item.IndexId] = -1f;
			}
			this.mTimeDict[item.IndexId] = Time.realtimeSinceStartup + effInfoDataById.BuffDurationSecond;
			mainPlayer.UseBuffDrag(item);
			logic.UpdateTime(this.mTimeDict[item.IndexId]);
		}
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x00117D4C File Offset: 0x00115F4C
	public void UpdatePotion()
	{
		this.Reset();
	}

	// Token: 0x04002983 RID: 10627
	public UIGrid Grid;

	// Token: 0x04002984 RID: 10628
	public List<CopyDrugItemLogic> DrugItemList = new List<CopyDrugItemLogic>();

	// Token: 0x04002985 RID: 10629
	private Dictionary<long, float> mTimeDict = new Dictionary<long, float>();

	// Token: 0x04002986 RID: 10630
	private List<GameItem> mList;

	// Token: 0x04002987 RID: 10631
	private PlayerData playerData;

	// Token: 0x04002988 RID: 10632
	private bool isFirtst = true;
}
