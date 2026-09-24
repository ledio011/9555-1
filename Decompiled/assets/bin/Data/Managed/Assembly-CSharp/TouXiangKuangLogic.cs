using System;
using SprotoType;
using UnityEngine;

// Token: 0x02000A54 RID: 2644
public class TouXiangKuangLogic : SingletonUnity<TouXiangKuangLogic>
{
	// Token: 0x06004D0A RID: 19722 RVA: 0x001A3370 File Offset: 0x001A1570
	public void RegisterTutorialEvent(TutorialManager.OnClickTutorialBtn tutorialEvent)
	{
		this.mOnClickTutorialBtn = tutorialEvent;
	}

	// Token: 0x06004D0B RID: 19723 RVA: 0x001A337C File Offset: 0x001A157C
	private void CheckTutorialEvent()
	{
		if (this.mOnClickTutorialBtn != null)
		{
			this.mOnClickTutorialBtn(false);
			this.mOnClickTutorialBtn = null;
		}
	}

	// Token: 0x06004D0C RID: 19724 RVA: 0x001A339C File Offset: 0x001A159C
	public void Init()
	{
		this.mPlayerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (this.mPlayerData == null)
		{
			return;
		}
		this.PlayerIcon.spriteName = GameDefine.Game_Player_Icon_pic[(int)this.mPlayerData.Profession];
		this.PlayerIcon.MakePixelPerfect();
		this.NameLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), this.mPlayerData.MainPlayerAttrData.ComboValue);
		this.mCurCombolValue = this.mPlayerData.MainPlayerAttrData.ComboValue;
		this.ChangeLevel(this.mPlayerData.Level);
		this.HPLineLength = this.HpBottomPic.width;
		this.SetHPLine(1f);
		long hp = this.mPlayerData.MainPlayerAttrData.HP;
		long maxHP = this.mPlayerData.MainPlayerAttrData.MaxHP;
		this.ChangeHP(hp, maxHP);
		if (SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType == MAPTYPE.TUTORIAL_CAR || string.IsNullOrEmpty(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.SaftyAreaId))
		{
			NGUITools.SetActive(this.PvpRoot, false);
		}
		else
		{
			NGUITools.SetActive(this.PvpRoot, true);
			SceneManager sceneManager = SingletonDontDestoryUnity<GameManager>.Instance.SceneManager;
			if (sceneManager.CurrentMapInofData.TargetPKMode == 2)
			{
				if (this.mPlayerData.IsHaveGuild())
				{
					if (this.mPlayerData.PlayerPkMode != 2)
					{
						request_change_pk_mode.request request = new request_change_pk_mode.request();
						request.pk = 2L;
						NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request, null);
						this.mPlayerData.SetPKModeState(2);
					}
				}
				else if (this.mPlayerData.PlayerPkMode != 1)
				{
					request_change_pk_mode.request request2 = new request_change_pk_mode.request();
					request2.pk = 1L;
					NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request2, null);
					this.mPlayerData.SetPKModeState(1);
				}
			}
			else if (this.mPlayerData.PlayerPkMode == 0)
			{
				request_change_pk_mode.request request3 = new request_change_pk_mode.request();
				request3.pk = 1L;
				NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request3, null);
				this.mPlayerData.SetPKModeState(1);
			}
			this.UpdateStateBtn();
			this.isLockPVP = (sceneManager.CurrentMapInofData.IsLockPVP == 1);
			if (this.CanShowPvpBtnTutorial())
			{
			}
		}
		if (GameManager.IsSupportCurDataVersion177())
		{
			ConfigData configDataByKey = DataManager.GetConfigDataByKey("pkModeCDTime");
			if (configDataByKey != null)
			{
				this.StateCDTime = configDataByKey.Valuef;
			}
			else
			{
				this.StateCDTime = 60f;
			}
		}
		else
		{
			this.StateCDTime = 60f;
		}
		if (TouXiangKuangLogic.LastChangeTime > 0f && Time.time - TouXiangKuangLogic.LastChangeTime < this.StateCDTime && Time.time - TouXiangKuangLogic.LastChangeTime >= 0f)
		{
			this.Iscdflag = true;
			this.CDFlag.fillAmount = 1f - (Time.time - TouXiangKuangLogic.LastChangeTime) / this.StateCDTime;
		}
		else
		{
			this.Iscdflag = false;
			this.CDFlag.fillAmount = 0f;
		}
	}

	// Token: 0x06004D0D RID: 19725 RVA: 0x001A36B8 File Offset: 0x001A18B8
	public void ChangeCarIcon(long curHp, long curMaxHp)
	{
		this.PlayerIcon.spriteName = "CZ_cheTouXiang";
		this.PlayerIcon.MakePixelPerfect();
		this.ChangeHP(curHp, curMaxHp);
	}

	// Token: 0x06004D0E RID: 19726 RVA: 0x001A36E0 File Offset: 0x001A18E0
	public bool CanShowPvpBtnTutorial()
	{
		return SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.MapType != MAPTYPE.TUTORIAL_CAR && !string.IsNullOrEmpty(SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.CurrentMapInofData.SaftyAreaId) && (SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.IsTutorialCanShow(FUNCTION_TYPE.PVP_BTN_TUTORIAL_TIP) && !SingletonUnity<UIManager>.Instance.IsHideBaseUI && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.LoadingFlag);
	}

	// Token: 0x06004D0F RID: 19727 RVA: 0x001A3768 File Offset: 0x001A1968
	public void ChangeHP(long nCurHp, long nMaxHp)
	{
		this.HpValueLable.text = string.Format("{0}/{1}", nCurHp, nMaxHp);
		this.mSliderProgress = (float)nCurHp / (float)nMaxHp;
		if (this.mSliderProgress < 0.2f)
		{
			RedScreenRootLogic.EnableRedScreen();
		}
		else
		{
			RedScreenRootLogic.DisableRedScreen();
		}
	}

	// Token: 0x06004D10 RID: 19728 RVA: 0x001A37C0 File Offset: 0x001A19C0
	public void ChangeLevel(int level)
	{
		if (this.preLevel != level)
		{
			this.preLevel = level;
			this.LvLable.text = string.Format("Lv.{0}", level);
		}
	}

	// Token: 0x06004D11 RID: 19729 RVA: 0x001A37FC File Offset: 0x001A19FC
	private void SetHPLine(float t)
	{
		this.HpLinePic.width = (int)(t * (float)this.HPLineLength);
	}

	// Token: 0x06004D12 RID: 19730 RVA: 0x001A3814 File Offset: 0x001A1A14
	private void UpdateHPValue()
	{
		float num = (float)this.HpLinePic.width / (float)this.HPLineLength;
		if (num > this.mSliderProgress)
		{
			num -= Time.deltaTime * 2f;
			if (num < this.mSliderProgress)
			{
				num = this.mSliderProgress;
			}
			this.SetHPLine(num);
		}
		else if (num < this.mSliderProgress)
		{
			num += Time.deltaTime * 2f;
			if (num > this.mSliderProgress)
			{
				num = this.mSliderProgress;
			}
			this.SetHPLine(num);
		}
		if (this.mPlayerData.MainPlayerAttrData.ComboValue != this.mCurCombolValue)
		{
			this.NameLabel.text = string.Format("{0} {1}", StrDictionary.GetDictionaryString("#{100421}", new object[0]), this.mPlayerData.MainPlayerAttrData.ComboValue);
			this.mCurCombolValue = this.mPlayerData.MainPlayerAttrData.ComboValue;
		}
	}

	// Token: 0x06004D13 RID: 19731 RVA: 0x001A3910 File Offset: 0x001A1B10
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06004D14 RID: 19732 RVA: 0x001A3918 File Offset: 0x001A1B18
	private void Update()
	{
		this.UpdateHPValue();
		this.UpdatePkModeState();
	}

	// Token: 0x06004D15 RID: 19733 RVA: 0x001A3928 File Offset: 0x001A1B28
	private void UpdatePkModeState()
	{
		if (this.Iscdflag)
		{
			if (TouXiangKuangLogic.LastChangeTime > 0f && Time.time - TouXiangKuangLogic.LastChangeTime < this.StateCDTime && Time.time - TouXiangKuangLogic.LastChangeTime >= 0f)
			{
				this.CDFlag.fillAmount = 1f - (Time.time - TouXiangKuangLogic.LastChangeTime) / this.StateCDTime;
			}
			else
			{
				this.Iscdflag = false;
				this.CDFlag.fillAmount = 0f;
			}
		}
	}

	// Token: 0x06004D16 RID: 19734 RVA: 0x001A39BC File Offset: 0x001A1BBC
	public void OnClickIcon()
	{
		if (!SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsBigWorld() && !SingletonDontDestoryUnity<GameManager>.Instance.SceneManager.IsTutorialScene())
		{
			return;
		}
		SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.OptionUIRootLogic, delegate(bool bSuccess, object param)
		{
			if (bSuccess)
			{
				SingletonUnity<OptionUIRootLogic>.Instance.Reset();
			}
		}, null);
	}

	// Token: 0x06004D17 RID: 19735 RVA: 0x001A3A20 File Offset: 0x001A1C20
	public void OnClickStateBtn()
	{
		if (this.isLockPVP)
		{
			return;
		}
		if (this.isOpenState)
		{
			this.StateListObj.alpha = 0f;
			NGUITools.SetActive(this.StateListObj.gameObject, false);
		}
		else
		{
			if (this.Iscdflag)
			{
				return;
			}
			this.StateListObj.alpha = 1f;
			NGUITools.SetActive(this.StateListObj.gameObject, true);
		}
		this.isOpenState = !this.isOpenState;
	}

	// Token: 0x06004D18 RID: 19736 RVA: 0x001A3AA8 File Offset: 0x001A1CA8
	public void OnClickPVPBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerPkMode != 1)
		{
			request_change_pk_mode.request request = new request_change_pk_mode.request();
			request.pk = 1L;
			NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request, null);
			playerData.SetPKModeState(1);
			this.CurStateLabel.text = StrDictionary.GetDictionaryString("#{100125}", new object[0]);
			TouXiangKuangLogic.LastChangeTime = Time.time;
			this.Iscdflag = true;
		}
		if (this.isOpenState)
		{
			this.OnClickStateBtn();
		}
	}

	// Token: 0x06004D19 RID: 19737 RVA: 0x001A3B2C File Offset: 0x001A1D2C
	public void OnClickPVEBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerPkMode != 0)
		{
			request_change_pk_mode.request request = new request_change_pk_mode.request();
			request.pk = 0L;
			NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request, null);
			playerData.SetPKModeState(0);
			this.CurStateLabel.text = StrDictionary.GetDictionaryString("#{100124}", new object[0]);
			TouXiangKuangLogic.LastChangeTime = Time.time;
			this.Iscdflag = true;
		}
		if (this.isOpenState)
		{
			this.OnClickStateBtn();
		}
	}

	// Token: 0x06004D1A RID: 19738 RVA: 0x001A3BB0 File Offset: 0x001A1DB0
	public void OnClickGangBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.IsHaveGuild() && playerData.PlayerPkMode != 2)
		{
			request_change_pk_mode.request request = new request_change_pk_mode.request();
			request.pk = 2L;
			NetLogic.GetInstance().Send<Protocol.request_change_pk_mode>(request, null);
			playerData.SetPKModeState(2);
			this.CurStateLabel.text = StrDictionary.GetDictionaryString("#{100114}", new object[0]);
			TouXiangKuangLogic.LastChangeTime = Time.time;
			this.Iscdflag = true;
		}
		if (this.isOpenState)
		{
			this.OnClickStateBtn();
		}
	}

	// Token: 0x06004D1B RID: 19739 RVA: 0x001A3C40 File Offset: 0x001A1E40
	public void UpdateStateBtn()
	{
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		if (playerData.PlayerPkMode == 0)
		{
			this.CurStateLabel.text = StrDictionary.GetDictionaryString("#{100124}", new object[0]);
		}
		else if (playerData.PlayerPkMode == 1)
		{
			this.CurStateLabel.text = StrDictionary.GetDictionaryString("#{100125}", new object[0]);
		}
		else if (playerData.PlayerPkMode == 2)
		{
			this.CurStateLabel.text = StrDictionary.GetDictionaryString("#{100114}", new object[0]);
		}
	}

	// Token: 0x04003AAB RID: 15019
	private TutorialManager.OnClickTutorialBtn mOnClickTutorialBtn;

	// Token: 0x04003AAC RID: 15020
	public UILabel HpValueLable;

	// Token: 0x04003AAD RID: 15021
	public UILabel NameLabel;

	// Token: 0x04003AAE RID: 15022
	public UISprite HpLinePic;

	// Token: 0x04003AAF RID: 15023
	public UISprite HpBottomPic;

	// Token: 0x04003AB0 RID: 15024
	public UISprite PlayerIcon;

	// Token: 0x04003AB1 RID: 15025
	public UILabel LvLable;

	// Token: 0x04003AB2 RID: 15026
	public GameObject PvpRoot;

	// Token: 0x04003AB3 RID: 15027
	public UISprite PvpBtnPic;

	// Token: 0x04003AB4 RID: 15028
	public UISprite PveBtnPic;

	// Token: 0x04003AB5 RID: 15029
	public UILabel PvpLabel;

	// Token: 0x04003AB6 RID: 15030
	public UILabel PveLabel;

	// Token: 0x04003AB7 RID: 15031
	public UILabel CurStateLabel;

	// Token: 0x04003AB8 RID: 15032
	public UIWidget StateListObj;

	// Token: 0x04003AB9 RID: 15033
	private bool isOpenState;

	// Token: 0x04003ABA RID: 15034
	private bool isLockPVP;

	// Token: 0x04003ABB RID: 15035
	private float mSliderProgress = 1f;

	// Token: 0x04003ABC RID: 15036
	private int preLevel = -1;

	// Token: 0x04003ABD RID: 15037
	private int HPLineLength;

	// Token: 0x04003ABE RID: 15038
	private int mCurCombolValue;

	// Token: 0x04003ABF RID: 15039
	private PlayerData mPlayerData;

	// Token: 0x04003AC0 RID: 15040
	private static float LastChangeTime;

	// Token: 0x04003AC1 RID: 15041
	private float StateCDTime = 60f;

	// Token: 0x04003AC2 RID: 15042
	public UISprite CDFlag;

	// Token: 0x04003AC3 RID: 15043
	private bool Iscdflag;
}
