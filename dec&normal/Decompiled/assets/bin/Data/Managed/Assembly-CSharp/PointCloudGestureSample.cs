using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000A RID: 10
[RequireComponent(typeof(PointCloudRegognizer))]
public class PointCloudGestureSample : SampleBase
{
	// Token: 0x06000034 RID: 52 RVA: 0x00003238 File Offset: 0x00001438
	protected override void Start()
	{
		base.Start();
		this.RenderGestureTemplates();
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003248 File Offset: 0x00001448
	private void OnCustomGesture(PointCloudGesture gesture)
	{
		string text = (gesture.MatchScore * 100f).ToString("N2");
		base.UI.StatusText = string.Concat(new string[]
		{
			"Matched ",
			gesture.RecognizedTemplate.name,
			" (score: ",
			text,
			"% distance:",
			gesture.MatchDistance.ToString("N2"),
			")"
		});
		Debug.Log(base.UI.StatusText);
		PointCloudGestureRenderer pointCloudGestureRenderer = this.FindGestureRenderer(gesture.RecognizedTemplate);
		if (pointCloudGestureRenderer)
		{
			pointCloudGestureRenderer.Blink();
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000032F8 File Offset: 0x000014F8
	private void OnFingerDown(FingerDownEvent e)
	{
		base.UI.StatusText = string.Empty;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000330C File Offset: 0x0000150C
	private void RenderGestureTemplates()
	{
		this.gestureRenderers = new List<PointCloudGestureRenderer>();
		Transform transform = new GameObject("Gesture Templates").transform;
		transform.parent = base.transform;
		transform.localScale = this.GestureScale * Vector3.one;
		PointCloudRegognizer component = base.GetComponent<PointCloudRegognizer>();
		Vector3 zero = Vector3.zero;
		int num = 0;
		int num2 = 0;
		float num3 = 0f;
		foreach (PointCloudGestureTemplate pointCloudGestureTemplate in component.Templates)
		{
			PointCloudGestureRenderer pointCloudGestureRenderer = Object.Instantiate(this.GestureRendererPrefab, transform.position, transform.rotation) as PointCloudGestureRenderer;
			pointCloudGestureRenderer.GestureTemplate = pointCloudGestureTemplate;
			pointCloudGestureRenderer.name = pointCloudGestureTemplate.name;
			pointCloudGestureRenderer.transform.parent = transform;
			pointCloudGestureRenderer.transform.localPosition = zero;
			pointCloudGestureRenderer.transform.localScale = Vector3.one;
			zero.x += this.GestureSpacing.x;
			num3 = Mathf.Max(num3, zero.x);
			if (++num >= this.MaxGesturesPerRaw)
			{
				zero.y += this.GestureSpacing.y;
				zero.x = 0f;
				num = 0;
				num2++;
			}
			this.gestureRenderers.Add(pointCloudGestureRenderer);
		}
		Vector3 zero2 = Vector3.zero;
		zero2.x -= this.GestureScale * 0.5f * (num3 - this.GestureSpacing.x);
		if (num2 > 0)
		{
			zero2.y -= this.GestureScale * 0.5f * (zero.y - this.GestureSpacing.y);
		}
		transform.localPosition = zero2;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003508 File Offset: 0x00001708
	private PointCloudGestureRenderer FindGestureRenderer(PointCloudGestureTemplate template)
	{
		return this.gestureRenderers.Find((PointCloudGestureRenderer gr) => gr.GestureTemplate == template);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000353C File Offset: 0x0000173C
	protected override string GetHelpText()
	{
		return "This sample demonstrates how to use the PointCloudGestureRecognizer to recognize custom gestures from a list of templates";
	}

	// Token: 0x04000033 RID: 51
	public PointCloudGestureRenderer GestureRendererPrefab;

	// Token: 0x04000034 RID: 52
	public float GestureScale = 8f;

	// Token: 0x04000035 RID: 53
	public Vector2 GestureSpacing = new Vector2(1.25f, 1f);

	// Token: 0x04000036 RID: 54
	public int MaxGesturesPerRaw = 2;

	// Token: 0x04000037 RID: 55
	private List<PointCloudGestureRenderer> gestureRenderers;
}
