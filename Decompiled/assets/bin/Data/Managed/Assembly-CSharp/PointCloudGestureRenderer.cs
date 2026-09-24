using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[RequireComponent(typeof(LineRenderer))]
public class PointCloudGestureRenderer : MonoBehaviour
{
	// Token: 0x0600002F RID: 47 RVA: 0x0000314C File Offset: 0x0000134C
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.lineRenderer.useWorldSpace = false;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003168 File Offset: 0x00001368
	private void Start()
	{
		if (this.GestureTemplate)
		{
			this.Render(this.GestureTemplate);
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003188 File Offset: 0x00001388
	public void Blink()
	{
		base.animation.Stop();
		base.animation.Play();
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000031AC File Offset: 0x000013AC
	public bool Render(PointCloudGestureTemplate template)
	{
		if (template.PointCount < 2)
		{
			return false;
		}
		this.lineRenderer.SetVertexCount(template.PointCount);
		for (int i = 0; i < template.PointCount; i++)
		{
			this.lineRenderer.SetPosition(i, template.GetPosition(i));
		}
		return true;
	}

	// Token: 0x04000031 RID: 49
	private LineRenderer lineRenderer;

	// Token: 0x04000032 RID: 50
	public PointCloudGestureTemplate GestureTemplate;
}
