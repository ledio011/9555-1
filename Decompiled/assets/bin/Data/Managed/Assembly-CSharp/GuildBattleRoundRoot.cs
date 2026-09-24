using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A15 RID: 2581
public class GuildBattleRoundRoot : MonoBehaviour
{
	// Token: 0x06004A42 RID: 19010 RVA: 0x00184694 File Offset: 0x00182894
	public void EnableReset()
	{
		for (int i = 0; i < this.GuildNameLabel.Length; i++)
		{
			this.GuildNameLabel[i].color = Color.gray;
			this.GuildNameLabel[i].text = StrDictionary.GetDictionaryString("#{105070}", new object[0]);
			this.WinPic[i].enabled = false;
		}
		this.EndLabel.enabled = false;
		NGUITools.SetActive(this.StateFlag.gameObject, false);
	}

	// Token: 0x06004A43 RID: 19011 RVA: 0x00184714 File Offset: 0x00182914
	public void Reset(guild_battle_round roundInfo, bool isFinal, long targetGuildId, int curstate, int roundid)
	{
		this.EnableReset();
		this.RoundId = roundid;
		int num = -1;
		bool flag = curstate % 2 == 0 && curstate >= 0;
		if (curstate == 1 || curstate == 0)
		{
			num = 1;
		}
		else if (curstate == 2 || curstate == 3)
		{
			num = 2;
		}
		else if (curstate == 4 || curstate == 5)
		{
			num = 3;
		}
		bool flag2 = num == this.RoundId;
		if (roundInfo != null && roundInfo.HasBattle_team)
		{
			List<guild_battle_team> list = new List<guild_battle_team>(roundInfo.battle_team.Values);
			for (int i = 0; i < list.Count; i++)
			{
				checked
				{
					if (list[i].HasGuildName)
					{
						this.GuildNameLabel[(int)((IntPtr)(unchecked(list[i].index - 1L)))].text = list[i].guildName;
						if (list[i].state == 4L)
						{
							this.GuildNameLabel[(int)((IntPtr)(unchecked(list[i].index - 1L)))].color = Color.gray;
						}
						else
						{
							this.GuildNameLabel[(int)((IntPtr)(unchecked(list[i].index - 1L)))].color = Color.white;
						}
						if (flag2 && targetGuildId != -1L && list[i].guildId == targetGuildId && flag2)
						{
							this.GuildNameLabel[(int)((IntPtr)(unchecked(list[i].index - 1L)))].color = Color.green;
						}
					}
					if (list[i].state == 3L)
					{
						if (list[i].HasGuildName)
						{
							this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = true;
						}
						if (isFinal)
						{
							if (unchecked(list[i].index - 1L) / 2L == 0L)
							{
								this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].spriteName = "CZ_gongHui_GuanJun";
							}
							else if (unchecked(list[i].index - 1L) / 2L == 1L)
							{
								this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].spriteName = "CZ_gongHui_JiJun";
							}
							else
							{
								this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = false;
							}
						}
					}
					else if (isFinal)
					{
						if (list[i].state == 4L)
						{
							if (unchecked(list[i].index - 1L) / 2L == 0L)
							{
								if (list[i].HasGuildName)
								{
									this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = true;
									this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].spriteName = "CZ_gongHui_YaJun";
								}
								else
								{
									this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = false;
								}
							}
							else
							{
								this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = false;
							}
						}
						else
						{
							this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = false;
						}
					}
					else
					{
						this.WinPic[(int)((IntPtr)(unchecked(list[i].index - 1L)))].enabled = false;
					}
				}
			}
		}
		this.BgAnima.ResetToBeginning();
		this.BgAnima.enabled = false;
		if (num == -1)
		{
			this.BottomPic.spriteName = "CZ_huaDongBG_2_XuanDing";
		}
		else if (num > this.RoundId)
		{
			this.BottomPic.spriteName = "CZ_huaDongBG_2_XuanDing";
			this.EndLabel.enabled = true;
		}
		else if (num == this.RoundId)
		{
			this.BottomPic.spriteName = "CZ_huaDongBG_1";
			NGUITools.SetActive(this.StateFlag.gameObject, true);
			if (flag)
			{
				this.FlagLabel.text = StrDictionary.GetDictionaryString("#{105081}", new object[0]);
			}
			else
			{
				this.BgAnima.PlayForward();
				this.FlagLabel.text = StrDictionary.GetDictionaryString("#{105079}", new object[0]);
			}
		}
		else
		{
			this.BottomPic.spriteName = "CZ_huaDongBG";
			NGUITools.SetActive(this.StateFlag.gameObject, true);
			this.FlagLabel.text = StrDictionary.GetDictionaryString("#{105080}", new object[0]);
		}
	}

	// Token: 0x040037B8 RID: 14264
	public UILabel[] GuildNameLabel;

	// Token: 0x040037B9 RID: 14265
	public UISprite[] WinPic;

	// Token: 0x040037BA RID: 14266
	public UISprite BottomPic;

	// Token: 0x040037BB RID: 14267
	public UILabel EndLabel;

	// Token: 0x040037BC RID: 14268
	public UISprite StateFlag;

	// Token: 0x040037BD RID: 14269
	public UILabel FlagLabel;

	// Token: 0x040037BE RID: 14270
	private int RoundId;

	// Token: 0x040037BF RID: 14271
	public TweenAlpha BgAnima;
}
