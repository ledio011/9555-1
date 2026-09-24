using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000962 RID: 2402
public class OtherPlayerInfoUILogic : SingletonUnity<OtherPlayerInfoUILogic>
{
	// Token: 0x06004377 RID: 17271 RVA: 0x0014D504 File Offset: 0x0014B704
	public void ResetEnable()
	{
		this.leftEffect.resetOnPlay = true;
		this.leftEffect.Play(true);
	}

	// Token: 0x06004378 RID: 17272 RVA: 0x0014D520 File Offset: 0x0014B720
	private void OnEnable()
	{
		this.curSelect = -1;
		this.ResetFakeObjRoot();
	}

	// Token: 0x06004379 RID: 17273 RVA: 0x0014D530 File Offset: 0x0014B730
	public void EnableReset()
	{
		UnityVersionUtil.SetActiveRecursive(this.FashionRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.BadgeRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RefineRoot.gameObject, false);
	}

	// Token: 0x0600437A RID: 17274 RVA: 0x0014D570 File Offset: 0x0014B770
	public void ResetOtherPlayerInfo(List<GameItem> list, PROFESSION_TYPE Type, string ModelName, CharacterAttributeData data, character_look look)
	{
		this.targetAttribute = data;
		this.mCurItemDataList.Clear();
		this.mCurItemDataList = ItemContainerTool.GetEquipItemList(list);
		this.mCurProfessionType = Type;
		this.CurCharacterLook = look;
		this.mModelName = ModelName;
		this.CurFashionItemList.Clear();
		if (look.HasFashion_equip)
		{
			List<GameItem> gameItemList = this.GetGameItemList(new List<gameitem>(look.fashion_equip.Values));
			this.CurFashionItemList = ItemContainerTool.GetFashionEquipItemList(gameItemList);
		}
		this.CurBadgeItemList.Clear();
		if (look.HasBadge_equip)
		{
			List<GameItem> gameItemList2 = this.GetGameItemList(new List<gameitem>(look.badge_equip.Values));
			this.CurBadgeItemList = ItemContainerTool.GetBadgeEquipItemList(gameItemList2);
		}
		this.NameLabel.text = string.Format("Lv:{0}  {1}", this.targetAttribute.Level, this.targetAttribute.Name);
		this.FightLabel.text = this.targetAttribute.ComboValue.ToString();
		this.jssInfo.Show(this.targetAttribute);
		this.OnClickEquipBtn();
		this.ResetModelVisual();
		if (this.CurCharacterLook.HasSkills)
		{
			this.UpdateSkillList(this.CurCharacterLook.skills);
		}
	}

	// Token: 0x17000FA2 RID: 4002
	// (get) Token: 0x0600437B RID: 17275 RVA: 0x0014D6B4 File Offset: 0x0014B8B4
	// (set) Token: 0x0600437C RID: 17276 RVA: 0x0014D6BC File Offset: 0x0014B8BC
	public List<CharacterSkillData> CharacterSkillData
	{
		get
		{
			return this.mCharacterSkillData;
		}
		set
		{
			this.mCharacterSkillData = value;
		}
	}

	// Token: 0x17000FA3 RID: 4003
	// (get) Token: 0x0600437D RID: 17277 RVA: 0x0014D6C8 File Offset: 0x0014B8C8
	// (set) Token: 0x0600437E RID: 17278 RVA: 0x0014D6D0 File Offset: 0x0014B8D0
	public List<string> PlayerSkillIDList
	{
		get
		{
			return this.mPlayerSkillIDList;
		}
		set
		{
			this.mPlayerSkillIDList = value;
		}
	}

	// Token: 0x0600437F RID: 17279 RVA: 0x0014D6DC File Offset: 0x0014B8DC
	public void UpdateSkillList(Dictionary<string, skill_info> skills)
	{
		this.CharacterSkillData.Clear();
		if (skills == null)
		{
			return;
		}
		int count = skills.Count;
		foreach (KeyValuePair<string, skill_info> keyValuePair in skills)
		{
			this.CharacterSkillData.Add(new CharacterSkillData(keyValuePair.Value.skillId, (int)keyValuePair.Value.skillLevel, (int)keyValuePair.Value.indexPos, (int)keyValuePair.Value.indexPos2, keyValuePair.Value.disable));
		}
	}

	// Token: 0x06004380 RID: 17280 RVA: 0x0014D7A0 File Offset: 0x0014B9A0
	public int GetPlayerSkillLevelByPos(int skillpos)
	{
		for (int i = 0; i < this.CharacterSkillData.Count; i++)
		{
			if (this.CharacterSkillData[i].Index2 == skillpos)
			{
				return this.CharacterSkillData[i].Level;
			}
		}
		return 0;
	}

	// Token: 0x06004381 RID: 17281 RVA: 0x0014D7F4 File Offset: 0x0014B9F4
	private void UpdateSelectItemBtn(int selectid)
	{
		this.curSelect = selectid;
		for (int i = 0; i < this.ItemBtns.Length; i++)
		{
			if (selectid == i)
			{
				this.ItemBtns[i].alpha = 1f;
				this.SelectDir[i].enabled = true;
			}
			else
			{
				this.ItemBtns[i].alpha = 0.19607843f;
				this.SelectDir[i].enabled = false;
			}
		}
	}

	// Token: 0x06004382 RID: 17282 RVA: 0x0014D870 File Offset: 0x0014BA70
	public void OnClickEquipBtn()
	{
		if (this.curSelect == 0)
		{
			return;
		}
		UnityVersionUtil.SetActiveRecursive(this.FashionRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.BadgeRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RefineRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.EquipRoot.gameObject, true);
		UnityVersionUtil.SetActiveRecursive(this.modelViewRoot.gameObject, true);
		this.UpdateSelectItemBtn(0);
		for (int i = 0; i < this.ItemShowList.Count; i++)
		{
			this.ItemShowList[i].SetItemEmpty();
			if (i < this.mCurItemDataList.Count && this.mCurItemDataList[i] != null && !this.mCurItemDataList[i].IsEmpty())
			{
				int level = 0;
				if (this.mCurItemDataList[i].ItemData.Type == GameDefine.ITEM_TYPE.EQUIP)
				{
					level = this.targetAttribute.GetEquipEnhanceLevel(this.mCurItemDataList[i].ItemData.SubType);
				}
				this.ItemShowList[i].UpdateItem(this.mCurItemDataList[i], level, true);
			}
		}
	}

	// Token: 0x06004383 RID: 17283 RVA: 0x0014D9AC File Offset: 0x0014BBAC
	public void OnClickFashionBtn()
	{
		if (this.curSelect == 1)
		{
			return;
		}
		UnityVersionUtil.SetActiveRecursive(this.EquipRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.BadgeRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RefineRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.FashionRoot.gameObject, true);
		UnityVersionUtil.SetActiveRecursive(this.modelViewRoot.gameObject, true);
		this.UpdateSelectItemBtn(1);
		for (int i = 0; i < this.FashionItemList.Count; i++)
		{
			this.FashionItemList[i].SetItemEmpty();
			if (i < this.CurFashionItemList.Count && this.CurFashionItemList[i] != null)
			{
				this.FashionItemList[i].UpdateItem(this.CurFashionItemList[i], 0, false);
			}
		}
	}

	// Token: 0x06004384 RID: 17284 RVA: 0x0014DA90 File Offset: 0x0014BC90
	public void OnClickBadgeBtn()
	{
		if (this.curSelect == 2)
		{
			return;
		}
		UnityVersionUtil.SetActiveRecursive(this.EquipRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.FashionRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.modelViewRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RefineRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.BadgeRoot.gameObject, true);
		this.UpdateSelectItemBtn(2);
		for (int i = 0; i < this.BadgeItemList.Count; i++)
		{
			this.BadgeItemList[i].SetItemEmpty();
			if (i < this.CurBadgeItemList.Count && this.CurBadgeItemList[i] != null)
			{
				this.BadgeItemList[this.CurBadgeItemList[i].Parm[0]].UpdateItem(this.CurBadgeItemList[i], 0, false);
			}
		}
		this.UpdateBageItem();
	}

	// Token: 0x06004385 RID: 17285 RVA: 0x0014DB8C File Offset: 0x0014BD8C
	public void OnClickRefineBtn()
	{
		if (this.curSelect == 3)
		{
			return;
		}
		UnityVersionUtil.SetActiveRecursive(this.EquipRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.BadgeRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.FashionRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.modelViewRoot.gameObject, false);
		UnityVersionUtil.SetActiveRecursive(this.RefineRoot.gameObject, true);
		this.RefineItemList[0].text = this.CurCharacterLook.attribute_other.refineNeckLevel.ToString();
		this.RefineItemList[1].text = this.CurCharacterLook.attribute_other.refineRing1Level.ToString();
		this.RefineItemList[2].text = this.CurCharacterLook.attribute_other.refineRing2Level.ToString();
		this.RefineItemList[3].text = this.CurCharacterLook.attribute_other.refineBeltLevel.ToString();
		this.UpdateSelectItemBtn(3);
		this.ResetEnable();
	}

	// Token: 0x06004386 RID: 17286 RVA: 0x0014DCAC File Offset: 0x0014BEAC
	private void UpdateBageItem()
	{
		int[] array = new int[3];
		int[] array2 = new int[3];
		int num = 1;
		int num2 = 0;
		for (int i = 0; i < this.CurBadgeItemList.Count; i++)
		{
			GameItem gameItem = this.CurBadgeItemList[i];
			if (gameItem != null && !gameItem.IsEmpty())
			{
				ItemData itemData = gameItem.ItemData;
				BadgeData badgeDataById = DataManager.GetBadgeDataById(gameItem.ItemId);
				array[badgeDataById.Color]++;
				array2[badgeDataById.Color] += badgeDataById.Lv;
				this.BadgeAttributeName[num].text = GameDefine.GetAttributeName_S(badgeDataById.Status1);
				this.BadgeAttributeICON[num].spriteName = GameDefine.GetAttributeIcon(badgeDataById.Status1);
				this.BadgeAttributeValue[num].text = GameDefine.GetAttributeValueStr(badgeDataById.Status1, badgeDataById.Value1);
				num2 += this.CurBadgeItemList[i].GetItemCombatVal();
				num++;
			}
		}
		int num3 = num;
		for (int j = 0; j < this.BadgeAttributeObj.Count; j++)
		{
			NGUITools.SetActive(this.BadgeAttributeObj[j], j < num3);
		}
		int num4 = -1;
		int num5 = 0;
		for (int k = 0; k < array.Length; k++)
		{
			if (array[k] >= 3)
			{
				num4 = k;
				num5 = array2[k];
				break;
			}
		}
		List<int> list = new List<int>();
		if (num4 > -1)
		{
			for (int l = 0; l < this.CurBadgeItemList.Count; l++)
			{
				GameItem gameItem2 = this.CurBadgeItemList[l];
				if (gameItem2 != null && !gameItem2.IsEmpty())
				{
					ItemData itemData2 = gameItem2.ItemData;
					BadgeData badgeDataById2 = DataManager.GetBadgeDataById(gameItem2.ItemId);
					if (badgeDataById2.Color == num4)
					{
						list.Add(l);
					}
				}
			}
		}
		for (int m = 0; m < this.ItemEffects.Length; m++)
		{
			if (list.Contains(m))
			{
				this.ItemEffects[m].alpha = 1f;
			}
			else
			{
				this.ItemEffects[m].alpha = 0f;
			}
		}
		NGUITools.SetActive(this.BadgeAttributeObj[0], num4 > -1);
		if (num4 == 1)
		{
			ConfigData configDataByKey = DataManager.GetConfigDataByKey("badge_parm_2");
			ConfigData configDataByKey2 = DataManager.GetConfigDataByKey("badge_attribute_2");
			if (configDataByKey != null)
			{
				int value = Mathf.FloorToInt((float)num5 / configDataByKey.Valuef);
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(configDataByKey2.Valuei);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(configDataByKey2.Valuei);
				this.BadgeAttributeValue[0].text = GameDefine.GetAttributeValueStr(configDataByKey2.Valuei, value);
			}
			else
			{
				float num6 = Mathf.Pow((float)num5, 1.25f) / 3.94822f / 1000f;
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(1010);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(1010);
				this.BadgeAttributeValue[0].text = string.Format("+{0:P1}", num6);
			}
		}
		else if (num4 == 0)
		{
			ConfigData configDataByKey3 = DataManager.GetConfigDataByKey("badge_parm_1");
			ConfigData configDataByKey4 = DataManager.GetConfigDataByKey("badge_attribute_1");
			if (configDataByKey3 != null)
			{
				int value2 = Mathf.FloorToInt((float)num5 / configDataByKey3.Valuef);
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(configDataByKey4.Valuei);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(configDataByKey4.Valuei);
				this.BadgeAttributeValue[0].text = GameDefine.GetAttributeValueStr(configDataByKey4.Valuei, value2);
			}
			else
			{
				float num7 = Mathf.Pow((float)num5, 0.5555f) / 1.8411f / 100f;
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(1012);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(1012);
				this.BadgeAttributeValue[0].text = string.Format("+{0:P1}", num7);
			}
		}
		else if (num4 == 2)
		{
			ConfigData configDataByKey5 = DataManager.GetConfigDataByKey("badge_parm_3");
			ConfigData configDataByKey6 = DataManager.GetConfigDataByKey("badge_attribute_3");
			if (configDataByKey5 != null)
			{
				int value3 = Mathf.FloorToInt((float)num5 / configDataByKey5.Valuef);
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(configDataByKey6.Valuei);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(configDataByKey6.Valuei);
				this.BadgeAttributeValue[0].text = GameDefine.GetAttributeValueStr(configDataByKey6.Valuei, value3);
			}
			else
			{
				float num8 = Mathf.Pow((float)num5, 0.5f) / 1.73205f * 0.5f;
				this.BadgeAttributeName[0].text = GameDefine.GetAttributeName_S(1011);
				this.BadgeAttributeICON[0].spriteName = GameDefine.GetAttributeIcon(1011);
				this.BadgeAttributeValue[0].text = string.Format("+{0:F1}", num8);
			}
		}
		this.BadgeGrid.Reposition();
	}

	// Token: 0x06004387 RID: 17287 RVA: 0x0014E260 File Offset: 0x0014C460
	private List<GameItem> GetGameItemList(List<gameitem> list)
	{
		List<GameItem> list2 = new List<GameItem>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null)
			{
				GameItem gameItem = ServerToClientTools.ServerGameItemToClientGameItem(list[i]);
				if (gameItem != null)
				{
					list2.Add(gameItem);
				}
			}
		}
		return list2;
	}

	// Token: 0x06004388 RID: 17288 RVA: 0x0014E2B8 File Offset: 0x0014C4B8
	private void ResetFakeObjRoot()
	{
		this.RotateModelBtnListener.onDrag = new UIEventListener.VectorDelegate(this.OnDragModelBtn);
		FakeObjOtherPlayer instance = SingletonUnity<FakeObjOtherPlayer>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeObjOtherPlayerRoot");
			instance = SingletonUnity<FakeObjOtherPlayer>.Instance;
		}
		SingletonUnity<FakeObjOtherPlayer>.Instance.EnableFakeObjRoot();
		this.ModelPic.mainTexture = instance.ModelPic;
	}

	// Token: 0x06004389 RID: 17289 RVA: 0x0014E31C File Offset: 0x0014C51C
	private void ResetModelVisual()
	{
		if (this.mCurFakeObj != null)
		{
			this.mCurFakeObj.DestroyFakeObj();
		}
		if (this.mCurFakeObj == null || this.mCurFakeObj.FakeObj == null)
		{
			this.mCurFakeObj = new FakeObjLogic();
			this.mCurFakeObj.InitFakeObject(this.CurCharacterLook.visual, this.mCurProfessionType.ToString(), this.mModelName, SingletonUnity<FakeObjOtherPlayer>.Instance.MeshRoot, null, "FakeObj2");
		}
		else
		{
			this.mCurFakeObj.CheckFakeObject(this.CurCharacterLook.visual, null);
			this.mCurFakeObj.PlayAnim("idle", this.mModelName);
		}
	}

	// Token: 0x0600438A RID: 17290 RVA: 0x0014E3DC File Offset: 0x0014C5DC
	public void UnLoadFakeObj()
	{
		if (this.mCurFakeObj != null)
		{
			this.mCurFakeObj.DestroyFakeObj();
			this.mCurFakeObj = null;
		}
		if (SingletonUnity<FakeObjOtherPlayer>.Instance != null)
		{
			SingletonUnity<FakeObjOtherPlayer>.Instance.DisableFakeObjRoot();
		}
	}

	// Token: 0x0600438B RID: 17291 RVA: 0x0014E428 File Offset: 0x0014C628
	private void OnDragModelBtn(GameObject btn, Vector2 delta)
	{
		if (this.mCurFakeObj != null && this.mCurFakeObj.FakeObj != null)
		{
			this.mCurFakeObj.FakeObj.transform.localEulerAngles -= delta.x * Vector3.up;
		}
	}

	// Token: 0x0600438C RID: 17292 RVA: 0x0014E488 File Offset: 0x0014C688
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.PlayerInfoMenuPlayerInfoRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.OtherPlayerInfoUILogicRoot);
		this.UnLoadFakeObj();
	}

	// Token: 0x0600438D RID: 17293 RVA: 0x0014E4BC File Offset: 0x0014C6BC
	private void OnDisable()
	{
		this.UnLoadFakeObj();
	}

	// Token: 0x04003006 RID: 12294
	public List<RewardItem> ItemShowList;

	// Token: 0x04003007 RID: 12295
	public JSSXKuangUILogic jssInfo;

	// Token: 0x04003008 RID: 12296
	private FakeObjLogic mCurFakeObj;

	// Token: 0x04003009 RID: 12297
	public UIEventListener RotateModelBtnListener;

	// Token: 0x0400300A RID: 12298
	public UITexture ModelPic;

	// Token: 0x0400300B RID: 12299
	public UILabel NameLabel;

	// Token: 0x0400300C RID: 12300
	public UILabel FightLabel;

	// Token: 0x0400300D RID: 12301
	private CharacterAttributeData targetAttribute;

	// Token: 0x0400300E RID: 12302
	private List<GameItem> mCurItemDataList = new List<GameItem>();

	// Token: 0x0400300F RID: 12303
	private PROFESSION_TYPE mCurProfessionType;

	// Token: 0x04003010 RID: 12304
	private string mModelName;

	// Token: 0x04003011 RID: 12305
	private character_look CurCharacterLook;

	// Token: 0x04003012 RID: 12306
	public GameObject EquipRoot;

	// Token: 0x04003013 RID: 12307
	public GameObject FashionRoot;

	// Token: 0x04003014 RID: 12308
	public GameObject BadgeRoot;

	// Token: 0x04003015 RID: 12309
	public GameObject RefineRoot;

	// Token: 0x04003016 RID: 12310
	public GameObject modelViewRoot;

	// Token: 0x04003017 RID: 12311
	public List<RewardItem> FashionItemList;

	// Token: 0x04003018 RID: 12312
	public List<RewardItem> BadgeItemList;

	// Token: 0x04003019 RID: 12313
	public List<UILabel> RefineItemList;

	// Token: 0x0400301A RID: 12314
	private List<GameItem> CurFashionItemList = new List<GameItem>();

	// Token: 0x0400301B RID: 12315
	private List<GameItem> CurBadgeItemList = new List<GameItem>();

	// Token: 0x0400301C RID: 12316
	public UITexture[] ItemEffects;

	// Token: 0x0400301D RID: 12317
	public List<UILabel> BadgeAttributeName;

	// Token: 0x0400301E RID: 12318
	public List<UISprite> BadgeAttributeICON;

	// Token: 0x0400301F RID: 12319
	public List<UILabel> BadgeAttributeValue;

	// Token: 0x04003020 RID: 12320
	public List<GameObject> BadgeAttributeObj;

	// Token: 0x04003021 RID: 12321
	public UILabel BottomLabel;

	// Token: 0x04003022 RID: 12322
	public UIGrid BadgeGrid;

	// Token: 0x04003023 RID: 12323
	public UISprite[] ItemBtns;

	// Token: 0x04003024 RID: 12324
	public UISprite[] SelectDir;

	// Token: 0x04003025 RID: 12325
	public UIPlayTween leftEffect;

	// Token: 0x04003026 RID: 12326
	private int curSelect = -1;

	// Token: 0x04003027 RID: 12327
	protected List<CharacterSkillData> mCharacterSkillData = new List<CharacterSkillData>();

	// Token: 0x04003028 RID: 12328
	private List<string> mPlayerSkillIDList;
}
