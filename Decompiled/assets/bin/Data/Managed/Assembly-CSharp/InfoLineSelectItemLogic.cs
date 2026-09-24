using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000942 RID: 2370
public class InfoLineSelectItemLogic : MonoBehaviour
{
	// Token: 0x060041F4 RID: 16884 RVA: 0x0013B1B4 File Offset: 0x001393B4
	public void ResetAtt(random_attri attinfo, int index, GameItem curitem, DelegateDefine.ThreeParamDelegate clickfun = null)
	{
		this.CurIndex = index;
		this.ClickFun = clickfun;
		this.SelectFlag.enabled = false;
		if (attinfo != null)
		{
			string attributeIcon = GameDefine.GetAttributeIcon((int)attinfo.id);
			string attributeName_S = GameDefine.GetAttributeName_S((int)attinfo.id);
			string attributeValueStr = GameDefine.GetAttributeValueStr((int)attinfo.id, (int)attinfo.value);
			if (!attinfo.HasQualityId)
			{
				EquipData equipDataById = DataManager.GetEquipDataById(curitem.ItemId);
				attinfo.qualityId = equipDataById.QualityID;
			}
			this.CurQuality = curitem.GetEquipAttQuality((int)attinfo.quality, attinfo.qualityId);
			Color colorByQuality = GameDefine.GetColorByQuality(this.CurQuality);
			this.CurKey = attinfo.id.ToString();
			this.ResetAtt(attributeIcon, attributeName_S, attributeValueStr, colorByQuality);
		}
		else
		{
			this.ResetEmpty();
		}
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x0013B284 File Offset: 0x00139484
	public void ResetSkill(random_attri attinfo, int index, GameItem curitem, DelegateDefine.ThreeParamDelegate clickfun = null)
	{
		this.CurIndex = index;
		this.ClickFun = clickfun;
		this.SelectFlag.enabled = false;
		if (attinfo != null)
		{
			if (!attinfo.HasQualityId)
			{
				EquipData equipDataById = DataManager.GetEquipDataById(curitem.ItemId);
				attinfo.qualityId = equipDataById.QualityID;
			}
			this.CurQuality = curitem.GetEquipAttQuality((int)attinfo.quality, attinfo.qualityId);
			SkillData skillDataById = DataManager.GetSkillDataById(attinfo.skillId);
			this.CurKey = skillDataById.TeamID.ToString();
			Color colorByQuality = GameDefine.GetColorByQuality(this.CurQuality);
			int playerSkillLevelByPos = Singleton<ObjManager>.Instance.MainPlayer.GetPlayerSkillLevelByPos((int)attinfo.index + 4);
			this.ResetSkill(skillDataById, colorByQuality, playerSkillLevelByPos);
		}
		else
		{
			this.ResetEmpty();
		}
	}

	// Token: 0x060041F6 RID: 16886 RVA: 0x0013B344 File Offset: 0x00139544
	public void ResetSkill(SkillData curdata, Color needcolor, int level)
	{
		if (curdata != null)
		{
			NGUITools.SetActive(this.Infoobj, true);
			this.Emptylabel.enabled = false;
			this.Icon.spriteName = curdata.Icon;
			this.NameLavel.text = string.Format("{0}'s", curdata.CDSecond);
			this.DesLabel.text = string.Format("{0}", curdata.TraceDistanceMeter);
			this.ValueLabel.text = string.Format("{0}", curdata.MaxAttackCount);
			this.LevelLabel.text = string.Format("Lv.{0}", level + 1);
			for (int i = 0; i < this.SkillLabelPic.Count; i++)
			{
				if (i < curdata.LabelIdList.Count)
				{
					SkillLabelData skillLabelDataByID = DataManager.GetSkillLabelDataByID(curdata.LabelIdList[i]);
					this.SkillLabelPic[i].color = skillLabelDataByID.LabelColor;
				}
				else
				{
					this.SkillLabelPic[i].color = Color.white;
				}
			}
			this.ColorSp.spriteName = "CZ_effect_liuGuang";
			this.ColorSp.color = needcolor;
		}
	}

	// Token: 0x060041F7 RID: 16887 RVA: 0x0013B48C File Offset: 0x0013968C
	public void ResetAtt(string iconname, string name, string value, Color showcolor)
	{
		NGUITools.SetActive(this.Infoobj, true);
		this.Emptylabel.enabled = false;
		this.Icon.spriteName = iconname;
		this.NameLavel.text = name;
		this.ValueLabel.text = value;
		this.NameLavel.color = showcolor;
		this.ValueLabel.color = showcolor;
	}

	// Token: 0x060041F8 RID: 16888 RVA: 0x0013B4F0 File Offset: 0x001396F0
	public void ResetEmpty()
	{
		NGUITools.SetActive(this.Infoobj, false);
		this.Emptylabel.enabled = true;
		this.CurKey = string.Empty;
		if (this.ColorSp != null)
		{
			this.ColorSp.spriteName = "CZ_shengJi_ShuXingTiao";
			this.ColorSp.color = Color.white;
		}
	}

	// Token: 0x060041F9 RID: 16889 RVA: 0x0013B554 File Offset: 0x00139754
	public void UpdateSelect(int selectindex)
	{
		if (selectindex == this.CurIndex)
		{
			this.SelectFlag.enabled = true;
		}
		else
		{
			this.SelectFlag.enabled = false;
		}
	}

	// Token: 0x060041FA RID: 16890 RVA: 0x0013B580 File Offset: 0x00139780
	public void OnClickSelect()
	{
		if (this.ClickFun != null)
		{
			this.ClickFun(this.CurIndex, this.CurQuality, this.CurKey, true);
		}
	}

	// Token: 0x060041FB RID: 16891 RVA: 0x0013B5AC File Offset: 0x001397AC
	public bool AutoSelectKey(string needkey)
	{
		if (string.IsNullOrEmpty(needkey))
		{
			return true;
		}
		if (string.IsNullOrEmpty(this.CurKey))
		{
			return false;
		}
		if (this.CurKey.Equals(needkey))
		{
			if (this.ClickFun != null)
			{
				this.ClickFun(this.CurIndex, this.CurQuality, needkey, false);
			}
			return true;
		}
		return false;
	}

	// Token: 0x060041FC RID: 16892 RVA: 0x0013B610 File Offset: 0x00139810
	public bool IsEqualsKey(string needkey)
	{
		return !string.IsNullOrEmpty(needkey) && !string.IsNullOrEmpty(this.CurKey) && this.CurKey.Equals(needkey);
	}

	// Token: 0x060041FD RID: 16893 RVA: 0x0013B644 File Offset: 0x00139844
	public bool SelectEmpty()
	{
		if (string.IsNullOrEmpty(this.CurKey))
		{
			if (this.ClickFun != null)
			{
				this.ClickFun(this.CurIndex, this.CurQuality, this.CurKey, false);
			}
			return true;
		}
		return false;
	}

	// Token: 0x04002DE5 RID: 11749
	public UISprite Icon;

	// Token: 0x04002DE6 RID: 11750
	public UILabel NameLavel;

	// Token: 0x04002DE7 RID: 11751
	public UILabel ValueLabel;

	// Token: 0x04002DE8 RID: 11752
	public UISprite SelectFlag;

	// Token: 0x04002DE9 RID: 11753
	public GameObject Infoobj;

	// Token: 0x04002DEA RID: 11754
	public UILabel Emptylabel;

	// Token: 0x04002DEB RID: 11755
	public UILabel DesLabel;

	// Token: 0x04002DEC RID: 11756
	public UISprite ColorSp;

	// Token: 0x04002DED RID: 11757
	private int CurIndex;

	// Token: 0x04002DEE RID: 11758
	private int CurQuality;

	// Token: 0x04002DEF RID: 11759
	private DelegateDefine.ThreeParamDelegate ClickFun;

	// Token: 0x04002DF0 RID: 11760
	public List<UISprite> SkillLabelPic;

	// Token: 0x04002DF1 RID: 11761
	public UILabel LevelLabel;

	// Token: 0x04002DF2 RID: 11762
	private string CurKey = string.Empty;
}
