using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000996 RID: 2454
public class SkillInfoBtnLogic : MonoBehaviour
{
	// Token: 0x17000FAC RID: 4012
	// (get) Token: 0x0600457C RID: 17788 RVA: 0x0015CE8C File Offset: 0x0015B08C
	public string SkillId
	{
		get
		{
			return this.mChaSkillData.ID;
		}
	}

	// Token: 0x17000FAD RID: 4013
	// (get) Token: 0x0600457D RID: 17789 RVA: 0x0015CE9C File Offset: 0x0015B09C
	public CharacterSkillData CharacterSkillData
	{
		get
		{
			return this.mChaSkillData;
		}
	}

	// Token: 0x0600457E RID: 17790 RVA: 0x0015CEA4 File Offset: 0x0015B0A4
	public void UpdateDrageSurface(int index)
	{
		if (this.DragSurface != null)
		{
			this.DragSurface.index = index;
		}
	}

	// Token: 0x0600457F RID: 17791 RVA: 0x0015CEC4 File Offset: 0x0015B0C4
	public void Reset(CharacterSkillData skData, bool isTop = false)
	{
		this.IsTopSlotFlag = isTop;
		if (this.RemoveBtnPic != null)
		{
			NGUITools.SetActive(this.RemoveBtnPic.gameObject, false);
		}
		if (skData == null)
		{
			if (this.SkillLevelLabel != null)
			{
				this.SkillLevelLabel.text = string.Empty;
			}
			this.SkillIconSprite.spriteName = "CZ_jiNeng_CD1";
			this.mChaSkillData = skData;
			this.mSkillData = null;
			NGUITools.SetActive(this.levelUpTips, false);
			if (this.SkillGroupPic != null)
			{
				NGUITools.SetActive(this.SkillGroupPic.gameObject, false);
			}
			if (this.PlusPicRoot != null)
			{
				NGUITools.SetActive(this.PlusPicRoot, true);
			}
			for (int i = 0; i < this.SkillLabelPic.Count; i++)
			{
				this.SkillLabelPic[i].color = Color.white;
			}
			if (this.DragItemLogic != null)
			{
				this.DragItemLogic.enabled = false;
			}
			return;
		}
		this.mChaSkillData = skData;
		this.mSkillData = DataManager.GetSkillDataById(this.mChaSkillData.ID);
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.SkillLevelLabel != null)
		{
			this.SkillLevelLabel.text = string.Empty;
		}
		if (this.SkillGroupLabel != null)
		{
			if (this.mSkillData == null)
			{
				this.SkillGroupLabel.text = string.Empty;
				NGUITools.SetActive(this.SkillGroupPic.gameObject, false);
			}
			else if (this.mChaSkillData.Index >= 4 && this.mChaSkillData.Index <= 6)
			{
				this.SkillGroupLabel.text = "1";
				NGUITools.SetActive(this.SkillGroupPic.gameObject, true);
				this.SkillGroupPic.color = this.Group1Color;
			}
			else if (this.mChaSkillData.Index >= 7 && this.mChaSkillData.Index <= 9)
			{
				this.SkillGroupLabel.text = "2";
				NGUITools.SetActive(this.SkillGroupPic.gameObject, true);
				this.SkillGroupPic.color = this.Group2Color;
			}
			else
			{
				this.SkillGroupLabel.text = string.Empty;
				NGUITools.SetActive(this.SkillGroupPic.gameObject, false);
			}
		}
		if (this.PlusPicRoot != null)
		{
			if (this.mSkillData == null)
			{
				NGUITools.SetActive(this.PlusPicRoot, true);
			}
			else
			{
				NGUITools.SetActive(this.PlusPicRoot, false);
			}
		}
		if (this.mSkillData != null && this.RemoveBtnPic != null)
		{
			NGUITools.SetActive(this.RemoveBtnPic.gameObject, true);
		}
		if (playerData != null)
		{
			if (this.DragItemLogic != null)
			{
				this.DragItemLogic.enabled = (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsFunctionUnlock(FUNCTION_TYPE.SKILL_DRAG) && playerData.MainPlayerAttrData.Level >= skData.UnlockLevel);
			}
			if (playerData.MainPlayerAttrData.Level < skData.UnlockLevel)
			{
				if (this.SkillLevelLabel != null)
				{
					this.SkillLevelLabel.text = string.Format("Lv.{0}", skData.UnlockLevel);
					this.SkillLevelLabel.color = this.lockPicColor;
				}
				this.SkillIconSprite.spriteName = this.lockPicName;
				NGUITools.SetActive(this.levelUpTips, false);
				if (this.SkillGroupLabel != null)
				{
					this.SkillGroupLabel.text = string.Empty;
				}
			}
			else
			{
				if (this.mSkillData != null && this.mSkillData.IsUpgrade == 1)
				{
					NGUITools.SetActive(this.levelUpTips, playerData.MainPlayerAttrData.Level > skData.Level + 1);
				}
				else
				{
					NGUITools.SetActive(this.levelUpTips, false);
				}
				if (skData.IsDisable)
				{
					this.SkillIconSprite.spriteName = "CZ_jiNeng_CD1";
					for (int j = 0; j < this.SkillLabelPic.Count; j++)
					{
						this.SkillLabelPic[j].color = Color.white;
					}
				}
				else
				{
					this.SkillIconSprite.spriteName = this.mSkillData.Icon;
					this.SkillIconSprite.color = Color.white;
					for (int k = 0; k < this.SkillLabelPic.Count; k++)
					{
						if (k < this.mSkillData.LabelIdList.Count)
						{
							SkillLabelData skillLabelDataByID = DataManager.GetSkillLabelDataByID(this.mSkillData.LabelIdList[k]);
							this.SkillLabelPic[k].color = skillLabelDataByID.LabelColor;
						}
						else
						{
							this.SkillLabelPic[k].color = Color.white;
						}
					}
				}
				if (this.SkillLevelLabel != null)
				{
					this.SkillLevelLabel.text = string.Format("Lv.{0}", skData.Level + 1);
				}
			}
		}
	}

	// Token: 0x06004580 RID: 17792 RVA: 0x0015D400 File Offset: 0x0015B600
	public void OnClickBtn()
	{
		if (this.IsTopSlotFlag)
		{
			if (this.mChaSkillData == null)
			{
				return;
			}
			SingletonUnity<SkillInfoRootLogic>.Instance.OnClickSkillBtn(this.mSkillData, this.mChaSkillData, base.gameObject);
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(4, 1f, null);
		}
		else
		{
			if (this.mSkillData == null || this.mChaSkillData == null)
			{
				if (TutorialManager.CurStep != TUTORIAL_STEP.SKILL_DRAG_MOVE)
				{
					TutorialManager.ShowTutorial(TUTORIAL_STEP.SKILL_DRAG_MOVE);
				}
				return;
			}
			SingletonUnity<SkillInfoRootLogic>.Instance.OnClickSkillBtn(this.mSkillData, this.mChaSkillData, base.gameObject);
			SingletonDontDestoryUnity<SoundManager>.Instance.PlaySoundEffect(4, 1f, null);
		}
	}

	// Token: 0x06004581 RID: 17793 RVA: 0x0015D4B0 File Offset: 0x0015B6B0
	public void OnClickRemoveBtn()
	{
		if (this.mSkillData != null)
		{
			SingletonUnity<SkillInfoRootLogic>.Instance.RemoveSkill(this.mSkillData.ID);
		}
	}

	// Token: 0x04003256 RID: 12886
	public UISprite SkillIconSprite;

	// Token: 0x04003257 RID: 12887
	public UILabel SkillLevelLabel;

	// Token: 0x04003258 RID: 12888
	public List<UISprite> SkillLabelPic;

	// Token: 0x04003259 RID: 12889
	public UILabel SkillGroupLabel;

	// Token: 0x0400325A RID: 12890
	public UISprite SkillGroupPic;

	// Token: 0x0400325B RID: 12891
	public GameObject PlusPicRoot;

	// Token: 0x0400325C RID: 12892
	public UISprite RemoveBtnPic;

	// Token: 0x0400325D RID: 12893
	private string lockPicName = "CZ_jiNengSuoDing";

	// Token: 0x0400325E RID: 12894
	private Vector2 lockPicSize = new Vector2(30f, 34f);

	// Token: 0x0400325F RID: 12895
	private Vector2 unLockPicSize = new Vector2(68f, 66f);

	// Token: 0x04003260 RID: 12896
	private Vector3 lockPicPos = new Vector3(0f, 9.445351f, 0f);

	// Token: 0x04003261 RID: 12897
	private Color lockPicColor = new Color(0f, 0.95686275f, 1f, 1f);

	// Token: 0x04003262 RID: 12898
	private Color unlockLabelColor = new Color(1f, 0.73333335f, 0f, 1f);

	// Token: 0x04003263 RID: 12899
	private CharacterSkillData mChaSkillData;

	// Token: 0x04003264 RID: 12900
	private SkillData mSkillData;

	// Token: 0x04003265 RID: 12901
	private Color Group1Color = new Color(0f, 0.37254903f, 0.49019608f, 0.6509804f);

	// Token: 0x04003266 RID: 12902
	private Color Group2Color = new Color(0f, 0.78039217f, 0.4f, 0.6509804f);

	// Token: 0x04003267 RID: 12903
	public GameObject levelUpTips;

	// Token: 0x04003268 RID: 12904
	public SkillDragItemLogic DragItemLogic;

	// Token: 0x04003269 RID: 12905
	public SkillDragSurface DragSurface;

	// Token: 0x0400326A RID: 12906
	private bool IsTopSlotFlag;
}
