using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class SendServerCheckManager
{
	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001273 RID: 4723 RVA: 0x00078EC4 File Offset: 0x000770C4
	// (set) Token: 0x06001274 RID: 4724 RVA: 0x00078ECC File Offset: 0x000770CC
	public Dictionary<int, SendServerCheckManager.SendServerCheckData> CheckEventDic
	{
		get
		{
			return this.mCheckEventDic;
		}
		set
		{
			this.mCheckEventDic = value;
		}
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x00078ED8 File Offset: 0x000770D8
	public void RegisterCheckEvent(int eventId, float Interval = 0.5f)
	{
		if (!this.mCheckEventDic.ContainsKey(eventId))
		{
			this.mCheckEventDic.Add(eventId, new SendServerCheckManager.SendServerCheckData(-2.1474836E+09f, Interval));
		}
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x00078F10 File Offset: 0x00077110
	public bool CanSendToServer(int eventId, float Interval = 0.5f)
	{
		if (this.mCheckEventDic.ContainsKey(eventId))
		{
			return this.mCheckEventDic[eventId].CanSendToServer();
		}
		this.RegisterCheckEvent(eventId, Interval);
		return this.mCheckEventDic[eventId].CanSendToServer();
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x00078F5C File Offset: 0x0007715C
	public void Reset()
	{
	}

	// Token: 0x040017B7 RID: 6071
	private Dictionary<int, SendServerCheckManager.SendServerCheckData> mCheckEventDic = new Dictionary<int, SendServerCheckManager.SendServerCheckData>();

	// Token: 0x0200020F RID: 527
	public class SendServerCheckData
	{
		// Token: 0x06001278 RID: 4728 RVA: 0x00078F60 File Offset: 0x00077160
		public SendServerCheckData(float lastTime, float interval = 0.5f)
		{
			this.mLastSendTime = lastTime;
			this.mSendInterval = interval;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00078F84 File Offset: 0x00077184
		public bool CanSendToServer()
		{
			if (Time.time - this.mLastSendTime > this.mSendInterval)
			{
				this.mLastSendTime = Time.time;
				return true;
			}
			return false;
		}

		// Token: 0x040017B8 RID: 6072
		private float mLastSendTime;

		// Token: 0x040017B9 RID: 6073
		private float mSendInterval = 0.5f;
	}
}
