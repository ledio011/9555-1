using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class UVDOAnimation : MonoBehaviour
{
	// Token: 0x060007C5 RID: 1989 RVA: 0x00037874 File Offset: 0x00035A74
	private void Start()
	{
		this.mFactor = 0f;
		if (this.isSharedMaterial)
		{
			this.CurMaterial = base.gameObject.renderer.sharedMaterial;
		}
		else
		{
			this.CurMaterial = base.gameObject.renderer.material;
		}
		if (this.CurMaterial == null)
		{
			Debug.LogError("Material is NULL!");
		}
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x000378E4 File Offset: 0x00035AE4
	private void Update()
	{
		if (this.CurMaterial != null && this.StartAnimation)
		{
			this.mFactor += this.Speed * Time.deltaTime;
			if (this.mCurStyle == UVDOAnimation.Style.Loop)
			{
				if (this.Speed < 0f)
				{
					this.Speed = -this.Speed;
				}
				if (this.mFactor > 1f)
				{
					this.mFactor -= Mathf.Floor(this.mFactor);
				}
			}
			else if (this.mCurStyle == UVDOAnimation.Style.PingPong)
			{
				if (this.mFactor > 1f)
				{
					this.mFactor = 1f - (this.mFactor - Mathf.Floor(this.mFactor));
					this.Speed = -this.Speed;
				}
				else if (this.mFactor < 0f)
				{
					this.mFactor = -this.mFactor;
					this.Speed = -this.Speed;
				}
			}
			else
			{
				if (this.Speed < 0f)
				{
					this.Speed = -this.Speed;
				}
				if (this.mFactor >= 1f)
				{
					this.mFactor = 0f;
					this.StartAnimation = false;
				}
			}
			if (this.isYanimation)
			{
				this.CurMaterial.mainTextureOffset = new Vector2(0f, this.mAnimationCurve.Evaluate(this.mFactor) * this.MaxValue);
			}
			else
			{
				this.CurMaterial.mainTextureOffset = new Vector2(this.mAnimationCurve.Evaluate(this.mFactor) * this.MaxValue, 0f);
			}
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00037AA0 File Offset: 0x00035CA0
	private void OnDisable()
	{
		if (this.CurMaterial != null)
		{
			this.CurMaterial.mainTextureOffset = Vector2.zero;
		}
	}

	// Token: 0x040006B1 RID: 1713
	public UVDOAnimation.Style mCurStyle;

	// Token: 0x040006B2 RID: 1714
	public AnimationCurve mAnimationCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040006B3 RID: 1715
	public float MaxValue = 1f;

	// Token: 0x040006B4 RID: 1716
	public bool isYanimation;

	// Token: 0x040006B5 RID: 1717
	private Material CurMaterial;

	// Token: 0x040006B6 RID: 1718
	public bool isSharedMaterial;

	// Token: 0x040006B7 RID: 1719
	public float Speed = 1f;

	// Token: 0x040006B8 RID: 1720
	private float mFactor;

	// Token: 0x040006B9 RID: 1721
	public bool StartAnimation;

	// Token: 0x020000F1 RID: 241
	public enum Style
	{
		// Token: 0x040006BB RID: 1723
		Once,
		// Token: 0x040006BC RID: 1724
		Loop,
		// Token: 0x040006BD RID: 1725
		PingPong
	}
}
