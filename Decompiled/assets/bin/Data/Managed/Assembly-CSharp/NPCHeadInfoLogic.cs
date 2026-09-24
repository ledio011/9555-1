using System;
using UnityEngine;

// Token: 0x020009E1 RID: 2529
public class NPCHeadInfoLogic : HeadInfoLogic
{
	// Token: 0x060047D7 RID: 18391 RVA: 0x0016F5C8 File Offset: 0x0016D7C8
	public void SetNameLabel(string name, GameDefine.CAMP_TYPE npcCamp, bool needShowHPLine = true, HEAD_PIC_TYPE headType = HEAD_PIC_TYPE.INVALID, bool needShowName = false)
	{
		this.NeedShowName = needShowName;
		this.NameLabel.text = name;
		switch (npcCamp)
		{
		case GameDefine.CAMP_TYPE.NORMAL_NPC:
			this.NameLabel.color = Color.red;
			if (!this.NeedShowName)
			{
				NGUITools.SetActive(this.NameLabel.gameObject, false);
			}
			goto IL_116;
		case GameDefine.CAMP_TYPE.NPC_ATTACK_NPC:
			this.NameLabel.color = Color.red;
			if (!this.NeedShowName)
			{
				NGUITools.SetActive(this.NameLabel.gameObject, false);
			}
			goto IL_116;
		case GameDefine.CAMP_TYPE.FUNCTION_NPC:
			this.NameLabel.color = Color.yellow;
			NGUITools.SetActive(this.NameLabel.gameObject, true);
			goto IL_116;
		case GameDefine.CAMP_TYPE.PLAYER_FRIEND_NPC:
			this.NameLabel.color = Color.yellow;
			NGUITools.SetActive(this.NameLabel.gameObject, true);
			goto IL_116;
		}
		this.NameLabel.color = Color.red;
		if (!this.NeedShowName)
		{
			NGUITools.SetActive(this.NameLabel.gameObject, false);
		}
		IL_116:
		switch (headType)
		{
		case HEAD_PIC_TYPE.INVALID:
			NGUITools.SetActive(this.HeadPic.gameObject, false);
			break;
		case HEAD_PIC_TYPE.SELF_ESCORT_NPC:
			NGUITools.SetActive(this.HeadPic.gameObject, true);
			this.HeadPic.spriteName = "CZ_renWuBiaoZhi";
			this.HeadPic.MakePixelPerfect();
			this.UITweenCol.enabled = true;
			this.UITweenCol.from = new Color(0f, 0.37254903f, 1f, 1f);
			this.UITweenCol.to = new Color(0f, 0.83137256f, 1f, 1f);
			break;
		case HEAD_PIC_TYPE.OTHER_ESCORT_NPC:
			NGUITools.SetActive(this.HeadPic.gameObject, true);
			this.HeadPic.spriteName = "CZ_renWuBiaoZhi";
			this.HeadPic.MakePixelPerfect();
			this.UITweenCol.enabled = true;
			this.UITweenCol.from = new Color(1f, 0.4392157f, 0f, 1f);
			this.UITweenCol.to = new Color(1f, 0f, 0f, 1f);
			break;
		case HEAD_PIC_TYPE.MISSION_COMPLETE_NPC:
			NGUITools.SetActive(this.HeadPic.gameObject, true);
			this.HeadPic.spriteName = "CZ_renWu_wenHao";
			this.HeadPic.MakePixelPerfect();
			this.HeadPic.color = Color.white;
			this.UITweenCol.enabled = false;
			break;
		case HEAD_PIC_TYPE.MISSION_TARGET_NPC:
			NGUITools.SetActive(this.HeadPic.gameObject, true);
			this.HeadPic.spriteName = "CZ_renWu_tanHao";
			this.HeadPic.MakePixelPerfect();
			this.HeadPic.color = Color.white;
			this.UITweenCol.enabled = false;
			break;
		case HEAD_PIC_TYPE.MISSION_ACCEPT_NPC:
			NGUITools.SetActive(this.HeadPic.gameObject, true);
			this.HeadPic.spriteName = "CZ_renWu_wenHao_hui";
			this.HeadPic.MakePixelPerfect();
			this.HeadPic.color = Color.white;
			this.UITweenCol.enabled = false;
			break;
		}
		if (!needShowHPLine)
		{
			UnityVersionUtil.SetActiveRecursive(this.mHpLineLogic.gameObject, false);
		}
	}

	// Token: 0x060047D8 RID: 18392 RVA: 0x0016F930 File Offset: 0x0016DB30
	public override void SetHpVal(float val)
	{
		this.ShowHpLine();
		if (this.mHpLineLogic != null)
		{
			this.mHpLineLogic.ChangeVal(val);
		}
	}

	// Token: 0x060047D9 RID: 18393 RVA: 0x0016F958 File Offset: 0x0016DB58
	public void ShowHpLine()
	{
		if (!this.NeedShowName)
		{
			NGUITools.SetActive(this.NameLabel.gameObject, true);
		}
		NGUITools.SetActive(this.mHpLineLogic.gameObject, true);
		this.lastChangeTime = Time.time;
	}

	// Token: 0x060047DA RID: 18394 RVA: 0x0016F9A0 File Offset: 0x0016DBA0
	public void HideHpLine()
	{
		if (!this.NeedShowName)
		{
			NGUITools.SetActive(this.NameLabel.gameObject, false);
		}
		NGUITools.SetActive(this.mHpLineLogic.gameObject, false);
	}

	// Token: 0x060047DB RID: 18395 RVA: 0x0016F9D0 File Offset: 0x0016DBD0
	private void Update()
	{
		if (UnityVersionUtil.IsActive(this.mHpLineLogic.gameObject) && Time.time - this.lastChangeTime > GameDefine.NPC_HP_LINE_SHOW_TIME)
		{
			this.HideHpLine();
		}
	}

	// Token: 0x0400352E RID: 13614
	public UISprite HeadPic;

	// Token: 0x0400352F RID: 13615
	public TweenColor UITweenCol;

	// Token: 0x04003530 RID: 13616
	private bool NeedShowName;

	// Token: 0x04003531 RID: 13617
	private float lastChangeTime;
}
