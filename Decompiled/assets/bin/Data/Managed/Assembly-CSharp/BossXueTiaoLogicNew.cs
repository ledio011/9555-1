using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008CF RID: 2255
public class BossXueTiaoLogicNew : SingletonUnity<BossXueTiaoLogicNew>
{
	// Token: 0x06003CB9 RID: 15545 RVA: 0x0010B710 File Offset: 0x00109910
	public void RegisterBoss(ObjNPC boss)
	{
		if (!this.BossList.Contains(boss))
		{
			this.BossList.Add(boss);
		}
	}

	// Token: 0x06003CBA RID: 15546 RVA: 0x0010B730 File Offset: 0x00109930
	public void RemoveBoss(ObjNPC boss)
	{
		if (this.BossList.Contains(boss))
		{
			this.BossList.Remove(boss);
		}
	}

	// Token: 0x06003CBB RID: 15547 RVA: 0x0010B750 File Offset: 0x00109950
	public void UpdateBossHp(ObjNPC curBoss, CharacterAttributeData attr)
	{
		if (this.mMainPlayer == null)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
		}
		if (Time.time - this.lastCheckTime > 1f)
		{
			this.lastCheckTime = Time.time;
			this.minBoss = null;
			this.minDis = float.MaxValue;
			for (int i = 0; i < this.BossList.Count; i++)
			{
				float num = Vector3.Distance(this.mMainPlayer.Position, this.BossList[i].Position);
				if (num < this.minDis)
				{
					this.minBoss = this.BossList[i];
					this.minDis = num;
				}
			}
		}
		if (this.minBoss != null && this.minBoss.ServerId == curBoss.ServerId)
		{
			if (SingletonUnity<BossXueTiaoLogicNew>.Exists)
			{
				if (this.minDis < this.mMinShowDisBossUI && !UnityVersionUtil.IsActive(SingletonUnity<BossXueTiaoLogicNew>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BossXueTiaoUI, delegate
					{
						SingletonUnity<BossXueTiaoLogicNew>.Instance.resetHPinfo(attr);
					}, null);
					SingletonUnity<UIManager>.Instance.CheckFunctionTop(true);
				}
				else if (this.minDis > this.mMaxShowDisBossUI && UnityVersionUtil.IsActive(SingletonUnity<BossXueTiaoLogicNew>.Instance.gameObject))
				{
					SingletonUnity<UIManager>.Instance.CloseUI(UIInfo.BossXueTiaoUI);
					SingletonUnity<UIManager>.Instance.CheckFunctionTop(false);
				}
			}
			else if (this.minDis < this.mMinShowDisBossUI)
			{
				SingletonUnity<UIManager>.Instance.ShowUI(UIInfo.BossXueTiaoUI, delegate
				{
					SingletonUnity<BossXueTiaoLogicNew>.Instance.resetHPinfo(attr);
				}, null);
				SingletonUnity<UIManager>.Instance.CheckFunctionTop(true);
			}
		}
	}

	// Token: 0x06003CBC RID: 15548 RVA: 0x0010B928 File Offset: 0x00109B28
	public void resetHPinfo(CharacterAttributeData AttributeData)
	{
		this.mPicWidth = this.HpBottomPic.width;
		this.Levellabel.text = string.Format("Lv.{0}", AttributeData.Level);
		this.namelabel.text = AttributeData.Name;
		this.mMaxHP = AttributeData.MaxHP;
		this.mCurHPVal = AttributeData.HP;
		this.valSpeed = (int)((float)this.picSpeed / (float)this.mPicWidth * (float)this.mMaxHP);
		this.mTargetProgress = ((float)this.mCurHPVal - 0.01f) / (float)this.mMaxHP;
		this.mTargetWidth = (int)(this.mTargetProgress * (float)this.mPicWidth);
		this.mCurWidth = this.mTargetWidth;
		this.HpLinePic.width = this.mTargetWidth;
		this.HpMiddlePic.width = this.mTargetWidth;
		this.mTargetHPVal = this.mCurHPVal;
		this.HPVal.text = this.mTargetHPVal.ToString();
	}

	// Token: 0x06003CBD RID: 15549 RVA: 0x0010BA30 File Offset: 0x00109C30
	public void ChangeHP(long curHP, ObjNPC curBoss)
	{
		if (this.minBoss != null && curBoss != null && this.minBoss.ServerId == curBoss.ServerId && curHP >= 0L)
		{
			this.mTargetProgress = ((float)curHP - 0.01f) / (float)this.mMaxHP;
			this.HpMiddlePic.width = this.mTargetWidth;
			this.mTargetWidth = (int)(this.mTargetProgress * (float)this.mPicWidth);
			this.HpLinePic.width = this.mTargetWidth;
			this.mTargetHPVal = curHP;
			if (curHP > this.mCurHPVal)
			{
				this.reduceFlag = false;
			}
			else
			{
				this.reduceFlag = true;
			}
		}
	}

	// Token: 0x06003CBE RID: 15550 RVA: 0x0010BAEC File Offset: 0x00109CEC
	private void UpdateSliderValue()
	{
		if (this.reduceFlag)
		{
			if (this.mCurWidth > this.mTargetWidth)
			{
				this.mCurWidth -= this.picSpeed;
				this.mCurHPVal -= (long)this.valSpeed;
				if (this.mCurWidth < this.mTargetWidth)
				{
					this.mCurWidth = this.mTargetWidth;
					this.mCurHPVal = this.mTargetHPVal;
				}
			}
			else
			{
				this.mCurWidth = this.mTargetWidth;
				this.mCurHPVal = this.mTargetHPVal;
			}
		}
		else if (this.mCurWidth < this.mTargetWidth)
		{
			this.mCurWidth += this.picSpeed;
			this.mCurHPVal += (long)this.valSpeed;
			if (this.mCurWidth > this.mTargetWidth)
			{
				this.mCurWidth = this.mTargetWidth;
				this.mCurHPVal = this.mTargetHPVal;
			}
		}
		else
		{
			this.mCurWidth = this.mTargetWidth;
			this.mCurHPVal = this.mTargetHPVal;
		}
		if (this.mCurWidth != this.HpMiddlePic.width)
		{
			this.HpMiddlePic.width = this.mCurWidth;
		}
		if (this.mCurHpLabelVal != this.mCurHPVal)
		{
			this.mCurHpLabelVal = this.mCurHPVal;
			this.HPVal.text = this.mCurHpLabelVal.ToString();
		}
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x0010BC60 File Offset: 0x00109E60
	private void Update()
	{
		this.UpdateSliderValue();
	}

	// Token: 0x04002804 RID: 10244
	public UILabel Levellabel;

	// Token: 0x04002805 RID: 10245
	public UILabel namelabel;

	// Token: 0x04002806 RID: 10246
	private float mTargetProgress;

	// Token: 0x04002807 RID: 10247
	private int mTargetWidth;

	// Token: 0x04002808 RID: 10248
	private int mCurWidth;

	// Token: 0x04002809 RID: 10249
	private int mPicWidth;

	// Token: 0x0400280A RID: 10250
	private long mMaxHP;

	// Token: 0x0400280B RID: 10251
	public UISprite HpLinePic;

	// Token: 0x0400280C RID: 10252
	public UISprite HpBottomPic;

	// Token: 0x0400280D RID: 10253
	public UISprite HpMiddlePic;

	// Token: 0x0400280E RID: 10254
	public UILabel HPVal;

	// Token: 0x0400280F RID: 10255
	private int picSpeed = 5;

	// Token: 0x04002810 RID: 10256
	private int valSpeed;

	// Token: 0x04002811 RID: 10257
	private long mCurHpLabelVal;

	// Token: 0x04002812 RID: 10258
	private long mCurHPVal;

	// Token: 0x04002813 RID: 10259
	private long mTargetHPVal;

	// Token: 0x04002814 RID: 10260
	private bool reduceFlag;

	// Token: 0x04002815 RID: 10261
	private List<ObjNPC> BossList = new List<ObjNPC>();

	// Token: 0x04002816 RID: 10262
	private float mMinShowDisBossUI = 8f;

	// Token: 0x04002817 RID: 10263
	private float mMaxShowDisBossUI = 9f;

	// Token: 0x04002818 RID: 10264
	private ObjMainPlayer mMainPlayer;

	// Token: 0x04002819 RID: 10265
	private float lastCheckTime;

	// Token: 0x0400281A RID: 10266
	private ObjNPC minBoss;

	// Token: 0x0400281B RID: 10267
	private float minDis;
}
