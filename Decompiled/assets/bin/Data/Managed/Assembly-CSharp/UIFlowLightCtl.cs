using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008C2 RID: 2242
public class UIFlowLightCtl : MonoBehaviour
{
	// Token: 0x06003C69 RID: 15465 RVA: 0x00108D20 File Offset: 0x00106F20
	private void Awake()
	{
		this.sp = base.gameObject.GetComponent<UISpriteFlowLight>();
		if (this.ShareGroup > -1)
		{
			if (UIFlowLightCtl.ShareGroudMat.ContainsKey(this.ShareGroup))
			{
				this.picMat = UIFlowLightCtl.ShareGroudMat[this.ShareGroup];
				Dictionary<int, int> referenceCount;
				Dictionary<int, int> dictionary = referenceCount = UIFlowLightCtl.ReferenceCount;
				int num2;
				int num = num2 = this.ShareGroup;
				num2 = referenceCount[num2];
				dictionary[num] = num2 + 1;
			}
			else
			{
				this.picMat = (Object.Instantiate(this.sp.material) as Material);
				UIFlowLightCtl.ShareGroudMat.Add(this.ShareGroup, this.picMat);
				UIFlowLightCtl.ReferenceCount.Add(this.ShareGroup, 1);
			}
			this.sp.mTestMat = this.picMat;
		}
		else
		{
			this.picMat = (Object.Instantiate(this.sp.material) as Material);
			this.sp.mTestMat = this.picMat;
		}
		this.widthRate = (float)this.sp.GetAtlasSprite().width * 1f / (float)this.sp.atlas.spriteMaterial.mainTexture.width;
		this.heightRate = (float)this.sp.GetAtlasSprite().height * 1f / (float)this.sp.atlas.spriteMaterial.mainTexture.height;
		this.xOffsetRate = (float)this.sp.GetAtlasSprite().x * 1f / (float)this.sp.atlas.spriteMaterial.mainTexture.width;
		this.yOffsetRate = (float)(this.sp.atlas.spriteMaterial.mainTexture.height - (this.sp.GetAtlasSprite().y + this.sp.GetAtlasSprite().height)) * 1f / (float)this.sp.atlas.spriteMaterial.mainTexture.height;
		this.sp.mTestMat = this.picMat;
		this.mUvAdd = 0f;
		this.mIsPlaying = true;
		this.sp.onRender = new UIDrawCall.OnRenderCallback(this.UpdateMaterial);
	}

	// Token: 0x06003C6A RID: 15466 RVA: 0x00108F68 File Offset: 0x00107168
	private void UpdateReference()
	{
		if (UIFlowLightCtl.ReferenceCount.ContainsKey(this.ShareGroup))
		{
			Dictionary<int, int> referenceCount;
			Dictionary<int, int> dictionary = referenceCount = UIFlowLightCtl.ReferenceCount;
			int num2;
			int num = num2 = this.ShareGroup;
			num2 = referenceCount[num2];
			dictionary[num] = num2 - 1;
			if (UIFlowLightCtl.ReferenceCount[this.ShareGroup] <= 0)
			{
				UIFlowLightCtl.ShareGroudMat.Remove(this.ShareGroup);
				UIFlowLightCtl.ReferenceCount.Remove(this.ShareGroup);
			}
		}
	}

	// Token: 0x06003C6B RID: 15467 RVA: 0x00108FE4 File Offset: 0x001071E4
	private void Start()
	{
		this.picMat.SetFloat("_WidthRate", this.widthRate);
		this.picMat.SetFloat("_HeightRate", this.heightRate);
		this.picMat.SetFloat("_XOffset", this.xOffsetRate);
		this.picMat.SetFloat("_YOffset", this.yOffsetRate);
	}

	// Token: 0x06003C6C RID: 15468 RVA: 0x0010904C File Offset: 0x0010724C
	private void OnDestroy()
	{
		this.UpdateReference();
	}

	// Token: 0x06003C6D RID: 15469 RVA: 0x00109054 File Offset: 0x00107254
	private void Update()
	{
		this.timeCount += Time.deltaTime;
		this.mUvAdd += this.mUvSpeed;
		if (this.mUvAdd >= 1f)
		{
			this.mUvAdd -= 1f;
		}
		if (this.timeCount >= this.mDuration)
		{
			this.timeCount -= this.mDuration;
		}
		this.powerVal = this.curve.Evaluate(this.timeCount / this.mDuration) * this.mPower;
	}

	// Token: 0x06003C6E RID: 15470 RVA: 0x001090F4 File Offset: 0x001072F4
	private void UpdateMaterial(Material mat)
	{
		mat.SetFloat("_FlowLightOffset", this.mUvAdd);
		mat.SetFloat("_FlowLightPower", this.powerVal);
	}

	// Token: 0x06003C6F RID: 15471 RVA: 0x00109124 File Offset: 0x00107324
	private void PlayOnceAgain()
	{
		this.mUvAdd = 0f;
		this.timeCount = 0f;
		this.mIsPlaying = true;
	}

	// Token: 0x0400279D RID: 10141
	public float mUvSpeed = 0.02f;

	// Token: 0x0400279E RID: 10142
	public float mDuration = 3f;

	// Token: 0x0400279F RID: 10143
	public float mPower = 2f;

	// Token: 0x040027A0 RID: 10144
	private float mUvAdd;

	// Token: 0x040027A1 RID: 10145
	private bool mIsPlaying;

	// Token: 0x040027A2 RID: 10146
	private Material picMat;

	// Token: 0x040027A3 RID: 10147
	public static Dictionary<int, Material> ShareGroudMat = new Dictionary<int, Material>();

	// Token: 0x040027A4 RID: 10148
	public static Dictionary<int, int> ReferenceCount = new Dictionary<int, int>();

	// Token: 0x040027A5 RID: 10149
	public int ShareGroup = -1;

	// Token: 0x040027A6 RID: 10150
	private float widthRate;

	// Token: 0x040027A7 RID: 10151
	private float heightRate;

	// Token: 0x040027A8 RID: 10152
	private float xOffsetRate;

	// Token: 0x040027A9 RID: 10153
	private float yOffsetRate;

	// Token: 0x040027AA RID: 10154
	private UISpriteFlowLight sp;

	// Token: 0x040027AB RID: 10155
	public AnimationCurve curve;

	// Token: 0x040027AC RID: 10156
	private float timeCount;

	// Token: 0x040027AD RID: 10157
	private float powerVal;
}
