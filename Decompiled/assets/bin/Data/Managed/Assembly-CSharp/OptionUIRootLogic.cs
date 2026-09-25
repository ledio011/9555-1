using System;
using UnityEngine;

// Token: 0x02000961 RID: 2401
public class OptionUIRootLogic : SingletonUnity<OptionUIRootLogic>
{
	// Token: 0x06004343 RID: 17219 RVA: 0x0014BF64 File Offset: 0x0014A164
	public void Reset()
	{
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.IDLabel.text = PlayerData.MainPlayerServerId.ToString();
		if (PlayerData.FaceBookBind < 0L)
		{
			this.BindSpriteIcon.spriteName = "CZ_zhuangHaoGuanLian";
			this.FaceBookLabel.text = StrDictionary.GetDictionaryString("#{200071}", new object[0]);
		}
		else
		{
			this.FaceBookLabel.text = StrDictionary.GetDictionaryString("#{200072}", new object[0]);
			if (PlayerData.FaceBookBind == 100L)
			{
				this.BindSpriteIcon.spriteName = "CZ_faceBook";
			}
			else
			{
				this.BindSpriteIcon.spriteName = "CZ_faceBook";
			}
		}
		this.CurPage = 0;
		this.OnClickFeaturesBtn();
	}

	// Token: 0x06004344 RID: 17220 RVA: 0x0014C040 File Offset: 0x0014A240
	public void OnClickFeaturesBtn()
	{
		if (this.CurPage != 1)
		{
			NGUITools.SetActive(this.ChatObj, false);
			NGUITools.SetActive(this.BattleObj, false);
			NGUITools.SetActive(this.FeaturesObj, true);
			this.ResetFeatures();
			this.SelectTra.parent = this.FeatureTra;
			this.SelectTra.localPosition = Vector3.zero;
			this.CurPage = 1;
		}
	}

	// Token: 0x06004345 RID: 17221 RVA: 0x0014C0AC File Offset: 0x0014A2AC
	public void OnClickBattleBtn()
	{
		if (this.CurPage != 2)
		{
			NGUITools.SetActive(this.ChatObj, false);
			NGUITools.SetActive(this.FeaturesObj, false);
			NGUITools.SetActive(this.BattleObj, true);
			this.ResetBattle();
			this.SelectTra.parent = this.BattleTra;
			this.SelectTra.localPosition = Vector3.zero;
			this.CurPage = 2;
		}
	}

	// Token: 0x06004346 RID: 17222 RVA: 0x0014C118 File Offset: 0x0014A318
	public void OnClickChatBtn()
	{
		if (this.CurPage != 3)
		{
			NGUITools.SetActive(this.BattleObj, false);
			NGUITools.SetActive(this.FeaturesObj, false);
			NGUITools.SetActive(this.ChatObj, true);
			this.ResetChat();
			this.SelectTra.parent = this.ChatTra;
			this.SelectTra.localPosition = Vector3.zero;
			this.CurPage = 3;
		}
	}

	// Token: 0x06004347 RID: 17223 RVA: 0x0014C184 File Offset: 0x0014A384
	public void ResetFeatures()
	{
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		if (this.playerData.SystemMusic == 1)
		{
			this.MusicSelectTra.localPosition = this.leftPos;
		}
		else
		{
			this.MusicSelectTra.localPosition = this.rightPos;
		}
		if (this.playerData.SystemSoundEffect == 1)
		{
			this.SoundSelectTra.localPosition = this.leftPos;
		}
		else
		{
			this.SoundSelectTra.localPosition = this.rightPos;
		}
		this.MusicSlider.value = this.playerData.MusicDragValue;
		this.SoundSlider.value = this.playerData.SoundDragValue;
		if (this.playerData.GraphicsQua == 1)
		{
			this.GraphicsSelect.parent = this.GraphicsHigh;
			this.GraphicsSelect.localPosition = this.selectPos;
		}
		else
		{
			this.GraphicsSelect.parent = this.GraphicsLow;
			this.GraphicsSelect.localPosition = this.selectPos;
		}
		if (this.playerData.StreetRacingMode == 1)
		{
			this.StreetSelect.parent = this.StreetNormal;
			this.StreetSelect.localPosition = this.selectPos;
		}
		else
		{
			this.StreetSelect.parent = this.StreetMotion;
			this.StreetSelect.localPosition = this.selectPos;
		}
		if (this.playerData.Screen_Vibrating == 1f)
		{
			this.Scr_VibratSelect.parent = this.Scr_on;
			this.Scr_VibratSelect.localPosition = this.selectPos;
		}
		else
		{
			this.Scr_VibratSelect.parent = this.Scr_off;
			this.Scr_VibratSelect.localPosition = this.selectPos;
		}
		if (this.playerData.Notify == 1f)
		{
			this.NotifySelect.parent = this.Notify_on;
			this.NotifySelect.localPosition = this.selectPos;
		}
		else
		{
			this.NotifySelect.parent = this.Notify_off;
			this.NotifySelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x06004348 RID: 17224 RVA: 0x0014C3C0 File Offset: 0x0014A5C0
	private void Update()
	{
		if (this.CurPage == 2 && this.reamainTime > 0L)
		{
			this.tempCountTime += Time.deltaTime;
			if (this.tempCountTime >= 1f)
			{
				this.reamainTime -= 1L;
				this.tempCountTime -= 1f;
				this.LevelInfoLabel.text = StrDictionary.GetDictionaryString("#{102126}", new object[]
				{
					TimeTools.GetFullTime(this.reamainTime)
				}) + string.Format("\n{0}", StrDictionary.GetDictionaryString("#{102127}", new object[0]));
			}
			if (this.reamainTime <= 0L)
			{
				this.tempCountTime = 0f;
				this.ShowLevelsealInfo();
			}
		}
	}

	// Token: 0x06004349 RID: 17225 RVA: 0x0014C490 File Offset: 0x0014A690
	public void ResetBattle()
	{
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		this.DragSlider.value = this.playerData.AutoUseDragThreshold / 0.8f;
		string dictionaryString = StrDictionary.GetDictionaryString("#{102127}", new object[0]);
		this.reamainTime = 0L;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData != null)
		{
			this.reamainTime = playerCommonData.ServerLevelSealTime - playerCommonData.GetCurServerTime();
		}
		if (this.reamainTime > 0L)
		{
			this.LevelInfoLabel.text = StrDictionary.GetDictionaryString("#{102126}", new object[]
			{
				TimeTools.GetFullTime(this.reamainTime)
			}) + string.Format("\n{0}", dictionaryString);
			this.DanceLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{102133}", new object[0]), "0");
			this.ExpLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{102134}", new object[0]), "100%");
		}
		else
		{
			this.ShowLevelsealInfo();
		}
		if (this.playerData.NonMissionTarget == 1)
		{
			this.NonMissionSelectSp.enabled = true;
		}
		else
		{
			this.NonMissionSelectSp.enabled = false;
		}
		if (this.playerData.VehicleTarget == 1)
		{
			this.VehicleSelectSp.enabled = true;
		}
		else
		{
			this.VehicleSelectSp.enabled = false;
		}
	}

	// Token: 0x0600434A RID: 17226 RVA: 0x0014C618 File Offset: 0x0014A818
	public void ShowLevelsealInfo()
	{
		int serverLevel = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.ServerLevel;
		int level = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.Level;
		string dictionaryString = StrDictionary.GetDictionaryString("#{102127}", new object[0]);
		this.DanceLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{102133}", new object[0]), serverLevel);
		if (level > serverLevel)
		{
			this.LevelInfoLabel.text = StrDictionary.GetDictionaryString("#{102125}", new object[]
			{
				serverLevel,
				StrDictionary.GetDictionaryString("#{102128}", new object[0])
			}) + string.Format(" {0}", level - serverLevel) + string.Format("\n{0}", dictionaryString);
			LevelSealData levelSealDataByID = DataManager.GetLevelSealDataByID((level - serverLevel).ToString());
			if (levelSealDataByID != null)
			{
				this.ExpLabel.text = string.Format("{0} {1}%", StrDictionary.GetDictionaryString("#{102134}", new object[0]), levelSealDataByID.Inhibit);
			}
		}
		else if (level == serverLevel)
		{
			this.LevelInfoLabel.text = StrDictionary.GetDictionaryString("#{102125}", new object[]
			{
				serverLevel,
				StrDictionary.GetDictionaryString("#{102130}", new object[0])
			}) + string.Format("\n{0}", dictionaryString);
			this.ExpLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{102134}", new object[0]), "100%");
		}
		else
		{
			this.LevelInfoLabel.text = StrDictionary.GetDictionaryString("#{102125}", new object[]
			{
				serverLevel,
				StrDictionary.GetDictionaryString("#{102129}", new object[0])
			}) + string.Format(" {0}", serverLevel - level) + string.Format("\n{0}", dictionaryString);
			LevelSealData levelSealDataByID2 = DataManager.GetLevelSealDataByID((serverLevel - level).ToString());
			if (levelSealDataByID2 != null)
			{
				this.ExpLabel.text = string.Format("{0} {1}%", StrDictionary.GetDictionaryString("#{102134}", new object[0]), levelSealDataByID2.Encourage);
			}
		}
	}

	// Token: 0x0600434B RID: 17227 RVA: 0x0014C850 File Offset: 0x0014AA50
	public void ResetChat()
	{
		this.playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		int getChannelValue = this.playerData.GetChannelValue;
		if ((getChannelValue & 1) != 0)
		{
			this.ChatSystemSelect.parent = this.ChatSystem_on;
			this.ChatSystemSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatSystemSelect.parent = this.ChatSystem_off;
			this.ChatSystemSelect.localPosition = this.selectPos;
		}
		if ((getChannelValue & 2) != 0)
		{
			this.ChatWorldSelect.parent = this.ChatWorld_on;
			this.ChatWorldSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatWorldSelect.parent = this.ChatWorld_off;
			this.ChatWorldSelect.localPosition = this.selectPos;
		}
		if ((getChannelValue & 4) != 0)
		{
			this.ChatNearbySelect.parent = this.ChatNearby_on;
			this.ChatNearbySelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatNearbySelect.parent = this.ChatNearby_off;
			this.ChatNearbySelect.localPosition = this.selectPos;
		}
		if ((getChannelValue & 8) != 0)
		{
			this.ChatTeamSelect.parent = this.ChatTeam_on;
			this.ChatTeamSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatTeamSelect.parent = this.ChatTeam_off;
			this.ChatTeamSelect.localPosition = this.selectPos;
		}
		if ((getChannelValue & 16) != 0)
		{
			this.ChatGuildSelect.parent = this.ChatGuild_on;
			this.ChatGuildSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatGuildSelect.parent = this.ChatGuild_off;
			this.ChatGuildSelect.localPosition = this.selectPos;
		}
		if ((getChannelValue & 32) != 0)
		{
			this.ChatPrivateSelect.parent = this.ChatPrivate_on;
			this.ChatPrivateSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatPrivateSelect.parent = this.ChatPrivate_off;
			this.ChatPrivateSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x0600434C RID: 17228 RVA: 0x0014CA74 File Offset: 0x0014AC74
	public void OnClickSupport()
	{
		if (GameManager.IsSupportCurDataVersion56())
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.ReportRoot, null, null);
		}
	}

	// Token: 0x0600434D RID: 17229 RVA: 0x0014CA94 File Offset: 0x0014AC94
	public void OnClickFaceBook()
	{
		if (PlayerData.FaceBookBind > -1L)
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{200091}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnClickYes), null, null, null);
		}
		else
		{
			MessageBoxLogic.OpenOKCancelBox(StrDictionary.GetDictionaryString("#{200090}", new object[0]), StrDictionary.GetDictionaryString("#{100127}", new object[0]), new MessageBoxLogic.OnYesClick(this.OnClickYes), null, null, null);
		}
	}

	// Token: 0x0600434E RID: 17230 RVA: 0x0014CB1C File Offset: 0x0014AD1C
	public void OnClickYes()
	{
		AccountVersionCheckRootLogic.ClickBindFaceBook();
	}

	// Token: 0x0600434F RID: 17231 RVA: 0x0014CB24 File Offset: 0x0014AD24
	public void OnClickGraphics_high()
	{
		this.SetGraphics(1);
	}

	// Token: 0x06004350 RID: 17232 RVA: 0x0014CB30 File Offset: 0x0014AD30
	public void OnClickGraphics_low()
	{
		this.SetGraphics(0);
	}

	// Token: 0x06004351 RID: 17233 RVA: 0x0014CB3C File Offset: 0x0014AD3C
	public void OnClickStreet_normal()
	{
		this.SetStreetRacing(1);
	}

	// Token: 0x06004352 RID: 17234 RVA: 0x0014CB48 File Offset: 0x0014AD48
	public void OnClickStreet_motion()
	{
		this.SetStreetRacing(0);
	}

	// Token: 0x06004353 RID: 17235 RVA: 0x0014CB54 File Offset: 0x0014AD54
	public void OnClickScreenVibrat_on()
	{
		this.SetScreenVibrat(1);
	}

	// Token: 0x06004354 RID: 17236 RVA: 0x0014CB60 File Offset: 0x0014AD60
	public void OnClickScreenVibrat_off()
	{
		this.SetScreenVibrat(0);
	}

	// Token: 0x06004355 RID: 17237 RVA: 0x0014CB6C File Offset: 0x0014AD6C
	public void OnClickNotify_on()
	{
		this.SetNotify(1);
	}

	// Token: 0x06004356 RID: 17238 RVA: 0x0014CB78 File Offset: 0x0014AD78
	public void OnClickNotify_off()
	{
		this.SetNotify(0);
	}

	// Token: 0x06004357 RID: 17239 RVA: 0x0014CB84 File Offset: 0x0014AD84
	private void SetGraphics(int val)
	{
		if (val != this.playerData.GraphicsQua)
		{
			this.playerData.SetGraphics(val);
			if (this.playerData.GraphicsQua == 1)
			{
				this.GraphicsSelect.parent = this.GraphicsHigh;
				this.GraphicsSelect.localPosition = this.selectPos;
			}
			else
			{
				this.GraphicsSelect.parent = this.GraphicsLow;
				this.GraphicsSelect.localPosition = this.selectPos;
			}
		}
	}

	// Token: 0x06004358 RID: 17240 RVA: 0x0014CC08 File Offset: 0x0014AE08
	private void SetStreetRacing(int value)
	{
		if (value == this.playerData.StreetRacingMode)
		{
			return;
		}
		this.playerData.SetStreetModel(value);
		if (this.playerData.StreetRacingMode == 1)
		{
			this.StreetSelect.parent = this.StreetNormal;
			this.StreetSelect.localPosition = this.selectPos;
		}
		else
		{
			this.StreetSelect.parent = this.StreetMotion;
			this.StreetSelect.localPosition = this.selectPos;
		}
		if (SingletonUnity<CarControllerRootLogic>.Exists && UnityVersionUtil.IsActive(SingletonUnity<CarControllerRootLogic>.Instance.gameObject))
		{
			SingletonUnity<CarControllerRootLogic>.Instance.Reset();
		}
	}

	// Token: 0x06004359 RID: 17241 RVA: 0x0014CCB8 File Offset: 0x0014AEB8
	private void SetScreenVibrat(int value)
	{
		if ((float)value == this.playerData.Screen_Vibrating)
		{
			return;
		}
		this.playerData.SetScreenVibrat(value);
		if (this.playerData.Screen_Vibrating == 1f)
		{
			this.Scr_VibratSelect.parent = this.Scr_on;
			this.Scr_VibratSelect.localPosition = this.selectPos;
		}
		else
		{
			this.Scr_VibratSelect.parent = this.Scr_off;
			this.Scr_VibratSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x0600435A RID: 17242 RVA: 0x0014CD44 File Offset: 0x0014AF44
	private void SetNotify(int value)
	{
		if ((float)value == this.playerData.Notify)
		{
			return;
		}
		this.playerData.SetNotify(value);
		if (this.playerData.Notify == 1f)
		{
			this.NotifySelect.parent = this.Notify_on;
			this.NotifySelect.localPosition = this.selectPos;
		}
		else
		{
			this.NotifySelect.parent = this.Notify_off;
			this.NotifySelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x0600435B RID: 17243 RVA: 0x0014CDD0 File Offset: 0x0014AFD0
	public void OnClickMusicBtn()
	{
		if (this.playerData.SystemMusic == 1)
		{
			this.playerData.SystemMusic = 0;
			this.MusicSelectTra.localPosition = this.rightPos;
			SingletonDontDestoryUnity<SoundManager>.Instance.EnableBGM = false;
		}
		else
		{
			this.playerData.SystemMusic = 1;
			this.MusicSelectTra.localPosition = this.leftPos;
			SingletonDontDestoryUnity<SoundManager>.Instance.EnableBGM = true;
		}
	}

	// Token: 0x0600435C RID: 17244 RVA: 0x0014CE44 File Offset: 0x0014B044
	public void OnClickSoundBtn()
	{
		if (this.playerData.SystemSoundEffect == 1)
		{
			this.playerData.SystemSoundEffect = 0;
			this.SoundSelectTra.localPosition = this.rightPos;
			SingletonDontDestoryUnity<SoundManager>.Instance.EnableSFX = false;
		}
		else
		{
			this.playerData.SystemSoundEffect = 1;
			this.SoundSelectTra.localPosition = this.leftPos;
			SingletonDontDestoryUnity<SoundManager>.Instance.EnableSFX = true;
		}
	}

	// Token: 0x0600435D RID: 17245 RVA: 0x0014CEB8 File Offset: 0x0014B0B8
	public void DragMusicSlider()
	{
		this.playerData.SetMusicDragValue(this.MusicSlider.value);
		SingletonDontDestoryUnity<SoundManager>.Instance.bgmVolume = this.MusicSlider.value;
	}

	// Token: 0x0600435E RID: 17246 RVA: 0x0014CEF0 File Offset: 0x0014B0F0
	public void DragSoundSlider()
	{
		this.playerData.SetSoundDragValue(this.SoundSlider.value);
		SingletonDontDestoryUnity<SoundManager>.Instance.sfxVolume = this.SoundSlider.value;
	}

	// Token: 0x0600435F RID: 17247 RVA: 0x0014CF28 File Offset: 0x0014B128
	public void UseDragValue()
	{
		this.DragLabel.text = Mathf.RoundToInt(this.DragSlider.value * 0.8f * 100f) + "%";
		this.playerData.SetDragValue(this.DragSlider.value * 0.8f);
	}

	// Token: 0x06004360 RID: 17248 RVA: 0x0014CF88 File Offset: 0x0014B188
	public void OnClikcSignOut()
	{
		SingletonDontDestoryUnity<NetManager>.Instance.LeaveGame();
		LoadingWindow.LoadScene(0);
	}

	// Token: 0x06004361 RID: 17249 RVA: 0x0014CF9C File Offset: 0x0014B19C
	public void OnClickClose()
	{
		SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.OptionUIRootLogic);
	}

	// Token: 0x06004362 RID: 17250 RVA: 0x0014CFB0 File Offset: 0x0014B1B0
	public void OnClickChatSystem_on()
	{
		this.SetChatSystem(1);
	}

	// Token: 0x06004363 RID: 17251 RVA: 0x0014CFBC File Offset: 0x0014B1BC
	public void OnClickChatSystem_off()
	{
		this.SetChatSystem(0);
	}

	// Token: 0x06004364 RID: 17252 RVA: 0x0014CFC8 File Offset: 0x0014B1C8
	private void SetChatSystem(int value)
	{
		int num = this.playerData.GetChannelValue;
		if ((num & 1) != 0)
		{
			if (value == 0)
			{
				num--;
				this.playerData.SetChannel(num);
			}
		}
		else if (value == 1)
		{
			num++;
			this.playerData.SetChannel(num);
		}
		if ((num & 1) != 0)
		{
			this.ChatSystemSelect.parent = this.ChatSystem_on;
			this.ChatSystemSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatSystemSelect.parent = this.ChatSystem_off;
			this.ChatSystemSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x06004365 RID: 17253 RVA: 0x0014D06C File Offset: 0x0014B26C
	public void OnClickChatWorld_on()
	{
		this.SetChatWorld(1);
	}

	// Token: 0x06004366 RID: 17254 RVA: 0x0014D078 File Offset: 0x0014B278
	public void OnClickChatWorld_off()
	{
		this.SetChatWorld(0);
	}

	// Token: 0x06004367 RID: 17255 RVA: 0x0014D084 File Offset: 0x0014B284
	private void SetChatWorld(int value)
	{
		int num = this.playerData.GetChannelValue;
		if ((num & 2) != 0)
		{
			if (value == 0)
			{
				num -= 2;
				this.playerData.SetChannel(num);
			}
		}
		else if (value == 1)
		{
			num += 2;
			this.playerData.SetChannel(num);
		}
		if ((num & 2) != 0)
		{
			this.ChatWorldSelect.parent = this.ChatWorld_on;
			this.ChatWorldSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatWorldSelect.parent = this.ChatWorld_off;
			this.ChatWorldSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x06004368 RID: 17256 RVA: 0x0014D128 File Offset: 0x0014B328
	public void OnClickChatNearby_on()
	{
		this.SetChatNearby(1);
	}

	// Token: 0x06004369 RID: 17257 RVA: 0x0014D134 File Offset: 0x0014B334
	public void OnClickChatNearby_off()
	{
		this.SetChatNearby(0);
	}

	// Token: 0x0600436A RID: 17258 RVA: 0x0014D140 File Offset: 0x0014B340
	private void SetChatNearby(int value)
	{
		int num = this.playerData.GetChannelValue;
		if ((num & 4) != 0)
		{
			if (value == 0)
			{
				num -= 4;
				this.playerData.SetChannel(num);
			}
		}
		else if (value == 1)
		{
			num += 4;
			this.playerData.SetChannel(num);
		}
		if ((num & 4) != 0)
		{
			this.ChatNearbySelect.parent = this.ChatNearby_on;
			this.ChatNearbySelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatNearbySelect.parent = this.ChatNearby_off;
			this.ChatNearbySelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x0600436B RID: 17259 RVA: 0x0014D1E4 File Offset: 0x0014B3E4
	public void OnClickChatTeam_on()
	{
		this.SetChatTeam(1);
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x0014D1F0 File Offset: 0x0014B3F0
	public void OnClickChaTeam_off()
	{
		this.SetChatTeam(0);
	}

	// Token: 0x0600436D RID: 17261 RVA: 0x0014D1FC File Offset: 0x0014B3FC
	private void SetChatTeam(int value)
	{
		int num = this.playerData.GetChannelValue;
		if ((num & 8) != 0)
		{
			if (value == 0)
			{
				num -= 8;
				this.playerData.SetChannel(num);
			}
		}
		else if (value == 1)
		{
			num += 8;
			this.playerData.SetChannel(num);
		}
		if ((num & 8) != 0)
		{
			this.ChatTeamSelect.parent = this.ChatTeam_on;
			this.ChatTeamSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatTeamSelect.parent = this.ChatTeam_off;
			this.ChatTeamSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x0600436E RID: 17262 RVA: 0x0014D2A0 File Offset: 0x0014B4A0
	public void OnClickChatGuild_on()
	{
		this.SetChatGuild(1);
	}

	// Token: 0x0600436F RID: 17263 RVA: 0x0014D2AC File Offset: 0x0014B4AC
	public void OnClickChaGuild_off()
	{
		this.SetChatGuild(0);
	}

	// Token: 0x06004370 RID: 17264 RVA: 0x0014D2B8 File Offset: 0x0014B4B8
	private void SetChatGuild(int value)
	{
		int num = this.playerData.GetChannelValue;
		if ((num & 16) != 0)
		{
			if (value == 0)
			{
				num -= 16;
				this.playerData.SetChannel(num);
			}
		}
		else if (value == 1)
		{
			num += 16;
			this.playerData.SetChannel(num);
		}
		if ((num & 16) != 0)
		{
			this.ChatGuildSelect.parent = this.ChatGuild_on;
			this.ChatGuildSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatGuildSelect.parent = this.ChatGuild_off;
			this.ChatGuildSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x06004371 RID: 17265 RVA: 0x0014D360 File Offset: 0x0014B560
	public void OnClickChatPrivate_on()
	{
		this.SetChatPrivate(1);
	}

	// Token: 0x06004372 RID: 17266 RVA: 0x0014D36C File Offset: 0x0014B56C
	public void OnClickChaPrivate_off()
	{
		this.SetChatPrivate(0);
	}

	// Token: 0x06004373 RID: 17267 RVA: 0x0014D378 File Offset: 0x0014B578
	private void SetChatPrivate(int value)
	{
		int num = this.playerData.GetChannelValue;
		if ((num & 32) != 0)
		{
			if (value == 0)
			{
				num -= 32;
				this.playerData.SetChannel(num);
			}
		}
		else if (value == 1)
		{
			num += 32;
			this.playerData.SetChannel(num);
		}
		if ((num & 32) != 0)
		{
			this.ChatPrivateSelect.parent = this.ChatPrivate_on;
			this.ChatPrivateSelect.localPosition = this.selectPos;
		}
		else
		{
			this.ChatPrivateSelect.parent = this.ChatPrivate_off;
			this.ChatPrivateSelect.localPosition = this.selectPos;
		}
	}

	// Token: 0x06004374 RID: 17268 RVA: 0x0014D420 File Offset: 0x0014B620
	public void OnClickNonmissionBtn()
	{
		if (this.playerData.NonMissionTarget == 1)
		{
			this.playerData.SetNonMissionTarget(0);
			this.NonMissionSelectSp.enabled = false;
		}
		else
		{
			this.playerData.SetNonMissionTarget(1);
			this.NonMissionSelectSp.enabled = true;
		}
	}

	// Token: 0x06004375 RID: 17269 RVA: 0x0014D474 File Offset: 0x0014B674
	public void OnClickVehicleBtn()
	{
		if (this.playerData.VehicleTarget == 1)
		{
			this.playerData.SetVehicleTarget(0);
			this.VehicleSelectSp.enabled = false;
		}
		else
		{
			this.playerData.SetVehicleTarget(1);
			this.VehicleSelectSp.enabled = true;
		}
	}

	// Token: 0x04002FCB RID: 12235
	public UILabel IDLabel;

	// Token: 0x04002FCC RID: 12236
	private PlayerData playerData;

	// Token: 0x04002FCD RID: 12237
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002FCE RID: 12238
	public UISlider DragSlider;

	// Token: 0x04002FCF RID: 12239
	public UILabel DragLabel;

	// Token: 0x04002FD0 RID: 12240
	public UILabel FaceBookLabel;

	// Token: 0x04002FD1 RID: 12241
	public Transform MusicSelectTra;

	// Token: 0x04002FD2 RID: 12242
	public Transform SoundSelectTra;

	// Token: 0x04002FD3 RID: 12243
	public UISlider MusicSlider;

	// Token: 0x04002FD4 RID: 12244
	public UISlider SoundSlider;

	// Token: 0x04002FD5 RID: 12245
	public UISprite BindSpriteIcon;

	// Token: 0x04002FD6 RID: 12246
	private Vector3 leftPos = new Vector3(-11.5f, 0f, 0f);

	// Token: 0x04002FD7 RID: 12247
	private Vector3 rightPos = new Vector3(12.5f, 0f, 0f);

	// Token: 0x04002FD8 RID: 12248
	public Transform GraphicsSelect;

	// Token: 0x04002FD9 RID: 12249
	public Transform GraphicsHigh;

	// Token: 0x04002FDA RID: 12250
	public Transform GraphicsLow;

	// Token: 0x04002FDB RID: 12251
	public Transform StreetSelect;

	// Token: 0x04002FDC RID: 12252
	public Transform StreetNormal;

	// Token: 0x04002FDD RID: 12253
	public Transform StreetMotion;

	// Token: 0x04002FDE RID: 12254
	public Transform Scr_VibratSelect;

	// Token: 0x04002FDF RID: 12255
	public Transform Scr_on;

	// Token: 0x04002FE0 RID: 12256
	public Transform Scr_off;

	// Token: 0x04002FE1 RID: 12257
	public Transform NotifySelect;

	// Token: 0x04002FE2 RID: 12258
	public Transform Notify_on;

	// Token: 0x04002FE3 RID: 12259
	public Transform Notify_off;

	// Token: 0x04002FE4 RID: 12260
	private Vector3 selectPos = new Vector3(14f, 5f, 0f);

	// Token: 0x04002FE5 RID: 12261
	private int CurPage;

	// Token: 0x04002FE6 RID: 12262
	public Transform SelectTra;

	// Token: 0x04002FE7 RID: 12263
	public Transform FeatureTra;

	// Token: 0x04002FE8 RID: 12264
	public Transform BattleTra;

	// Token: 0x04002FE9 RID: 12265
	public Transform ChatTra;

	// Token: 0x04002FEA RID: 12266
	public GameObject FeaturesObj;

	// Token: 0x04002FEB RID: 12267
	public GameObject BattleObj;

	// Token: 0x04002FEC RID: 12268
	public GameObject ChatObj;

	// Token: 0x04002FED RID: 12269
	public Transform ChatSystemSelect;

	// Token: 0x04002FEE RID: 12270
	public Transform ChatSystem_on;

	// Token: 0x04002FEF RID: 12271
	public Transform ChatSystem_off;

	// Token: 0x04002FF0 RID: 12272
	public Transform ChatWorldSelect;

	// Token: 0x04002FF1 RID: 12273
	public Transform ChatWorld_on;

	// Token: 0x04002FF2 RID: 12274
	public Transform ChatWorld_off;

	// Token: 0x04002FF3 RID: 12275
	public Transform ChatNearbySelect;

	// Token: 0x04002FF4 RID: 12276
	public Transform ChatNearby_on;

	// Token: 0x04002FF5 RID: 12277
	public Transform ChatNearby_off;

	// Token: 0x04002FF6 RID: 12278
	public Transform ChatTeamSelect;

	// Token: 0x04002FF7 RID: 12279
	public Transform ChatTeam_on;

	// Token: 0x04002FF8 RID: 12280
	public Transform ChatTeam_off;

	// Token: 0x04002FF9 RID: 12281
	public Transform ChatGuildSelect;

	// Token: 0x04002FFA RID: 12282
	public Transform ChatGuild_on;

	// Token: 0x04002FFB RID: 12283
	public Transform ChatGuild_off;

	// Token: 0x04002FFC RID: 12284
	public Transform ChatPrivateSelect;

	// Token: 0x04002FFD RID: 12285
	public Transform ChatPrivate_on;

	// Token: 0x04002FFE RID: 12286
	public Transform ChatPrivate_off;

	// Token: 0x04002FFF RID: 12287
	public UILabel LevelInfoLabel;

	// Token: 0x04003000 RID: 12288
	public UILabel DanceLabel;

	// Token: 0x04003001 RID: 12289
	public UILabel ExpLabel;

	// Token: 0x04003002 RID: 12290
	public UISprite NonMissionSelectSp;

	// Token: 0x04003003 RID: 12291
	public UISprite VehicleSelectSp;

	// Token: 0x04003004 RID: 12292
	private long reamainTime;

	// Token: 0x04003005 RID: 12293
	private float tempCountTime;
}
