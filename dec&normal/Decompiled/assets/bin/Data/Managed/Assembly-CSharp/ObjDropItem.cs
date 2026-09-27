using System;
using SprotoType;
using UnityEngine;

// Token: 0x0200081B RID: 2075
public class ObjDropItem : Obj
{
	// Token: 0x0600329A RID: 12954 RVA: 0x000C58AC File Offset: 0x000C3AAC
	private ObjDropItem()
	{
		this.mObjType = GameDefine.OBJ_TYPE.OBJ_DROP_ITEM;
	}

	// Token: 0x17000E5A RID: 3674
	// (get) Token: 0x0600329B RID: 12955 RVA: 0x000C5908 File Offset: 0x000C3B08
	// (set) Token: 0x0600329C RID: 12956 RVA: 0x000C5910 File Offset: 0x000C3B10
	public GameDefine.ITEM_TYPE ItemType
	{
		get
		{
			return this.mItemType;
		}
		set
		{
			this.mItemType = value;
		}
	}

	// Token: 0x17000E5B RID: 3675
	// (get) Token: 0x0600329D RID: 12957 RVA: 0x000C591C File Offset: 0x000C3B1C
	// (set) Token: 0x0600329E RID: 12958 RVA: 0x000C5924 File Offset: 0x000C3B24
	public string ItemId
	{
		get
		{
			return this.mItemId;
		}
		set
		{
			this.mItemId = value;
		}
	}

	// Token: 0x17000E5C RID: 3676
	// (get) Token: 0x0600329F RID: 12959 RVA: 0x000C5930 File Offset: 0x000C3B30
	// (set) Token: 0x060032A0 RID: 12960 RVA: 0x000C5938 File Offset: 0x000C3B38
	public int ItemNum
	{
		get
		{
			return this.mItemNum;
		}
		set
		{
			this.mItemNum = value;
		}
	}

	// Token: 0x17000E5D RID: 3677
	// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000C5944 File Offset: 0x000C3B44
	// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000C594C File Offset: 0x000C3B4C
	public long OwnerServerId
	{
		get
		{
			return this.mOwnerServerId;
		}
		set
		{
			this.mOwnerServerId = value;
		}
	}

	// Token: 0x060032A3 RID: 12963 RVA: 0x000C5958 File Offset: 0x000C3B58
	public void Init(ObjInitDropItemData initData)
	{
		if (this.mTransform == null)
		{
			this.mTransform = base.transform;
		}
		this.ItemType = initData.ItemType;
		this.ItemId = initData.item.itemId;
		this.ItemNum = (int)initData.item.itemCount;
		this.ServerId = initData.ServerID;
		this.OwnerServerId = initData.ownerServerId;
		if (this.OwnerServerId == Singleton<ObjManager>.Instance.MainPlayer.ServerId)
		{
			this.mMainPlayer = Singleton<ObjManager>.Instance.MainPlayer;
			this.mTransform.position = initData.Pos + Vector3.up * 0.3f;
			this.SetItemLabel();
			this.SetItemSprite();
			this.mDropCheckTime = Time.realtimeSinceStartup;
			this.mDropTime = Time.realtimeSinceStartup;
			this.mPickingUpFlag = false;
			vp_Timer.In(0.5f, delegate()
			{
				if (this.mTransform == null)
				{
					return;
				}
				this.mPickUpTime = Time.realtimeSinceStartup;
				this.mPickingUpFlag = true;
				this.mItemStartMovePos = this.mTransform.position;
			}, this.handle);
			return;
		}
		this.mMainPlayer = null;
	}

	// Token: 0x060032A4 RID: 12964 RVA: 0x000C5A6C File Offset: 0x000C3C6C
	public void SetItemLabel()
	{
	}

	// Token: 0x060032A5 RID: 12965 RVA: 0x000C5A70 File Offset: 0x000C3C70
	public void SetItemSprite()
	{
	}

	// Token: 0x060032A6 RID: 12966 RVA: 0x000C5A74 File Offset: 0x000C3C74
	private void Start()
	{
	}

	// Token: 0x060032A7 RID: 12967 RVA: 0x000C5A78 File Offset: 0x000C3C78
	private void Update()
	{
		if (this.mPickingUpFlag)
		{
			if (Time.realtimeSinceStartup - this.mPickUpTime > this.MoveTime - 0.1f)
			{
				this.PickedUpItem();
				UnityVersionUtil.SetActiveRecursive(base.gameObject, false);
				Singleton<ObjManager>.Instance.RecycleDropItem(this);
			}
			else
			{
				this.UpdateMove();
			}
			return;
		}
	}

	// Token: 0x060032A8 RID: 12968 RVA: 0x000C5AD8 File Offset: 0x000C3CD8
	private void PickedUpItem()
	{
		if (this.ItemType == GameDefine.ITEM_TYPE.ADD_COIN)
		{
			SimpleRewardRootLogic.AddReward(new item
			{
				itemId = this.ItemId,
				itemCount = (long)this.ItemNum
			});
		}
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x000C5B1C File Offset: 0x000C3D1C
	private void UpdateMove()
	{
		if (this.mMainPlayer != null && !this.mMainPlayer.IsDie)
		{
			float num = (Time.realtimeSinceStartup - this.mPickUpTime) / this.MoveTime;
			num *= num;
			this.mTransform.position = Vector3.Lerp(this.mItemStartMovePos, this.mMainPlayer.Position + Vector3.up * 0.8f, num);
		}
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x000C5B98 File Offset: 0x000C3D98
	private void OnDisable()
	{
		this.handle.Cancel();
	}

	// Token: 0x04002193 RID: 8595
	private const float CheckPickInterval = 0.5f;

	// Token: 0x04002194 RID: 8596
	private const float CheckPickRange = 5f;

	// Token: 0x04002195 RID: 8597
	private const float ItemActiveTime = 10f;

	// Token: 0x04002196 RID: 8598
	private const string DROP_MONEYICON = "DiaoLuo_Coin_5";

	// Token: 0x04002197 RID: 8599
	private GameDefine.ITEM_TYPE mItemType = GameDefine.ITEM_TYPE.INVALID;

	// Token: 0x04002198 RID: 8600
	private string mItemId = string.Empty;

	// Token: 0x04002199 RID: 8601
	private int mItemNum = -1;

	// Token: 0x0400219A RID: 8602
	private long mOwnerServerId = -1L;

	// Token: 0x0400219B RID: 8603
	private ObjMainPlayer mMainPlayer;

	// Token: 0x0400219C RID: 8604
	public float MoveTime = 0.6f;

	// Token: 0x0400219D RID: 8605
	private float mDropCheckTime;

	// Token: 0x0400219E RID: 8606
	private float mPickUpTime;

	// Token: 0x0400219F RID: 8607
	private float mDropTime;

	// Token: 0x040021A0 RID: 8608
	private bool mPickingUpFlag;

	// Token: 0x040021A1 RID: 8609
	private Vector3 mItemStartMovePos = Vector3.zero;

	// Token: 0x040021A2 RID: 8610
	private vp_Timer.Handle handle = new vp_Timer.Handle();
}
