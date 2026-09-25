using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
[AddComponentMenu("FingerGestures/Toolbox/Camera/Look At Tap")]
[RequireComponent(typeof(TapRecognizer))]
public class TBLookAtTap : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x00007850 File Offset: 0x00005A50
	private void Awake()
	{
		this.dragView = base.GetComponent<TBDragView>();
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x00007860 File Offset: 0x00005A60
	private void Start()
	{
		if (!base.GetComponent<TapRecognizer>())
		{
			Debug.LogWarning("No tap recognizer found on " + base.name + ". Disabling TBLookAtTap.");
			base.enabled = false;
		}
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000078A0 File Offset: 0x00005AA0
	private void OnTap(TapGesture gesture)
	{
		Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			if (this.dragView)
			{
				this.dragView.LookAt(raycastHit.point);
			}
			else
			{
				base.transform.LookAt(raycastHit.point);
			}
		}
	}

	// Token: 0x04000125 RID: 293
	private TBDragView dragView;
}
