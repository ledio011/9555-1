using System;
using UnityEngine;

// Token: 0x020008C7 RID: 2247
public class UIMaterialEffect : MonoBehaviour
{
	// Token: 0x06003C8F RID: 15503 RVA: 0x00109EC4 File Offset: 0x001080C4
	private void OnEnable()
	{
		if (this.CurTexture == null)
		{
			this.CurTexture = base.gameObject.GetComponent<UITexture>();
		}
		if (this.CurTexture != null)
		{
			this.curMaterial = this.CurTexture.material;
		}
		if (this.curMaterial != null)
		{
			this.curMaterial.SetFloat("_Ratio", (float)this.CurTexture.width / (float)this.CurTexture.height);
			this.CurTexture.onRender = new UIDrawCall.OnRenderCallback(this.UpdateMaterial);
		}
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x00109F68 File Offset: 0x00108168
	private void UpdateMaterial(Material mat)
	{
		mat.SetFloat("_Edge", this.EdgeFloat);
		mat.SetFloat("_FlowLight", this.FlowLight);
		mat.SetFloat("_FlowSpeed", this.FlowSpeed);
		if (this.CurTexture != null)
		{
			mat.SetFloat("_Ratio", (float)this.CurTexture.width / (float)this.CurTexture.height);
		}
	}

	// Token: 0x040027CC RID: 10188
	public UITexture CurTexture;

	// Token: 0x040027CD RID: 10189
	private Material curMaterial;

	// Token: 0x040027CE RID: 10190
	public float EdgeFloat = 0.043f;

	// Token: 0x040027CF RID: 10191
	public float FlowLight = 2f;

	// Token: 0x040027D0 RID: 10192
	public float FlowSpeed = 3f;
}
