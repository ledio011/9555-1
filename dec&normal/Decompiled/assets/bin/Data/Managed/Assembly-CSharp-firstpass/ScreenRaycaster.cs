using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[AddComponentMenu("FingerGestures/Components/Screen Raycaster")]
public class ScreenRaycaster : MonoBehaviour
{
	// Token: 0x0600008A RID: 138 RVA: 0x00003BB8 File Offset: 0x00001DB8
	private void Start()
	{
		if (this.Cameras == null || this.Cameras.Length == 0)
		{
			this.Cameras = new Camera[]
			{
				Camera.main
			};
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00003BF4 File Offset: 0x00001DF4
	public bool Raycast(Vector2 screenPos, out ScreenRaycastData hitData)
	{
		for (int i = 0; i < this.Cameras.Length; i++)
		{
			Camera camera = this.Cameras[i];
			if (camera && camera.enabled)
			{
				if (camera.gameObject.activeInHierarchy)
				{
					if (this.Raycast(camera, screenPos, out hitData))
					{
						return true;
					}
				}
			}
		}
		hitData = default(ScreenRaycastData);
		return false;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00003C6C File Offset: 0x00001E6C
	private bool Raycast(Camera cam, Vector2 screenPos, out ScreenRaycastData hitData)
	{
		Ray ray = cam.ScreenPointToRay(screenPos);
		bool flag = false;
		hitData = default(ScreenRaycastData);
		if (this.UsePhysics2D && cam.orthographic)
		{
			hitData.Hit2D = Physics2D.Raycast(ray.origin, Vector2.zero, float.PositiveInfinity, ~this.IgnoreLayerMask);
			if (hitData.Hit2D.collider)
			{
				hitData.Is2D = true;
				flag = true;
			}
		}
		if (!flag)
		{
			hitData.Is2D = false;
			if (this.RayThickness > 0f)
			{
				flag = Physics.SphereCast(ray, 0.5f * this.RayThickness, out hitData.Hit3D, float.PositiveInfinity, ~this.IgnoreLayerMask);
			}
			else
			{
				flag = Physics.Raycast(ray, out hitData.Hit3D, float.PositiveInfinity, ~this.IgnoreLayerMask);
			}
		}
		return flag;
	}

	// Token: 0x04000055 RID: 85
	public Camera[] Cameras;

	// Token: 0x04000056 RID: 86
	public LayerMask IgnoreLayerMask;

	// Token: 0x04000057 RID: 87
	public float RayThickness;

	// Token: 0x04000058 RID: 88
	public bool VisualizeRaycasts = true;

	// Token: 0x04000059 RID: 89
	public bool UsePhysics2D = true;
}
