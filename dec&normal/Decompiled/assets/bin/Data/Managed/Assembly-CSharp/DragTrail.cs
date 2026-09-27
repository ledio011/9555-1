using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class DragTrail : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x0000354C File Offset: 0x0000174C
	private void Start()
	{
		this.lineRenderer = (Object.Instantiate(this.lineRendererPrefab, base.transform.position, base.transform.rotation) as LineRenderer);
		this.lineRenderer.transform.parent = base.transform;
		this.lineRenderer.enabled = false;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000035A8 File Offset: 0x000017A8
	private void Update()
	{
		if (this.lineRenderer.enabled)
		{
			this.lineRenderer.SetPosition(1, base.transform.position);
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x000035DC File Offset: 0x000017DC
	private void OnDrag(DragGesture gesture)
	{
		if (gesture.Phase == 1)
		{
			this.lineRenderer.enabled = true;
			this.lineRenderer.SetPosition(0, base.transform.position);
			this.lineRenderer.SetPosition(1, base.transform.position);
			this.lineRenderer.SetWidth(0.01f, base.transform.localScale.x);
		}
		else if (gesture.Phase == 3)
		{
			this.lineRenderer.enabled = false;
		}
	}

	// Token: 0x04000038 RID: 56
	public LineRenderer lineRendererPrefab;

	// Token: 0x04000039 RID: 57
	private LineRenderer lineRenderer;
}
