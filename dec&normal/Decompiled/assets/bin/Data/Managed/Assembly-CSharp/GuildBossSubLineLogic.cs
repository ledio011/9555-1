using System;
using SprotoType;
using UnityEngine;

// Token: 0x020009D9 RID: 2521
public class GuildBossSubLineLogic : MonoBehaviour
{
	// Token: 0x060047A8 RID: 18344 RVA: 0x0016E274 File Offset: 0x0016C474
	public void ResetItem(int pIndex, int infoindex, guild_boss info, DelegateDefine.ThirdIntParamDelegate clickFunc)
	{
		this.curIndex = infoindex;
		this.parentIndex = pIndex;
		GuildBossData guildBossDataByID = DataManager.GetGuildBossDataByID(info.id);
		NpcData npcDataByID = DataManager.GetNpcDataByID(guildBossDataByID.BossID);
		Guild playerGuild = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild;
		this.enableLineFlag = 0;
		this.IconSprite.spriteName = guildBossDataByID.Icon;
		this.NameLabel.text = StrDictionary.GetDictionaryString(npcDataByID.Name, new object[0]);
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		int num = (infoindex + TimeTools.GetOffsetDay((long)guildBossDataByID.StartTime, playerCommonData.TimeOffset) + GameDefine.WEEK_NAME.Count) % GameDefine.WEEK_NAME.Count;
		this.LevelLabel.text = StrDictionary.GetDictionaryString(GameDefine.WEEK_NAME[num], new object[0]);
		if (info.state == 2L)
		{
			this.CompleteSprite.enabled = true;
		}
		else
		{
			this.CompleteSprite.enabled = false;
		}
		if (info.state != 1L)
		{
			this.enableLineFlag = 2;
		}
		if (SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.GuilLevel < guildBossDataByID.LevelMin)
		{
			this.enableLineFlag = 1;
		}
		this.RefreshSelect(-1);
		this.onClickItem = clickFunc;
	}

	// Token: 0x060047A9 RID: 18345 RVA: 0x0016E3BC File Offset: 0x0016C5BC
	public bool CheckGuildLevel(int minLevel, int maxlevel)
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.PlayerGuild.CheckGuildLevel(minLevel, maxlevel);
	}

	// Token: 0x060047AA RID: 18346 RVA: 0x0016E3E0 File Offset: 0x0016C5E0
	public void OnClikcItemBtn()
	{
		if (this.onClickItem != null)
		{
			this.onClickItem(this.parentIndex, this.curIndex, this.enableLineFlag);
		}
	}

	// Token: 0x060047AB RID: 18347 RVA: 0x0016E418 File Offset: 0x0016C618
	private void SetLabelWarining(UILabel label, bool needWarning)
	{
		if (needWarning)
		{
			label.color = Color.red;
		}
		else
		{
			label.color = Color.white;
		}
	}

	// Token: 0x060047AC RID: 18348 RVA: 0x0016E43C File Offset: 0x0016C63C
	public void RefreshSelect(int index)
	{
		if (this.enableLineFlag == 0)
		{
			this.SelectBkSprite.spriteName = "CZ_wuPinYanSe_3";
		}
		else
		{
			this.SelectBkSprite.spriteName = "CZ_tongYongDi_zhuYao_4_1";
		}
		if (index != this.curIndex)
		{
			UnityVersionUtil.SetActiveRecursive(this.SelectUpSprite, false);
		}
		else
		{
			UnityVersionUtil.SetActiveRecursive(this.SelectUpSprite, true);
		}
	}

	// Token: 0x040034EF RID: 13551
	public UILabel NameLabel;

	// Token: 0x040034F0 RID: 13552
	public UISprite IconSprite;

	// Token: 0x040034F1 RID: 13553
	public UILabel LevelLabel;

	// Token: 0x040034F2 RID: 13554
	public UISprite SelectBkSprite;

	// Token: 0x040034F3 RID: 13555
	public UISprite CompleteSprite;

	// Token: 0x040034F4 RID: 13556
	public DelegateDefine.ThirdIntParamDelegate onClickItem;

	// Token: 0x040034F5 RID: 13557
	public GameObject SelectUpSprite;

	// Token: 0x040034F6 RID: 13558
	public int enableLineFlag;

	// Token: 0x040034F7 RID: 13559
	public int curIndex = -1;

	// Token: 0x040034F8 RID: 13560
	public int parentIndex = -1;
}
