using System;
using SprotoType;

// Token: 0x02000965 RID: 2405
public class PVPBeforeStartRootLogic : SingletonUnity<PVPBeforeStartRootLogic>
{
	// Token: 0x06004393 RID: 17299 RVA: 0x0014E8B8 File Offset: 0x0014CAB8
	public void ResetRankPVPPage(character character)
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.LeftNameLabel.text = playerData.MainPlayerAttrData.Name;
		this.RightNameLabel.text = character.general.name;
		this.LeftCombolValueLabel.text = playerData.MainPlayerAttrData.ComboValue.ToString();
		this.LeftPlayerPic.spriteName = GameDefine.Player_Icon_Pic[(int)playerData.Profession];
		this.RightPlayerPic.spriteName = GameDefine.Player_Icon_Pic[(int)(checked((IntPtr)character.general.profession))];
		this.RightCombolValueLabel.text = character.attribute_other.combValue.ToString();
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType == MAPTYPE.DOMIN_MAP)
		{
			this.TitleLabel.text = string.Empty;
		}
		else
		{
			this.TitleLabel.text = "RankPVP";
		}
	}

	// Token: 0x04003030 RID: 12336
	public UILabel LeftNameLabel;

	// Token: 0x04003031 RID: 12337
	public UILabel RightNameLabel;

	// Token: 0x04003032 RID: 12338
	public UISprite LeftPlayerPic;

	// Token: 0x04003033 RID: 12339
	public UISprite RightPlayerPic;

	// Token: 0x04003034 RID: 12340
	public UILabel TitleLabel;

	// Token: 0x04003035 RID: 12341
	public UILabel LeftCombolValueLabel;

	// Token: 0x04003036 RID: 12342
	public UILabel RightCombolValueLabel;
}
