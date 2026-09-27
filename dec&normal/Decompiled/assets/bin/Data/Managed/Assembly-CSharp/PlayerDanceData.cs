using System;
using System.Collections.Generic;
using SprotoType;

// Token: 0x02000863 RID: 2147
public class PlayerDanceData
{
	// Token: 0x17000F34 RID: 3892
	// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000E58CC File Offset: 0x000E3ACC
	public Dictionary<string, dance_info> PlayerDanceInfoDic
	{
		get
		{
			return this.mPlayerDanceInfoDic;
		}
	}

	// Token: 0x17000F35 RID: 3893
	// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000E58D4 File Offset: 0x000E3AD4
	// (set) Token: 0x060037B3 RID: 14259 RVA: 0x000E58DC File Offset: 0x000E3ADC
	public string CurDanceId
	{
		get
		{
			return this.mCurDanceId;
		}
		set
		{
			this.mCurDanceId = value;
		}
	}

	// Token: 0x17000F36 RID: 3894
	// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000E58E8 File Offset: 0x000E3AE8
	// (set) Token: 0x060037B5 RID: 14261 RVA: 0x000E58F0 File Offset: 0x000E3AF0
	public DanceData CurDanceData
	{
		get
		{
			return this.mCurDanceData;
		}
		set
		{
			this.mCurDanceData = value;
		}
	}

	// Token: 0x17000F37 RID: 3895
	// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000E58FC File Offset: 0x000E3AFC
	public DanceData CurNormalDanceData
	{
		get
		{
			return this.mCurNormalDanceData;
		}
	}

	// Token: 0x17000F38 RID: 3896
	// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000E5904 File Offset: 0x000E3B04
	public DanceData CurSpecialDanceData
	{
		get
		{
			return this.mCurSpecialDanceData;
		}
	}

	// Token: 0x060037B8 RID: 14264 RVA: 0x000E590C File Offset: 0x000E3B0C
	public bool IsHaveDanceData()
	{
		return this.mPlayerDanceInfoDic != null;
	}

	// Token: 0x060037B9 RID: 14265 RVA: 0x000E591C File Offset: 0x000E3B1C
	public bool IsSpecialDanceDataEnable()
	{
		return this.mPlayerDanceInfoDic[this.mCurSpecialDanceData.ID].enable && this.mPlayerDanceInfoDic[this.mCurSpecialDanceData.ID].endTime > SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData.GetCurServerTime();
	}

	// Token: 0x060037BA RID: 14266 RVA: 0x000E5978 File Offset: 0x000E3B78
	public void SyncPlayerDanceInfo(ret_request_dance_info.request request)
	{
		if (request.HasCurUse)
		{
			this.mCurDanceId = request.curUse;
			this.mCurDanceData = DataManager.GetDanceDataById(this.mCurDanceId);
		}
		if (request.HasDance_info)
		{
			this.mPlayerDanceInfoDic = request.dance_info;
			List<dance_info> list = new List<dance_info>(this.mPlayerDanceInfoDic.Values);
			for (int i = 0; i < list.Count; i++)
			{
				DanceData danceDataById = DataManager.GetDanceDataById(list[i].ID);
				if (danceDataById.Price == 0)
				{
					this.mCurNormalDanceData = danceDataById;
				}
				else
				{
					this.mCurSpecialDanceData = danceDataById;
				}
			}
		}
	}

	// Token: 0x060037BB RID: 14267 RVA: 0x000E5A1C File Offset: 0x000E3C1C
	public void SyncPlayerDanceInfo(start_participate_dance.request request)
	{
		if (request.HasCurUse)
		{
			this.mCurDanceId = request.curUse;
		}
		if (request.HasDance_info)
		{
			this.mPlayerDanceInfoDic = request.dance_info;
			List<dance_info> list = new List<dance_info>(this.mPlayerDanceInfoDic.Values);
			for (int i = 0; i < list.Count; i++)
			{
				DanceData danceDataById = DataManager.GetDanceDataById(list[i].ID);
				if (danceDataById.Price == 0)
				{
					this.mCurNormalDanceData = danceDataById;
				}
				else
				{
					this.mCurSpecialDanceData = danceDataById;
				}
			}
		}
	}

	// Token: 0x060037BC RID: 14268 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
	public void Reset()
	{
		this.mCurDanceId = string.Empty;
		this.mCurDanceData = null;
		this.mPlayerDanceInfoDic = null;
	}

	// Token: 0x040024D2 RID: 9426
	private Dictionary<string, dance_info> mPlayerDanceInfoDic;

	// Token: 0x040024D3 RID: 9427
	private string mCurDanceId;

	// Token: 0x040024D4 RID: 9428
	private DanceData mCurDanceData;

	// Token: 0x040024D5 RID: 9429
	private DanceData mCurNormalDanceData;

	// Token: 0x040024D6 RID: 9430
	private DanceData mCurSpecialDanceData;
}
