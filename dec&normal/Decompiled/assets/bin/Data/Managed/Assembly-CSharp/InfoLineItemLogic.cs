using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000941 RID: 2369
public class InfoLineItemLogic : MonoBehaviour
{
	// Token: 0x060041ED RID: 16877 RVA: 0x0013ADEC File Offset: 0x00138FEC
	public void ResetBase(string iconname, string name, string value, string enhancestr)
	{
		this.Icon.spriteName = iconname;
		this.NameLavel.text = name;
		this.ValueLabel.text = value;
		if (this.EnhanceLabel != null)
		{
			this.EnhanceLabel.text = enhancestr;
		}
	}

	// Token: 0x060041EE RID: 16878 RVA: 0x0013AE3C File Offset: 0x0013903C
	public void ResetAtt(random_attri randomatt, GameItem curitem)
	{
		string attributeIcon = GameDefine.GetAttributeIcon((int)randomatt.id);
		string attributeName_S = GameDefine.GetAttributeName_S((int)randomatt.id);
		string attributeValueStr = GameDefine.GetAttributeValueStr((int)randomatt.id, (int)randomatt.value);
		if (!randomatt.HasQualityId)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(curitem.ItemId);
			randomatt.qualityId = equipDataById.QualityID;
		}
		Color colorByQuality = GameDefine.GetColorByQuality(curitem.GetEquipAttQuality((int)randomatt.quality, randomatt.qualityId));
		this.ResetAtt(attributeIcon, attributeName_S, attributeValueStr, colorByQuality, false, false);
	}

	// Token: 0x060041EF RID: 16879 RVA: 0x0013AEC4 File Offset: 0x001390C4
	public void ResetSkill(random_attri randomatt, GameItem curitem)
	{
		SkillData skillDataById = DataManager.GetSkillDataById(randomatt.skillId);
		if (!randomatt.HasQualityId)
		{
			EquipData equipDataById = DataManager.GetEquipDataById(curitem.ItemId);
			randomatt.qualityId = equipDataById.QualityID;
		}
		Color colorByQuality = GameDefine.GetColorByQuality(curitem.GetEquipAttQuality((int)randomatt.quality, randomatt.qualityId));
		int playerSkillLevelByPos;
		if (SingletonUnity<OtherPlayerInfoUILogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<OtherPlayerInfoUILogic>.Instance.gameObject))
		{
			playerSkillLevelByPos = SingletonUnity<OtherPlayerInfoUILogic>.Instance.GetPlayerSkillLevelByPos((int)randomatt.index + 4);
		}
		else
		{
			playerSkillLevelByPos = Singleton<ObjManager>.Instance.MainPlayer.GetPlayerSkillLevelByPos((int)randomatt.index + 4);
		}
		this.ResetSkill(skillDataById, colorByQuality, playerSkillLevelByPos);
	}

	// Token: 0x060041F0 RID: 16880 RVA: 0x0013AF74 File Offset: 0x00139174
	public void ResetInlay(inlay inlayinfo)
	{
		ItemData itemDataByID = DataManager.GetItemDataByID(inlayinfo.itemId);
	}

	// Token: 0x060041F1 RID: 16881 RVA: 0x0013AF90 File Offset: 0x00139190
	public void ResetAtt(string iconname, string name, string value, Color showcolor, bool showArrow = false, bool isup = false)
	{
		this.Icon.spriteName = iconname;
		this.NameLavel.text = name;
		this.ValueLabel.text = value;
		this.NameLavel.color = showcolor;
		this.ValueLabel.color = showcolor;
		if (showArrow)
		{
			if (this.UpArrowObj != null && this.DownArrowObj != null)
			{
				if (isup)
				{
					NGUITools.SetActive(this.UpArrowObj, true);
					NGUITools.SetActive(this.DownArrowObj, false);
				}
				else
				{
					NGUITools.SetActive(this.UpArrowObj, false);
					NGUITools.SetActive(this.DownArrowObj, true);
				}
			}
		}
		else if (this.UpArrowObj != null && this.DownArrowObj != null)
		{
			NGUITools.SetActive(this.UpArrowObj, false);
			NGUITools.SetActive(this.DownArrowObj, false);
		}
	}

	// Token: 0x060041F2 RID: 16882 RVA: 0x0013B080 File Offset: 0x00139280
	public void ResetSkill(SkillData curdata, Color needcolor, int level)
	{
		if (curdata != null)
		{
			this.Icon.spriteName = curdata.Icon;
			this.CDlabel.text = string.Format("{0}'s", curdata.CDSecond);
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
			this.ColorSp.color = needcolor;
		}
	}

	// Token: 0x04002DDA RID: 11738
	public UISprite Icon;

	// Token: 0x04002DDB RID: 11739
	public UILabel NameLavel;

	// Token: 0x04002DDC RID: 11740
	public UILabel ValueLabel;

	// Token: 0x04002DDD RID: 11741
	public UILabel EnhanceLabel;

	// Token: 0x04002DDE RID: 11742
	public GameObject UpArrowObj;

	// Token: 0x04002DDF RID: 11743
	public GameObject DownArrowObj;

	// Token: 0x04002DE0 RID: 11744
	public UILabel CDlabel;

	// Token: 0x04002DE1 RID: 11745
	public UILabel DesLabel;

	// Token: 0x04002DE2 RID: 11746
	public UILabel LevelLabel;

	// Token: 0x04002DE3 RID: 11747
	public UISprite ColorSp;

	// Token: 0x04002DE4 RID: 11748
	public List<UISprite> SkillLabelPic;
}
