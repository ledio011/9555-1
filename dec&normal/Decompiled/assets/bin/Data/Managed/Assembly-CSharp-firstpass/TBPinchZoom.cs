using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(PinchRecognizer))]
[AddComponentMenu("FingerGestures/Toolbox/Camera/Pinch-Zoom")]
public class TBPinchZoom : MonoBehaviour
{
	// Token: 0x17000063 RID: 99
	// (get) Token: 0x060001DD RID: 477 RVA: 0x0000845C File Offset: 0x0000665C
	// (set) Token: 0x060001DE RID: 478 RVA: 0x00008464 File Offset: 0x00006664
	public Vector3 DefaultPos
	{
		get
		{
			return this.defaultPos;
		}
		set
		{
			this.defaultPos = value;
		}
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060001DF RID: 479 RVA: 0x00008470 File Offset: 0x00006670
	// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008478 File Offset: 0x00006678
	public float DefaultFov
	{
		get
		{
			return this.defaultFov;
		}
		set
		{
			this.defaultFov = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008484 File Offset: 0x00006684
	// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000848C File Offset: 0x0000668C
	public float DefaultOrthoSize
	{
		get
		{
			return this.defaultOrthoSize;
		}
		set
		{
			this.defaultOrthoSize = value;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008498 File Offset: 0x00006698
	// (set) Token: 0x060001E4 RID: 484 RVA: 0x000084A0 File Offset: 0x000066A0
	public float IdealZoomAmount
	{
		get
		{
			return this.idealZoomAmount;
		}
		set
		{
			this.idealZoomAmount = Mathf.Clamp(value, this.minZoomAmount, this.maxZoomAmount);
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x060001E5 RID: 485 RVA: 0x000084BC File Offset: 0x000066BC
	// (set) Token: 0x060001E6 RID: 486 RVA: 0x000084C4 File Offset: 0x000066C4
	public float ZoomAmount
	{
		get
		{
			return this.zoomAmount;
		}
		set
		{
			this.zoomAmount = Mathf.Clamp(value, this.minZoomAmount, this.maxZoomAmount);
			TBPinchZoom.ZoomMethod zoomMethod = this.zoomMethod;
			if (zoomMethod != TBPinchZoom.ZoomMethod.Position)
			{
				if (zoomMethod == TBPinchZoom.ZoomMethod.FOV)
				{
					if (base.camera.orthographic)
					{
						base.camera.orthographicSize = Mathf.Max(this.defaultOrthoSize - this.zoomAmount, 0.1f);
					}
					else
					{
						this.CameraFov = Mathf.Max(this.defaultFov - this.zoomAmount, 0.1f);
					}
				}
			}
			else
			{
				base.transform.position = this.defaultPos + this.zoomAmount * base.transform.forward;
			}
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000858C File Offset: 0x0000678C
	// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000859C File Offset: 0x0000679C
	private float CameraFov
	{
		get
		{
			return base.camera.fieldOfView;
		}
		set
		{
			base.camera.fieldOfView = value;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060001E9 RID: 489 RVA: 0x000085AC File Offset: 0x000067AC
	public float ZoomPercent
	{
		get
		{
			return (this.ZoomAmount - this.minZoomAmount) / (this.maxZoomAmount - this.minZoomAmount);
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x000085CC File Offset: 0x000067CC
	private void Start()
	{
		if (!base.GetComponent<PinchRecognizer>())
		{
			Debug.LogWarning("No pinch recognizer found on " + base.name + ". Disabling TBPinchZoom.");
			base.enabled = false;
		}
		this.SetDefaults();
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00008610 File Offset: 0x00006810
	private void Update()
	{
		this.ZoomAmount = Mathf.Lerp(this.ZoomAmount, this.IdealZoomAmount, Time.deltaTime * this.SmoothSpeed);
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00008640 File Offset: 0x00006840
	public void SetDefaults()
	{
		this.DefaultPos = base.transform.position;
		this.DefaultFov = this.CameraFov;
		this.DefaultOrthoSize = base.camera.orthographicSize;
	}

	// Token: 0x060001ED RID: 493 RVA: 0x0000867C File Offset: 0x0000687C
	private void OnPinch(PinchGesture gesture)
	{
		this.IdealZoomAmount += this.zoomSpeed * gesture.Delta.Centimeters();
	}

	// Token: 0x04000157 RID: 343
	public TBPinchZoom.ZoomMethod zoomMethod;

	// Token: 0x04000158 RID: 344
	public float zoomSpeed = 5f;

	// Token: 0x04000159 RID: 345
	public float minZoomAmount;

	// Token: 0x0400015A RID: 346
	public float maxZoomAmount = 50f;

	// Token: 0x0400015B RID: 347
	public float SmoothSpeed = 4f;

	// Token: 0x0400015C RID: 348
	private Vector3 defaultPos = Vector3.zero;

	// Token: 0x0400015D RID: 349
	private float defaultFov;

	// Token: 0x0400015E RID: 350
	private float defaultOrthoSize;

	// Token: 0x0400015F RID: 351
	private float idealZoomAmount;

	// Token: 0x04000160 RID: 352
	private float zoomAmount;

	// Token: 0x02000046 RID: 70
	public enum ZoomMethod
	{
		// Token: 0x04000162 RID: 354
		Position,
		// Token: 0x04000163 RID: 355
		FOV
	}
}
