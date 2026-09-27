using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000929 RID: 2345
public class ConsignBuyLogic : MonoBehaviour
{
	// Token: 0x0600411E RID: 16670 RVA: 0x0013504C File Offset: 0x0013324C
	public ConsignBuyLogic()
	{
		List<int> list = new List<int>();
		list.Add(-1);
		list.Add(1);
		list.Add(2);
		list.Add(3);
		list.Add(4);
		list.Add(5);
		list.Add(6);
		list.Add(7);
		list.Add(8);
		this.levelIndexList = list;
		List<string> list2 = new List<string>();
		list2.Add("ALL");
		list2.Add("lv.1");
		list2.Add("lv.2");
		list2.Add("lv.3");
		list2.Add("lv.4");
		list2.Add("lv.5");
		list2.Add("lv.6");
		list2.Add("lv.7");
		list2.Add("lv.8");
		this.levelStrList = list2;
		this.preTabIndex = -1;
		base..ctor();
	}

	// Token: 0x0600411F RID: 16671 RVA: 0x00135158 File Offset: 0x00133358
	private void OnEnable()
	{
		this.EnableReset();
	}

	// Token: 0x06004120 RID: 16672 RVA: 0x00135160 File Offset: 0x00133360
	private void EnableReset()
	{
		List<List<ConsignBuyTabData>> consignBuyTabData = DataManager.GetConsignBuyTabData();
		int num = consignBuyTabData.Count - this.leftTabList.Count;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = Object.Instantiate(this.leftTabList[0].gameObject) as GameObject;
			ConsignBuyTabLogic component = gameObject.GetComponent<ConsignBuyTabLogic>();
			this.leftTabList.Add(component);
			gameObject.transform.parent = this.leftTabList[0].transform.parent;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.gameObject.name = string.Format("Container{0:d2}", this.leftTabList.Count);
		}
		for (int j = 0; j < consignBuyTabData.Count; j++)
		{
			this.leftTabList[j].Reset(consignBuyTabData[j], new ConsignBuyTabLogic.OnClickTabDelegate(this.OnClickLeftTab), j, new ConsignBuyTabLogic.OnClickTopTabDelegate(this.OnClickTopTab));
		}
		UnityVersionUtil.SetActiveRecursive(this.ChoosedTabLinePic.gameObject, false);
		this.ChoosedTabLinePic.transform.localScale = Vector3.zero;
	}

	// Token: 0x06004121 RID: 16673 RVA: 0x001352A4 File Offset: 0x001334A4
	public void Reset()
	{
		this.RefreshPage(null, this.CurPage, true);
		UnityVersionUtil.SetActiveRecursive(this.ChoosedTabLinePic.gameObject, false);
		this.type = -1;
		UnityVersionUtil.SetActiveRecursive(this.LeftPopList.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RightPopList.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.QualityPopRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.LevelPopRoot, true);
		UnityVersionUtil.SetActiveRecursive(this.ProfessionPopList.gameObject, true);
		if (this.quality == -1)
		{
			UnityVersionUtil.SetActiveRecursive(this.QualityLabel.gameObject, true);
			UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.QualityLabel.gameObject, false);
			UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, true);
		}
		this.leftTabList[0].SubTweenRoot.PlayForward();
		this.leftTabList[0].SetMinusPic(true);
		this.leftTabList[0].SubTabList[0].OnClickTab();
		this.preTabIndex = 0;
	}

	// Token: 0x06004122 RID: 16674 RVA: 0x001353C4 File Offset: 0x001335C4
	private void OnClickLeftTab(int itemType, int itemSubType, int tabIndex, int subTabIndex)
	{
		if (itemType != this.type || this.subType != itemSubType)
		{
			UnityVersionUtil.SetActiveRecursive(this.ChoosedTabLinePic.gameObject, true);
			this.ChoosedTabLinePic.transform.parent = this.leftTabList[tabIndex].SubTabList[subTabIndex].transform;
			this.ChoosedTabLinePic.transform.localPosition = Vector3.zero;
			this.ChoosedTabLinePic.transform.localScale = Vector3.one;
			this.type = itemType;
			this.subType = itemSubType;
			this.quality = -1;
			this.levelrange = -1;
			this.CurPage = 0;
			this.profession = -1;
			this.SendRequest();
			if (itemType == 6)
			{
				UnityVersionUtil.SetActiveRecursive(this.LeftPopList.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.RightPopList.gameObject, true);
				List<int> list = new List<int>();
				List<string> list2 = new List<string>();
				DataManager.GetBadgeTypeByColor(this.subType, list, list2);
				list.Insert(0, -1);
				list2.Insert(0, "ALL");
				this.LeftPopList.Init(list, list2, new ConsignPopItem.OnClickItemDelegate(this.OnClickConsignTypeItem));
				this.RightPopList.Init(this.levelIndexList, this.levelStrList, new ConsignPopItem.OnClickItemDelegate(this.OnClickConsignLevelItem));
				this.LeftPopList.textLabel.text = "ALL";
				this.RightPopList.textLabel.text = "ALL";
				UnityVersionUtil.SetActiveRecursive(this.QualityPopRoot, false);
				UnityVersionUtil.SetActiveRecursive(this.LevelPopRoot, false);
				UnityVersionUtil.SetActiveRecursive(this.ProfessionPopList.gameObject, false);
				this.QualityTween.ResetToBeginning();
				this.LevelTween.ResetToBeginning();
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.LeftPopList.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.RightPopList.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.QualityPopRoot, true);
				if (this.quality == -1)
				{
					UnityVersionUtil.SetActiveRecursive(this.QualityLabel.gameObject, true);
					UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, false);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.QualityLabel.gameObject, false);
					UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, true);
				}
				if (itemType == 2)
				{
					UnityVersionUtil.SetActiveRecursive(this.ProfessionPopList.gameObject, true);
					UnityVersionUtil.SetActiveRecursive(this.LevelPopRoot, true);
					List<int> list3 = new List<int>();
					list3.Add(-1);
					list3.Add(0);
					list3.Add(1);
					list3.Add(2);
					List<int> itemIndexList = list3;
					List<string> list4 = new List<string>();
					list4.Add(StrDictionary.GetDictionaryString("#{100663}", new object[0]));
					list4.Add(StrDictionary.GetDictionaryString("#{100128}", new object[0]));
					list4.Add(StrDictionary.GetDictionaryString("#{100129}", new object[0]));
					list4.Add(StrDictionary.GetDictionaryString("#{100130}", new object[0]));
					List<string> itemTxtList = list4;
					this.ProfessionPopList.Init(itemIndexList, itemTxtList, new ConsignPopItem.OnClickItemDelegate(this.OnClickProfessionTypeItem));
					this.ProfessionPopList.textLabel.text = StrDictionary.GetDictionaryString("#{100663}", new object[0]);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.ProfessionPopList.gameObject, false);
					UnityVersionUtil.SetActiveRecursive(this.LevelPopRoot, false);
				}
				this.LevelShow.text = "ALL";
			}
		}
		this.ClosePopAnima();
	}

	// Token: 0x06004123 RID: 16675 RVA: 0x0013572C File Offset: 0x0013392C
	public void OnClickProfessionTypeItem(int index, string str)
	{
		this.profession = index;
		if (this.type != -1)
		{
			this.SendRequest();
		}
	}

	// Token: 0x06004124 RID: 16676 RVA: 0x00135748 File Offset: 0x00133948
	public void OnClickConsignTypeItem(int index, string str)
	{
		this.quality = index;
		this.CurPage = 0;
		if (this.type != -1)
		{
			this.SendRequest();
		}
	}

	// Token: 0x06004125 RID: 16677 RVA: 0x00135778 File Offset: 0x00133978
	public void OnClickConsignLevelItem(int index, string str)
	{
		this.levelrange = index;
		this.CurPage = 0;
		if (this.type != -1)
		{
			this.SendRequest();
		}
	}

	// Token: 0x06004126 RID: 16678 RVA: 0x001357A8 File Offset: 0x001339A8
	private void OnClickTopTab(int tabIndex)
	{
		if (this.preTabIndex != tabIndex && this.preTabIndex >= 0)
		{
			this.leftTabList[this.preTabIndex].SubTweenRoot.PlayReverse();
			this.leftTabList[this.preTabIndex].SetMinusPic(false);
		}
		this.preTabIndex = tabIndex;
		this.ClosePopAnima();
	}

	// Token: 0x06004127 RID: 16679 RVA: 0x0013580C File Offset: 0x00133A0C
	private void InitBottomTab()
	{
	}

	// Token: 0x06004128 RID: 16680 RVA: 0x00135810 File Offset: 0x00133A10
	private void InitShow()
	{
		this.useNowToggle.value = this.use;
	}

	// Token: 0x06004129 RID: 16681 RVA: 0x00135824 File Offset: 0x00133A24
	public void RefreshPage(List<consign_item> list, int page, bool isRefresh)
	{
		if (list != null)
		{
			int num = Mathf.Min(list.Count, this.PageCount) - this.consignItemLogicList.Count;
			int count = this.consignItemLogicList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.consignItemLogicList[0].gameObject) as GameObject;
					gameObject.name = string.Format("ItemLine_{0}", count + i + 1);
					ConsignItemLogic component = gameObject.GetComponent<ConsignItemLogic>();
					if (component != null)
					{
						component.transform.parent = this.consignItemLogicList[0].transform.parent;
						component.transform.localScale = Vector3.one;
						component.transform.localPosition = Vector3.zero;
						this.consignItemLogicList.Add(component);
					}
				}
			}
			for (int j = 0; j < this.consignItemLogicList.Count; j++)
			{
				if (j < list.Count)
				{
					UnityVersionUtil.SetActiveRecursive(this.consignItemLogicList[j].gameObject, true);
					this.consignItemLogicList[j].Reset(list[j]);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.consignItemLogicList[j].gameObject, false);
				}
			}
		}
		else
		{
			for (int k = 0; k < this.consignItemLogicList.Count; k++)
			{
				UnityVersionUtil.SetActiveRecursive(this.consignItemLogicList[k].gameObject, false);
			}
		}
		if (isRefresh)
		{
			this.LeftScrollView.ResetPosition();
			this.CenterGrid.NowReposition();
			this.CenterScrollView.ResetPosition();
		}
		this.PageLabel.text = string.Format("{0}/{1}", Mathf.Min(this.CurPage + 1, this.MaxPage), this.MaxPage);
	}

	// Token: 0x0600412A RID: 16682 RVA: 0x00135A30 File Offset: 0x00133C30
	public void BuySuccess(long id)
	{
		bool flag = false;
		for (int i = 0; i < this.currentConsignList.Count; i++)
		{
			if (this.currentConsignList[i].id == id)
			{
				this.currentConsignList.RemoveAt(i);
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.RefreshPage(this.currentConsignList, this.CurPage, true);
		}
	}

	// Token: 0x0600412B RID: 16683 RVA: 0x00135AA0 File Offset: 0x00133CA0
	public void ClickQuality(int index)
	{
		if (this.quality != index)
		{
			this.quality = index;
			this.CurPage = 0;
			if (this.type != -1)
			{
				this.SendRequest();
			}
			if (index == -1)
			{
				UnityVersionUtil.SetActiveRecursive(this.QualityLabel.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, false);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.QualityLabel.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.QualitySprite.gameObject, true);
				switch (index)
				{
				case 0:
					this.QualitySprite.color = Color.white;
					break;
				case 1:
					this.QualitySprite.color = Color.green;
					break;
				case 2:
					this.QualitySprite.color = Color.blue;
					break;
				case 3:
					this.QualitySprite.color = new Color(1f, 0f, 1f, 1f);
					break;
				}
			}
		}
		this.QualityTween.PlayReverse();
	}

	// Token: 0x0600412C RID: 16684 RVA: 0x00135BBC File Offset: 0x00133DBC
	public void ClickLevel(int level)
	{
		if (this.levelrange != level)
		{
			this.levelrange = level;
			this.CurPage = 0;
			if (this.type != -1)
			{
				this.SendRequest();
			}
			switch (level + 1)
			{
			case 0:
				this.LevelShow.text = string.Format("ALL", new object[0]);
				break;
			case 1:
				this.LevelShow.text = string.Format("0-29", new object[0]);
				break;
			case 2:
				this.LevelShow.text = string.Format("30-59", new object[0]);
				break;
			case 3:
				this.LevelShow.text = string.Format("60-89", new object[0]);
				break;
			case 4:
				this.LevelShow.text = string.Format("90+", new object[0]);
				break;
			}
		}
		this.LevelTween.PlayReverse();
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x00135CC4 File Offset: 0x00133EC4
	public void ClickUseNow(bool isUse)
	{
		if (this.use != isUse)
		{
			this.use = isUse;
			this.CurPage = 0;
			if (this.type != -1)
			{
				this.SendRequest();
			}
		}
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x00135D00 File Offset: 0x00133F00
	public void UpdateList(ret_consign_ask_items_info.request request)
	{
		if (request.success == 0L)
		{
			this.CurPage = (int)request.curPage;
			this.MaxPage = (int)request.maxPage;
			if (request.HasConsign_items)
			{
				this.currentConsignList = new List<consign_item>(request.consign_items.Values);
			}
			else
			{
				this.currentConsignList = null;
			}
			this.RefreshPage(this.currentConsignList, this.CurPage, true);
		}
		else
		{
			this.CurPage = 0;
			this.MaxPage = 0;
			this.RefreshPage(null, this.CurPage, true);
		}
	}

	// Token: 0x0600412F RID: 16687 RVA: 0x00135D94 File Offset: 0x00133F94
	private void SendRequest()
	{
		consign_ask_items_info.request request = new consign_ask_items_info.request();
		request.type = (long)this.type;
		request.subType = (long)this.subType;
		request.quality = (long)this.quality;
		request.levelRange = (long)this.levelrange;
		request.use = this.use;
		request.curPage = (long)this.CurPage;
		if (this.profession != -1)
		{
			request.profession = (long)this.profession;
		}
		NetLogic.GetInstance().Send<Protocol.consign_ask_items_info>(request, null);
	}

	// Token: 0x06004130 RID: 16688 RVA: 0x00135E1C File Offset: 0x0013401C
	public void OnClickQualityBtn(GameObject btn)
	{
		int index = -1;
		int.TryParse(btn.name, ref index);
		this.ClickQuality(index);
	}

	// Token: 0x06004131 RID: 16689 RVA: 0x00135E40 File Offset: 0x00134040
	public void OnClickLevelBtn(GameObject btn)
	{
		int level = -1;
		int.TryParse(btn.name, ref level);
		this.ClickLevel(level);
	}

	// Token: 0x06004132 RID: 16690 RVA: 0x00135E64 File Offset: 0x00134064
	public void OnClickLeftPageBtn()
	{
		if (this.CurPage > 0)
		{
			this.CurPage--;
			this.SendRequest();
		}
	}

	// Token: 0x06004133 RID: 16691 RVA: 0x00135E94 File Offset: 0x00134094
	public void OnClickRightPageBtn()
	{
		if (this.CurPage < this.MaxPage - 1)
		{
			this.CurPage++;
			this.SendRequest();
		}
	}

	// Token: 0x06004134 RID: 16692 RVA: 0x00135EC0 File Offset: 0x001340C0
	private void OnDisable()
	{
		if (this.QualityTween != null)
		{
			this.QualityTween.ResetToBeginning();
		}
		if (this.LevelTween != null)
		{
			this.LevelTween.ResetToBeginning();
		}
	}

	// Token: 0x06004135 RID: 16693 RVA: 0x00135F08 File Offset: 0x00134108
	public void ClosePopAnima()
	{
		this.QualityTween.ResetToBeginning();
		this.LevelTween.ResetToBeginning();
		this.LeftPopList.CloseAnima();
		this.RightPopList.CloseAnima();
		this.ProfessionPopList.CloseAnima();
	}

	// Token: 0x04002CD7 RID: 11479
	public UIScrollView LeftScrollView;

	// Token: 0x04002CD8 RID: 11480
	public UITable LeftTable;

	// Token: 0x04002CD9 RID: 11481
	public UIScrollView CenterScrollView;

	// Token: 0x04002CDA RID: 11482
	public UIGrid CenterGrid;

	// Token: 0x04002CDB RID: 11483
	public UILabel pageInfoLabel;

	// Token: 0x04002CDC RID: 11484
	public List<ConsignItemLogic> consignItemLogicList = new List<ConsignItemLogic>();

	// Token: 0x04002CDD RID: 11485
	private List<consign_item> currentConsignList;

	// Token: 0x04002CDE RID: 11486
	private int PageCount = 10;

	// Token: 0x04002CDF RID: 11487
	private int MaxPage;

	// Token: 0x04002CE0 RID: 11488
	private int CurPage;

	// Token: 0x04002CE1 RID: 11489
	private int type = 2;

	// Token: 0x04002CE2 RID: 11490
	private int subType = 2;

	// Token: 0x04002CE3 RID: 11491
	private int quality = -1;

	// Token: 0x04002CE4 RID: 11492
	private int levelrange = -1;

	// Token: 0x04002CE5 RID: 11493
	private bool use;

	// Token: 0x04002CE6 RID: 11494
	private int profession = -1;

	// Token: 0x04002CE7 RID: 11495
	public UIToggle useNowToggle;

	// Token: 0x04002CE8 RID: 11496
	public UILabel LevelShow;

	// Token: 0x04002CE9 RID: 11497
	public UILabel QualityLabel;

	// Token: 0x04002CEA RID: 11498
	public UISprite QualitySprite;

	// Token: 0x04002CEB RID: 11499
	public UISprite ChoosedTabLinePic;

	// Token: 0x04002CEC RID: 11500
	public TweenScale QualityTween;

	// Token: 0x04002CED RID: 11501
	public TweenScale LevelTween;

	// Token: 0x04002CEE RID: 11502
	public GameObject QualityPopRoot;

	// Token: 0x04002CEF RID: 11503
	public GameObject LevelPopRoot;

	// Token: 0x04002CF0 RID: 11504
	public ConsignPopList LeftPopList;

	// Token: 0x04002CF1 RID: 11505
	public ConsignPopList RightPopList;

	// Token: 0x04002CF2 RID: 11506
	public ConsignPopList ProfessionPopList;

	// Token: 0x04002CF3 RID: 11507
	public List<ConsignBuyTabLogic> leftTabList;

	// Token: 0x04002CF4 RID: 11508
	public UILabel PageLabel;

	// Token: 0x04002CF5 RID: 11509
	private List<int> levelIndexList;

	// Token: 0x04002CF6 RID: 11510
	private List<string> levelStrList;

	// Token: 0x04002CF7 RID: 11511
	private int preTabIndex;
}
