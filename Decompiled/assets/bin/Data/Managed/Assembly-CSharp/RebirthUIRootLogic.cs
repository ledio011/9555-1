using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000982 RID: 2434
public class RebirthUIRootLogic : SingletonUnity<RebirthUIRootLogic>
{
	// Token: 0x060044D2 RID: 17618 RVA: 0x00157E58 File Offset: 0x00156058
	public void Reset(notice_relife_player.request request)
	{
		TutorialManager.CloseTutorial();
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mCurRebirthType = (REBIRTH_TYPE)request.type;
		this.mCurItemCost = (int)request.cost;
		this.mCurItemId = request.itemId;
		this.mStartTime = Time.time;
		if (this.mCurRebirthType == REBIRTH_TYPE.MAIN_CITY_REBIRTH)
		{
			this.ReturnBtnLabel.text = StrDictionary.GetDictionaryString("#{100502}", new object[0]);
		}
		else
		{
			this.ReturnBtnLabel.text = StrDictionary.GetDictionaryString("#{100503}", new object[0]);
		}
		ItemData itemDataByID = DataManager.GetItemDataByID(this.mCurItemId);
		this.IconPic.spriteName = itemDataByID.BackPackIcon;
		this.IconQuality.spriteName = itemDataByID.QualityType.ToString();
		bool flag = false;
		this.KillID = -1L;
		if (request.HasCharacterid && !SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo.IsAlreadyEnemy(request.characterid))
		{
			this.KillID = request.characterid;
			flag = true;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP))
		{
			this.refershItemInfo();
			if (flag)
			{
				this.BackTra.transform.localPosition = new Vector3(-170f, -77f, 0f);
				UnityVersionUtil.SetActiveRecursive(this.relifeTra.transform.gameObject, true);
				this.relifeTra.transform.localPosition = new Vector3(0f, -77f, 0f);
				UnityVersionUtil.SetActiveRecursive(this.AddFoeTra.transform.gameObject, true);
				this.InfoLabel.text = StrDictionary.GetDictionaryString("#{103307}", new object[]
				{
					request.name,
					request.name
				});
				this.AddFoeTra.transform.localPosition = new Vector3(170f, -77f, 0f);
			}
			else
			{
				this.BackTra.transform.localPosition = new Vector3(-99f, -77f, 0f);
				UnityVersionUtil.SetActiveRecursive(this.relifeTra.transform.gameObject, true);
				this.relifeTra.transform.localPosition = new Vector3(125f, -77f, 0f);
				UnityVersionUtil.SetActiveRecursive(this.AddFoeTra.transform.gameObject, false);
				this.InfoLabel.text = string.Empty;
			}
			this.mCurTimeCount = this.mWaitTime;
			this.TimeLabel.text = string.Format("{0}s", this.mCurTimeCount);
			this.IsBtnEnable = false;
			this.BackCityBtn.spriteName = GameDefine.BtnIcon[2];
		}
		else
		{
			List<GameItem> itemByItemId = this.mPlayerData.ItemBackPack.GetItemByItemId(this.mCurItemId);
			this.mCurItemNum = 0;
			for (int i = 0; i < itemByItemId.Count; i++)
			{
				this.mCurItemNum += itemByItemId[i].StackNum;
			}
			this.ItemNumLabel.text = string.Format("{0}/{1}", this.mCurItemCost, this.mCurItemNum);
			if (this.mCurItemCost <= this.mCurItemNum)
			{
				this.ItemNumLabel.color = Color.white;
				if (flag)
				{
					this.BackTra.transform.localPosition = new Vector3(-170f, -77f, 0f);
					UnityVersionUtil.SetActiveRecursive(this.relifeTra.transform.gameObject, true);
					this.relifeTra.transform.localPosition = new Vector3(0f, -77f, 0f);
					UnityVersionUtil.SetActiveRecursive(this.AddFoeTra.transform.gameObject, true);
					this.InfoLabel.text = StrDictionary.GetDictionaryString("#{103307}", new object[]
					{
						request.name,
						request.name
					});
					this.AddFoeTra.transform.localPosition = new Vector3(170f, -77f, 0f);
				}
				else
				{
					this.BackTra.transform.localPosition = new Vector3(-99f, -77f, 0f);
					UnityVersionUtil.SetActiveRecursive(this.relifeTra.transform.gameObject, true);
					this.relifeTra.transform.localPosition = new Vector3(125f, -77f, 0f);
					UnityVersionUtil.SetActiveRecursive(this.AddFoeTra.transform.gameObject, false);
					this.InfoLabel.text = string.Empty;
				}
				this.mCurTimeCount = this.mWaitTime;
				this.TimeLabel.text = string.Format("{0}s", this.mCurTimeCount);
				this.IsBtnEnable = false;
				this.BackCityBtn.spriteName = GameDefine.BtnIcon[2];
			}
			else
			{
				this.mCurTimeCount = 0;
				this.TimeLabel.text = string.Empty;
				this.IsBtnEnable = true;
				this.BackCityBtn.spriteName = GameDefine.BtnIcon[0];
				if (flag)
				{
					this.BackTra.transform.localPosition = new Vector3(-99f, -77f, 0f);
					UnityVersionUtil.SetActiveRecursive(this.relifeTra.transform.gameObject, false);
					UnityVersionUtil.SetActiveRecursive(this.AddFoeTra.transform.gameObject, true);
					this.InfoLabel.text = StrDictionary.GetDictionaryString("#{103307}", new object[]
					{
						request.name,
						request.name
					});
					this.AddFoeTra.transform.localPosition = new Vector3(99f, -77f, 0f);
				}
				else
				{
					this.ItemNumLabel.color = Color.red;
					this.BackTra.transform.localPosition = new Vector3(0f, -63f, 0f);
					UnityVersionUtil.SetActiveRecursive(this.relifeTra.transform.gameObject, false);
					UnityVersionUtil.SetActiveRecursive(this.AddFoeTra.transform.gameObject, false);
					this.InfoLabel.text = string.Empty;
				}
			}
		}
		LocalDataSaveManager.SetDiedFlag(1);
		this.UpdateBtnInfo();
	}

	// Token: 0x060044D3 RID: 17619 RVA: 0x001584B8 File Offset: 0x001566B8
	public void OnClickInPlaceBtn()
	{
		if (this.mCurItemNum >= this.mCurItemCost)
		{
			relife_player.request request = new relife_player.request();
			request.isInplace = true;
			NetLogic.GetInstance().Send<Protocol.relife_player>(request, null);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RebirthUIRoot);
		}
		else
		{
			GameMoneyHelper.ShowItemProductTop(this.mCurItemId, GameDefine.SHOP_TYPE.EQUIP_SHOP);
		}
	}

	// Token: 0x060044D4 RID: 17620 RVA: 0x00158510 File Offset: 0x00156710
	public void OnClickBackRebirthBtn()
	{
		if (this.IsBtnEnable)
		{
			relife_player.request request = new relife_player.request();
			request.isInplace = false;
			NetLogic.GetInstance().Send<Protocol.relife_player>(request, null);
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RebirthUIRoot);
		}
	}

	// Token: 0x060044D5 RID: 17621 RVA: 0x00158550 File Offset: 0x00156750
	public void OnClickAddFoeBtn()
	{
		if (this.KillID == -1L)
		{
			return;
		}
		FriendInfo friendInfo = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.FriendInfo;
		if (friendInfo.IsCanAddEnemy() && !friendInfo.IsAlreadyEnemy(this.KillID))
		{
			NoticeLogic.AddNotifyData("#{103320}", true, false);
			add_friend.request request = new add_friend.request();
			request.characterId = this.KillID;
			request.type = 1L;
			NetLogic.GetInstance().Send<Protocol.add_friend>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Social", "Enemy", "add_times");
		}
		else
		{
			NoticeLogic.AddNotifyData("#{100265}", true, false);
		}
	}

	// Token: 0x060044D6 RID: 17622 RVA: 0x001585F4 File Offset: 0x001567F4
	public void refershItemInfo()
	{
		List<GameItem> itemByItemId = this.mPlayerData.ItemBackPack.GetItemByItemId(this.mCurItemId);
		this.mCurItemNum = 0;
		for (int i = 0; i < itemByItemId.Count; i++)
		{
			this.mCurItemNum += itemByItemId[i].StackNum;
		}
		this.ItemNumLabel.text = string.Format("{0}/{1}", this.mCurItemCost, this.mCurItemNum);
		if (this.mCurItemCost <= this.mCurItemNum)
		{
			this.ItemNumLabel.color = Color.white;
		}
		else
		{
			this.ItemNumLabel.color = Color.red;
		}
	}

	// Token: 0x060044D7 RID: 17623 RVA: 0x001586B0 File Offset: 0x001568B0
	private void Update()
	{
		if (this.IsBtnEnable)
		{
			return;
		}
		this.tempTime = (int)(Time.time - this.mStartTime);
		if (this.mCurTimeCount != this.mWaitTime - this.tempTime)
		{
			this.mCurTimeCount = this.mWaitTime - this.tempTime;
			if (this.mCurTimeCount < 0)
			{
				this.IsBtnEnable = true;
				this.BackCityBtn.spriteName = GameDefine.BtnIcon[0];
				this.TimeLabel.text = string.Empty;
			}
			else
			{
				this.TimeLabel.text = string.Format("{0}s", this.mCurTimeCount);
			}
		}
	}

	// Token: 0x060044D8 RID: 17624 RVA: 0x00158764 File Offset: 0x00156964
	private void BackToRebirth()
	{
		relife_player.request request = new relife_player.request();
		request.isInplace = false;
		NetLogic.GetInstance().Send<Protocol.relife_player>(request, null);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.RebirthUIRoot);
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x0015879C File Offset: 0x0015699C
	public void UpdateBtnInfo()
	{
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		NGUITools.SetActive(this.TopObj.gameObject, true);
		NGUITools.SetActive(this.DownObj.gameObject, true);
		bool flag = false;
		bool flag2 = false;
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SHOP))
		{
			NGUITools.SetActive(this.ShopBtn.gameObject, true);
			flag = true;
		}
		else
		{
			NGUITools.SetActive(this.ShopBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.BIGSALES) && playerCommonData.Big_PackFlag)
		{
			NGUITools.SetActive(this.BigSaleBtn.gameObject, true);
			flag = true;
		}
		else
		{
			NGUITools.SetActive(this.BigSaleBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.LOTTO))
		{
			NGUITools.SetActive(this.SlotBtn.gameObject, true);
			flag = true;
		}
		else
		{
			NGUITools.SetActive(this.SlotBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.GIFT))
		{
			NGUITools.SetActive(this.WelfareBtn.gameObject, true);
			flag = true;
		}
		else
		{
			NGUITools.SetActive(this.WelfareBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.MYSTERYSHOP))
		{
			if (playerCommonData.Push != -1L && (playerCommonData.Push & 16L) == 0L)
			{
				NGUITools.SetActive(this.MysteryBtn.gameObject, false);
			}
			else
			{
				NGUITools.SetActive(this.MysteryBtn.gameObject, true);
				flag = true;
			}
		}
		else
		{
			NGUITools.SetActive(this.MysteryBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL) && this.CheckSkillUpdateTips())
		{
			NGUITools.SetActive(this.SkillBtn.gameObject, true);
			flag2 = true;
		}
		else
		{
			NGUITools.SetActive(this.SkillBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_EQUIP) && playerData.IsHaveEnhanceTips())
		{
			NGUITools.SetActive(this.CustomizeBtn.gameObject, true);
			flag2 = true;
		}
		else
		{
			NGUITools.SetActive(this.CustomizeBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.ENHANCE_STAR) && playerData.IsHaveRefineTips())
		{
			NGUITools.SetActive(this.StrengthBtn.gameObject, true);
			flag2 = true;
		}
		else
		{
			NGUITools.SetActive(this.StrengthBtn.gameObject, false);
		}
		if (playerCommonData.IsFunctionUnlock(FUNCTION_TYPE.CHARACTER_EQUIP) && playerData.IsHaveCanInherit())
		{
			NGUITools.SetActive(this.InhertBtn.gameObject, true);
			flag2 = true;
		}
		else
		{
			NGUITools.SetActive(this.InhertBtn.gameObject, false);
		}
		this.TopGrid.Reposition();
		this.DownGrid.Reposition();
		if (!flag)
		{
			NGUITools.SetActive(this.TopObj.gameObject, false);
		}
		if (!flag2)
		{
			NGUITools.SetActive(this.DownObj.gameObject, false);
		}
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x00158A94 File Offset: 0x00156C94
	public void OnClickBigSaleBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.BIGSALE);
		this.BackToRebirth();
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x00158AA4 File Offset: 0x00156CA4
	public void OnClickShopBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.SHOP);
		this.BackToRebirth();
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x00158AB4 File Offset: 0x00156CB4
	public void OnClickSlotBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.SLOT);
		this.BackToRebirth();
	}

	// Token: 0x060044DD RID: 17629 RVA: 0x00158AC4 File Offset: 0x00156CC4
	public void OnClickWelfareBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.WELFARE);
		this.BackToRebirth();
	}

	// Token: 0x060044DE RID: 17630 RVA: 0x00158AD4 File Offset: 0x00156CD4
	public void OnClickMysteryBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.MYSTERY);
		this.BackToRebirth();
	}

	// Token: 0x060044DF RID: 17631 RVA: 0x00158AE4 File Offset: 0x00156CE4
	public void OnClickSkillBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.SKILL);
		this.BackToRebirth();
	}

	// Token: 0x060044E0 RID: 17632 RVA: 0x00158AF4 File Offset: 0x00156CF4
	public void OnClickCustomizeBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.ENHANCE_EQUIP);
		this.BackToRebirth();
	}

	// Token: 0x060044E1 RID: 17633 RVA: 0x00158B04 File Offset: 0x00156D04
	public void OnClickStrengthBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.ENHANCE_STAR);
		this.BackToRebirth();
	}

	// Token: 0x060044E2 RID: 17634 RVA: 0x00158B14 File Offset: 0x00156D14
	public void OnClickInhertBtn()
	{
		UIManager.SetSpecialType(UIManager.SHOW_TYPE.INHERT);
		this.BackToRebirth();
	}

	// Token: 0x060044E3 RID: 17635 RVA: 0x00158B24 File Offset: 0x00156D24
	public bool CheckSkillUpdateTips()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL))
		{
			return false;
		}
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		for (int i = 0; i < mainPlayer.CharacterSkillData.Count; i++)
		{
			CharacterSkillData characterSkillData = mainPlayer.CharacterSkillData[i];
			if (characterSkillData != null && SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.CheckLevel(characterSkillData.UnlockLevel))
			{
				int index = characterSkillData.Index;
				if (index >= 4 && index <= 6)
				{
					SkillData skillDataById = DataManager.GetSkillDataById(characterSkillData.ID);
					SkillupgradeData skillupgradeDataByLevel = DataManager.GetSkillupgradeDataByLevel(characterSkillData.Level + 1);
					if (skillDataById != null && skillupgradeDataByLevel != null && skillDataById.IsUpgrade != 0)
					{
						if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level > characterSkillData.Level + 1 && GameMoneyHelper.GetMoneyNum(skillupgradeDataByLevel.PriceType) > (long)skillupgradeDataByLevel.PriceValue)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04003185 RID: 12677
	public UILabel TimeLabel;

	// Token: 0x04003186 RID: 12678
	public UILabel ItemNumLabel;

	// Token: 0x04003187 RID: 12679
	public UISprite IconPic;

	// Token: 0x04003188 RID: 12680
	public UISprite IconQuality;

	// Token: 0x04003189 RID: 12681
	public UILabel InPlaceBtnLabel;

	// Token: 0x0400318A RID: 12682
	public UILabel ReturnBtnLabel;

	// Token: 0x0400318B RID: 12683
	private REBIRTH_TYPE mCurRebirthType;

	// Token: 0x0400318C RID: 12684
	private int mCurItemCost;

	// Token: 0x0400318D RID: 12685
	private string mCurItemId;

	// Token: 0x0400318E RID: 12686
	private int mCurItemNum;

	// Token: 0x0400318F RID: 12687
	private int mWaitTime = 10;

	// Token: 0x04003190 RID: 12688
	private int mCurTimeCount;

	// Token: 0x04003191 RID: 12689
	private float mStartTime;

	// Token: 0x04003192 RID: 12690
	private PlayerData mPlayerData;

	// Token: 0x04003193 RID: 12691
	private bool IsBtnEnable;

	// Token: 0x04003194 RID: 12692
	public UISprite BackCityBtn;

	// Token: 0x04003195 RID: 12693
	public Transform BackTra;

	// Token: 0x04003196 RID: 12694
	public Transform relifeTra;

	// Token: 0x04003197 RID: 12695
	public Transform AddFoeTra;

	// Token: 0x04003198 RID: 12696
	public UILabel InfoLabel;

	// Token: 0x04003199 RID: 12697
	public GameObject ShopBtn;

	// Token: 0x0400319A RID: 12698
	public GameObject BigSaleBtn;

	// Token: 0x0400319B RID: 12699
	public GameObject SlotBtn;

	// Token: 0x0400319C RID: 12700
	public GameObject WelfareBtn;

	// Token: 0x0400319D RID: 12701
	public GameObject MysteryBtn;

	// Token: 0x0400319E RID: 12702
	public GameObject SkillBtn;

	// Token: 0x0400319F RID: 12703
	public GameObject CustomizeBtn;

	// Token: 0x040031A0 RID: 12704
	public GameObject StrengthBtn;

	// Token: 0x040031A1 RID: 12705
	public GameObject InhertBtn;

	// Token: 0x040031A2 RID: 12706
	public GameObject TopObj;

	// Token: 0x040031A3 RID: 12707
	public GameObject DownObj;

	// Token: 0x040031A4 RID: 12708
	public UIGrid TopGrid;

	// Token: 0x040031A5 RID: 12709
	public UIGrid DownGrid;

	// Token: 0x040031A6 RID: 12710
	private long KillID;

	// Token: 0x040031A7 RID: 12711
	private int tempTime;
}
