using System;
using UnityEngine;

// Token: 0x02000819 RID: 2073
public class Obj : MonoBehaviour
{
	// Token: 0x17000E33 RID: 3635
	// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000C353C File Offset: 0x000C173C
	// (set) Token: 0x060031F1 RID: 12785 RVA: 0x000C3544 File Offset: 0x000C1744
	public virtual long ServerId
	{
		get
		{
			return this.mServerId;
		}
		set
		{
			this.mServerId = value;
		}
	}

	// Token: 0x17000E34 RID: 3636
	// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000C3550 File Offset: 0x000C1750
	public string ModelId
	{
		get
		{
			return this.mCharacterModelData.ID;
		}
	}

	// Token: 0x17000E35 RID: 3637
	// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000C3560 File Offset: 0x000C1760
	public CharacterModelData CurrentCharacterModelData
	{
		get
		{
			return this.mCharacterModelData;
		}
	}

	// Token: 0x17000E36 RID: 3638
	// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000C3568 File Offset: 0x000C1768
	public virtual float ModelHeight
	{
		get
		{
			return this.mCharacterModelData.ModelHeight;
		}
	}

	// Token: 0x17000E37 RID: 3639
	// (get) Token: 0x060031F5 RID: 12789 RVA: 0x000C3578 File Offset: 0x000C1778
	// (set) Token: 0x060031F6 RID: 12790 RVA: 0x000C3588 File Offset: 0x000C1788
	public string IndexName
	{
		get
		{
			return this.mCharacterModelData.IndexName;
		}
		set
		{
			this.mCharacterModelData.IndexName = value;
		}
	}

	// Token: 0x17000E38 RID: 3640
	// (get) Token: 0x060031F7 RID: 12791 RVA: 0x000C3598 File Offset: 0x000C1798
	public Transform CacheTransform
	{
		get
		{
			if (this.mTransform == null)
			{
				this.mTransform = base.transform;
			}
			return this.mTransform;
		}
	}

	// Token: 0x17000E39 RID: 3641
	// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000C35C0 File Offset: 0x000C17C0
	// (set) Token: 0x060031F9 RID: 12793 RVA: 0x000C35D0 File Offset: 0x000C17D0
	public Vector3 Position
	{
		get
		{
			return this.CacheTransform.position;
		}
		set
		{
			this.CacheTransform.localPosition = value;
		}
	}

	// Token: 0x17000E3A RID: 3642
	// (get) Token: 0x060031FA RID: 12794 RVA: 0x000C35E0 File Offset: 0x000C17E0
	public Quaternion Rotation
	{
		get
		{
			return this.CacheTransform.localRotation;
		}
	}

	// Token: 0x17000E3B RID: 3643
	// (get) Token: 0x060031FB RID: 12795 RVA: 0x000C35F0 File Offset: 0x000C17F0
	// (set) Token: 0x060031FC RID: 12796 RVA: 0x000C3600 File Offset: 0x000C1800
	public Vector3 Scale
	{
		get
		{
			return this.CacheTransform.localScale;
		}
		set
		{
			this.CacheTransform.localScale = value;
		}
	}

	// Token: 0x17000E3C RID: 3644
	// (get) Token: 0x060031FD RID: 12797 RVA: 0x000C3610 File Offset: 0x000C1810
	public GameDefine.OBJ_TYPE ObjType
	{
		get
		{
			return this.mObjType;
		}
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x000C3618 File Offset: 0x000C1818
	public virtual void Init()
	{
	}

	// Token: 0x04002158 RID: 8536
	protected long mServerId;

	// Token: 0x04002159 RID: 8537
	protected CharacterModelData mCharacterModelData;

	// Token: 0x0400215A RID: 8538
	protected Transform mTransform;

	// Token: 0x0400215B RID: 8539
	protected Vector3 mPosition;

	// Token: 0x0400215C RID: 8540
	protected Quaternion mRotation;

	// Token: 0x0400215D RID: 8541
	protected Vector3 mScale;

	// Token: 0x0400215E RID: 8542
	protected GameDefine.OBJ_TYPE mObjType;
}
