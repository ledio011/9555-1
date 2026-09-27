using System;
using UnityEngine;

// Token: 0x020009E8 RID: 2536
public class JueseJiNengQuLogic : SingletonUnity<JueseJiNengQuLogic>
{
	// Token: 0x06004827 RID: 18471 RVA: 0x00171B0C File Offset: 0x0016FD0C
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004828 RID: 18472 RVA: 0x00171B18 File Offset: 0x0016FD18
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mainPlayer.CameraController.smoothOrbitSpeed = 10f;
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06004829 RID: 18473 RVA: 0x00171B50 File Offset: 0x0016FD50
	private void UpdateSKill()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (!this.ResetFlag)
		{
			this.ResetIcon();
		}
		if (this.mainPlayer != null)
		{
			this.skillCDTimeMaskSprites[0].fillAmount = this.mainPlayer.GetComboSKillTimePercent();
			for (int i = 0; i < this.mainPlayer.PlayerSkillIDList.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.mainPlayer.PlayerSkillIDList[i]))
				{
					this.skillCDTimeMaskSprites[i + 1].fillAmount = this.mainPlayer.GetSkillTimePercent(this.mainPlayer.PlayerSkillIDList[i]);
				}
				else
				{
					this.skillCDTimeMaskSprites[i + 1].fillAmount = 0f;
				}
			}
			this.SwitchCDPic.fillAmount = this.mainPlayer.GetSwitchSkillCDPercent();
		}
	}

	// Token: 0x0600482A RID: 18474 RVA: 0x00171C50 File Offset: 0x0016FE50
	public void UseSkill_1_Onclick()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			if (this.mOnClickTutorialBtn != null && TutorialManager.CurStep == TUTORIAL_STEP.NORMAL_ATTACK_BUTTON)
			{
				this.CheckTutorialEvent();
			}
			this.mainPlayer.ClearAutoSelectCharacter();
			this.mainPlayer.UseComboSkill();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x0600482B RID: 18475 RVA: 0x00171CD4 File Offset: 0x0016FED4
	public void UseSkill_2_Onclick()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			if (this.mOnClickTutorialBtn != null && TutorialManager.CurStep == TUTORIAL_STEP.SKILL1_BUTTON)
			{
				this.CheckTutorialEvent();
			}
			this.mainPlayer.ClearAutoSelectCharacter();
			if (!string.IsNullOrEmpty(this.mainPlayer.PlayerSkillIDList[1]))
			{
				this.mainPlayer.UseSkill(this.mainPlayer.PlayerSkillIDList[1], null);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100681}", true, false);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x0600482C RID: 18476 RVA: 0x00171D94 File Offset: 0x0016FF94
	public void UseSkill_3_Onclick()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			this.mainPlayer.ClearAutoSelectCharacter();
			if (this.mOnClickTutorialBtn != null && TutorialManager.CurStep == TUTORIAL_STEP.SKILL2_BUTTON)
			{
				this.CheckTutorialEvent();
			}
			if (!string.IsNullOrEmpty(this.mainPlayer.PlayerSkillIDList[2]))
			{
				this.mainPlayer.UseSkill(this.mainPlayer.PlayerSkillIDList[2], null);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100681}", true, false);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x0600482D RID: 18477 RVA: 0x00171E54 File Offset: 0x00170054
	public void UseSkill_4_Onclick()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			this.mainPlayer.ClearAutoSelectCharacter();
			if (!string.IsNullOrEmpty(this.mainPlayer.PlayerSkillIDList[3]))
			{
				this.mainPlayer.UseSkill(this.mainPlayer.PlayerSkillIDList[3], null);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100681}", true, false);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x0600482E RID: 18478 RVA: 0x00171EF8 File Offset: 0x001700F8
	public void UseSkill_S_Onclick()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			this.mainPlayer.ClearAutoSelectCharacter();
			if (!string.IsNullOrEmpty(this.mainPlayer.PlayerSkillIDList[0]))
			{
				this.mainPlayer.UseSkill(this.mainPlayer.PlayerSkillIDList[0], null);
			}
			else
			{
				NoticeLogic.AddNotifyData("#{100681}", true, false);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x0600482F RID: 18479 RVA: 0x00171F9C File Offset: 0x0017019C
	private new void Awake()
	{
		base.Awake();
		this.ResetFlag = false;
	}

	// Token: 0x06004830 RID: 18480 RVA: 0x00171FAC File Offset: 0x001701AC
	private void ResetIcon()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer != null)
		{
			this.UpdateIcon();
			this.ResetFlag = true;
		}
	}

	// Token: 0x06004831 RID: 18481 RVA: 0x00171FF8 File Offset: 0x001701F8
	public void UpdateIcon()
	{
		if (UnityVersionUtil.IsActive(base.gameObject))
		{
			for (int i = 0; i < this.SkillBtnRoot.Length; i++)
			{
				this.skillIconSprites[i].spriteName = "CZ_jiNengSuoDing";
				this.skillCDTimeMaskSprites[i].fillAmount = 1f;
			}
			for (int j = 0; j < this.BtnLabelPicList.Length; j++)
			{
				for (int k = 0; k < this.BtnLabelPicList[j].SkillLabelPic.Count; k++)
				{
					this.BtnLabelPicList[j].SkillLabelPic[k].color = Color.white;
				}
			}
			for (int l = 0; l < this.mainPlayer.CharacterSkillData.Count; l++)
			{
				if (!this.mainPlayer.CharacterSkillData[l].IsDisable)
				{
					int index = this.mainPlayer.CharacterSkillData[l].Index;
					if (index == 0)
					{
						SkillData skillDataById = DataManager.GetSkillDataById(this.mainPlayer.CharacterSkillData[l].ID);
						this.skillIconSprites[index].spriteName = skillDataById.Icon;
					}
					if (this.mainPlayer.SkillIndex == 0)
					{
						if (index > 2 && index < 7)
						{
							if (this.mainPlayer.CharacterSkillData[l].UnlockLevel > this.mainPlayer.AttributeData.Level)
							{
								this.skillIconSprites[index - 2].spriteName = "CZ_jiNengSuoDing";
							}
							else
							{
								SkillData skillDataById2 = DataManager.GetSkillDataById(this.mainPlayer.CharacterSkillData[l].ID);
								if (skillDataById2 != null)
								{
									this.skillIconSprites[index - 2].spriteName = skillDataById2.Icon;
									if (index != 3)
									{
										for (int m = 0; m < this.BtnLabelPicList[index - 4].SkillLabelPic.Count; m++)
										{
											if (m < skillDataById2.LabelIdList.Count)
											{
												SkillLabelData skillLabelDataByID = DataManager.GetSkillLabelDataByID(skillDataById2.LabelIdList[m]);
												this.BtnLabelPicList[index - 4].SkillLabelPic[m].color = skillLabelDataByID.LabelColor;
											}
											else
											{
												this.BtnLabelPicList[index - 4].SkillLabelPic[m].color = Color.white;
											}
										}
									}
								}
							}
						}
					}
					else if (index == 3)
					{
						if (this.mainPlayer.CharacterSkillData[l].UnlockLevel > this.mainPlayer.AttributeData.Level)
						{
							this.skillIconSprites[index - 2].spriteName = "CZ_jiNengSuoDing";
						}
						else
						{
							SkillData skillDataById3 = DataManager.GetSkillDataById(this.mainPlayer.CharacterSkillData[l].ID);
							this.skillIconSprites[index - 2].spriteName = skillDataById3.Icon;
						}
					}
					else if (index >= 7 && index <= 9)
					{
						if (this.mainPlayer.CharacterSkillData[l].UnlockLevel > this.mainPlayer.AttributeData.Level)
						{
							this.skillIconSprites[index - 5].spriteName = "CZ_jiNengSuoDing";
						}
						else
						{
							SkillData skillDataById4 = DataManager.GetSkillDataById(this.mainPlayer.CharacterSkillData[l].ID);
							this.skillIconSprites[index - 5].spriteName = skillDataById4.Icon;
							for (int n = 0; n < this.BtnLabelPicList[index - 7].SkillLabelPic.Count; n++)
							{
								if (n < skillDataById4.LabelIdList.Count)
								{
									SkillLabelData skillLabelDataByID2 = DataManager.GetSkillLabelDataByID(skillDataById4.LabelIdList[n]);
									this.BtnLabelPicList[index - 7].SkillLabelPic[n].color = skillLabelDataByID2.LabelColor;
								}
								else
								{
									this.BtnLabelPicList[index - 7].SkillLabelPic[n].color = Color.white;
								}
							}
						}
					}
				}
			}
			if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL))
			{
				NGUITools.SetActive(this.SwitchBtnRoot, true);
				this.SkillGroupLabel.text = string.Format("{0}", this.mainPlayer.SkillIndex + 1);
			}
			else
			{
				NGUITools.SetActive(this.SwitchBtnRoot, false);
			}
		}
	}

	// Token: 0x06004832 RID: 18482 RVA: 0x00172490 File Offset: 0x00170690
	public void Reset(bool hideAllBtn = false)
	{
		this.ResetIcon();
		if (hideAllBtn)
		{
			for (int i = 0; i < this.SkillBtnRoot.Length; i++)
			{
				NGUITools.SetActive(this.SkillBtnRoot[i].gameObject, false);
			}
		}
	}

	// Token: 0x06004833 RID: 18483 RVA: 0x001724D8 File Offset: 0x001706D8
	public void ShowSkillBtn(int index)
	{
		if (index >= 0 && index < this.SkillBtnRoot.Length)
		{
			NGUITools.SetActive(this.SkillBtnRoot[index].gameObject, true);
		}
	}

	// Token: 0x06004834 RID: 18484 RVA: 0x00172510 File Offset: 0x00170710
	public void ShowAllSkillBtn()
	{
		for (int i = 0; i < this.SkillBtnRoot.Length; i++)
		{
			NGUITools.SetActive(this.SkillBtnRoot[i].gameObject, true);
		}
		this.UpdateIcon();
	}

	// Token: 0x06004835 RID: 18485 RVA: 0x00172550 File Offset: 0x00170750
	private void Update()
	{
		this.UpdateSKill();
	}

	// Token: 0x06004836 RID: 18486 RVA: 0x00172558 File Offset: 0x00170758
	public void OnClickSwitchBtn()
	{
		if (this.mainPlayer.SwitchSkillGroup())
		{
			this.UpdateIcon();
		}
	}

	// Token: 0x06004837 RID: 18487 RVA: 0x00172570 File Offset: 0x00170770
	private void OnEnable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Combine(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateIcon));
	}

	// Token: 0x06004838 RID: 18488 RVA: 0x001725A0 File Offset: 0x001707A0
	private void OnDisable()
	{
		UIUpdateEvent.LevelUpEvent = (UIUpdateEvent.UpdateNoParamEvent)Delegate.Remove(UIUpdateEvent.LevelUpEvent, new UIUpdateEvent.UpdateNoParamEvent(this.UpdateIcon));
	}

	// Token: 0x04003583 RID: 13699
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003584 RID: 13700
	public UISprite[] skillIconSprites;

	// Token: 0x04003585 RID: 13701
	public UISprite[] skillCDTimeMaskSprites;

	// Token: 0x04003586 RID: 13702
	public UIWidget[] SkillBtnRoot;

	// Token: 0x04003587 RID: 13703
	public SkillInfoBtnLogic[] BtnLabelPicList;

	// Token: 0x04003588 RID: 13704
	public GameObject SwitchBtnRoot;

	// Token: 0x04003589 RID: 13705
	public UISprite SwitchCDPic;

	// Token: 0x0400358A RID: 13706
	public UILabel SkillGroupLabel;

	// Token: 0x0400358B RID: 13707
	private ObjMainPlayer mainPlayer;

	// Token: 0x0400358C RID: 13708
	private bool ResetFlag;
}
