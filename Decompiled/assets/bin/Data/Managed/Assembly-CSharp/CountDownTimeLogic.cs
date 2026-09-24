using System;
using UnityEngine;

// Token: 0x020008F0 RID: 2288
public class CountDownTimeLogic : SingletonUnity<CountDownTimeLogic>
{
	// Token: 0x06003DEE RID: 15854 RVA: 0x00117F74 File Offset: 0x00116174
	protected override void Awake()
	{
		base.Awake();
		this.bkSprite.enabled = false;
		this.timeLable.text = string.Empty;
	}

	// Token: 0x06003DEF RID: 15855 RVA: 0x00117FA4 File Offset: 0x001161A4
	public void SetReamainTime(long time, long type = 0L)
	{
		this.mType = type;
		PlayerCommonData playerCommonData = SingletonDontDestoryUnity<GameManager>.Instance.PlayerCommonData;
		if (playerCommonData != null)
		{
			this.reamainTime = (float)(time - playerCommonData.GetCurServerTime());
		}
		this.bkSprite.enabled = true;
		if (this.reamainTime <= 10f)
		{
			this.timeLable.color = Color.red;
		}
		else
		{
			this.timeLable.color = Color.white;
		}
		if (this.reamainTime <= 0f)
		{
			this.Close();
		}
	}

	// Token: 0x06003DF0 RID: 15856 RVA: 0x00118030 File Offset: 0x00116230
	public void Close()
	{
		this.bkSprite.enabled = false;
		this.timeLable.text = string.Empty;
		this.reamainTime = -1f;
	}

	// Token: 0x06003DF1 RID: 15857 RVA: 0x0011805C File Offset: 0x0011625C
	public static void CloseTime()
	{
		if (SingletonUnity<CountDownTimeLogic>.Exists)
		{
			SingletonUnity<CountDownTimeLogic>.Instance.Close();
		}
	}

	// Token: 0x06003DF2 RID: 15858 RVA: 0x00118074 File Offset: 0x00116274
	private void Update()
	{
		if (this.reamainTime > 0f)
		{
			this.reamainTime -= Time.deltaTime;
			if (this.reamainTime < 0f)
			{
				this.reamainTime = 0f;
				if (this.mType > 1L)
				{
					CountDownTimeLogic.CloseTime();
					return;
				}
			}
			if (this.reamainTime <= 10f)
			{
				this.timeLable.color = Color.red;
			}
			else
			{
				this.timeLable.color = Color.white;
			}
			if (this.mType == 1L)
			{
				this.timeLable.text = string.Format("Start Time:{0}", new TimeSpan(0, 0, (int)this.reamainTime));
			}
			else
			{
				this.timeLable.text = string.Format("{0}", new TimeSpan(0, 0, (int)this.reamainTime));
			}
		}
	}

	// Token: 0x04002990 RID: 10640
	public UILabel timeLable;

	// Token: 0x04002991 RID: 10641
	public UISprite bkSprite;

	// Token: 0x04002992 RID: 10642
	private float reamainTime = -1f;

	// Token: 0x04002993 RID: 10643
	private long mType;

	// Token: 0x04002994 RID: 10644
	public Transform Offset;
}
