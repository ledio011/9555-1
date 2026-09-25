using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000997 RID: 2455
public class SkillInfoRootLogic : SingletonUnity<SkillInfoRootLogic>
{
	// Token: 0x06004583 RID: 17795 RVA: 0x0015D580 File Offset: 0x0015B780
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mClickUpgradeBtnCount = 0;
		this.mOnClickTutorialBtn = tutorialEvent;
		if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK)
		{
			this.clickHandle = new vp_Timer.Handle();
		}
	}

	// Token: 0x06004584 RID: 17796 RVA: 0x0015D5A8 File Offset: 0x0015B7A8
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			TutorialManager.OnClickTutorialBtn onClickTutorialBtn = this.mOnClickTutorialBtn;
			this.mOnClickTutorialBtn = null;
			onClickTutorialBtn(false);
		}
	}

	// Token: 0x06004585 RID: 17797 RVA: 0x0015D5D8 File Offset: 0x0015B7D8
	private void UpgradeBtnTutorialCheck()
	{
		if (TutorialManager.CurStep != TUTORIAL_STEP.SKILL_UPGRADE_CLICK)
		{
			return;
		}
		this.mClickUpgradeBtnCount++;
		NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS.gameObject, false);
		NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.TipCircleSprite.gameObject, false);
		if (this.clickHandle != null)
		{
			this.clickHandle.Cancel();
		}
		vp_Timer.In(0.5f, delegate()
		{
			NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.HandTipTweenS.gameObject, true);
			NGUITools.SetActive(SingletonUnity<TutorialUIRootLogic>.Instance.TipCircleSprite.gameObject, true);
		}, this.clickHandle);
		if (this.mClickUpgradeBtnCount >= 3 && TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004586 RID: 17798 RVA: 0x0015D68C File Offset: 0x0015B88C
	private void CloseTutorial()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn = null;
			TutorialManager.CloseTutorial();
			SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.SetTutorialShowFinish(FUNCTION_TYPE.SKILL);
		}
	}

	// Token: 0x17000FAE RID: 4014
	// (get) Token: 0x06004587 RID: 17799 RVA: 0x0015D6BC File Offset: 0x0015B8BC
	// (set) Token: 0x06004588 RID: 17800 RVA: 0x0015D6C4 File Offset: 0x0015B8C4
	public FakeObjLogic PlayerModelVisual
	{
		get
		{
			return this.mPlayerModelVisual;
		}
		set
		{
			this.mPlayerModelVisual = value;
		}
	}

	// Token: 0x06004589 RID: 17801 RVA: 0x0015D6D0 File Offset: 0x0015B8D0
	public void PlaySkillLevelAnim(int level)
	{
		if (this.mCurLevel < level)
		{
			this.tweenAlpha.ResetToBeginning();
			this.tweenAlpha.PlayForward();
			this.tweenPosition.ResetToBeginning();
			this.tweenPosition.PlayForward();
		}
	}

	// Token: 0x0600458A RID: 17802 RVA: 0x0015D718 File Offset: 0x0015B918
	public void ResetModelVisual()
	{
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = this.playerData.FashionEquipPack;
		ItemContainer equipPack = this.playerData.EquipPack;
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		if (this.playerData.IsShowFashion)
		{
			text = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, this.playerData.Profession, true);
			if (string.IsNullOrEmpty(text))
			{
				text = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, this.playerData.Profession, false);
			}
			if (this.CheckWeaponIsSame())
			{
				text2 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, this.playerData.Profession, true);
				if (string.IsNullOrEmpty(text2))
				{
					text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, this.playerData.Profession, false);
				}
			}
			else
			{
				text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, this.playerData.Profession, false);
			}
			text3 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, this.playerData.Profession, true);
			if (string.IsNullOrEmpty(text3))
			{
				text3 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, this.playerData.Profession, false);
			}
			text4 = fashionEquipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, this.playerData.Profession, true);
			if (string.IsNullOrEmpty(text4))
			{
				text4 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, this.playerData.Profession, false);
			}
		}
		else
		{
			text = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.HEAD, this.playerData.Profession, false);
			text2 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.WEAPON, this.playerData.Profession, false);
			text3 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.BODY, this.playerData.Profession, false);
			text4 = equipPack.GetEquipModelIdByEquipType(EQUIP_BACKPACK_TYPE.LEG, this.playerData.Profession, false);
		}
		if (this.mPlayerModelVisual == null)
		{
			this.mPlayerModelVisual = new FakeObjLogic();
			this.mPlayerModelVisual.InitFakeObject(text2, text, text3, text4, this.playerData.Profession, SingletonUnity<FakeObjRootLogic>.Instance.MeshRoot, null, "FakeObj");
		}
		else
		{
			this.mPlayerModelVisual.CheckFakeObject(text2, text, text3, text4, null);
			this.mPlayerModelVisual.PlayAnim("idle", this.playerData.CharacterModelData.ModelFirstType);
		}
	}

	// Token: 0x0600458B RID: 17803 RVA: 0x0015D940 File Offset: 0x0015BB40
	public bool CheckWeaponIsSame()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		ItemContainer fashionEquipPack = playerData.FashionEquipPack;
		ItemContainer equipPack = playerData.EquipPack;
		EquipData equipWeaponData = equipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		EquipData equipWeaponData2 = fashionEquipPack.GetEquipWeaponData(EQUIP_BACKPACK_TYPE.WEAPON);
		return equipWeaponData == null || equipWeaponData2 == null || equipWeaponData.WeaponType == equipWeaponData2.WeaponType;
	}

	// Token: 0x0600458C RID: 17804 RVA: 0x0015D998 File Offset: 0x0015BB98
	public void OnClickCloseBtn()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.GameMenuSkillInfoRootUI);
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.MenuBaseRootUI);
		this.UnLoadFakeObj();
	}

	// Token: 0x0600458D RID: 17805 RVA: 0x0015D9CC File Offset: 0x0015BBCC
	public void UnLoadFakeObj()
	{
		if (this.mPlayerModelVisual != null)
		{
			this.mPlayerModelVisual.DestroyFakeObj();
			this.mPlayerModelVisual = null;
		}
		if (SingletonUnity<FakeObjRootLogic>.Instance != null)
		{
			SingletonUnity<FakeObjRootLogic>.Instance.DisableFakeObjRoot();
		}
	}

	// Token: 0x0600458E RID: 17806 RVA: 0x0015DA18 File Offset: 0x0015BC18
	private void OnEnable()
	{
		FakeObjRootLogic instance = SingletonUnity<FakeObjRootLogic>.Instance;
		if (instance == null)
		{
			ResourcesManager.LoadAndInstantiate("UIRoot/FakeObjRoot");
			instance = SingletonUnity<FakeObjRootLogic>.Instance;
		}
		SingletonUnity<FakeObjRootLogic>.Instance.SetPicValue(0.8f);
		SingletonUnity<FakeObjRootLogic>.Instance.EnableFakeObjRoot();
		this.ResetModelVisual();
		instance.objCam.targetTexture.Release();
		instance.objCam.targetTexture = null;
		instance.objCam.targetTexture = SingletonUnity<FakeObjRootLogic>.Instance.ModelPic;
		instance.objCam.ResetAspect();
		this.mPlayerModelVisual.FakeObj.transform.localPosition = this.ModelPos;
		this.mPlayerModelVisual.FakeObj.transform.localEulerAngles = this.ModelAngle;
		this.PlayerModelPic.mainTexture = instance.ModelPic;
		NGUITools.SetActive(this.PlayerModelPic.gameObject, false);
		this.curSelect = 0;
		this.ResetBtn(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SkillIndex);
		this.SkillBtnLogicList[this.curSelect].OnClickBtn();
	}

	// Token: 0x0600458F RID: 17807 RVA: 0x0015DB30 File Offset: 0x0015BD30
	private void OnDisable()
	{
		if (SingletonUnity<FakeObjRootLogic>.Exists)
		{
			if (this.mPlayerModelVisual.FakeObj != null)
			{
				this.mPlayerModelVisual.FakeObj.transform.localPosition = Vector3.zero;
				this.mPlayerModelVisual.FakeObj.transform.localEulerAngles = Vector3.zero;
			}
			SingletonUnity<FakeObjRootLogic>.Instance.objCam.targetTexture.Release();
			SingletonUnity<FakeObjRootLogic>.Instance.objCam.targetTexture = null;
			SingletonUnity<FakeObjRootLogic>.Instance.ModelPic.height = 512;
			SingletonUnity<FakeObjRootLogic>.Instance.objCam.targetTexture = SingletonUnity<FakeObjRootLogic>.Instance.ModelPic;
			this.UnLoadFakeObj();
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_DRAG_MOVE)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x0015DBFC File Offset: 0x0015BDFC
	public void OnClickUpgradeBtn()
	{
		if (!GameMoneyHelper.BeforeCheckBuy(this.skillNextUpgradeData.PriceType, this.skillNextUpgradeData.PriceValue))
		{
			this.CloseTutorial();
			return;
		}
		if (this.mCurChaSkillData.Level + 1 < this.mPlayerLevel)
		{
			skill_level_up.request request = new skill_level_up.request();
			request.skillId = this.mCurChaSkillData.ID;
			request.curLevel = (long)this.mCurChaSkillData.Level;
			NetLogic.GetInstance().Send<Protocol.skill_level_up>(request, null);
			if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK)
			{
				this.CheckTutorialEvent();
			}
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Skill", "click_updateone", "times");
		}
		else
		{
			this.CloseTutorial();
			NoticeLogic.AddNotifyData("#{101143}", true, false);
		}
	}

	// Token: 0x06004591 RID: 17809 RVA: 0x0015DCC0 File Offset: 0x0015BEC0
	public void OnClickUpgradeALLBtn()
	{
		if (!GameMoneyHelper.BeforeCheckBuy(this.skillNextUpgradeData.PriceType, this.skillNextUpgradeData.PriceValue))
		{
			if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK_ALL)
			{
				this.CloseTutorial();
			}
			return;
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_UPGRADE_CLICK_ALL)
		{
			this.CheckTutorialEvent();
		}
		if (this.mCurChaSkillData.Level + 1 < this.mPlayerLevel)
		{
			skill_level_up.request request = new skill_level_up.request();
			request.skillId = this.mCurChaSkillData.ID;
			request.curLevel = (long)this.mCurChaSkillData.Level;
			request.all = 1L;
			NetLogic.GetInstance().Send<Protocol.skill_level_up>(request, null);
			SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Skill", "click_updateall", "times");
		}
		else
		{
			NoticeLogic.AddNotifyData("#{101143}", true, false);
		}
	}

	// Token: 0x06004592 RID: 17810 RVA: 0x0015DD94 File Offset: 0x0015BF94
	private void OnClickOkUpgradeBtn()
	{
	}

	// Token: 0x06004593 RID: 17811 RVA: 0x0015DD98 File Offset: 0x0015BF98
	public void OnClickSkillBtn(SkillData skillData, CharacterSkillData curChaSkillData, GameObject obj)
	{
		this.Selectobj = obj;
		this.SelectSprite.transform.position = obj.transform.position;
		this.ResetSkillInfo(skillData, curChaSkillData);
		this.mCurChaSkillData = curChaSkillData;
	}

	// Token: 0x06004594 RID: 17812 RVA: 0x0015DDD8 File Offset: 0x0015BFD8
	public void ResetSkillInfo(SkillData skillData, CharacterSkillData chaSkillData)
	{
		int skill_MAX_LEVEL = GameDefine.SKILL_MAX_LEVEL;
		NGUITools.SetActive(this.PlayerModelPic.gameObject, false);
		if (chaSkillData != null)
		{
			int level = chaSkillData.Level;
			this.skillNextUpgradeData = DataManager.GetSkillupgradeDataByLevel(chaSkillData.Level + 1);
			this.CurLevelLabel.text = string.Format("Lv.{0}", level + 1);
			NGUITools.SetActive(this.UpgradeBtnRoot.gameObject, true);
			NGUITools.SetActive(this.UpgradeAllBtnRoot.gameObject, true);
			if (skillData != null)
			{
				NGUITools.SetActive(this.AnimationBtnRoot, true);
				this.SkillNameLabel.text = skillData.MName;
				this.SkillDescLabel.text = skillData.MDescription;
				this.SkillTypeLabel.text = StrDictionary.GetDictionaryString("#{101156}", new object[]
				{
					"Instant"
				});
				this.SkillCDLabel.text = StrDictionary.GetDictionaryString("#{101157}", new object[]
				{
					skillData.CDSecond
				});
				this.SkillDisLabel.text = StrDictionary.GetDictionaryString("#{101158}", new object[]
				{
					skillData.TraceDistanceMeter
				});
				this.SkillTargetLabel.text = StrDictionary.GetDictionaryString("#{101159}", new object[]
				{
					skillData.MaxAttackCount
				});
				if (skillData.IsUpgrade == 1)
				{
					NGUITools.SetActive(this.SkillCurLevelDamageLabel.gameObject, true);
					NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, true);
					if (chaSkillData.Level > 0 && this.skillNextUpgradeData != null)
					{
						this.SkillCurLevelDamageLabel.text = StrDictionary.GetDictionaryString("#{101102}", new object[]
						{
							skillData.GetSkillDamageMultiVal(chaSkillData.Level) * 100f,
							skillData.GetSkillDamageVal(chaSkillData.Level)
						});
					}
					else if (skillData.IsUpgrade == 0)
					{
						if (skillData.Job == 0)
						{
							this.SkillCurLevelDamageLabel.text = StrDictionary.GetDictionaryString("#{101147}", new object[0]);
						}
						else if (skillData.Job == 1)
						{
							this.SkillCurLevelDamageLabel.text = StrDictionary.GetDictionaryString("#{101148}", new object[0]);
						}
						else
						{
							this.SkillCurLevelDamageLabel.text = StrDictionary.GetDictionaryString("#{101149}", new object[0]);
						}
					}
					else
					{
						this.SkillCurLevelDamageLabel.text = StrDictionary.GetDictionaryString("#{101102}", new object[]
						{
							skillData.GetSkillDamageMultiVal(chaSkillData.Level) * 100f,
							skillData.GetSkillDamageVal(chaSkillData.Level)
						});
					}
					if (this.skillNextUpgradeData != null)
					{
						this.SkillNextLevelDamageLabel.text = StrDictionary.GetDictionaryString("#{101102}", new object[]
						{
							skillData.GetSkillDamageMultiVal(chaSkillData.Level + 1) * 100f,
							skillData.GetSkillDamageVal(chaSkillData.Level + 1)
						});
					}
				}
				else
				{
					NGUITools.SetActive(this.SkillCurLevelDamageLabel.gameObject, false);
					NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, false);
				}
			}
			else
			{
				NGUITools.SetActive(this.SkillCurLevelDamageLabel.gameObject, false);
				NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, false);
				this.SkillNameLabel.text = string.Empty;
				this.SkillDescLabel.text = string.Empty;
				this.SkillTypeLabel.text = StrDictionary.GetDictionaryString("#{101156}", new object[]
				{
					string.Empty
				});
				this.SkillCDLabel.text = StrDictionary.GetDictionaryString("#{101157}", new object[]
				{
					0
				});
				this.SkillDisLabel.text = StrDictionary.GetDictionaryString("#{101158}", new object[]
				{
					0
				});
				this.SkillTargetLabel.text = StrDictionary.GetDictionaryString("#{101159}", new object[]
				{
					0
				});
				NGUITools.SetActive(this.AnimationBtnRoot, false);
			}
			if (this.skillNextUpgradeData != null)
			{
				UnityVersionUtil.SetActiveRecursive(this.UpgradeSkillCostInfoLabel.gameObject, true);
				NGUITools.SetActive(this.UpgradeBtnRoot.gameObject, true);
				NGUITools.SetActive(this.UpgradeAllBtnRoot.gameObject, true);
				this.NextLevelLabel.text = string.Format("Lv.{0}", level + 2);
				this.UpgradeSkillCostInfoLabel.text = GameMoneyHelper.GetMoneyValStr(this.skillNextUpgradeData.PriceValue, this.skillNextUpgradeData.PriceType);
				if (chaSkillData.Level + 1 < this.mPlayerLevel)
				{
					this.UpgradeBtnRoot.spriteName = GameDefine.BtnIcon[1];
					this.UpgradeAllBtnRoot.spriteName = GameDefine.BtnIcon[1];
				}
				else
				{
					this.UpgradeBtnRoot.spriteName = GameDefine.BtnIcon[2];
					this.UpgradeAllBtnRoot.spriteName = GameDefine.BtnIcon[2];
				}
			}
			else
			{
				NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, false);
				UnityVersionUtil.SetActiveRecursive(this.UpgradeSkillCostInfoLabel.gameObject, false);
				NGUITools.SetActive(this.UpgradeBtnRoot.gameObject, false);
				NGUITools.SetActive(this.UpgradeAllBtnRoot.gameObject, false);
				this.UpgradeBtnRoot.spriteName = GameDefine.BtnIcon[2];
				this.UpgradeAllBtnRoot.spriteName = GameDefine.BtnIcon[2];
			}
			if (chaSkillData.UnlockLevel > this.mPlayerLevel)
			{
				NGUITools.SetActive(this.UpgradeBtnRoot.gameObject, false);
				NGUITools.SetActive(this.UpgradeAllBtnRoot.gameObject, false);
			}
			this.mCurLevel = level;
			this.NextLevelLabel.color = new Color(0.7058824f, 0.8392157f, 0.9098039f, 1f);
			NGUITools.SetActive(this.NextLevelLabel.gameObject, true);
			if (skill_MAX_LEVEL > 0)
			{
				if (level + 1 < skill_MAX_LEVEL)
				{
					if (level + 1 < this.mPlayerLevel)
					{
						this.LevelInfoLabel.text = string.Empty;
					}
					else
					{
						this.LevelInfoLabel.text = StrDictionary.GetDictionaryString("#{101166}", new object[]
						{
							this.mPlayerLevel + 1
						});
						this.NextLevelLabel.color = Color.red;
						NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, false);
					}
					UnityVersionUtil.SetActiveRecursive(this.levelinfoFlag, true);
				}
				else
				{
					this.LevelInfoLabel.text = string.Empty;
					UnityVersionUtil.SetActiveRecursive(this.levelinfoFlag, false);
					NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, false);
					NGUITools.SetActive(this.NextLevelLabel.gameObject, false);
					this.CurLevelLabel.text = "Max";
				}
			}
			else
			{
				this.LevelInfoLabel.text = string.Empty;
				this.LevelInfoLabel.text = string.Empty;
				UnityVersionUtil.SetActiveRecursive(this.levelinfoFlag, false);
				NGUITools.SetActive(this.SkillNextLevelDamageLabel.gameObject, false);
			}
			int num = 0;
			for (int i = 0; i < this.SkillLabelLabelList.Count; i++)
			{
				if (skillData != null)
				{
					if (i < skillData.LabelIdList.Count)
					{
						SkillLabelData skillLabelDataByID = DataManager.GetSkillLabelDataByID(skillData.LabelIdList[i]);
						if (skillLabelDataByID != null)
						{
							this.SkillLabelLabelList[i].text = StrDictionary.GetDictionaryString(skillLabelDataByID.localization, skillLabelDataByID.ValueArray);
							this.SkillScoresLabelList[i].text = string.Format("{0}", skillLabelDataByID.score);
							this.SkillLabelPicList[i].color = skillLabelDataByID.LabelColor;
							num += skillLabelDataByID.score;
						}
						else
						{
							this.SkillLabelPicList[i].color = Color.white;
							this.SkillLabelLabelList[i].text = StrDictionary.GetDictionaryString(this.UnlockLevelStr[i], new object[0]);
							this.SkillScoresLabelList[i].text = string.Empty;
						}
					}
					else
					{
						this.SkillLabelPicList[i].color = Color.white;
						this.SkillLabelLabelList[i].text = StrDictionary.GetDictionaryString(this.UnlockLevelStr[i], new object[0]);
						this.SkillScoresLabelList[i].text = string.Empty;
					}
				}
				else
				{
					this.SkillLabelPicList[i].color = Color.white;
					this.SkillLabelLabelList[i].text = string.Empty;
					this.SkillScoresLabelList[i].text = string.Empty;
				}
			}
			this.ScoresLabel.text = string.Format("{0}", num);
			return;
		}
		Debug.Log("chaSkillData==null!!!!!!!!!!");
		NGUITools.SetActive(this.UpgradeBtnRoot.gameObject, false);
		NGUITools.SetActive(this.UpgradeAllBtnRoot.gameObject, false);
	}

	// Token: 0x06004595 RID: 17813 RVA: 0x0015E6C8 File Offset: 0x0015C8C8
	private void ResetBtn(int skillIndex)
	{
		this.curSkillIndex = skillIndex;
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		for (int i = 0; i < this.SkillBtnLogicList.Count; i++)
		{
			this.SkillBtnLogicList[i].Reset(null, true);
		}
		for (int j = 0; j < this.SkillBtnUseLogicList.Count; j++)
		{
			this.SkillBtnUseLogicList[j].Reset(null, false);
		}
		for (int k = 0; k < mainPlayer.CharacterSkillData.Count; k++)
		{
			int index = mainPlayer.CharacterSkillData[k].Index;
			if (index == 0)
			{
				this.SkillBtnUseLogicList[index].Reset(mainPlayer.CharacterSkillData[k], false);
			}
			if (skillIndex == 0)
			{
				if (index >= 4 && index <= 6)
				{
					this.SkillBtnUseLogicList[index - 3].Reset(mainPlayer.CharacterSkillData[k], false);
				}
				this.Plan1BtnPic.spriteName = this.PlanEnablePicName;
				this.Plan2BtnPic.spriteName = this.PlanDisablePicName;
			}
			else
			{
				if (index >= 7 && index <= 9)
				{
					this.SkillBtnUseLogicList[index - 6].Reset(mainPlayer.CharacterSkillData[k], false);
				}
				this.Plan1BtnPic.spriteName = this.PlanDisablePicName;
				this.Plan2BtnPic.spriteName = this.PlanEnablePicName;
			}
			if (index > 3)
			{
				this.SkillBtnLogicList[mainPlayer.CharacterSkillData[k].Index2 - 5].Reset(mainPlayer.CharacterSkillData[k], true);
			}
		}
		for (int l = 1; l < this.SkillBtnUseLogicList.Count; l++)
		{
			if (skillIndex == 0)
			{
				this.SkillBtnUseLogicList[l].UpdateDrageSurface(l + 3);
			}
			else
			{
				this.SkillBtnUseLogicList[l].UpdateDrageSurface(l + 6);
			}
		}
		this.mPlayerLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x0015E8F0 File Offset: 0x0015CAF0
	public void SelectSkill(string id, int newIndex)
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.ChangeSkillPosition(id, newIndex);
		}
		this.ResetBtn(this.curSkillIndex);
		if (SingletonUnity<JueseJiNengQuLogic>.Exists)
		{
			SingletonUnity<JueseJiNengQuLogic>.Instance.UpdateIcon();
		}
		if (TutorialManager.CurStep == TUTORIAL_STEP.SKILL_DRAG_MOVE)
		{
			this.CheckTutorialEvent();
		}
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x0015E950 File Offset: 0x0015CB50
	public void RemoveSkill(string id)
	{
		ObjMainPlayer mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (mainPlayer != null)
		{
			mainPlayer.RemoveSkillPosition(id);
		}
		this.ResetBtn(this.curSkillIndex);
		if (SingletonUnity<JueseJiNengQuLogic>.Exists)
		{
			SingletonUnity<JueseJiNengQuLogic>.Instance.UpdateIcon();
		}
	}

	// Token: 0x06004598 RID: 17816 RVA: 0x0015E99C File Offset: 0x0015CB9C
	public void Reset()
	{
		this.ResetBtn(SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.SkillIndex);
		this.SkillBtnLogicList[this.curSelect].OnClickBtn();
	}

	// Token: 0x06004599 RID: 17817 RVA: 0x0015E9D4 File Offset: 0x0015CBD4
	public void SyncPage()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			this.ResetBtn(this.curSkillIndex);
			for (int i = 0; i < this.SkillBtnLogicList.Count; i++)
			{
				if (this.SkillBtnLogicList[i].CharacterSkillData != null && this.SkillBtnLogicList[i].SkillId.Equals(this.mCurChaSkillData.ID))
				{
					this.PlaySkillLevelAnim(this.SkillBtnLogicList[i].CharacterSkillData.Level);
					this.SkillBtnLogicList[i].OnClickBtn();
					SingletonDontDestoryUnity<GameManager>.Instance.FlurryLogEventMap("Skill", string.Format("skill_{0}", this.mCurChaSkillData.ID), string.Format("skilllevel_{0}", this.mCurChaSkillData.Level + 1));
					break;
				}
			}
		}
	}

	// Token: 0x0600459A RID: 17818 RVA: 0x0015EAC8 File Offset: 0x0015CCC8
	public void OnClickPlaySkillBtn()
	{
		if (this.mCurChaSkillData == null)
		{
			return;
		}
		NGUITools.SetActive(this.PlayerModelPic.gameObject, true);
		this.mPlayerModelVisual.UseSkill(this.mCurChaSkillData.ID, delegate
		{
			NGUITools.SetActive(this.PlayerModelPic.gameObject, false);
		});
	}

	// Token: 0x0600459B RID: 17819 RVA: 0x0015EB14 File Offset: 0x0015CD14
	public void OnClickCloseAnimaBtn()
	{
		NGUITools.SetActive(this.PlayerModelPic.gameObject, false);
	}

	// Token: 0x0600459C RID: 17820 RVA: 0x0015EB28 File Offset: 0x0015CD28
	public void OnClickPlan1Btn()
	{
		this.ResetBtn(0);
	}

	// Token: 0x0600459D RID: 17821 RVA: 0x0015EB34 File Offset: 0x0015CD34
	public void OnClickPlan2Btn()
	{
		this.ResetBtn(1);
	}

	// Token: 0x0400326B RID: 12907
	private int mClickUpgradeBtnCount;

	// Token: 0x0400326C RID: 12908
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x0400326D RID: 12909
	private vp_Timer.Handle clickHandle;

	// Token: 0x0400326E RID: 12910
	public TweenAlpha tweenAlpha;

	// Token: 0x0400326F RID: 12911
	public TweenPosition tweenPosition;

	// Token: 0x04003270 RID: 12912
	private Vector3 ModelPos = new Vector3(-0.6819441f, 0.2052425f, -2.390722f);

	// Token: 0x04003271 RID: 12913
	private Vector3 ModelAngle = new Vector3(30.60997f, 13.9f, 0f);

	// Token: 0x04003272 RID: 12914
	public UISprite SelectSprite;

	// Token: 0x04003273 RID: 12915
	private int curSelect;

	// Token: 0x04003274 RID: 12916
	private GameObject Selectobj;

	// Token: 0x04003275 RID: 12917
	public List<SkillInfoBtnLogic> SkillBtnLogicList = new List<SkillInfoBtnLogic>();

	// Token: 0x04003276 RID: 12918
	public List<SkillInfoBtnLogic> SkillBtnUseLogicList = new List<SkillInfoBtnLogic>();

	// Token: 0x04003277 RID: 12919
	public UILabel SkillNameLabel;

	// Token: 0x04003278 RID: 12920
	public UILabel SkillTypeLabel;

	// Token: 0x04003279 RID: 12921
	public UILabel SkillCDLabel;

	// Token: 0x0400327A RID: 12922
	public UILabel SkillDisLabel;

	// Token: 0x0400327B RID: 12923
	public UILabel SkillTargetLabel;

	// Token: 0x0400327C RID: 12924
	public UILabel CurLevelLabel;

	// Token: 0x0400327D RID: 12925
	public UILabel SkillCurLevelDamageLabel;

	// Token: 0x0400327E RID: 12926
	public UILabel NextLevelLabel;

	// Token: 0x0400327F RID: 12927
	public UILabel SkillNextLevelDamageLabel;

	// Token: 0x04003280 RID: 12928
	public UILabel SkillDescLabel;

	// Token: 0x04003281 RID: 12929
	public List<UILabel> SkillLabelLabelList;

	// Token: 0x04003282 RID: 12930
	public List<UILabel> SkillScoresLabelList;

	// Token: 0x04003283 RID: 12931
	public List<UISprite> SkillLabelPicList;

	// Token: 0x04003284 RID: 12932
	public UILabel ScoresLabel;

	// Token: 0x04003285 RID: 12933
	public UILabel UpgradeSkillCostInfoLabel;

	// Token: 0x04003286 RID: 12934
	public UISprite Plan1BtnPic;

	// Token: 0x04003287 RID: 12935
	public UISprite Plan2BtnPic;

	// Token: 0x04003288 RID: 12936
	private string PlanEnablePicName = "CZ_huaDongBG_1";

	// Token: 0x04003289 RID: 12937
	private string PlanDisablePicName = "CZ_huaDongBG";

	// Token: 0x0400328A RID: 12938
	public UISprite UpgradeBtnRoot;

	// Token: 0x0400328B RID: 12939
	public UISprite UpgradeAllBtnRoot;

	// Token: 0x0400328C RID: 12940
	public GameObject PlaySkillBtn;

	// Token: 0x0400328D RID: 12941
	private CharacterSkillData mCurChaSkillData;

	// Token: 0x0400328E RID: 12942
	private int mPlayerLevel;

	// Token: 0x0400328F RID: 12943
	public GameObject levelinfoFlag;

	// Token: 0x04003290 RID: 12944
	public UILabel LevelInfoLabel;

	// Token: 0x04003291 RID: 12945
	public UITexture PlayerModelPic;

	// Token: 0x04003292 RID: 12946
	public GameObject AnimationBtnRoot;

	// Token: 0x04003293 RID: 12947
	private PlayerData playerData;

	// Token: 0x04003294 RID: 12948
	private int mCurLevel;

	// Token: 0x04003295 RID: 12949
	public SkillupgradeData skillNextUpgradeData;

	// Token: 0x04003296 RID: 12950
	private FakeObjLogic mPlayerModelVisual;

	// Token: 0x04003297 RID: 12951
	private string[] UnlockLevelStr = new string[]
	{
		string.Empty,
		"#{101163}",
		"#{101164}",
		"#{101165}"
	};

	// Token: 0x04003298 RID: 12952
	private int curSkillIndex;
}
