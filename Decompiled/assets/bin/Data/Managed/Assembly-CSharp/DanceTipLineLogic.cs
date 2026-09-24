using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A03 RID: 2563
public class DanceTipLineLogic : MonoBehaviour
{
	// Token: 0x06004955 RID: 18773 RVA: 0x0017AE00 File Offset: 0x00179000
	public void Reset(dance_state_info curinfo)
	{
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.CurInfo = curinfo;
		this.CurData = DataManager.GetCityDanceDataById(curinfo.ID);
		this.Icon.spriteName = StrDictionary.GetDictionaryString(this.CurData.MissionIcon, new object[0]);
		this.nameLabel.text = StrDictionary.GetDictionaryString(this.CurData.Name, new object[0]);
		this.allTime = (long)this.CurData.DurationTime;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		this.singleDanceFlag = false;
		switch (this.CurData.Type)
		{
		case 0:
			this.reamaintime = this.CurInfo.duration;
			this.singleDanceFlag = true;
			break;
		case 1:
		case 2:
		case 3:
			this.reamaintime = this.CurInfo.end_time - playerCommonData.GetCurServerTime();
			break;
		}
		this.temptime = 0f;
		if (this.reamaintime > 0L)
		{
			this.SliderPro.value = 1f - (float)this.reamaintime / (float)this.allTime;
			this.SliderSp.color = this.procolor;
			this.infolabel.color = this.procolor;
			this.infolabel.text = TimeTools.GetMinuteSecondStr(this.reamaintime);
			this.AnimaScale.enabled = true;
		}
		else
		{
			this.SliderPro.value = 1f;
			this.SliderSp.color = this.finishcolor;
			this.infolabel.color = this.finishcolor;
			this.infolabel.text = StrDictionary.GetDictionaryString("#{105078}", new object[0]);
			this.AnimaScale.ResetToBeginning();
			this.AnimaScale.enabled = false;
		}
	}

	// Token: 0x06004956 RID: 18774 RVA: 0x0017AFE4 File Offset: 0x001791E4
	private void Update()
	{
		if (this.mMainPlayer == null)
		{
			return;
		}
		if (this.singleDanceFlag && this.mMainPlayer.CurPlayerState != PLAYER_STATE.DANCE)
		{
			return;
		}
		if (this.reamaintime > 0L)
		{
			this.temptime += Time.deltaTime;
			if (this.temptime >= 1f)
			{
				this.reamaintime -= 1L;
				this.temptime -= 1f;
				this.infolabel.text = TimeTools.GetMinuteSecondStr(this.reamaintime);
				this.SliderPro.value = 1f - (float)this.reamaintime / (float)this.allTime;
			}
			if (this.reamaintime <= 0L)
			{
				this.SliderPro.value = 1f;
				this.SliderSp.color = this.finishcolor;
				this.infolabel.color = this.finishcolor;
				this.infolabel.text = StrDictionary.GetDictionaryString("#{105078}", new object[0]);
				this.AnimaScale.ResetToBeginning();
				this.AnimaScale.enabled = false;
			}
		}
	}

	// Token: 0x04003697 RID: 13975
	private dance_state_info CurInfo;

	// Token: 0x04003698 RID: 13976
	private CityDanceData CurData;

	// Token: 0x04003699 RID: 13977
	public UISprite Icon;

	// Token: 0x0400369A RID: 13978
	public UILabel nameLabel;

	// Token: 0x0400369B RID: 13979
	public UISprite SliderSp;

	// Token: 0x0400369C RID: 13980
	public UISlider SliderPro;

	// Token: 0x0400369D RID: 13981
	public UILabel infolabel;

	// Token: 0x0400369E RID: 13982
	public TweenScale AnimaScale;

	// Token: 0x0400369F RID: 13983
	private Color finishcolor = new Color(1f, 0.69411767f, 0f);

	// Token: 0x040036A0 RID: 13984
	private Color procolor = new Color(0.68235296f, 1f, 0f);

	// Token: 0x040036A1 RID: 13985
	private long reamaintime;

	// Token: 0x040036A2 RID: 13986
	private long allTime;

	// Token: 0x040036A3 RID: 13987
	private float temptime;

	// Token: 0x040036A4 RID: 13988
	private ObjMainPlayer mMainPlayer;

	// Token: 0x040036A5 RID: 13989
	private bool singleDanceFlag;
}
