using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000979 RID: 2425
public class PlayerRankInfoRootLogic : SingletonUnity<PlayerRankInfoRootLogic>
{
	// Token: 0x06004480 RID: 17536 RVA: 0x00155C9C File Offset: 0x00153E9C
	private void OnEnable()
	{
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> leftBtnInfo = new List<MenuTabBtnInfo>();
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(leftBtnInfo, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), true, null);
		}, null);
		List<MenuTabBtnInfo> list = new List<MenuTabBtnInfo>();
		MenuTabBtnInfo menuTabBtnInfo = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickLevelBtn), false, "icon", StrDictionary.GetDictionaryString("#{101301}", new object[0]), FUNCTION_TYPE.COUNT, null);
		MenuTabBtnInfo menuTabBtnInfo2 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickFightBtn), false, "icon", StrDictionary.GetDictionaryString("#{101302}", new object[0]), FUNCTION_TYPE.COUNT, null);
		MenuTabBtnInfo menuTabBtnInfo3 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickPVPBtn), false, "icon", StrDictionary.GetDictionaryString("#{101303}", new object[0]), FUNCTION_TYPE.COUNT, null);
		MenuTabBtnInfo menuTabBtnInfo4 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickTowerrBtn), false, "icon", StrDictionary.GetDictionaryString("#{101304}", new object[0]), FUNCTION_TYPE.COUNT, null);
		MenuTabBtnInfo menuTabBtnInfo5 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickGuildBtn), false, "icon", StrDictionary.GetDictionaryString("#{101305}", new object[0]), FUNCTION_TYPE.COUNT, null);
		MenuTabBtnInfo menuTabBtnInfo6 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickCarBtn), false, "icon", StrDictionary.GetDictionaryString("#{101306}", new object[0]), FUNCTION_TYPE.COUNT, null);
		MenuTabBtnInfo menuTabBtnInfo7 = new MenuTabBtnInfo(new DelegateDefine.NoParamDelegate(this.OnClickSexBtn), false, "icon", StrDictionary.GetDictionaryString("#{101629}", new object[0]), FUNCTION_TYPE.COUNT, null);
		list.Add(menuTabBtnInfo);
		list.Add(menuTabBtnInfo2);
		list.Add(menuTabBtnInfo3);
		list.Add(menuTabBtnInfo4);
		list.Add(menuTabBtnInfo5);
		list.Add(menuTabBtnInfo6);
		list.Add(menuTabBtnInfo7);
		this.ResetPage(list);
		this.ambientLight = RenderSettings.ambientLight;
	}

	// Token: 0x06004481 RID: 17537 RVA: 0x00155E58 File Offset: 0x00154058
	public void SetCarLight()
	{
		RenderSettings.ambientLight = new Color(0f, 0f, 0f, 1f);
	}

	// Token: 0x06004482 RID: 17538 RVA: 0x00155E8C File Offset: 0x0015408C
	public void ResetNormalLight()
	{
		RenderSettings.ambientLight = this.ambientLight;
	}

	// Token: 0x06004483 RID: 17539 RVA: 0x00155E9C File Offset: 0x0015409C
	public void ResetPage(List<MenuTabBtnInfo> leftBtnInfo)
	{
		if (leftBtnInfo != null)
		{
			this.mCurMenuTabBtnInfoList = leftBtnInfo;
			int num = this.mCurMenuTabBtnInfoList.Count - this.mCurBtnList.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = Object.Instantiate(this.BtnPrefab.gameObject) as GameObject;
					MenuBaseTabBtnLogic component = gameObject.GetComponent<MenuBaseTabBtnLogic>();
					gameObject.name = this.mCurBtnList.Count.ToString();
					gameObject.transform.parent = this.LeftTabGrid.transform;
					gameObject.transform.localScale = Vector3.one;
					this.mCurBtnList.Add(component);
				}
			}
			for (int j = 0; j < this.mCurBtnList.Count; j++)
			{
				if (j < this.mCurMenuTabBtnInfoList.Count)
				{
					UnityVersionUtil.SetActiveRecursive(this.mCurBtnList[j].gameObject, true);
					this.mCurBtnList[j].Reset(this.mCurMenuTabBtnInfoList[j]);
				}
				else
				{
					UnityVersionUtil.SetActiveRecursive(this.mCurBtnList[j].gameObject, false);
				}
			}
			this.LeftTabGrid.Reposition();
		}
	}

	// Token: 0x06004484 RID: 17540 RVA: 0x00155FE4 File Offset: 0x001541E4
	public void OnClickFightBtn()
	{
		this.OnClickRankType(1);
	}

	// Token: 0x06004485 RID: 17541 RVA: 0x00155FF0 File Offset: 0x001541F0
	public void OnClickLevelBtn()
	{
		this.OnClickRankType(2);
	}

	// Token: 0x06004486 RID: 17542 RVA: 0x00155FFC File Offset: 0x001541FC
	public void OnClickPVPBtn()
	{
		this.OnClickRankType(3);
	}

	// Token: 0x06004487 RID: 17543 RVA: 0x00156008 File Offset: 0x00154208
	public void OnClickTowerrBtn()
	{
		this.OnClickRankType(5);
	}

	// Token: 0x06004488 RID: 17544 RVA: 0x00156014 File Offset: 0x00154214
	public void OnClickGuildBtn()
	{
		this.OnClickRankType(6);
	}

	// Token: 0x06004489 RID: 17545 RVA: 0x00156020 File Offset: 0x00154220
	public void OnClickCarBtn()
	{
		this.OnClickRankType(7);
	}

	// Token: 0x0600448A RID: 17546 RVA: 0x0015602C File Offset: 0x0015422C
	public void OnClickSexBtn()
	{
		this.OnClickRankType(8);
	}

	// Token: 0x0600448B RID: 17547 RVA: 0x00156038 File Offset: 0x00154238
	public void SetTargetBtnToggleEnable(int index)
	{
		for (int i = 0; i < this.mCurBtnList.Count; i++)
		{
			if (i == index)
			{
				this.mCurBtnList[i].BtnToggle.alpha = 1f;
				if (SingletonUnity<MenuBaseRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<MenuBaseRootLogic>.Instance.gameObject))
				{
					SingletonUnity<MenuBaseRootLogic>.Instance.SetPageLabel(this.mCurMenuTabBtnInfoList[i].PageName);
				}
			}
			else
			{
				this.mCurBtnList[i].BtnToggle.alpha = 0f;
			}
		}
	}

	// Token: 0x0600448C RID: 17548 RVA: 0x001560DC File Offset: 0x001542DC
	public void Reset()
	{
		this.mPreRankType = RANK_TYPE.INVALID;
		UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
		this.SetTargetBtnToggleEnable(0);
		this.OnClickRankType(2);
		this.BackRankType = RANK_TYPE.INVALID;
	}

	// Token: 0x0600448D RID: 17549 RVA: 0x00156128 File Offset: 0x00154328
	public void ResetToTower()
	{
		this.mPreRankType = RANK_TYPE.INVALID;
		UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
		this.SetTargetBtnToggleEnable(3);
		this.OnClickRankType(5);
		this.BackRankType = RANK_TYPE.TOWER;
	}

	// Token: 0x0600448E RID: 17550 RVA: 0x00156174 File Offset: 0x00154374
	public void ResetToCar()
	{
		this.mPreRankType = RANK_TYPE.INVALID;
		UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
		this.SetTargetBtnToggleEnable(5);
		this.OnClickRankType(7);
		this.BackRankType = RANK_TYPE.CAR;
	}

	// Token: 0x0600448F RID: 17551 RVA: 0x001561C0 File Offset: 0x001543C0
	public void ResetToSexMini()
	{
		this.mPreRankType = RANK_TYPE.INVALID;
		UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
		this.SetTargetBtnToggleEnable(6);
		this.OnClickRankType(8);
		this.BackRankType = RANK_TYPE.SEX;
	}

	// Token: 0x06004490 RID: 17552 RVA: 0x0015620C File Offset: 0x0015440C
	public void ResetToPVP()
	{
		this.mPreRankType = RANK_TYPE.INVALID;
		UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
		this.SetTargetBtnToggleEnable(2);
		this.OnClickRankType(3);
		this.BackRankType = RANK_TYPE.LADDER;
	}

	// Token: 0x06004491 RID: 17553 RVA: 0x00156258 File Offset: 0x00154458
	private void OnClickRankType(int rankType)
	{
		if (rankType == (int)this.mPreRankType)
		{
			return;
		}
		WaitResponseUIRootLogic.OpenWaitBox(191, 10f, 0f, null);
		request_top_rank_list.request request = new request_top_rank_list.request();
		request.sortType = (long)rankType;
		NetLogic.GetInstance().Send<Protocol.request_top_rank_list>(request, null);
	}

	// Token: 0x06004492 RID: 17554 RVA: 0x001562A4 File Offset: 0x001544A4
	public void UpdateRankTypeList(ret_top_rank_list.request request)
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.mCurRankList.Clear();
			if (request.HasSort_items)
			{
				this.mCurRankList = request.sort_items;
			}
			this.mCurRankType = (RANK_TYPE)request.sortType;
			switch (this.mCurRankType)
			{
			case RANK_TYPE.FIGHT:
				this.ResetNormalLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
				this.mModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(1);
				break;
			case RANK_TYPE.LEVEL:
				this.ResetNormalLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
				this.mModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(0);
				break;
			case RANK_TYPE.LADDER:
				this.ResetNormalLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
				this.mModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(2);
				break;
			case RANK_TYPE.TOWER:
				this.ResetNormalLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
				this.mModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(3);
				break;
			case RANK_TYPE.GUILD:
				this.ResetNormalLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, true);
				this.mNoModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(4);
				break;
			case RANK_TYPE.CAR:
				this.SetCarLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
				this.mModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(5);
				break;
			case RANK_TYPE.SEX:
				this.ResetNormalLight();
				UnityVersionUtil.SetActiveRecursive(this.mModelRankLogic.gameObject, true);
				UnityVersionUtil.SetActiveRecursive(this.mNoModelRankLogic.gameObject, false);
				this.mModelRankLogic.Reset(this.mCurRankList, this.mCurRankType);
				this.SetTargetBtnToggleEnable(6);
				break;
			}
			this.mPreRankType = this.mCurRankType;
		}
	}

	// Token: 0x06004493 RID: 17555 RVA: 0x00156540 File Offset: 0x00154740
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerRankInfoRoot);
		this.mModelRankLogic.UnLoadFakeObj();
		switch (this.BackRankType)
		{
		case RANK_TYPE.LADDER:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.ResetToPVP();
			}, null);
			break;
		case RANK_TYPE.TOWER:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.OnClickDailyBtn(MAPTYPE.INVALID, null, null, GameDefine.ACTIVITY_TYPE.TOWER);
			}, null);
			break;
		case RANK_TYPE.CAR:
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.NewActivityUIRootLogic, delegate
			{
				SingletonUnity<NewActivityUIRootLogic>.Instance.ResetToDaily();
			}, null);
			break;
		}
		this.ResetNormalLight();
	}

	// Token: 0x06004494 RID: 17556 RVA: 0x0015663C File Offset: 0x0015483C
	private void OnDisable()
	{
		this.mModelRankLogic.UnLoadFakeObj();
		this.ResetNormalLight();
	}

	// Token: 0x0400312A RID: 12586
	public UIGrid LeftTabGrid;

	// Token: 0x0400312B RID: 12587
	public MenuBaseTabBtnLogic BtnPrefab;

	// Token: 0x0400312C RID: 12588
	public List<MenuBaseTabBtnLogic> mCurBtnList;

	// Token: 0x0400312D RID: 12589
	private List<MenuTabBtnInfo> mCurMenuTabBtnInfoList;

	// Token: 0x0400312E RID: 12590
	public ModelRankRootLogic mModelRankLogic;

	// Token: 0x0400312F RID: 12591
	public NoModelRankRootLogic mNoModelRankLogic;

	// Token: 0x04003130 RID: 12592
	private RANK_TYPE mCurRankType;

	// Token: 0x04003131 RID: 12593
	private RANK_TYPE mPreRankType;

	// Token: 0x04003132 RID: 12594
	private RANK_TYPE BackRankType;

	// Token: 0x04003133 RID: 12595
	private List<sort_item> mCurRankList = new List<sort_item>();

	// Token: 0x04003134 RID: 12596
	private Color ambientLight;
}
