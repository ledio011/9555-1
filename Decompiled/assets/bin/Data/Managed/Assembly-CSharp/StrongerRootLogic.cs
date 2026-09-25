using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009B3 RID: 2483
public class StrongerRootLogic : SingletonUnity<StrongerRootLogic>
{
	// Token: 0x060046A9 RID: 18089 RVA: 0x00166660 File Offset: 0x00164860
	private void OnEnable()
	{
		this.TargetTypePage = -1;
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.MenuBaseRootUI, delegate(bool bSuccess, object param)
		{
			List<MenuTabBtnInfo> leftBtnInfo = new List<MenuTabBtnInfo>();
			SingletonUnity<MenuBaseRootLogic>.Instance.ResetPage(leftBtnInfo, new DelegateDefine.NoParamDelegate(this.OnClickCloseBtn), true, null);
			SingletonUnity<MenuBaseRootLogic>.Instance.SetPageLabel(StrDictionary.GetDictionaryString("#{800101}", new object[0]));
		}, null);
		this.InitTexture();
	}

	// Token: 0x060046AA RID: 18090 RVA: 0x0016668C File Offset: 0x0016488C
	public void InitTexture()
	{
		if (this.BannerTexture.mainTexture == null && UnityVersionUtil.IsactiveInHierarchy(base.gameObject))
		{
			base.StartCoroutine(BundleManager.LoadTexture(GameDefine.TestureBannerStronger, new BundleManager.LoadTextureFinish(this.TextureLoadFinish)));
		}
	}

	// Token: 0x060046AB RID: 18091 RVA: 0x001666DC File Offset: 0x001648DC
	private void TextureLoadFinish(string name, Texture textureObj)
	{
		this.BannerTexture.mainTexture = textureObj;
	}

	// Token: 0x060046AC RID: 18092 RVA: 0x001666EC File Offset: 0x001648EC
	public void SetTargetPage(StrongerRootLogic.LeftPage targetid)
	{
		this.TargetTypePage = (int)targetid;
	}

	// Token: 0x060046AD RID: 18093 RVA: 0x001666F8 File Offset: 0x001648F8
	public void Reset(GameDefine.UIBACKTYPE needback = GameDefine.UIBACKTYPE.NOTHINTG)
	{
		this.curType = -1;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.LevelLabel.text = string.Format("Lv.{0}", playerData.Level);
		this.CurPower.text = playerData.MainPlayerAttrData.ComboValue.ToString();
		BaseLvData levelDataByLevel = DataManager.GetLevelDataByLevel(playerData.Level);
		this.TargetPower.text = string.Format("{0}", levelDataByLevel.RecomPower);
		float num = (float)playerData.MainPlayerAttrData.ComboValue / (float)levelDataByLevel.RecomPower;
		if (num >= 0.9f)
		{
			this.Rank1Label.text = "A";
			this.Rank2Label.text = "A";
		}
		else if (num >= 0.75f)
		{
			this.Rank1Label.text = "B";
			this.Rank2Label.text = "B";
		}
		else if (num >= 0.5f)
		{
			this.Rank1Label.text = "C";
			this.Rank2Label.text = "C";
		}
		else
		{
			this.Rank1Label.text = "D";
			this.Rank2Label.text = "D";
		}
		int num2 = GameDefine.STRONGER_TYPE_NAME.Count - this.leftItemList.Count;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = Object.Instantiate(this.leftItemList[0].gameObject) as GameObject;
				this.LeftGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("left{0:d2}", this.leftItemList.Count);
				this.leftItemList.Add(gameObject.GetComponent<StrongerItemLogic>());
			}
		}
		int num3 = 0;
		for (int j = 0; j < this.leftItemList.Count; j++)
		{
			if (j < GameDefine.STRONGER_TYPE_NAME.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.leftItemList[j].gameObject, true);
				this.leftItemList[j].Reset(j + 1, new DelegateDefine.OneIntParamDelegate(this.OnClickLeftBtn));
				if (j + 1 == this.TargetTypePage)
				{
					num3 = j;
				}
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.leftItemList[j].gameObject, false);
			}
		}
		this.LeftScrollView.ResetPosition();
		this.LeftGrid.Reposition();
		this.leftItemList[num3].OnClickItemBtn();
		if (num3 > 6)
		{
			this.LeftSlider.value = 1f;
		}
		this.curBackType = needback;
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("BeStronger", "BeStronger", "open");
	}

	// Token: 0x060046AE RID: 18094 RVA: 0x00166A04 File Offset: 0x00164C04
	public void OnClickLeftBtn(int selecttype)
	{
		if (this.curType == selecttype)
		{
			return;
		}
		this.curType = selecttype;
		this.SelectLeftItem();
		this.curBackType = GameDefine.UIBACKTYPE.NOTHINTG;
		this.GetCurList();
		int num = this.curStrongList.Count - this.rightLineList.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate(this.rightLineList[0].gameObject) as GameObject;
				this.RightGrid.AddChild(gameObject.transform);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.name = string.Format("right{0:d2}", this.rightLineList.Count);
				this.rightLineList.Add(gameObject.GetComponent<StrongerLineLogic>());
			}
		}
		for (int j = 0; j < this.rightLineList.Count; j++)
		{
			if (j < this.curStrongList.Count)
			{
				UnityVersionUtil.SetActiveRecursive(this.rightLineList[j].gameObject, true);
				this.rightLineList[j].Reset(this.curStrongList[j]);
			}
			else
			{
				UnityVersionUtil.SetActiveRecursive(this.rightLineList[j].gameObject, false);
			}
		}
		this.RightScrollview.ResetPosition();
		this.RightGrid.Reposition();
		SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("BeStronger", "BeStronger", string.Format("clicktab_{0}", this.curType));
	}

	// Token: 0x060046AF RID: 18095 RVA: 0x00166BA8 File Offset: 0x00164DA8
	private void SelectLeftItem()
	{
		for (int i = 0; i < this.leftItemList.Count; i++)
		{
			this.leftItemList[i].RefershSelect(this.curType);
		}
	}

	// Token: 0x060046B0 RID: 18096 RVA: 0x00166BE8 File Offset: 0x00164DE8
	public void GetCurList()
	{
		this.curStrongList.Clear();
		List<StrongerData> strongerDataList = DataManager.GetStrongerDataList();
		for (int i = 0; i < strongerDataList.Count; i++)
		{
			if (strongerDataList[i].StrongerType == this.curType)
			{
				if (this.curType == 1)
				{
					if (this.CheckType((GameDefine.STRONGER_ACTIVITY)strongerDataList[i].Type) && this.CheckLevel(strongerDataList[i].MinLevel, strongerDataList[i].MaxLevel))
					{
						this.curStrongList.Add(strongerDataList[i]);
					}
				}
				else
				{
					this.curStrongList.Add(strongerDataList[i]);
				}
			}
		}
		this.curStrongList.Sort(delegate(StrongerData x, StrongerData y)
		{
			if (x.SortID == y.SortID)
			{
				return int.Parse(x.ID) - int.Parse(y.ID);
			}
			return x.SortID - y.SortID;
		});
	}

	// Token: 0x060046B1 RID: 18097 RVA: 0x00166CCC File Offset: 0x00164ECC
	public bool CheckLevel(int minlevel, int maxlevel)
	{
		if (maxlevel <= 0)
		{
			return true;
		}
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		return playerData.Level >= minlevel && playerData.Level < maxlevel;
	}

	// Token: 0x060046B2 RID: 18098 RVA: 0x00166D08 File Offset: 0x00164F08
	public bool CheckType(GameDefine.STRONGER_ACTIVITY strongertype)
	{
		if (strongertype == GameDefine.STRONGER_ACTIVITY.FIRSTBUY)
		{
			PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			return playerCommonData.First_PackFlag;
		}
		if (strongertype == GameDefine.STRONGER_ACTIVITY.BIGSALE)
		{
			PlayerCommonData playerCommonData2 = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
			return playerCommonData2.Big_PackFlag;
		}
		return true;
	}

	// Token: 0x060046B3 RID: 18099 RVA: 0x00166D4C File Offset: 0x00164F4C
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.StrongerRoot);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		if (this.curBackType == GameDefine.UIBACKTYPE.EQUIP)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickEquipBackPackBtn();
			}, null);
		}
		else if (this.curBackType == GameDefine.UIBACKTYPE.BADGE)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickBadgeBtn();
			}, null);
		}
		else if (this.curBackType == GameDefine.UIBACKTYPE.ITEM)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.PlayerInfoMenuRoot, delegate(bool isSuccess, object param)
			{
				SingletonUnity<PlayerInfoMenuRootLogic>.Instance.OnClickItemBackPackBtn();
			}, null);
		}
	}

	// Token: 0x040033AD RID: 13229
	private GameDefine.UIBACKTYPE curBackType;

	// Token: 0x040033AE RID: 13230
	public UIGrid LeftGrid;

	// Token: 0x040033AF RID: 13231
	public List<StrongerItemLogic> leftItemList;

	// Token: 0x040033B0 RID: 13232
	public UIScrollView LeftScrollView;

	// Token: 0x040033B1 RID: 13233
	public UIGrid RightGrid;

	// Token: 0x040033B2 RID: 13234
	public List<StrongerLineLogic> rightLineList;

	// Token: 0x040033B3 RID: 13235
	public UIScrollView RightScrollview;

	// Token: 0x040033B4 RID: 13236
	public UILabel LevelLabel;

	// Token: 0x040033B5 RID: 13237
	public UILabel TargetPower;

	// Token: 0x040033B6 RID: 13238
	public UILabel CurPower;

	// Token: 0x040033B7 RID: 13239
	public UILabel Rank1Label;

	// Token: 0x040033B8 RID: 13240
	public UILabel Rank2Label;

	// Token: 0x040033B9 RID: 13241
	public UITexture BannerTexture;

	// Token: 0x040033BA RID: 13242
	public UISlider LeftSlider;

	// Token: 0x040033BB RID: 13243
	private List<StrongerData> curStrongList = new List<StrongerData>();

	// Token: 0x040033BC RID: 13244
	private int curType;

	// Token: 0x040033BD RID: 13245
	private int TargetTypePage = -1;

	// Token: 0x020009B4 RID: 2484
	public enum LeftPage
	{
		// Token: 0x040033C3 RID: 13251
		expPage = 2,
		// Token: 0x040033C4 RID: 13252
		equipPage = 6,
		// Token: 0x040033C5 RID: 13253
		badgePage,
		// Token: 0x040033C6 RID: 13254
		MaterialPage = 12
	}
}
