using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class FingerDebug : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x0000212C File Offset: 0x0000032C
	private void Start()
	{
		if (!FingerGestures.Instance)
		{
			Debug.LogError("FG instance not present");
			base.enabled = false;
			return;
		}
		this.icons = new GUITexture[FingerGestures.Instance.MaxFingers];
		for (int i = 0; i < this.icons.Length; i++)
		{
			GUITexture guitexture = UnityEngine.Object.Instantiate(this.FingerIcon) as GUITexture;
			guitexture.transform.parent = base.transform;
			guitexture.enabled = false;
			this.icons[i] = guitexture;
		}
		this.FingerIcon.enabled = false;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000021C8 File Offset: 0x000003C8
	private void Update()
	{
		if (!FingerGestures.Instance)
		{
			return;
		}
		if (FingerGestures.Touches.Count >= 2)
		{
			this.distance = Vector2.Distance(FingerGestures.Touches[0].Position, FingerGestures.Touches[1].Position);
		}
		else
		{
			this.distance = -1f;
		}
		int i;
		for (i = 0; i < FingerGestures.Touches.Count; i++)
		{
			FingerGestures.Finger finger = FingerGestures.Touches[i];
			Rect pixelInset = this.icons[i].pixelInset;
			pixelInset.x = finger.Position.x - pixelInset.width / 2f;
			pixelInset.y = finger.Position.y - pixelInset.height / 2f;
			this.icons[i].pixelInset = pixelInset;
			this.icons[i].enabled = true;
		}
		while (i < this.icons.Length)
		{
			this.icons[i].enabled = false;
			i++;
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000022F0 File Offset: 0x000004F0
	private void OnGUI()
	{
		if (!this.ShowGUI)
		{
			return;
		}
		if (!FingerGestures.Instance)
		{
			return;
		}
		GUILayout.BeginArea(this.GuiRect);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Label("Input.Touches: " + Input.touchCount, new GUILayoutOption[0]);
		GUILayout.Label("FingerGestures: " + FingerGestures.Touches.Count, new GUILayoutOption[0]);
		foreach (FingerGestures.Finger finger in FingerGestures.Touches)
		{
			GUILayout.Label(string.Format("{0} moving:{1}", finger, finger.IsMoving), new GUILayoutOption[0]);
			foreach (GestureRecognizer arg in finger.GestureRecognizers)
			{
				GUILayout.Label(finger.ToString() + ": " + arg, new GUILayoutOption[0]);
			}
		}
		if (this.distance >= 0f)
		{
			GUILayout.Label("Finger[0->1] Distance: " + this.distance.ToString("N0"), new GUILayoutOption[0]);
		}
		GUILayout.Space(5f);
		GUILayout.Label(string.Concat(new object[]
		{
			"Clusters: ",
			FingerGestures.DefaultClusterManager.Clusters.Count,
			" [Pool: ",
			FingerGestures.DefaultClusterManager.GetClustersPool().Count,
			"]"
		}), new GUILayoutOption[0]);
		foreach (FingerClusterManager.Cluster cluster in FingerGestures.DefaultClusterManager.Clusters)
		{
			GUILayout.Label(string.Concat(new object[]
			{
				"  -> Cluster #",
				cluster.Id,
				": ",
				cluster.Fingers.Count,
				" fingers"
			}), new GUILayoutOption[0]);
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	// Token: 0x04000001 RID: 1
	public GUITexture FingerIcon;

	// Token: 0x04000002 RID: 2
	public bool ShowGUI;

	// Token: 0x04000003 RID: 3
	public Rect GuiRect = new Rect(5f, 5f, 500f, 500f);

	// Token: 0x04000004 RID: 4
	private GUITexture[] icons;

	// Token: 0x04000005 RID: 5
	private float distance = -1f;
}
