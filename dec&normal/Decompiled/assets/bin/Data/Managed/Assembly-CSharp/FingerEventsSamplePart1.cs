using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
[RequireComponent(typeof(FingerMotionDetector))]
[RequireComponent(typeof(FingerHoverDetector))]
[RequireComponent(typeof(FingerUpDetector))]
[RequireComponent(typeof(ScreenRaycaster))]
[RequireComponent(typeof(FingerDownDetector))]
public class FingerEventsSamplePart1 : SampleBase
{
	// Token: 0x06000008 RID: 8 RVA: 0x000023D8 File Offset: 0x000005D8
	private void OnFingerDown(FingerDownEvent e)
	{
		if (e.Selection == this.fingerDownObject)
		{
			this.SpawnParticles(this.fingerDownObject);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002408 File Offset: 0x00000608
	private void OnFingerUp(FingerUpEvent e)
	{
		if (e.Selection == this.fingerUpObject)
		{
			this.SpawnParticles(this.fingerUpObject);
		}
		FingerGestures.Finger finger = e.Finger;
		Debug.Log(string.Concat(new object[]
		{
			"Finger was lifted up on ",
			(!e.Selection) ? "<nothing>" : e.Selection.name,
			" at ",
			finger.Position,
			" and moved ",
			finger.DistanceFromStart.ToString("N0"),
			" pixels from its initial position at ",
			finger.StartPosition,
			". It was held down for ",
			e.TimeHeldDown,
			" seconds"
		}));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000024EC File Offset: 0x000006EC
	private void OnFingerHover(FingerHoverEvent e)
	{
		if (e.Selection == this.fingerHoverObject)
		{
			if (e.Phase == 1)
			{
				base.UI.StatusText = "Finger entered " + this.fingerHoverObject.name;
				this.originalHoverMaterial = this.fingerHoverObject.renderer.sharedMaterial;
				this.fingerHoverObject.renderer.sharedMaterial = this.highlightMaterial;
			}
			else if (e.Phase == 2)
			{
				base.UI.StatusText = "Finger left " + this.fingerHoverObject.name;
				this.fingerHoverObject.renderer.sharedMaterial = this.originalHoverMaterial;
			}
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000025B0 File Offset: 0x000007B0
	private void OnFingerStationary(FingerMotionEvent e)
	{
		if (e.Phase == 1)
		{
			if (this.stationaryFingerIndex != -1)
			{
				return;
			}
			GameObject selection = e.Selection;
			if (selection == this.fingerStationaryObject)
			{
				base.UI.StatusText = "Begin stationary on finger " + e.Finger.Index;
				this.stationaryFingerIndex = e.Finger.Index;
				this.originalStationaryMaterial = selection.renderer.sharedMaterial;
				selection.renderer.sharedMaterial = this.highlightMaterial;
			}
		}
		else if (e.Phase == 2)
		{
			if (e.ElapsedTime < this.chargeDelay)
			{
				return;
			}
			if (e.Selection == this.fingerStationaryObject)
			{
				float num = Mathf.Clamp01((e.ElapsedTime - this.chargeDelay) / this.chargeTime);
				float num2 = Mathf.Lerp(this.minSationaryParticleEmissionCount, this.maxSationaryParticleEmissionCount, num);
				this.stationaryParticleEmitter.minEmission = num2;
				this.stationaryParticleEmitter.maxEmission = num2;
				this.stationaryParticleEmitter.emit = true;
				base.UI.StatusText = "Charge: " + (100f * num).ToString("N1") + "%";
			}
		}
		else if (e.Phase == 3 && e.Finger.Index == this.stationaryFingerIndex)
		{
			float elapsedTime = e.ElapsedTime;
			base.UI.StatusText = string.Concat(new object[]
			{
				"Stationary ended on finger ",
				e.Finger,
				" - ",
				elapsedTime.ToString("N1"),
				" seconds elapsed"
			});
			this.StopStationaryParticleEmitter();
			this.fingerStationaryObject.renderer.sharedMaterial = this.originalStationaryMaterial;
			this.stationaryFingerIndex = -1;
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002798 File Offset: 0x00000998
	protected override string GetHelpText()
	{
		return "This sample lets you visualize and understand the OnFingerDown, OnFingerStationary and OnFingerUp events.\r\n\r\nINSTRUCTIONS:\r\n- Press, hold and release the red and blue spheres\r\n- Press & hold the green sphere without moving for a few seconds\r\n- Move your finger over and out of the cyan OnFingerHover sphere";
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000027A0 File Offset: 0x000009A0
	protected override void Start()
	{
		base.Start();
		if (this.fingerStationaryObject)
		{
			this.stationaryParticleEmitter = this.fingerStationaryObject.GetComponentInChildren<ParticleEmitter>();
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000027CC File Offset: 0x000009CC
	private void StopStationaryParticleEmitter()
	{
		this.stationaryParticleEmitter.emit = false;
		base.UI.StatusText = string.Empty;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000027EC File Offset: 0x000009EC
	private void SpawnParticles(GameObject obj)
	{
		ParticleEmitter componentInChildren = obj.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.Emit();
		}
	}

	// Token: 0x04000009 RID: 9
	public GameObject fingerDownObject;

	// Token: 0x0400000A RID: 10
	public GameObject fingerStationaryObject;

	// Token: 0x0400000B RID: 11
	public GameObject fingerHoverObject;

	// Token: 0x0400000C RID: 12
	public GameObject fingerUpObject;

	// Token: 0x0400000D RID: 13
	public float chargeDelay = 0.5f;

	// Token: 0x0400000E RID: 14
	public float chargeTime = 5f;

	// Token: 0x0400000F RID: 15
	public float minSationaryParticleEmissionCount = 5f;

	// Token: 0x04000010 RID: 16
	public float maxSationaryParticleEmissionCount = 50f;

	// Token: 0x04000011 RID: 17
	public Material highlightMaterial;

	// Token: 0x04000012 RID: 18
	private int stationaryFingerIndex = -1;

	// Token: 0x04000013 RID: 19
	private Material originalStationaryMaterial;

	// Token: 0x04000014 RID: 20
	private Material originalHoverMaterial;

	// Token: 0x04000015 RID: 21
	private ParticleEmitter stationaryParticleEmitter;
}
