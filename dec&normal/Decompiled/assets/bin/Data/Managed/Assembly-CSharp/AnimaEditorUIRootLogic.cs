using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class AnimaEditorUIRootLogic : SingletonUnity<AnimaEditorUIRootLogic>
{
	// Token: 0x060007A9 RID: 1961 RVA: 0x000355B8 File Offset: 0x000337B8
	private new void Awake()
	{
		base.Awake();
		this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		SingletonUnity<MyEvent>.Instance.Register("OnNPCDie", this, "OnNPCDie");
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x000355F4 File Offset: 0x000337F4
	public void OnNPCDie(object obj)
	{
		ObjNPC objNPC = obj as ObjNPC;
		if (objNPC == this.curNPC)
		{
			this.curNPC = null;
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00035620 File Offset: 0x00033820
	public void Init()
	{
		this.mCurChaSkillData = this.mainPlayer.CharacterSkillData;
		List<string> list = new List<string>();
		for (int i = 0; i < this.mCurChaSkillData.Count; i++)
		{
			this.mCurSkillDataDic.Add(this.mCurChaSkillData[i].ID, DataManager.GetSkillDataById(this.mCurChaSkillData[i].ID));
			list.Add(this.mCurChaSkillData[i].ID);
		}
		this.uiPopList.items = list;
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x000356B8 File Offset: 0x000338B8
	public void UseSkill_1_Onclick()
	{
		this.OnChangeSkillVal();
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			this.mainPlayer.UseComboSkill();
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00035718 File Offset: 0x00033918
	public void UseSkill_2_Onclick()
	{
		this.OnChangeSkillVal();
		if (this.IsCtlPlayer.value)
		{
			if (this.mainPlayer == null)
			{
				this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			}
			if (this.mainPlayer.IsHaveWeapon())
			{
				if (!string.IsNullOrEmpty(this.curSkillID))
				{
					this.mainPlayer.UseSkill(this.curSkillID, null);
				}
			}
			else
			{
				NoticeLogic.AddNotifyData("#{200062}", true, false);
			}
		}
		else
		{
			if (this.curNPC == null)
			{
				return;
			}
			this.curNPC.UseSkill(this.curSkillID, null);
		}
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x000357C8 File Offset: 0x000339C8
	public void UseSkill_S_Onclick()
	{
		this.OnChangeSkillVal();
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer.IsHaveWeapon())
		{
			if (!string.IsNullOrEmpty(this.mCurChaSkillData[3].ID))
			{
				this.mainPlayer.UseSkill(this.mCurChaSkillData[3].ID, null);
			}
		}
		else
		{
			NoticeLogic.AddNotifyData("#{200062}", true, false);
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00035858 File Offset: 0x00033A58
	public void OnSelectSkill(GameObject labelObj)
	{
		UILabel component = labelObj.GetComponent<UILabel>();
		if (component == null)
		{
			return;
		}
		this.SelectSkill(component.text);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00035888 File Offset: 0x00033A88
	public void SelectSkill(string id)
	{
		this.SkillIdLabel.text = id;
		this.curSkillID = id;
		this.curSkillData = this.mCurSkillDataDic[this.curSkillID];
		if (this.IsCtlPlayer.value)
		{
			this.curActionData = DataManager.GetActionDataByName(this.mainPlayer.GetActionName(this.curSkillData.ActionName));
		}
		else
		{
			this.curActionData = DataManager.GetActionDataByName(this.curNPC.GetActionName(this.curSkillData.ActionName));
		}
		this.curFxEffInfoDataDic.Clear();
		this.EffIdLabel.text = string.Empty;
		if (!string.IsNullOrEmpty(this.curActionData.FxEffID))
		{
			List<FxEffInfoData> fxEffInfoDataListById = DataManager.GetFxEffInfoDataListById(this.curActionData.FxEffID);
			for (int i = 0; i < fxEffInfoDataListById.Count; i++)
			{
				this.curFxEffInfoDataDic.Add(fxEffInfoDataListById[i]);
			}
		}
		this.SetSkillData();
		this.SkillBtnPic.spriteName = this.curSkillData.Icon;
		this.curAttackedActionDataDic.Clear();
		if (!string.IsNullOrEmpty(this.curSkillData.EffId_0))
		{
			EffInfoData effInfoDataById = DataManager.GetEffInfoDataById(this.curSkillData.EffId_0);
			string name = string.Empty;
			if (this.IsCtlPlayer.value)
			{
				if (this.curNPC != null)
				{
					name = this.curNPC.GetActionName(effInfoDataById.HitAction);
				}
			}
			else
			{
				name = this.mainPlayer.GetActionName(effInfoDataById.HitAction);
			}
			ActionData actionDataByName = DataManager.GetActionDataByName(name);
			if (actionDataByName != null && !this.curAttackedActionDataDic.ContainsKey(actionDataByName.ID))
			{
				this.curAttackedActionDataDic.Add(actionDataByName.ID, actionDataByName);
			}
		}
		if (!string.IsNullOrEmpty(this.curSkillData.EffId_1))
		{
			EffInfoData effInfoDataById2 = DataManager.GetEffInfoDataById(this.curSkillData.EffId_1);
			string name2 = string.Empty;
			if (this.IsCtlPlayer.value)
			{
				if (this.curNPC != null)
				{
					name2 = this.curNPC.GetActionName(effInfoDataById2.HitAction);
				}
			}
			else
			{
				name2 = this.mainPlayer.GetActionName(effInfoDataById2.HitAction);
			}
			ActionData actionDataByName2 = DataManager.GetActionDataByName(name2);
			if (actionDataByName2 != null && !this.curAttackedActionDataDic.ContainsKey(actionDataByName2.ID))
			{
				this.curAttackedActionDataDic.Add(actionDataByName2.ID, actionDataByName2);
			}
		}
		if (!string.IsNullOrEmpty(this.curSkillData.EffId_2))
		{
			EffInfoData effInfoDataById3 = DataManager.GetEffInfoDataById(this.curSkillData.EffId_2);
			string name3 = string.Empty;
			if (this.IsCtlPlayer.value)
			{
				if (this.curNPC != null)
				{
					name3 = this.curNPC.GetActionName(effInfoDataById3.HitAction);
				}
			}
			else
			{
				name3 = this.mainPlayer.GetActionName(effInfoDataById3.HitAction);
			}
			ActionData actionDataByName3 = DataManager.GetActionDataByName(name3);
			if (actionDataByName3 != null && !this.curAttackedActionDataDic.ContainsKey(actionDataByName3.ID))
			{
				this.curAttackedActionDataDic.Add(actionDataByName3.ID, actionDataByName3);
			}
		}
		if (this.curAttackedActionDataDic.Count > 0)
		{
			List<string> items = new List<string>(this.curAttackedActionDataDic.Keys);
			this.B_ActPopList.items.Clear();
			this.B_ActPopList.items = items;
			this.B_ActIDLabel.text = this.B_ActPopList.items[0];
			this.SetAttackedAct(this.B_ActPopList.items[0]);
		}
		else
		{
			this.SetAttackedAct(string.Empty);
		}
		this.mCurCamRockDataDic.Clear();
		if (this.curSkillData.CamRockIDList != null && this.curSkillData.CamRockIDList.Length > 0)
		{
			this.CamRockIDPopList.items.Clear();
			this.CamRockIDPopList.items = new List<string>(this.curSkillData.CamRockIDList);
			this.CamRockIDLabel.text = this.CamRockIDPopList.items[0];
			for (int j = 0; j < this.curSkillData.CamRockIDList.Length; j++)
			{
				this.mCurCamRockDataDic.Add(this.curSkillData.CamRockIDList[j], DataManager.GetCamRockDataByID(this.curSkillData.CamRockIDList[j]));
			}
			this.SelectCamRockID(this.CamRockIDPopList.items[0]);
		}
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00035D24 File Offset: 0x00033F24
	public void OnSelectCamRockID(GameObject label)
	{
		UILabel component = label.GetComponent<UILabel>();
		this.SelectCamRockID(component.text);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00035D44 File Offset: 0x00033F44
	private void SelectCamRockID(string id)
	{
		if (!string.IsNullOrEmpty(id))
		{
			this.mCurCamRockData = this.mCurCamRockDataDic[id];
			this.CamRockTimeInput.value = string.Format("{0}", this.mCurCamRockData.NeedRockTime);
			this.CamRockDelayInput.value = string.Format("{0}", this.mCurCamRockData.DelayTime);
		}
		else
		{
			this.mCurCamRockData = null;
			this.CamRockTimeInput.value = string.Empty;
			this.CamRockDelayInput.value = string.Empty;
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00035DE4 File Offset: 0x00033FE4
	public void OnSelectAttackedAct(GameObject label)
	{
		UILabel component = label.GetComponent<UILabel>();
		this.SetAttackedAct(component.text);
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00035E04 File Offset: 0x00034004
	public void SetAttackedAct(string actId)
	{
		if (!string.IsNullOrEmpty(actId))
		{
			this.curAttackedActionData = this.curAttackedActionDataDic[actId];
			this.B_AnimaDurationInput.value = string.Format("{0}", this.curAttackedActionData.AnimDurationTime);
			this.B_CrossInTimeInput.value = string.Format("{0}", this.curAttackedActionData.CrossInTime);
			this.B_CrossOutTimeInput.value = string.Format("{0}", this.curAttackedActionData.CrossOutTime);
			this.curAttackedEffectDataDic.Clear();
			this.B_EffIDLabel.text = string.Empty;
			if (!string.IsNullOrEmpty(this.curAttackedActionData.FxEffID))
			{
				List<FxEffInfoData> fxEffInfoDataListById = DataManager.GetFxEffInfoDataListById(this.curAttackedActionData.FxEffID);
				for (int i = 0; i < fxEffInfoDataListById.Count; i++)
				{
					this.curAttackedEffectDataDic.Add(fxEffInfoDataListById[i].EffName, fxEffInfoDataListById[i]);
				}
			}
			this.B_EffPopList.items.Clear();
			if (this.curAttackedEffectDataDic != null && this.curAttackedEffectDataDic.Count != 0)
			{
				List<string> list = new List<string>(this.curAttackedEffectDataDic.Keys);
				this.B_EffPopList.items = list;
				this.B_EffIDLabel.text = list[0];
				this.SetAttackedFxEff(list[0]);
			}
			else
			{
				this.SetAttackedFxEff(string.Empty);
			}
		}
		else
		{
			this.curAttackedActionData = null;
			this.B_AnimaDurationInput.value = string.Empty;
			this.B_CrossInTimeInput.value = string.Empty;
			this.B_CrossOutTimeInput.value = string.Empty;
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00035FC8 File Offset: 0x000341C8
	public void OnSelectAttackedEff(GameObject label)
	{
		UILabel component = label.GetComponent<UILabel>();
		this.SetAttackedFxEff(component.text);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00035FE8 File Offset: 0x000341E8
	public void SetAttackedFxEff(string fxEffId)
	{
		if (!string.IsNullOrEmpty(fxEffId))
		{
			this.curAttackedEffectData = this.curAttackedEffectDataDic[fxEffId];
			this.B_FxEffTimeInput.value = string.Format("{0}", this.curAttackedEffectData.EffDurationTime);
			this.B_FxEffDelayInput.value = string.Format("{0}", this.curAttackedEffectData.EffDelayTime);
			this.B_FXPosX.value = string.Format("{0}", this.curAttackedEffectData.PX);
			this.B_FXPosY.value = string.Format("{0}", this.curAttackedEffectData.PY);
			this.B_FXPosZ.value = string.Format("{0}", this.curAttackedEffectData.PZ);
			this.B_FXAngleX.value = string.Format("{0}", this.curAttackedEffectData.AX);
			this.B_FXAngleY.value = string.Format("{0}", this.curAttackedEffectData.AY);
			this.B_FXAngleZ.value = string.Format("{0}", this.curAttackedEffectData.AZ);
			this.B_LinkNode.value = this.curAttackedEffectData.EffLinkNode;
		}
		else
		{
			this.curAttackedEffectData = null;
			this.B_FxEffTimeInput.value = string.Empty;
			this.B_FxEffDelayInput.value = string.Empty;
			this.B_FXPosX.value = string.Empty;
			this.B_FXPosY.value = string.Empty;
			this.B_FXPosZ.value = string.Empty;
			this.B_FXAngleX.value = string.Empty;
			this.B_FXAngleY.value = string.Empty;
			this.B_FXAngleZ.value = string.Empty;
			this.B_LinkNode.value = string.Empty;
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x000361EC File Offset: 0x000343EC
	public void SetSkillData()
	{
		if (this.curSkillData != null)
		{
			this.CDInput.value = string.Format("{0}", this.curSkillData.CD);
			this.TraceDistanceInput.value = string.Format("{0}", this.curSkillData.TraceDistance);
			this.MoveTimeInput.value = string.Format("{0}", this.curSkillData.MoveTime);
			this.MoveDisInput.value = string.Format("{0}", this.curSkillData.MoveDistance);
			this.EffTime1Input.value = string.Format("{0}", this.curSkillData.EffTime_0);
			this.EffTime2Input.value = string.Format("{0}", this.curSkillData.EffTime_1);
			this.EffTime3Input.value = string.Format("{0}", this.curSkillData.EffTime_2);
		}
		if (this.curActionData != null)
		{
			this.AnimaDurationInput.value = string.Format("{0}", this.curActionData.AnimDurationTime);
			this.CrossInTimeInput.value = string.Format("{0}", this.curActionData.CrossInTime);
			this.CrossOutTimeInput.value = string.Format("{0}", this.curActionData.CrossOutTime);
		}
		this.EffPopList.items.Clear();
		if (this.curFxEffInfoDataDic != null && this.curFxEffInfoDataDic.Count != 0)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.curFxEffInfoDataDic.Count; i++)
			{
				list.Add(i.ToString());
			}
			this.EffPopList.items = list;
			this.EffIdLabel.text = list[0];
			this.SelectFxEff(list[0]);
		}
		else
		{
			this.SelectFxEff(string.Empty);
		}
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00036418 File Offset: 0x00034618
	public void OnSelectFxEff(GameObject labelObj)
	{
		UILabel component = labelObj.GetComponent<UILabel>();
		if (component == null)
		{
			return;
		}
		this.SelectFxEff(component.text);
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00036448 File Offset: 0x00034648
	private void SelectFxEff(string id)
	{
		if (!string.IsNullOrEmpty(id))
		{
			this.curEffID = int.Parse(id);
			this.curFxEffInfoData = this.curFxEffInfoDataDic[this.curEffID];
			this.FxEffTimeInput.value = string.Format("{0}", this.curFxEffInfoData.EffDurationTime);
			this.FxEffDelayInput.value = string.Format("{0}", this.curFxEffInfoData.EffDelayTime);
			this.FxEffLinkNodeInput.value = this.curFxEffInfoData.EffLinkNode;
			this.FXPosX.value = string.Format("{0}", this.curFxEffInfoData.PX);
			this.FXPosY.value = string.Format("{0}", this.curFxEffInfoData.PY);
			this.FXPosZ.value = string.Format("{0}", this.curFxEffInfoData.PZ);
			this.FXAngleX.value = string.Format("{0}", this.curFxEffInfoData.AX);
			this.FXAngleY.value = string.Format("{0}", this.curFxEffInfoData.AY);
			this.FXAngleZ.value = string.Format("{0}", this.curFxEffInfoData.AZ);
		}
		else
		{
			this.curFxEffInfoData = null;
			this.FxEffTimeInput.value = string.Empty;
			this.FxEffDelayInput.value = string.Empty;
			this.FxEffLinkNodeInput.value = string.Empty;
			this.FXPosX.value = string.Empty;
			this.FXPosY.value = string.Empty;
			this.FXPosZ.value = string.Empty;
			this.FXAngleX.value = string.Empty;
			this.FXAngleY.value = string.Empty;
			this.FXAngleZ.value = string.Empty;
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00036660 File Offset: 0x00034860
	private void Update()
	{
		this.UpdateSKill();
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00036668 File Offset: 0x00034868
	private void UpdateSKill()
	{
		if (this.mainPlayer == null)
		{
			this.mainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (this.mainPlayer != null)
		{
			this.ComboSkillCDPic.fillAmount = this.mainPlayer.GetComboSKillTimePercent();
			this.MoveSkillCDPic.fillAmount = this.mainPlayer.GetSkillTimePercent(this.mCurChaSkillData[3].ID);
			this.NormalSkillCDPic.fillAmount = this.mainPlayer.GetSkillTimePercent(this.curSkillID);
		}
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00036700 File Offset: 0x00034900
	public void OnChangeSkillVal()
	{
		if (this.curSkillData != null)
		{
			int cd = 0;
			if (int.TryParse(this.CDInput.value, ref cd))
			{
				this.curSkillData.CD = cd;
			}
			int traceDistance = 0;
			if (int.TryParse(this.TraceDistanceInput.value, ref traceDistance))
			{
				this.curSkillData.TraceDistance = traceDistance;
			}
			int moveTime = 0;
			if (int.TryParse(this.MoveTimeInput.value, ref moveTime))
			{
				this.curSkillData.MoveTime = moveTime;
			}
			int moveDistance = 0;
			if (int.TryParse(this.MoveDisInput.value, ref moveDistance))
			{
				this.curSkillData.MoveDistance = moveDistance;
			}
			int effTime_ = 0;
			if (int.TryParse(this.EffTime1Input.value, ref effTime_))
			{
				this.curSkillData.EffTime_0 = effTime_;
			}
			int effTime_2 = 0;
			if (int.TryParse(this.EffTime2Input.value, ref effTime_2))
			{
				this.curSkillData.EffTime_1 = effTime_2;
			}
			int effTime_3 = 0;
			if (int.TryParse(this.EffTime3Input.value, ref effTime_3))
			{
				this.curSkillData.EffTime_2 = effTime_3;
			}
			if (!this.ChangedSkillData.ContainsKey(this.curSkillData.ID))
			{
				this.ChangedSkillData.Add(this.curSkillData.ID, this.curSkillData);
			}
		}
		if (this.curActionData != null)
		{
			int animDurationTime = 0;
			if (int.TryParse(this.AnimaDurationInput.value, ref animDurationTime))
			{
				this.curActionData.AnimDurationTime = animDurationTime;
			}
			int crossInTime = 0;
			if (int.TryParse(this.CrossInTimeInput.value, ref crossInTime))
			{
				this.curActionData.CrossInTime = crossInTime;
			}
			int crossOutTime = 0;
			if (int.TryParse(this.CrossOutTimeInput.value, ref crossOutTime))
			{
				this.curActionData.CrossOutTime = crossOutTime;
			}
			if (!this.ChangedActionData.Contains(this.curActionData))
			{
				this.ChangedActionData.Add(this.curActionData);
			}
		}
		if (this.curFxEffInfoData != null)
		{
			int effDurationTime = 0;
			if (int.TryParse(this.FxEffTimeInput.value, ref effDurationTime))
			{
				this.curFxEffInfoData.EffDurationTime = effDurationTime;
			}
			int effDelayTime = 0;
			if (int.TryParse(this.FxEffDelayInput.value, ref effDelayTime))
			{
				this.curFxEffInfoData.EffDelayTime = effDelayTime;
			}
			this.curFxEffInfoData.EffLinkNode = this.FxEffLinkNodeInput.value;
			float px = 0f;
			if (float.TryParse(this.FXPosX.value, ref px))
			{
				this.curFxEffInfoData.PX = px;
			}
			float py = 0f;
			if (float.TryParse(this.FXPosY.value, ref py))
			{
				this.curFxEffInfoData.PY = py;
			}
			float pz = 0f;
			if (float.TryParse(this.FXPosZ.value, ref pz))
			{
				this.curFxEffInfoData.PZ = pz;
			}
			float ax = 0f;
			if (float.TryParse(this.FXAngleX.value, ref ax))
			{
				this.curFxEffInfoData.AX = ax;
			}
			float ay = 0f;
			if (float.TryParse(this.FXAngleY.value, ref ay))
			{
				this.curFxEffInfoData.AY = ay;
			}
			float az = 0f;
			if (float.TryParse(this.FXAngleZ.value, ref az))
			{
				this.curFxEffInfoData.AZ = az;
			}
			if (!this.ChangedFxEffInfoData.Contains(this.curFxEffInfoData))
			{
				this.ChangedFxEffInfoData.Add(this.curFxEffInfoData);
			}
		}
		if (this.curAttackedActionData != null)
		{
			int animDurationTime2 = 0;
			if (int.TryParse(this.B_AnimaDurationInput.value, ref animDurationTime2))
			{
				this.curAttackedActionData.AnimDurationTime = animDurationTime2;
			}
			int crossInTime2 = 0;
			if (int.TryParse(this.B_CrossInTimeInput.value, ref crossInTime2))
			{
				this.curAttackedActionData.CrossInTime = crossInTime2;
			}
			int crossOutTime2 = 0;
			if (int.TryParse(this.B_CrossOutTimeInput.value, ref crossOutTime2))
			{
				this.curAttackedActionData.CrossOutTime = crossOutTime2;
			}
			if (!this.ChangedActionData.Contains(this.curAttackedActionData))
			{
				this.ChangedActionData.Add(this.curAttackedActionData);
			}
		}
		if (this.curAttackedEffectData != null)
		{
			int effDurationTime2 = 0;
			if (int.TryParse(this.B_FxEffTimeInput.value, ref effDurationTime2))
			{
				this.curAttackedEffectData.EffDurationTime = effDurationTime2;
			}
			int effDelayTime2 = 0;
			if (int.TryParse(this.B_FxEffDelayInput.value, ref effDelayTime2))
			{
				this.curAttackedEffectData.EffDelayTime = effDelayTime2;
			}
			float px2 = 0f;
			if (float.TryParse(this.B_FXPosX.value, ref px2))
			{
				this.curAttackedEffectData.PX = px2;
			}
			float py2 = 0f;
			if (float.TryParse(this.B_FXPosY.value, ref py2))
			{
				this.curAttackedEffectData.PY = py2;
			}
			float pz2 = 0f;
			if (float.TryParse(this.B_FXPosZ.value, ref pz2))
			{
				this.curAttackedEffectData.PZ = pz2;
			}
			float ax2 = 0f;
			if (float.TryParse(this.B_FXAngleX.value, ref ax2))
			{
				this.curAttackedEffectData.AX = ax2;
			}
			float ay2 = 0f;
			if (float.TryParse(this.B_FXAngleY.value, ref ay2))
			{
				this.curAttackedEffectData.AY = ay2;
			}
			float az2 = 0f;
			if (float.TryParse(this.B_FXAngleZ.value, ref az2))
			{
				this.curAttackedEffectData.AZ = az2;
			}
			this.curAttackedEffectData.EffLinkNode = this.B_LinkNode.value;
			if (!this.ChangedFxEffInfoData.Contains(this.curAttackedEffectData))
			{
				this.ChangedFxEffInfoData.Add(this.curAttackedEffectData);
			}
		}
		if (this.mCurCamRockData != null)
		{
			int needRockTime = 0;
			if (int.TryParse(this.CamRockTimeInput.value, ref needRockTime))
			{
				this.mCurCamRockData.NeedRockTime = needRockTime;
			}
			int delayTime = 0;
			if (int.TryParse(this.CamRockDelayInput.value, ref delayTime))
			{
				this.mCurCamRockData.DelayTime = delayTime;
			}
			if (!this.mChangedCamRockData.Contains(this.mCurCamRockData))
			{
				this.mChangedCamRockData.Add(this.mCurCamRockData);
			}
		}
		if (this.curNPC != null)
		{
			this.curNPC.AttributeData.MaxHP = (long)int.Parse(this.NPCHPInput.value);
			this.curNPC.AttributeData.HP = this.curNPC.AttributeData.MaxHP;
		}
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00036DA8 File Offset: 0x00034FA8
	public void SaveData()
	{
		this.OnChangeSkillVal();
		string text = Application.dataPath + "/AnimationEditorData";
		string text2 = "SkillData.csv";
		string text3 = "ActionData.csv";
		string text4 = "FxEffectData.csv";
		MyFileUtil.CheckPath(text);
		MyFileUtil.DeleteFile(text + "/" + text2);
		MyFileUtil.DeleteFile(text + "/" + text3);
		MyFileUtil.DeleteFile(text + "/" + text4);
		FileStream fileStream = new FileStream(text + "/" + text2, 4);
		StreamWriter streamWriter = new StreamWriter(fileStream);
		SkillData skillData = new SkillData();
		List<SkillData> list = new List<SkillData>(this.ChangedSkillData.Values);
		for (int i = 0; i < list.Count; i++)
		{
			SkillData skillData2 = list[i];
			string text5 = string.Concat(new object[]
			{
				"*,",
				skillData2.ID,
				",",
				skillData2.Name,
				",",
				skillData2.Icon,
				",",
				skillData2.Description,
				",",
				this.IsEqual(skillData2.CastTime, skillData.CastTime),
				",",
				this.IsEqual(skillData2.CastAction, skillData.CastAction),
				",",
				skillData2.ActionName,
				",",
				skillData2.CD,
				",",
				this.IsEqual(skillData2.TraceDistance, skillData.TraceDistance),
				",",
				this.IsEqual(skillData2.ComboStart, skillData.ComboStart),
				",",
				this.IsEqual(skillData2.ComboValidTime, skillData.ComboValidTime),
				",",
				this.IsEqual(skillData2.NextSkill, skillData.NextSkill),
				",,",
				this.curSkillData.HoldTime,
				",,",
				this.IsEqual(skillData2.MoveTime, skillData.MoveTime),
				",",
				this.IsEqual(skillData2.MoveDistance, skillData.MoveDistance),
				",",
				this.IsEqual(skillData2.MoveAngle, skillData.MoveAngle),
				",",
				skillData2.EffId_0,
				",",
				this.IsEqual(skillData2.EffTime_0, skillData.EffTime_0),
				",",
				skillData2.EffId_1,
				",",
				this.IsEqual(skillData2.EffTime_1, skillData.EffTime_1),
				",",
				skillData2.EffId_2,
				",",
				this.IsEqual(skillData2.EffTime_2, skillData.EffTime_2),
				",",
				skillData2.CamRockID
			});
			streamWriter.WriteLine(text5);
		}
		streamWriter.Close();
		fileStream.Close();
		FileStream fileStream2 = new FileStream(text + "/" + text3, 4);
		StreamWriter streamWriter2 = new StreamWriter(fileStream2);
		ActionData actionData = new ActionData();
		List<ActionData> changedActionData = this.ChangedActionData;
		for (int j = 0; j < changedActionData.Count; j++)
		{
			ActionData actionData2 = changedActionData[j];
			string text6 = string.Concat(new string[]
			{
				"*,",
				actionData2.ID,
				",",
				actionData2.AnimName,
				",",
				this.IsEqual(actionData2.AnimWrapMode, actionData.AnimWrapMode),
				",",
				this.IsEqual(actionData2.AnimDurationTime, actionData.AnimDurationTime),
				",",
				this.IsEqual(actionData2.AnimCanBeBreak, actionData.AnimCanBeBreak),
				",",
				actionData2.NextActionName,
				",",
				actionData2.FxEffID,
				",",
				this.IsEqual(actionData2.CrossInTime, actionData.CrossInTime),
				",",
				this.IsEqual(actionData2.CrossOutTime, actionData.CrossOutTime)
			});
			streamWriter2.WriteLine(text6);
		}
		streamWriter2.Close();
		fileStream2.Close();
		FileStream fileStream3 = new FileStream(text + "/" + text4, 4);
		StreamWriter streamWriter3 = new StreamWriter(fileStream3);
		FxEffInfoData fxEffInfoData = new FxEffInfoData();
		List<FxEffInfoData> changedFxEffInfoData = this.ChangedFxEffInfoData;
		for (int k = 0; k < changedFxEffInfoData.Count; k++)
		{
			FxEffInfoData fxEffInfoData2 = changedFxEffInfoData[k];
			string text7 = string.Concat(new object[]
			{
				"*,",
				fxEffInfoData2.ID,
				",",
				fxEffInfoData2.EffName,
				",",
				fxEffInfoData2.EffFilePath,
				",",
				fxEffInfoData2.EffDurationTime,
				",",
				this.IsEqual(fxEffInfoData2.EffDelayTime, fxEffInfoData.EffDelayTime),
				",",
				this.IsEqual(fxEffInfoData2.EffLinkNode, fxEffInfoData.EffLinkNode),
				",",
				this.IsEqual(fxEffInfoData2.PX, fxEffInfoData.PX),
				",",
				this.IsEqual(fxEffInfoData2.PY, fxEffInfoData.PY),
				",",
				this.IsEqual(fxEffInfoData2.PZ, fxEffInfoData.PZ),
				",",
				this.IsEqual(fxEffInfoData2.AX, fxEffInfoData.AX),
				",",
				this.IsEqual(fxEffInfoData2.AY, fxEffInfoData.AY),
				",",
				this.IsEqual(fxEffInfoData2.AZ, fxEffInfoData.AZ),
				",",
				this.IsEqual(fxEffInfoData2.AutoMove, fxEffInfoData.AutoMove)
			});
			streamWriter3.WriteLine(text7);
		}
		streamWriter3.Close();
		fileStream3.Close();
		string text8 = "CamRockData.csv";
		MyFileUtil.DeleteFile(text + "/" + text8);
		FileStream fileStream4 = new FileStream(text + "/" + text8, 4);
		StreamWriter streamWriter4 = new StreamWriter(fileStream4);
		CamRockData camRockData = new CamRockData();
		List<CamRockData> list2 = this.mChangedCamRockData;
		for (int l = 0; l < list2.Count; l++)
		{
			CamRockData camRockData2 = list2[l];
			string text9 = string.Concat(new string[]
			{
				"*,",
				camRockData2.ID,
				",",
				camRockData2.CurveName,
				",",
				this.IsEqual(camRockData2.NeedRockTime, camRockData.NeedRockTime),
				",",
				this.IsEqual(camRockData2.DelayTime, camRockData.DelayTime)
			});
			streamWriter4.WriteLine(text9);
		}
		streamWriter4.Close();
		fileStream4.Close();
		Debug.Log("Write~!!!!!!");
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00037554 File Offset: 0x00035754
	private string IsEqual(string src, string target)
	{
		return (!src.Equals(target)) ? src : string.Empty;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00037570 File Offset: 0x00035770
	private string IsEqual(int src, int target)
	{
		return (src != target) ? src.ToString() : string.Empty;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0003758C File Offset: 0x0003578C
	private string IsEqual(float src, float target)
	{
		return (Mathf.Abs(src - target) >= float.Epsilon) ? src.ToString() : string.Empty;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000375B4 File Offset: 0x000357B4
	public void CreateNPC()
	{
		if (this.curNPC != null)
		{
			return;
		}
		ObjInitNpcData objInitNpcData = new ObjInitNpcData();
		objInitNpcData.mServerID = UUID.GenUUID();
		objInitNpcData.mPos = Vector3.forward * 5f;
		NpcData npcDataByID = DataManager.GetNpcDataByID(this.NPCIDInput.value);
		if (npcDataByID == null)
		{
			return;
		}
		objInitNpcData.HP = npcDataByID.Hp;
		objInitNpcData.MaxHP = npcDataByID.Hp;
		objInitNpcData.npcInfoData = npcDataByID;
		Singleton<ObjManager>.Instance.CreateNPC(objInitNpcData, delegate(ObjNPC npc)
		{
			this.curNPC = npc;
		}, null);
		this.NPCHPInput.value = npcDataByID.Hp.ToString();
		this.SelectSkill(this.curSkillID);
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0003766C File Offset: 0x0003586C
	public void OnToggleValChange()
	{
		if (this.IsCtlPlayer.value)
		{
			this.mCurChaSkillData = this.mainPlayer.CharacterSkillData;
			List<string> list = new List<string>();
			this.mCurSkillDataDic.Clear();
			for (int i = 0; i < this.mCurChaSkillData.Count; i++)
			{
				this.mCurSkillDataDic.Add(this.mCurChaSkillData[i].ID, DataManager.GetSkillDataById(this.mCurChaSkillData[i].ID));
				list.Add(this.mCurChaSkillData[i].ID);
			}
			this.uiPopList.items = list;
			this.SelectSkill(list[0]);
		}
		else
		{
			if (this.curNPC == null)
			{
				return;
			}
			List<string> list2 = new List<string>();
			for (int j = 0; j < this.curNPC.CharacterSkillData.Count; j++)
			{
				list2.Add(this.curNPC.CharacterSkillData[j].ID);
			}
			this.mCurSkillDataDic.Clear();
			for (int k = 0; k < list2.Count; k++)
			{
				this.mCurSkillDataDic.Add(list2[k], DataManager.GetSkillDataById(list2[k]));
			}
			this.uiPopList.items = list2;
			this.SelectSkill(list2[0]);
		}
	}

	// Token: 0x0400066B RID: 1643
	public UISprite SkillBtnPic;

	// Token: 0x0400066C RID: 1644
	public UIPopupList uiPopList;

	// Token: 0x0400066D RID: 1645
	public UIPopupList EffPopList;

	// Token: 0x0400066E RID: 1646
	public UILabel EffIdLabel;

	// Token: 0x0400066F RID: 1647
	public UILabel SkillIdLabel;

	// Token: 0x04000670 RID: 1648
	public UISprite ComboSkillCDPic;

	// Token: 0x04000671 RID: 1649
	public UISprite MoveSkillCDPic;

	// Token: 0x04000672 RID: 1650
	public UISprite NormalSkillCDPic;

	// Token: 0x04000673 RID: 1651
	public UIInput CDInput;

	// Token: 0x04000674 RID: 1652
	public UIInput TraceDistanceInput;

	// Token: 0x04000675 RID: 1653
	public UIInput MoveTimeInput;

	// Token: 0x04000676 RID: 1654
	public UIInput MoveDisInput;

	// Token: 0x04000677 RID: 1655
	public UIInput EffTime1Input;

	// Token: 0x04000678 RID: 1656
	public UIInput EffTime2Input;

	// Token: 0x04000679 RID: 1657
	public UIInput EffTime3Input;

	// Token: 0x0400067A RID: 1658
	public UIInput AnimaDurationInput;

	// Token: 0x0400067B RID: 1659
	public UIInput CrossInTimeInput;

	// Token: 0x0400067C RID: 1660
	public UIInput CrossOutTimeInput;

	// Token: 0x0400067D RID: 1661
	public UIInput FxEffTimeInput;

	// Token: 0x0400067E RID: 1662
	public UIInput FxEffDelayInput;

	// Token: 0x0400067F RID: 1663
	public UIInput FxEffLinkNodeInput;

	// Token: 0x04000680 RID: 1664
	public UIInput NPCIDInput;

	// Token: 0x04000681 RID: 1665
	public UIInput FXPosX;

	// Token: 0x04000682 RID: 1666
	public UIInput FXPosY;

	// Token: 0x04000683 RID: 1667
	public UIInput FXPosZ;

	// Token: 0x04000684 RID: 1668
	public UIInput FXAngleX;

	// Token: 0x04000685 RID: 1669
	public UIInput FXAngleY;

	// Token: 0x04000686 RID: 1670
	public UIInput FXAngleZ;

	// Token: 0x04000687 RID: 1671
	public UIInput NPCHPInput;

	// Token: 0x04000688 RID: 1672
	public UIToggle IsCtlPlayer;

	// Token: 0x04000689 RID: 1673
	public UIPopupList B_ActPopList;

	// Token: 0x0400068A RID: 1674
	public UIPopupList B_EffPopList;

	// Token: 0x0400068B RID: 1675
	public UILabel B_ActIDLabel;

	// Token: 0x0400068C RID: 1676
	public UILabel B_EffIDLabel;

	// Token: 0x0400068D RID: 1677
	public UIInput B_AnimaDurationInput;

	// Token: 0x0400068E RID: 1678
	public UIInput B_CrossInTimeInput;

	// Token: 0x0400068F RID: 1679
	public UIInput B_CrossOutTimeInput;

	// Token: 0x04000690 RID: 1680
	public UIInput B_FxEffTimeInput;

	// Token: 0x04000691 RID: 1681
	public UIInput B_FxEffDelayInput;

	// Token: 0x04000692 RID: 1682
	public UIInput B_FXPosX;

	// Token: 0x04000693 RID: 1683
	public UIInput B_FXPosY;

	// Token: 0x04000694 RID: 1684
	public UIInput B_FXPosZ;

	// Token: 0x04000695 RID: 1685
	public UIInput B_FXAngleX;

	// Token: 0x04000696 RID: 1686
	public UIInput B_FXAngleY;

	// Token: 0x04000697 RID: 1687
	public UIInput B_FXAngleZ;

	// Token: 0x04000698 RID: 1688
	public UIInput B_LinkNode;

	// Token: 0x04000699 RID: 1689
	public UIPopupList CamRockIDPopList;

	// Token: 0x0400069A RID: 1690
	public UILabel CamRockIDLabel;

	// Token: 0x0400069B RID: 1691
	public UIInput CamRockTimeInput;

	// Token: 0x0400069C RID: 1692
	public UIInput CamRockDelayInput;

	// Token: 0x0400069D RID: 1693
	private ObjMainPlayer mainPlayer;

	// Token: 0x0400069E RID: 1694
	private ObjNPC curNPC;

	// Token: 0x0400069F RID: 1695
	private string curSkillID = string.Empty;

	// Token: 0x040006A0 RID: 1696
	private SkillData curSkillData;

	// Token: 0x040006A1 RID: 1697
	private ActionData curActionData;

	// Token: 0x040006A2 RID: 1698
	private int curEffID = -1;

	// Token: 0x040006A3 RID: 1699
	private Dictionary<string, ActionData> curAttackedActionDataDic = new Dictionary<string, ActionData>();

	// Token: 0x040006A4 RID: 1700
	private ActionData curAttackedActionData;

	// Token: 0x040006A5 RID: 1701
	private Dictionary<string, FxEffInfoData> curAttackedEffectDataDic = new Dictionary<string, FxEffInfoData>();

	// Token: 0x040006A6 RID: 1702
	private FxEffInfoData curAttackedEffectData;

	// Token: 0x040006A7 RID: 1703
	private List<FxEffInfoData> curFxEffInfoDataDic = new List<FxEffInfoData>();

	// Token: 0x040006A8 RID: 1704
	private FxEffInfoData curFxEffInfoData;

	// Token: 0x040006A9 RID: 1705
	private List<CharacterSkillData> mCurChaSkillData = new List<CharacterSkillData>();

	// Token: 0x040006AA RID: 1706
	private Dictionary<string, SkillData> mCurSkillDataDic = new Dictionary<string, SkillData>();

	// Token: 0x040006AB RID: 1707
	private List<CamRockData> mChangedCamRockData = new List<CamRockData>();

	// Token: 0x040006AC RID: 1708
	private Dictionary<string, CamRockData> mCurCamRockDataDic = new Dictionary<string, CamRockData>();

	// Token: 0x040006AD RID: 1709
	private CamRockData mCurCamRockData;

	// Token: 0x040006AE RID: 1710
	private Dictionary<string, SkillData> ChangedSkillData = new Dictionary<string, SkillData>();

	// Token: 0x040006AF RID: 1711
	private List<ActionData> ChangedActionData = new List<ActionData>();

	// Token: 0x040006B0 RID: 1712
	private List<FxEffInfoData> ChangedFxEffInfoData = new List<FxEffInfoData>();
}
