using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A50 RID: 2640
public class LevelUpUIRoot : SingletonUnity<LevelUpUIRoot>
{
	// Token: 0x06004CF2 RID: 19698 RVA: 0x001A2310 File Offset: 0x001A0510
	protected override void Awake()
	{
		base.Awake();
		this.array = base.GetComponentsInChildren<UITweener>();
	}

	// Token: 0x06004CF3 RID: 19699 RVA: 0x001A2324 File Offset: 0x001A0524
	private void Update()
	{
		if (this.startTime > 0f && this.startTime <= Time.time)
		{
			SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.LevelUpUIRoot);
			this.startTime = -1f;
			this.checkTime = -1f;
		}
		this.UpdateCheckShow();
	}

	// Token: 0x06004CF4 RID: 19700 RVA: 0x001A237C File Offset: 0x001A057C
	public void CheckShowLevelPack()
	{
		int num = this.mNextlevel;
		List<LevelPackageData> LevelDataList = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData.welfareData.GetUnGetLevelPack();
		if (LevelDataList != null && LevelDataList.Count > 0)
		{
			SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.LevelRewardGetRoot, delegate
			{
				SingletonUnity<LevelRewardGetLogic>.Instance.EnableReset();
				SingletonUnity<LevelRewardGetLogic>.Instance.ShowRewardList(LevelDataList);
			}, null);
		}
	}

	// Token: 0x06004CF5 RID: 19701 RVA: 0x001A23E8 File Offset: 0x001A05E8
	private bool CanShowLevelUp()
	{
		return (!SingletonUnity<DialogMissionUIRoot>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<DialogMissionUIRoot>.Instance.gameObject)) && (!SingletonUnity<MissionPassShowRootLogic>.Exists || !UnityVersionUtil.IsActive(SingletonUnity<MissionPassShowRootLogic>.Instance.gameObject));
	}

	// Token: 0x06004CF6 RID: 19702 RVA: 0x001A2434 File Offset: 0x001A0634
	private void UpdateCheckShow()
	{
		if (this.checkTime < 0f)
		{
			return;
		}
		this.checkTime -= Time.deltaTime;
		if (this.checkTime <= 0f)
		{
			if (!this.CanShowLevelUp())
			{
				this.checkTime = 0.1f;
			}
			else
			{
				this.checkTime = -1f;
				this.PlayAnimal(this.mCurLevel, this.mNextlevel);
			}
		}
	}

	// Token: 0x06004CF7 RID: 19703 RVA: 0x001A24AC File Offset: 0x001A06AC
	private void PlayAnimal(int curLevel, int nextLevel)
	{
		NGUITools.SetActive(this.childObj, true);
		this.particleSystem.Clear(false);
		this.particleSystem.Play();
		for (int i = 0; i < this.array.Length; i++)
		{
			this.array[i].ResetToBeginning();
			this.array[i].PlayForward();
		}
		BaseLvData levelDataByLevel = DataManager.GetLevelDataByLevel(curLevel);
		BaseLvData levelDataByLevel2 = DataManager.GetLevelDataByLevel(nextLevel);
		PlayerData playerData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerData;
		this.startTime = Time.time + 4f;
		this.nextLabel.text = string.Format("Lv.{0}", nextLevel);
		for (int j = 0; j < this.array.Length; j++)
		{
			this.array[j].ResetToBeginning();
		}
		for (int k = 0; k < this.array.Length; k++)
		{
			this.array[k].Play();
		}
		if (playerData.Profession == PROFESSION_TYPE.XD)
		{
			this.curLabels[0].text = levelDataByLevel.ATKXD.ToString();
			this.curLabels[1].text = levelDataByLevel.HPXD.ToString();
			this.curLabels[2].text = levelDataByLevel.DEFXD.ToString();
			this.curLabels[3].text = levelDataByLevel.HITXD.ToString();
			this.curLabels[4].text = levelDataByLevel.DGEXD.ToString();
			this.curLabels[5].text = levelDataByLevel.CRIXD.ToString();
			this.curLabels[6].text = levelDataByLevel.RESXD.ToString();
			this.nextLabels[0].text = levelDataByLevel2.ATKXD.ToString();
			this.nextLabels[1].text = levelDataByLevel2.HPXD.ToString();
			this.nextLabels[2].text = levelDataByLevel2.DEFXD.ToString();
			this.nextLabels[3].text = levelDataByLevel2.HITXD.ToString();
			this.nextLabels[4].text = levelDataByLevel2.DGEXD.ToString();
			this.nextLabels[5].text = levelDataByLevel2.CRIXD.ToString();
			this.nextLabels[6].text = levelDataByLevel2.RESXD.ToString();
		}
		else if (playerData.Profession == PROFESSION_TYPE.QJ)
		{
			this.curLabels[0].text = levelDataByLevel.ATKQJ.ToString();
			this.curLabels[1].text = levelDataByLevel.HPQJ.ToString();
			this.curLabels[2].text = levelDataByLevel.DEFQJ.ToString();
			this.curLabels[3].text = levelDataByLevel.HITQJ.ToString();
			this.curLabels[4].text = levelDataByLevel.DGEQJ.ToString();
			this.curLabels[5].text = levelDataByLevel.CRIQJ.ToString();
			this.curLabels[6].text = levelDataByLevel.RESQJ.ToString();
			this.nextLabels[0].text = levelDataByLevel2.ATKQJ.ToString();
			this.nextLabels[1].text = levelDataByLevel2.HPQJ.ToString();
			this.nextLabels[2].text = levelDataByLevel2.DEFQJ.ToString();
			this.nextLabels[3].text = levelDataByLevel2.HITQJ.ToString();
			this.nextLabels[4].text = levelDataByLevel2.DGEQJ.ToString();
			this.nextLabels[5].text = levelDataByLevel2.CRIQJ.ToString();
			this.nextLabels[6].text = levelDataByLevel2.RESQJ.ToString();
		}
		else if (playerData.Profession == PROFESSION_TYPE.NQS)
		{
			this.curLabels[0].text = levelDataByLevel.ATKNQ.ToString();
			this.curLabels[1].text = levelDataByLevel.HPNQ.ToString();
			this.curLabels[2].text = levelDataByLevel.DEFNQ.ToString();
			this.curLabels[3].text = levelDataByLevel.HITNQ.ToString();
			this.curLabels[4].text = levelDataByLevel.DGENQ.ToString();
			this.curLabels[5].text = levelDataByLevel.CRINQ.ToString();
			this.curLabels[6].text = levelDataByLevel.RESNQ.ToString();
			this.nextLabels[0].text = levelDataByLevel2.ATKNQ.ToString();
			this.nextLabels[1].text = levelDataByLevel2.HPNQ.ToString();
			this.nextLabels[2].text = levelDataByLevel2.DEFNQ.ToString();
			this.nextLabels[3].text = levelDataByLevel2.HITNQ.ToString();
			this.nextLabels[4].text = levelDataByLevel2.DGENQ.ToString();
			this.nextLabels[5].text = levelDataByLevel2.CRINQ.ToString();
			this.nextLabels[6].text = levelDataByLevel2.RESNQ.ToString();
		}
		this.mCurLevel = nextLevel;
		if (this.mNextComb != this.mCurComb)
		{
			FightingValUpgradeRootLogic.ShowFinghtingValUpgradeRoot((long)this.mCurComb, (long)this.mNextComb);
		}
	}

	// Token: 0x06004CF8 RID: 19704 RVA: 0x001A29F4 File Offset: 0x001A0BF4
	public void Reset(int curLevel, int nextLevel, int curValue, int nextValue)
	{
		this.mNextlevel = nextLevel;
		if (curLevel < this.mCurLevel)
		{
			this.mCurLevel = curLevel;
		}
		this.mCurComb = curValue;
		this.mNextComb = nextValue;
		if (this.CanShowLevelUp())
		{
			this.PlayAnimal(this.mCurLevel, this.mNextlevel);
		}
		else
		{
			NGUITools.SetActive(this.childObj, false);
			this.checkTime = 0.1f;
		}
	}

	// Token: 0x04003A86 RID: 14982
	public UILabel[] curLabels;

	// Token: 0x04003A87 RID: 14983
	public UILabel[] nextLabels;

	// Token: 0x04003A88 RID: 14984
	private float startTime = -1f;

	// Token: 0x04003A89 RID: 14985
	public UILabel nextLabel;

	// Token: 0x04003A8A RID: 14986
	private UITweener[] array;

	// Token: 0x04003A8B RID: 14987
	public GameObject childObj;

	// Token: 0x04003A8C RID: 14988
	private int mNextlevel;

	// Token: 0x04003A8D RID: 14989
	private int mCurLevel = int.MaxValue;

	// Token: 0x04003A8E RID: 14990
	private int mNextComb;

	// Token: 0x04003A8F RID: 14991
	private int mCurComb = int.MaxValue;

	// Token: 0x04003A90 RID: 14992
	private float checkTime = -1f;

	// Token: 0x04003A91 RID: 14993
	public ParticleSystem particleSystem;
}
