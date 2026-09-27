using System;
using UnityEngine;

// Token: 0x020008B4 RID: 2228
public class MaterialRenderQuene : MonoBehaviour
{
	// Token: 0x06003C18 RID: 15384 RVA: 0x00106A44 File Offset: 0x00104C44
	private void Start()
	{
		if (this.IsShare)
		{
			this.mMaterial = base.renderer.sharedMaterial;
		}
		else
		{
			this.mMaterial = base.renderer.material;
		}
		if (this.mMaterial != null)
		{
			if (this.CurWidget != null)
			{
				UIPanel panel = this.CurWidget.panel;
				if (panel != null)
				{
					this.mMaterial.renderQueue = panel.startingRenderQueue + 1;
					this.flag = true;
				}
			}
		}
		else
		{
			this.mMaterial.renderQueue = this.DefaultQuenue;
			this.flag = true;
		}
	}

	// Token: 0x06003C19 RID: 15385 RVA: 0x00106AF4 File Offset: 0x00104CF4
	private void Update()
	{
		if (this.flag)
		{
			base.enabled = false;
			return;
		}
		if (this.mMaterial != null)
		{
			if (this.CurWidget != null)
			{
				UIPanel panel = this.CurWidget.panel;
				if (panel != null)
				{
					this.mMaterial.renderQueue = panel.startingRenderQueue + 1;
					this.flag = true;
				}
			}
		}
		else
		{
			this.mMaterial.renderQueue = this.DefaultQuenue;
			this.flag = true;
		}
	}

	// Token: 0x0400273F RID: 10047
	public bool IsShare = true;

	// Token: 0x04002740 RID: 10048
	public UIWidget CurWidget;

	// Token: 0x04002741 RID: 10049
	private Material mMaterial;

	// Token: 0x04002742 RID: 10050
	public int DefaultQuenue = 3200;

	// Token: 0x04002743 RID: 10051
	private bool flag;
}
