using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000004 RID: 4
[RequireComponent(typeof(FingerDownDetector))]
[RequireComponent(typeof(FingerMotionDetector))]
[RequireComponent(typeof(FingerUpDetector))]
[RequireComponent(typeof(ScreenRaycaster))]
public class FingerEventsSamplePart2 : SampleBase
{
	// Token: 0x06000011 RID: 17 RVA: 0x0000281C File Offset: 0x00000A1C
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = "Drag your fingers anywhere on the screen";
		this.paths = new FingerEventsSamplePart2.PathRenderer[FingerGestures.Instance.MaxFingers];
		for (int i = 0; i < this.paths.Length; i++)
		{
			this.paths[i] = new FingerEventsSamplePart2.PathRenderer(i, this.lineRendererPrefab);
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002884 File Offset: 0x00000A84
	protected override string GetHelpText()
	{
		return "This sample lets you visualize the FingerDown, FingerMoveBegin, FingerMove, FingerMoveEnd and FingerUp events.\r\n\r\nINSTRUCTIONS:\r\nMove your finger accross the screen and observe what happens.\r\n\r\nLEGEND:\r\n- Red Circle = FingerDown position\r\n- Yellow Square = FingerMoveBegin position\r\n- Green Sphere = FingerMoveEnd position\r\n- Blue Circle = FingerUp position";
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000288C File Offset: 0x00000A8C
	private void OnFingerDown(FingerDownEvent e)
	{
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[e.Finger.Index];
		pathRenderer.Reset();
		pathRenderer.AddPoint(e.Finger.Position, this.fingerDownMarkerPrefab);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000028CC File Offset: 0x00000ACC
	private void OnFingerMove(FingerMotionEvent e)
	{
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[e.Finger.Index];
		if (e.Phase == 1)
		{
			base.UI.StatusText = "Started moving " + e.Finger;
			pathRenderer.AddPoint(e.Position, this.fingerMoveBeginMarkerPrefab);
		}
		else if (e.Phase == 2)
		{
			pathRenderer.AddPoint(e.Position);
		}
		else
		{
			base.UI.StatusText = "Stopped moving " + e.Finger;
			pathRenderer.AddPoint(e.Position, this.fingerMoveEndMarkerPrefab);
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002974 File Offset: 0x00000B74
	private void OnFingerUp(FingerUpEvent e)
	{
		FingerEventsSamplePart2.PathRenderer pathRenderer = this.paths[e.Finger.Index];
		pathRenderer.AddPoint(e.Finger.Position, this.fingerUpMarkerPrefab);
		base.UI.StatusText = string.Concat(new object[]
		{
			"Finger ",
			e.Finger,
			" was held down for ",
			e.TimeHeldDown.ToString("N2"),
			" seconds"
		});
	}

	// Token: 0x04000016 RID: 22
	public LineRenderer lineRendererPrefab;

	// Token: 0x04000017 RID: 23
	public GameObject fingerDownMarkerPrefab;

	// Token: 0x04000018 RID: 24
	public GameObject fingerMoveBeginMarkerPrefab;

	// Token: 0x04000019 RID: 25
	public GameObject fingerMoveEndMarkerPrefab;

	// Token: 0x0400001A RID: 26
	public GameObject fingerUpMarkerPrefab;

	// Token: 0x0400001B RID: 27
	private FingerEventsSamplePart2.PathRenderer[] paths;

	// Token: 0x02000005 RID: 5
	private class PathRenderer
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000029F8 File Offset: 0x00000BF8
		public PathRenderer(int index, LineRenderer lineRendererPrefab)
		{
			this.lineRenderer = (Object.Instantiate(lineRendererPrefab) as LineRenderer);
			this.lineRenderer.name = lineRendererPrefab.name + index;
			this.lineRenderer.enabled = true;
			this.UpdateLines();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A60 File Offset: 0x00000C60
		public void Reset()
		{
			this.points.Clear();
			this.UpdateLines();
			foreach (GameObject gameObject in this.markers)
			{
				Object.Destroy(gameObject);
			}
			this.markers.Clear();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public void AddPoint(Vector2 screenPos)
		{
			this.AddPoint(screenPos, null);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void AddPoint(Vector2 screenPos, GameObject markerPrefab)
		{
			Vector3 worldPos = SampleBase.GetWorldPos(screenPos);
			if (markerPrefab)
			{
				this.AddMarker(worldPos, markerPrefab);
			}
			this.points.Add(worldPos);
			this.UpdateLines();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B30 File Offset: 0x00000D30
		private GameObject AddMarker(Vector2 pos, GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
			gameObject.name = string.Concat(new object[]
			{
				prefab.name,
				"(",
				this.markers.Count,
				")"
			});
			this.markers.Add(gameObject);
			return gameObject;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002BA0 File Offset: 0x00000DA0
		private void UpdateLines()
		{
			this.lineRenderer.SetVertexCount(this.points.Count);
			for (int i = 0; i < this.points.Count; i++)
			{
				this.lineRenderer.SetPosition(i, this.points[i]);
			}
		}

		// Token: 0x0400001C RID: 28
		private LineRenderer lineRenderer;

		// Token: 0x0400001D RID: 29
		private List<Vector3> points = new List<Vector3>();

		// Token: 0x0400001E RID: 30
		private List<GameObject> markers = new List<GameObject>();
	}
}
