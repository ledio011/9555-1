using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class UI2DSpriteAnimation : MonoBehaviour
{
	// Token: 0x06000513 RID: 1299 RVA: 0x00021EF0 File Offset: 0x000200F0
	private void Start()
	{
		this.mUnitySprite = base.GetComponent<SpriteRenderer>();
		this.mNguiSprite = base.GetComponent<UI2DSprite>();
		if (this.framerate > 0)
		{
			this.mUpdate = ((!this.ignoreTimeScale) ? Time.time : RealTime.time) + 1f / (float)this.framerate;
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00021F50 File Offset: 0x00020150
	private void Update()
	{
		if (this.framerate != 0 && this.frames != null && this.frames.Length > 0)
		{
			float num = (!this.ignoreTimeScale) ? Time.time : RealTime.time;
			if (this.mUpdate < num)
			{
				this.mUpdate = num;
				this.mIndex = NGUIMath.RepeatIndex((this.framerate <= 0) ? (this.mIndex - 1) : (this.mIndex + 1), this.frames.Length);
				this.mUpdate = num + Mathf.Abs(1f / (float)this.framerate);
				if (this.mUnitySprite != null)
				{
					this.mUnitySprite.sprite = this.frames[this.mIndex];
				}
				else if (this.mNguiSprite != null)
				{
					this.mNguiSprite.nextSprite = this.frames[this.mIndex];
				}
			}
		}
	}

	// Token: 0x04000458 RID: 1112
	public int framerate = 20;

	// Token: 0x04000459 RID: 1113
	public bool ignoreTimeScale = true;

	// Token: 0x0400045A RID: 1114
	public Sprite[] frames;

	// Token: 0x0400045B RID: 1115
	private SpriteRenderer mUnitySprite;

	// Token: 0x0400045C RID: 1116
	private UI2DSprite mNguiSprite;

	// Token: 0x0400045D RID: 1117
	private int mIndex;

	// Token: 0x0400045E RID: 1118
	private float mUpdate;
}
