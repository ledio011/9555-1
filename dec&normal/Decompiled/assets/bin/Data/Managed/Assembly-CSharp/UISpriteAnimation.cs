using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D2 RID: 210
[AddComponentMenu("NGUI/UI/Sprite Animation")]
[ExecuteInEditMode]
[RequireComponent(typeof(UISprite))]
public class UISpriteAnimation : MonoBehaviour
{
	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000698 RID: 1688 RVA: 0x0002F64C File Offset: 0x0002D84C
	public int frames
	{
		get
		{
			return this.mSpriteNames.Count;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000699 RID: 1689 RVA: 0x0002F65C File Offset: 0x0002D85C
	// (set) Token: 0x0600069A RID: 1690 RVA: 0x0002F664 File Offset: 0x0002D864
	public int framesPerSecond
	{
		get
		{
			return this.mFPS;
		}
		set
		{
			this.mFPS = value;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600069B RID: 1691 RVA: 0x0002F670 File Offset: 0x0002D870
	// (set) Token: 0x0600069C RID: 1692 RVA: 0x0002F678 File Offset: 0x0002D878
	public string namePrefix
	{
		get
		{
			return this.mPrefix;
		}
		set
		{
			if (this.mPrefix != value)
			{
				this.mPrefix = value;
				this.RebuildSpriteList();
			}
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x0600069D RID: 1693 RVA: 0x0002F698 File Offset: 0x0002D898
	// (set) Token: 0x0600069E RID: 1694 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
	public bool loop
	{
		get
		{
			return this.mLoop;
		}
		set
		{
			this.mLoop = value;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x0600069F RID: 1695 RVA: 0x0002F6AC File Offset: 0x0002D8AC
	public bool isPlaying
	{
		get
		{
			return this.mActive;
		}
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
	protected virtual void Start()
	{
		this.RebuildSpriteList();
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x0002F6BC File Offset: 0x0002D8BC
	protected virtual void Update()
	{
		if (this.mActive && this.mSpriteNames.Count > 1 && Application.isPlaying && (float)this.mFPS > 0f)
		{
			this.mDelta += RealTime.deltaTime;
			float num = 1f / (float)this.mFPS;
			if (num < this.mDelta)
			{
				this.mDelta = ((num <= 0f) ? 0f : (this.mDelta - num));
				if (++this.mIndex >= this.mSpriteNames.Count)
				{
					this.mIndex = 0;
					this.mActive = this.loop;
				}
				if (this.mActive)
				{
					this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
					this.mSprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x0002F7B8 File Offset: 0x0002D9B8
	public void RebuildSpriteList()
	{
		if (this.mSprite == null)
		{
			this.mSprite = base.GetComponent<UISprite>();
		}
		this.mSpriteNames.Clear();
		if (this.mSprite != null && this.mSprite.atlas != null)
		{
			List<UISpriteData> spriteList = this.mSprite.atlas.spriteList;
			int i = 0;
			int count = spriteList.Count;
			while (i < count)
			{
				UISpriteData uispriteData = spriteList[i];
				if (string.IsNullOrEmpty(this.mPrefix) || uispriteData.name.StartsWith(this.mPrefix))
				{
					this.mSpriteNames.Add(uispriteData.name);
				}
				i++;
			}
			this.mSpriteNames.Sort();
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0002F888 File Offset: 0x0002DA88
	public void Reset()
	{
		this.mActive = true;
		this.mIndex = 0;
		if (this.mSprite != null && this.mSpriteNames.Count > 0)
		{
			this.mSprite.spriteName = this.mSpriteNames[this.mIndex];
			this.mSprite.MakePixelPerfect();
		}
	}

	// Token: 0x040005B6 RID: 1462
	[HideInInspector]
	[SerializeField]
	protected int mFPS = 30;

	// Token: 0x040005B7 RID: 1463
	[SerializeField]
	[HideInInspector]
	protected string mPrefix = string.Empty;

	// Token: 0x040005B8 RID: 1464
	[HideInInspector]
	[SerializeField]
	protected bool mLoop = true;

	// Token: 0x040005B9 RID: 1465
	protected UISprite mSprite;

	// Token: 0x040005BA RID: 1466
	protected float mDelta;

	// Token: 0x040005BB RID: 1467
	protected int mIndex;

	// Token: 0x040005BC RID: 1468
	protected bool mActive = true;

	// Token: 0x040005BD RID: 1469
	protected List<string> mSpriteNames = new List<string>();
}
