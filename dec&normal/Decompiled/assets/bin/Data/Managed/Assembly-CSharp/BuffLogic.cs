using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000837 RID: 2103
public class BuffLogic : MonoBehaviour
{
	// Token: 0x060035B1 RID: 13745 RVA: 0x000DB7D0 File Offset: 0x000D99D0
	public void AddBuffInfoData(string buffID, float delayTime, float duration, ObjCharacter sender)
	{
		if (!this.owner.IsDie)
		{
			this.mBuffInfoDataList.Add(new PlayBuffInfoData(buffID, delayTime, duration, sender));
		}
	}

	// Token: 0x060035B2 RID: 13746 RVA: 0x000DB7F8 File Offset: 0x000D99F8
	private void Start()
	{
	}

	// Token: 0x060035B3 RID: 13747 RVA: 0x000DB7FC File Offset: 0x000D99FC
	public void Init(ObjCharacter ownerObj)
	{
		this.owner = ownerObj;
	}

	// Token: 0x060035B4 RID: 13748 RVA: 0x000DB808 File Offset: 0x000D9A08
	public void ClearBuff()
	{
		if (this.CurBuffInfoDataList.Count > 0)
		{
			for (int i = this.CurBuffInfoDataList.Count - 1; i >= 0; i--)
			{
				this.RemoveCurBuffList(this.CurBuffInfoDataList[i]);
			}
		}
		this.CurBuffInfoDataList.Clear();
		this.mBuffInfoDataList.Clear();
	}

	// Token: 0x060035B5 RID: 13749 RVA: 0x000DB86C File Offset: 0x000D9A6C
	public void RegisterOnStun(BuffLogic.BuffDelegate func)
	{
		this.onStun = (BuffLogic.BuffDelegate)Delegate.Combine(this.onStun, func);
	}

	// Token: 0x060035B6 RID: 13750 RVA: 0x000DB888 File Offset: 0x000D9A88
	public void DeRegisterOnStun(BuffLogic.BuffDelegate func)
	{
		if (this.onStun != null)
		{
			this.onStun = (BuffLogic.BuffDelegate)Delegate.Remove(this.onStun, func);
		}
	}

	// Token: 0x060035B7 RID: 13751 RVA: 0x000DB8B8 File Offset: 0x000D9AB8
	public void RegisterOnStunDone(BuffLogic.BuffDelegate func)
	{
		this.onStunDone = (BuffLogic.BuffDelegate)Delegate.Combine(this.onStunDone, func);
	}

	// Token: 0x060035B8 RID: 13752 RVA: 0x000DB8D4 File Offset: 0x000D9AD4
	public void DeRegisterOnStunDone(BuffLogic.BuffDelegate func)
	{
		if (this.onStunDone != null)
		{
			this.onStunDone = (BuffLogic.BuffDelegate)Delegate.Remove(this.onStunDone, func);
		}
	}

	// Token: 0x060035B9 RID: 13753 RVA: 0x000DB904 File Offset: 0x000D9B04
	public void RegisterOnKnockDown(BuffLogic.BuffSenderDelegate func)
	{
		this.onKnockDown = (BuffLogic.BuffSenderDelegate)Delegate.Combine(this.onKnockDown, func);
	}

	// Token: 0x060035BA RID: 13754 RVA: 0x000DB920 File Offset: 0x000D9B20
	public void DeRegisterOnKnockDown(BuffLogic.BuffSenderDelegate func)
	{
		if (this.onKnockDown != null)
		{
			this.onKnockDown = (BuffLogic.BuffSenderDelegate)Delegate.Remove(this.onKnockDown, func);
		}
	}

	// Token: 0x060035BB RID: 13755 RVA: 0x000DB950 File Offset: 0x000D9B50
	public void RegisterOnKnockDownDone(BuffLogic.BuffDelegate func)
	{
		this.onKnockDownDone = (BuffLogic.BuffDelegate)Delegate.Combine(this.onKnockDownDone, func);
	}

	// Token: 0x060035BC RID: 13756 RVA: 0x000DB96C File Offset: 0x000D9B6C
	public void DeRegisterOnKnockDownDone(BuffLogic.BuffDelegate func)
	{
		if (this.onKnockDownDone != null)
		{
			this.onKnockDownDone = (BuffLogic.BuffDelegate)Delegate.Remove(this.onKnockDownDone, func);
		}
	}

	// Token: 0x060035BD RID: 13757 RVA: 0x000DB99C File Offset: 0x000D9B9C
	public void RegisterOnSleep(BuffLogic.BuffDelegate func)
	{
		this.onSleep = (BuffLogic.BuffDelegate)Delegate.Combine(this.onSleep, func);
	}

	// Token: 0x060035BE RID: 13758 RVA: 0x000DB9B8 File Offset: 0x000D9BB8
	public void DeRegisterOnSleep(BuffLogic.BuffDelegate func)
	{
		if (this.onSleep != null)
		{
			this.onSleep = (BuffLogic.BuffDelegate)Delegate.Remove(this.onSleep, func);
		}
	}

	// Token: 0x060035BF RID: 13759 RVA: 0x000DB9E8 File Offset: 0x000D9BE8
	public void RegisterOnSleepDone(BuffLogic.BuffDelegate func)
	{
		this.onSleepDone = (BuffLogic.BuffDelegate)Delegate.Combine(this.onSleepDone, func);
	}

	// Token: 0x060035C0 RID: 13760 RVA: 0x000DBA04 File Offset: 0x000D9C04
	public void DeRegisterOnSleepDone(BuffLogic.BuffDelegate func)
	{
		if (this.onSleepDone != null)
		{
			this.onSleepDone = (BuffLogic.BuffDelegate)Delegate.Remove(this.onSleepDone, func);
		}
	}

	// Token: 0x060035C1 RID: 13761 RVA: 0x000DBA34 File Offset: 0x000D9C34
	public void RegisterOnChangeAttr(BuffLogic.BuffDelegate func)
	{
		this.onChangeAttr = (BuffLogic.BuffDelegate)Delegate.Combine(this.onChangeAttr, func);
	}

	// Token: 0x060035C2 RID: 13762 RVA: 0x000DBA50 File Offset: 0x000D9C50
	public void DeRegisterOnChangeAttr(BuffLogic.BuffDelegate func)
	{
		if (this.onChangeAttr != null)
		{
			this.onChangeAttr = (BuffLogic.BuffDelegate)Delegate.Remove(this.onChangeAttr, func);
		}
	}

	// Token: 0x060035C3 RID: 13763 RVA: 0x000DBA80 File Offset: 0x000D9C80
	public void RegisterOnChangeAttrDone(BuffLogic.BuffDelegate func)
	{
		this.onChangeAttrDone = (BuffLogic.BuffDelegate)Delegate.Combine(this.onChangeAttrDone, func);
	}

	// Token: 0x060035C4 RID: 13764 RVA: 0x000DBA9C File Offset: 0x000D9C9C
	public void DeRegisterOnChangeAttrDone(BuffLogic.BuffDelegate func)
	{
		if (this.onChangeAttrDone != null)
		{
			this.onChangeAttrDone = (BuffLogic.BuffDelegate)Delegate.Remove(this.onChangeAttrDone, func);
		}
	}

	// Token: 0x060035C5 RID: 13765 RVA: 0x000DBACC File Offset: 0x000D9CCC
	private void Update()
	{
	}

	// Token: 0x060035C6 RID: 13766 RVA: 0x000DBAD0 File Offset: 0x000D9CD0
	public void UpdateBuffLogic()
	{
		if (this.mBuffInfoDataList.Count > 0)
		{
			for (int i = this.mBuffInfoDataList.Count - 1; i >= 0; i--)
			{
				this.mBuffInfoDataList[i].DelayTime -= Time.deltaTime;
				if (this.mBuffInfoDataList[i].DelayTime <= 0f)
				{
					this.ResetBuff(this.mBuffInfoDataList[i]);
					this.mBuffInfoDataList.RemoveAt(i);
				}
			}
		}
		if (this.CurBuffInfoDataList.Count > 0)
		{
			for (int j = this.CurBuffInfoDataList.Count - 1; j >= 0; j--)
			{
				if (this.CurBuffInfoDataList[j].Duration != -1f)
				{
					this.CurBuffInfoDataList[j].Duration -= Time.deltaTime;
					if (this.CurBuffInfoDataList[j].Duration <= 0f)
					{
						this.RemoveBuff(this.CurBuffInfoDataList[j]);
					}
				}
			}
		}
	}

	// Token: 0x060035C7 RID: 13767 RVA: 0x000DBBFC File Offset: 0x000D9DFC
	private PlayBuffInfoData GetCurTypeBuff(BUFF_TYPE type)
	{
		for (int i = 0; i < this.CurBuffInfoDataList.Count; i++)
		{
			if (this.CurBuffInfoDataList[i].BuffInfoData.BufType == type)
			{
				return this.CurBuffInfoDataList[i];
			}
		}
		return null;
	}

	// Token: 0x060035C8 RID: 13768 RVA: 0x000DBC50 File Offset: 0x000D9E50
	private PlayBuffInfoData GetCurBuffById(string id)
	{
		for (int i = 0; i < this.CurBuffInfoDataList.Count; i++)
		{
			if (this.CurBuffInfoDataList[i].BuffInfoData.ID.Equals(id))
			{
				return this.CurBuffInfoDataList[i];
			}
		}
		return null;
	}

	// Token: 0x060035C9 RID: 13769 RVA: 0x000DBCA8 File Offset: 0x000D9EA8
	private void ResetBuff(PlayBuffInfoData buff)
	{
		if (buff.BuffInfoData.BufType == BUFF_TYPE.STUN)
		{
			if (this.GetCurTypeBuff(BUFF_TYPE.KNOCK_DOWN) != null)
			{
				return;
			}
			PlayBuffInfoData curTypeBuff = this.GetCurTypeBuff(BUFF_TYPE.STUN);
			if (curTypeBuff != null)
			{
				curTypeBuff.Duration = Mathf.Max(buff.Duration, curTypeBuff.Duration);
				return;
			}
			this.CurBuffInfoDataList.Add(buff);
		}
		else if (buff.BuffInfoData.BufType == BUFF_TYPE.KNOCK_DOWN)
		{
			PlayBuffInfoData curTypeBuff2 = this.GetCurTypeBuff(BUFF_TYPE.STUN);
			if (curTypeBuff2 != null)
			{
				this.RemoveBuff(curTypeBuff2);
			}
			if (this.GetCurTypeBuff(BUFF_TYPE.KNOCK_DOWN) != null)
			{
				return;
			}
			this.CurBuffInfoDataList.Add(buff);
		}
		else if (buff.BuffInfoData.BufType == BUFF_TYPE.CHANGE_ATTR)
		{
			PlayBuffInfoData curBuffById = this.GetCurBuffById(buff.BuffInfoData.ID);
			if (curBuffById != null)
			{
				curBuffById.Duration = buff.Duration;
				return;
			}
			this.CurBuffInfoDataList.Add(buff);
		}
		else
		{
			this.CurBuffInfoDataList.Add(buff);
		}
		switch (buff.BuffInfoData.BufType)
		{
		case BUFF_TYPE.CHANGE_ATTR:
			this.ResetAttrBuff(buff.BuffInfoData);
			break;
		case BUFF_TYPE.STUN:
			this.ResetStunBuff(buff.BuffInfoData);
			break;
		case BUFF_TYPE.SLEEP:
			this.ResetSleepBuff(buff.BuffInfoData);
			break;
		case BUFF_TYPE.KNOCK_DOWN:
			this.ResetKnockDownBuff(buff.BuffInfoData, buff.Sender);
			break;
		case BUFF_TYPE.INVINCIBLE:
			this.ResetInvincibleBuff();
			break;
		default:
			MonoBehaviour.print("buff Type Error :: " + buff.BuffInfoData.BufType);
			break;
		}
		if (!string.IsNullOrEmpty(buff.BuffInfoData.Effect))
		{
			this.owner.AddPlayeBufEffInfoData(buff.BuffInfoData.Effect, 0f, float.MaxValue, this.owner.Position);
			buff.FxEffectId = buff.BuffInfoData.Effect;
		}
	}

	// Token: 0x060035CA RID: 13770 RVA: 0x000DBEAC File Offset: 0x000DA0AC
	private void ResetAttrBuff(BuffInfoData buffInfoData)
	{
		if (this.onChangeAttr != null)
		{
			this.onChangeAttr(buffInfoData);
		}
	}

	// Token: 0x060035CB RID: 13771 RVA: 0x000DBEC8 File Offset: 0x000DA0C8
	private void ResetStunBuff(BuffInfoData buffInfoData)
	{
		if (this.onStun != null)
		{
			this.onStun(buffInfoData);
		}
	}

	// Token: 0x060035CC RID: 13772 RVA: 0x000DBEE4 File Offset: 0x000DA0E4
	private void ResetKnockDownBuff(BuffInfoData buffInfoData, ObjCharacter sender)
	{
		if (this.onKnockDown != null)
		{
			this.onKnockDown(buffInfoData, sender);
		}
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x000DBF00 File Offset: 0x000DA100
	private void ResetSleepBuff(BuffInfoData buffInfoData)
	{
		if (this.onSleep != null)
		{
			this.onSleep(buffInfoData);
		}
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x000DBF1C File Offset: 0x000DA11C
	private void ResetInvincibleBuff()
	{
		this.owner.InvincibleFlag = true;
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x000DBF2C File Offset: 0x000DA12C
	private void RemoveBuff(PlayBuffInfoData buff)
	{
		switch (buff.BuffInfoData.BufType)
		{
		case BUFF_TYPE.CHANGE_ATTR:
			this.RemoveAttrBuff(buff.BuffInfoData);
			break;
		case BUFF_TYPE.STUN:
			this.RemoveStunBuff(buff.BuffInfoData);
			break;
		case BUFF_TYPE.SLEEP:
			this.RemoveSleepBuff(buff.BuffInfoData);
			break;
		case BUFF_TYPE.KNOCK_DOWN:
			this.RemoveKnockDown(buff.BuffInfoData);
			break;
		case BUFF_TYPE.INVINCIBLE:
			this.RemoveInvincibleBuff();
			break;
		default:
			MonoBehaviour.print("buff Type Error");
			break;
		}
		this.RemoveCurBuffList(buff);
	}

	// Token: 0x060035D0 RID: 13776 RVA: 0x000DBFCC File Offset: 0x000DA1CC
	private void RemoveCurBuffList(PlayBuffInfoData buff)
	{
		if (this.CurBuffInfoDataList.Contains(buff))
		{
			this.owner.EffectLogic.BreakEffect(buff.FxEffectId);
			this.CurBuffInfoDataList.Remove(buff);
		}
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x000DC010 File Offset: 0x000DA210
	private void RemoveAttrBuff(BuffInfoData buffInfoData)
	{
		if (this.onChangeAttrDone != null)
		{
			this.onChangeAttrDone(buffInfoData);
		}
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x000DC02C File Offset: 0x000DA22C
	private void RemoveStunBuff(BuffInfoData buffInfoData)
	{
		if (this.onStunDone != null)
		{
			this.onStunDone(buffInfoData);
		}
	}

	// Token: 0x060035D3 RID: 13779 RVA: 0x000DC048 File Offset: 0x000DA248
	private void RemoveKnockDown(BuffInfoData buffInfoData)
	{
		if (this.onKnockDownDone != null)
		{
			this.onKnockDownDone(buffInfoData);
		}
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x000DC064 File Offset: 0x000DA264
	private void RemoveSleepBuff(BuffInfoData buffInfoData)
	{
		if (this.onSleepDone != null)
		{
			this.onSleepDone(buffInfoData);
		}
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x000DC080 File Offset: 0x000DA280
	private void RemoveInvincibleBuff()
	{
		this.owner.InvincibleFlag = false;
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x000DC090 File Offset: 0x000DA290
	public void BreakBuff(BuffInfoData buffInfo)
	{
		for (int i = 0; i < this.CurBuffInfoDataList.Count; i++)
		{
			if (this.CurBuffInfoDataList[i].BuffInfoData.ID == buffInfo.ID)
			{
				this.RemoveBuff(this.CurBuffInfoDataList[i]);
			}
		}
	}

	// Token: 0x0400231C RID: 8988
	public BuffLogic.BuffDelegate onStun;

	// Token: 0x0400231D RID: 8989
	public BuffLogic.BuffDelegate onStunDone;

	// Token: 0x0400231E RID: 8990
	public BuffLogic.BuffSenderDelegate onKnockDown;

	// Token: 0x0400231F RID: 8991
	public BuffLogic.BuffDelegate onKnockDownDone;

	// Token: 0x04002320 RID: 8992
	public BuffLogic.BuffDelegate onSleep;

	// Token: 0x04002321 RID: 8993
	public BuffLogic.BuffDelegate onSleepDone;

	// Token: 0x04002322 RID: 8994
	public BuffLogic.BuffDelegate onChangeAttr;

	// Token: 0x04002323 RID: 8995
	public BuffLogic.BuffDelegate onChangeAttrDone;

	// Token: 0x04002324 RID: 8996
	private ObjCharacter owner;

	// Token: 0x04002325 RID: 8997
	private List<PlayBuffInfoData> mBuffInfoDataList = new List<PlayBuffInfoData>();

	// Token: 0x04002326 RID: 8998
	public List<PlayBuffInfoData> CurBuffInfoDataList = new List<PlayBuffInfoData>();

	// Token: 0x02000AE0 RID: 2784
	// (Invoke) Token: 0x06005009 RID: 20489
	public delegate void BuffDelegate(BuffInfoData bufInfoData);

	// Token: 0x02000AE1 RID: 2785
	// (Invoke) Token: 0x0600500D RID: 20493
	public delegate void BuffSenderDelegate(BuffInfoData bufInfoData, ObjCharacter sender);
}
