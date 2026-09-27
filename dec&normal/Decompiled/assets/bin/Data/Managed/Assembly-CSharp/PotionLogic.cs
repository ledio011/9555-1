using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A3E RID: 2622
public class PotionLogic : SingletonUnity<PotionLogic>
{
	// Token: 0x06004C77 RID: 19575 RVA: 0x0019EAB4 File Offset: 0x0019CCB4
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004C78 RID: 19576 RVA: 0x0019EAC0 File Offset: 0x0019CCC0
	public void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06004C79 RID: 19577 RVA: 0x0019EAE0 File Offset: 0x0019CCE0
	public void ClearTutorialEvent()
	{
		this.mOnClickTutorialBtn = null;
	}

	// Token: 0x06004C7A RID: 19578 RVA: 0x0019EAEC File Offset: 0x0019CCEC
	protected override void Awake()
	{
		base.Awake();
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
	}

	// Token: 0x06004C7B RID: 19579 RVA: 0x0019EB04 File Offset: 0x0019CD04
	public void OpenPotionList(List<GameItem> list)
	{
		if (list == null || list.Count <= 0)
		{
			GameMoneyHelper.ShowBuyPotion();
			return;
		}
		UnityVersionUtil.SetActiveRecursive(this.PotionListObj, true);
		UnityVersionUtil.SetActiveRecursive(this.PotionBlackObj, true);
		int num = list.Count - this.PotionItemList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.PotionItemList[0].gameObject) as GameObject;
				if (gameObject != null)
				{
					this.Grid.AddChild(gameObject.transform);
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = string.Format("jiaXue_{0}", this.PotionItemList.Count);
					PotionItem component = gameObject.GetComponent<PotionItem>();
					if (component != null)
					{
						this.PotionItemList.Add(component);
					}
				}
			}
		}
		for (int j = 0; j < this.PotionItemList.Count; j++)
		{
			NGUITools.SetActive(this.PotionItemList[j].gameObject, j < list.Count);
		}
		for (int k = 0; k < list.Count; k++)
		{
			this.PotionItemList[k].Init(list[k]);
		}
		this.Grid.Reposition();
	}

	// Token: 0x06004C7C RID: 19580 RVA: 0x0019EC78 File Offset: 0x0019CE78
	private void OnEnable()
	{
		NGUITools.SetActive(this.PotionListObj, false);
		NGUITools.SetActive(this.PotionBlackObj, false);
		UIUpdateEvent.SyncBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.SyncBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
		if (this.playerData == null)
		{
			this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		}
		this.currentUseItem = this.playerData.GetPotionItem();
		this.isSelect = false;
		this.Reset();
		this.UpdateAutoSelectDrag();
	}

	// Token: 0x06004C7D RID: 19581 RVA: 0x0019ED1C File Offset: 0x0019CF1C
	private void OnDisable()
	{
		UIUpdateEvent.SyncBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.SyncBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
		UIUpdateEvent.UpdateBackPackEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.UpdateBackPackEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdatePotion));
	}

	// Token: 0x06004C7E RID: 19582 RVA: 0x0019ED6C File Offset: 0x0019CF6C
	public void UpdatePotion()
	{
		this.currentUseItem = this.playerData.GetPotionItem();
		this.Reset();
		this.UpdateAutoSelectDrag();
	}

	// Token: 0x06004C7F RID: 19583 RVA: 0x0019ED8C File Offset: 0x0019CF8C
	private void UpdateAutoSelectDrag()
	{
		if (this.currentUseItem == null || this.currentUseItem.IsEmpty())
		{
			GameItem gameItem = Singleton<ObjManager>.Instance.MainPlayer.SelectDragItem();
			if (gameItem != null)
			{
				this.ClickSelectItem(gameItem);
			}
		}
	}

	// Token: 0x06004C80 RID: 19584 RVA: 0x0019EDD4 File Offset: 0x0019CFD4
	public void ClickUsePotion()
	{
		if (TutorialManager.CurStep == TUTORIAL_STEP.DRUG_TIP_START || TutorialManager.CurStep == TUTORIAL_STEP.DRUG_USE_TIP_START)
		{
			this.CheckTutorialEvent();
		}
		if (this.isSelect)
		{
			return;
		}
		if (UnityVersionUtil.IsActive(this.PotionListObj))
		{
			this.ClosePotionList();
			return;
		}
		if (this.currentUseItem != null && !this.currentUseItem.IsEmpty())
		{
			Singleton<ObjManager>.Instance.MainPlayer.UseDrag(this.currentUseItem);
		}
		else if (!UnityVersionUtil.IsActive(this.PotionListObj))
		{
			List<GameItem> targetPotionItemLevel = ItemContainerTool.GetTargetPotionItemLevel(this.playerData.ItemBackPack, this.playerData.Level);
			this.OpenPotionList(targetPotionItemLevel);
		}
	}

	// Token: 0x06004C81 RID: 19585 RVA: 0x0019EE94 File Offset: 0x0019D094
	public void OnPress()
	{
		this.pressTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06004C82 RID: 19586 RVA: 0x0019EEA4 File Offset: 0x0019D0A4
	public void OnRelease()
	{
		float num = Time.realtimeSinceStartup - this.pressTime;
		this.isSelect = (num > 0.8f);
		if (this.isSelect)
		{
			this.ClickShowList();
		}
	}

	// Token: 0x06004C83 RID: 19587 RVA: 0x0019EEE0 File Offset: 0x0019D0E0
	public void ClickShowList()
	{
		if (!UnityVersionUtil.IsActive(this.PotionListObj))
		{
			List<GameItem> targetPotionItemLevel = ItemContainerTool.GetTargetPotionItemLevel(this.playerData.ItemBackPack, this.playerData.Level);
			this.OpenPotionList(targetPotionItemLevel);
		}
		else
		{
			this.ClosePotionList();
		}
	}

	// Token: 0x06004C84 RID: 19588 RVA: 0x0019EF2C File Offset: 0x0019D12C
	public void ClosePotionList()
	{
		UnityVersionUtil.SetActiveRecursive(this.PotionListObj, false);
		NGUITools.SetActive(this.PotionBlackObj, false);
	}

	// Token: 0x06004C85 RID: 19589 RVA: 0x0019EF48 File Offset: 0x0019D148
	public void ClickSelectItem(GameItem item)
	{
		if (item == null)
		{
			return;
		}
		if (this.currentUseItem == null || this.currentUseItem.IndexId != item.IndexId)
		{
			this.currentUseItem = item;
			this.playerData.CurSelectPotionIndex = this.currentUseItem.IndexId;
			this.Reset();
			change_potion.request request = new change_potion.request();
			request.indexId = item.IndexId;
			NetLogic.GetInstance().Send<Protocol.change_potion>(request, null);
		}
		this.ClosePotionList();
	}

	// Token: 0x06004C86 RID: 19590 RVA: 0x0019EFC4 File Offset: 0x0019D1C4
	public static void AutoUpdateSelectItem(GameItem item)
	{
		if (SingletonUnity<PotionLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<PotionLogic>.Instance.gameObject))
		{
			SingletonUnity<PotionLogic>.Instance.ClickSelectItem(item);
		}
	}

	// Token: 0x06004C87 RID: 19591 RVA: 0x0019EFFC File Offset: 0x0019D1FC
	public void Reset()
	{
		if (this.currentUseItem != null && !this.currentUseItem.IsEmpty())
		{
			ItemData itemDataByID = DataManager.GetItemDataByID(this.currentUseItem.ItemId);
			this.PotionShowSprite.spriteName = itemDataByID.BackPackIcon + "_Min";
			this.PotionNumLabel.text = this.currentUseItem.StackNum.ToString();
			if (TutorialManager.CurStep == TUTORIAL_STEP.DRUG_TIP_START)
			{
				TutorialManager.MoveNext(false);
			}
		}
		else
		{
			this.PotionShowSprite.spriteName = "CZ_jiaXue_tuBiao_0";
			this.PotionNumLabel.text = string.Empty;
			if (TutorialManager.IsTutorialCanShow() && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.IsFinishDownload && SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.USE_DRUG_TIP))
			{
				TutorialManager.ShowTutorial(TUTORIAL_STEP.DRUG_TIP_START);
			}
		}
		NGUITools.SetActive(this.PotionListObj, false);
		NGUITools.SetActive(this.PotionBlackObj, false);
	}

	// Token: 0x06004C88 RID: 19592 RVA: 0x0019F100 File Offset: 0x0019D300
	private void Update()
	{
		this.DragCdSprite.fillAmount = this.playerData.GetDragTime01();
	}

	// Token: 0x04003A27 RID: 14887
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003A28 RID: 14888
	public UISprite PotionShowSprite;

	// Token: 0x04003A29 RID: 14889
	public UILabel PotionNumLabel;

	// Token: 0x04003A2A RID: 14890
	public UIScrollView scrollview;

	// Token: 0x04003A2B RID: 14891
	public UIGrid Grid;

	// Token: 0x04003A2C RID: 14892
	public GameObject PotionListObj;

	// Token: 0x04003A2D RID: 14893
	public GameObject PotionBlackObj;

	// Token: 0x04003A2E RID: 14894
	public List<PotionItem> PotionItemList = new List<PotionItem>();

	// Token: 0x04003A2F RID: 14895
	private PlayerData playerData;

	// Token: 0x04003A30 RID: 14896
	private GameItem currentUseItem;

	// Token: 0x04003A31 RID: 14897
	private bool isSelect;

	// Token: 0x04003A32 RID: 14898
	private float pressTime;

	// Token: 0x04003A33 RID: 14899
	public UISprite DragCdSprite;
}
