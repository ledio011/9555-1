using System;
using System.Collections.Generic;
using SprotoType;
using UnityEngine;

// Token: 0x02000A26 RID: 2598
public class DominLineLogic : MonoBehaviour
{
	// Token: 0x06004B16 RID: 19222 RVA: 0x0018D818 File Offset: 0x0018BA18
	public void Reset(domin_info info, character_look cha, DelegateDefine.OneStringParamDelegate func)
	{
		this.curInfo = info;
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.curData = DataManager.GetDominDataByID(info.id);
		this.ZoneLabel.text = StrDictionary.GetDictionaryString(this.curData.ZoneName, new object[0]);
		if (this.curData.Entrytype == 0)
		{
			NGUITools.SetActive(this.AloneSP.gameObject, true);
			NGUITools.SetActive(this.TeamSp.gameObject, false);
			NGUITools.SetActive(this.GuildSp.gameObject, false);
		}
		else if (this.curData.Entrytype == 2 || this.curData.Entrytype == 1)
		{
			NGUITools.SetActive(this.AloneSP.gameObject, false);
			NGUITools.SetActive(this.TeamSp.gameObject, true);
			NGUITools.SetActive(this.GuildSp.gameObject, false);
		}
		else if (this.curData.Entrytype == 3)
		{
			NGUITools.SetActive(this.AloneSP.gameObject, false);
			NGUITools.SetActive(this.TeamSp.gameObject, false);
			NGUITools.SetActive(this.GuildSp.gameObject, true);
		}
		else
		{
			NGUITools.SetActive(this.AloneSP.gameObject, false);
			NGUITools.SetActive(this.TeamSp.gameObject, false);
			NGUITools.SetActive(this.GuildSp.gameObject, false);
		}
		if (this.curData.IsOpen == 0)
		{
			NGUITools.SetActive(this.LockIcon.gameObject, true);
			this.PlayerNameLabel.text = StrDictionary.GetDictionaryString("#{103024}", new object[0]);
			this.PlayerNameLabel.color = Color.red;
			NGUITools.SetActive(this.WinPicRoot, false);
			this.LevelLabel.text = string.Empty;
		}
		else if (playerData.Level < this.curData.LevelMin)
		{
			NGUITools.SetActive(this.LockIcon.gameObject, true);
			this.PlayerNameLabel.text = string.Format("Lv.{0}", this.curData.LevelMin);
			this.PlayerNameLabel.color = Color.red;
			NGUITools.SetActive(this.WinPicRoot, false);
			this.LevelLabel.text = string.Empty;
		}
		else
		{
			NGUITools.SetActive(this.LockIcon.gameObject, false);
			this.PlayerNameLabel.color = Color.white;
			if (this.curInfo.state == 0L)
			{
				NGUITools.SetActive(this.WinPicRoot, false);
				if (cha != null)
				{
					this.PlayerIcon.spriteName = GameDefine.Game_Player_Icon_pic[(int)(checked((IntPtr)cha.general.profession))];
					this.PlayerNameLabel.text = cha.general.name;
					this.LevelLabel.text = "Lv." + cha.attribute_other.level;
				}
				else
				{
					Debug.Log("No Character Info");
				}
			}
			else
			{
				NGUITools.SetActive(this.WinPicRoot, true);
				this.PlayerIcon.spriteName = GameDefine.Game_Player_Icon_pic[(int)playerData.Profession];
				this.PlayerNameLabel.text = StrDictionary.GetDictionaryString("#{103004}", new object[0]);
				this.LevelLabel.text = "Lv." + playerData.Level;
			}
		}
		this.onClickItem = func;
		List<string> list = new List<string>();
		list.Add(this.curData.Resources1);
		list.Add(this.curData.Resources2);
		List<int> list2 = new List<int>();
		ItemData itemDataByID = DataManager.GetItemDataByID(this.curData.Resources1);
		list2.Add(itemDataByID.Quality);
		ItemData itemDataByID2 = DataManager.GetItemDataByID(this.curData.Resources2);
		list2.Add(itemDataByID2.Quality);
		List<int> list3 = new List<int>();
		list3.Add(0);
		list3.Add(0);
		this.ShowReward.ShowRewards(list, list2, list3);
		this.UpdatePercent();
	}

	// Token: 0x06004B17 RID: 19223 RVA: 0x0018DC1C File Offset: 0x0018BE1C
	public void UpdatePercent()
	{
		if (this.curInfo.state == 1L)
		{
			long serverTime = PlayerCommonData.GetServerTime();
			long num = serverTime - this.curInfo.donmin_time;
			long num2 = this.curInfo.end_time - this.curInfo.donmin_time;
			float picLine = 1f - (float)num / (float)num2;
			this.SetPicLine(picLine);
		}
		else
		{
			this.SetPicLine(0f);
		}
	}

	// Token: 0x06004B18 RID: 19224 RVA: 0x0018DC8C File Offset: 0x0018BE8C
	public void SetPicLine(float percent)
	{
		if (percent > 1f)
		{
			percent = 1f;
		}
		if (percent > 1E-45f)
		{
			if (!this.PercentLinePic.enabled)
			{
				this.PercentLinePic.enabled = true;
			}
			this.PercentLinePic.width = (int)((float)this.PercentLineBottom.width * percent);
		}
		else
		{
			this.PercentLinePic.enabled = false;
		}
		this.PercentLabel.text = string.Format("{0}%", (int)(percent * 100f));
	}

	// Token: 0x06004B19 RID: 19225 RVA: 0x0018DD20 File Offset: 0x0018BF20
	private void Update()
	{
		if (this.curInfo != null && this.curInfo.state == 1L)
		{
			this.updateTimeCount += Time.deltaTime;
			if (this.updateTimeCount > 1f)
			{
				this.updateTimeCount = 0f;
				this.UpdatePercent();
			}
		}
	}

	// Token: 0x06004B1A RID: 19226 RVA: 0x0018DD80 File Offset: 0x0018BF80
	public void OnClickItem()
	{
		if (this.curData.IsOpen == 0)
		{
			NoticeLogic.AddNotifyData("#{103019}", true, false);
		}
		else if (!UnityVersionUtil.IsActive(this.LockIcon.gameObject))
		{
			if (this.onClickItem != null)
			{
				this.onClickItem(this.curInfo.id);
			}
			else
			{
				Debug.Log("onClickItem == null");
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{103009}", true, false);
		}
	}

	// Token: 0x040038C7 RID: 14535
	public UISprite PlayerIcon;

	// Token: 0x040038C8 RID: 14536
	public UISprite LockIcon;

	// Token: 0x040038C9 RID: 14537
	public UILabel ZoneLabel;

	// Token: 0x040038CA RID: 14538
	public UILabel PlayerNameLabel;

	// Token: 0x040038CB RID: 14539
	public UILabel PercentLabel;

	// Token: 0x040038CC RID: 14540
	public UISprite PercentLinePic;

	// Token: 0x040038CD RID: 14541
	public UISprite PercentLineBottom;

	// Token: 0x040038CE RID: 14542
	public ShowRewardItems ShowReward;

	// Token: 0x040038CF RID: 14543
	public UIWidget AloneSP;

	// Token: 0x040038D0 RID: 14544
	public UIWidget TeamSp;

	// Token: 0x040038D1 RID: 14545
	public UIWidget GuildSp;

	// Token: 0x040038D2 RID: 14546
	public GameObject WinPicRoot;

	// Token: 0x040038D3 RID: 14547
	public UILabel LevelLabel;

	// Token: 0x040038D4 RID: 14548
	private domin_info curInfo;

	// Token: 0x040038D5 RID: 14549
	private DominData curData;

	// Token: 0x040038D6 RID: 14550
	private DelegateDefine.OneStringParamDelegate onClickItem;

	// Token: 0x040038D7 RID: 14551
	private float updateTimeCount;
}
