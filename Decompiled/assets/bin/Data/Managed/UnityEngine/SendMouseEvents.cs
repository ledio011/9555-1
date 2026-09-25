using System;

namespace UnityEngine
{
	// Token: 0x020000FA RID: 250
	internal class SendMouseEvents
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x00014E34 File Offset: 0x00013034
		[NotRenamed]
		private static void DoSendMouseEvents(int mouseUsed, int skipRTCameras)
		{
			Vector3 mousePosition = Input.mousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			if (SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != allCamerasCount)
			{
				SendMouseEvents.m_Cameras = new Camera[allCamerasCount];
			}
			int allCameras = Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			for (int i = 0; i < SendMouseEvents.m_CurrentHit.Length; i++)
			{
				SendMouseEvents.m_CurrentHit[i] = default(SendMouseEvents.HitInfo);
			}
			if (mouseUsed == 0)
			{
				for (int j = 0; j < allCameras; j++)
				{
					Camera camera = SendMouseEvents.m_Cameras[j];
					if (!(camera == null) && (skipRTCameras == 0 || !(camera.targetTexture != null)))
					{
						if (camera.pixelRect.Contains(mousePosition))
						{
							GUILayer component = camera.GetComponent<GUILayer>();
							if (component)
							{
								GUIElement guielement = component.HitTest(mousePosition);
								if (guielement)
								{
									SendMouseEvents.m_CurrentHit[0].target = guielement.gameObject;
									SendMouseEvents.m_CurrentHit[0].camera = camera;
								}
								else
								{
									SendMouseEvents.m_CurrentHit[0].target = null;
									SendMouseEvents.m_CurrentHit[0].camera = null;
								}
							}
							if (camera.eventMask != 0)
							{
								Ray ray = camera.ScreenPointToRay(mousePosition);
								float z = ray.direction.z;
								float num = (!Mathf.Approximately(0f, z)) ? Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z) : float.PositiveInfinity;
								RaycastHit raycastHit;
								if (Physics.Raycast(ray, out raycastHit, num + 1f, camera.cullingMask & camera.eventMask & -5))
								{
									SendMouseEvents.m_CurrentHit[1].camera = camera;
									SendMouseEvents.m_CurrentHit[1].target = ((!raycastHit.rigidbody) ? raycastHit.collider.gameObject : raycastHit.rigidbody.gameObject);
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[1].target = null;
									SendMouseEvents.m_CurrentHit[1].camera = null;
								}
								if (Physics2D.GetRayIntersectionNonAlloc(ray, SendMouseEvents.m_MouseRayHits2D, num, camera.cullingMask & camera.eventMask & -5) == 1)
								{
									SendMouseEvents.m_CurrentHit[2].camera = camera;
									SendMouseEvents.m_CurrentHit[2].target = ((!SendMouseEvents.m_MouseRayHits2D[0].rigidbody) ? SendMouseEvents.m_MouseRayHits2D[0].collider.gameObject : SendMouseEvents.m_MouseRayHits2D[0].rigidbody.gameObject);
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[2].target = null;
									SendMouseEvents.m_CurrentHit[2].camera = null;
								}
							}
						}
					}
				}
			}
			for (int k = 0; k < SendMouseEvents.m_CurrentHit.Length; k++)
			{
				SendMouseEvents.SendEvents(k, SendMouseEvents.m_CurrentHit[k]);
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000151B4 File Offset: 0x000133B4
		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			if (mouseButtonDown)
			{
				if (hit)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else if (!mouseButton)
			{
				if (SendMouseEvents.m_MouseDownHit[i])
				{
					if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]))
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
					}
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
					SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
				}
			}
			else if (SendMouseEvents.m_MouseDownHit[i])
			{
				SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
			}
			if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]))
			{
				if (hit)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				if (SendMouseEvents.m_LastHit[i])
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				if (hit)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}

		// Token: 0x040003AD RID: 941
		private const int m_HitIndexGUI = 0;

		// Token: 0x040003AE RID: 942
		private const int m_HitIndexPhysics3D = 1;

		// Token: 0x040003AF RID: 943
		private const int m_HitIndexPhysics2D = 2;

		// Token: 0x040003B0 RID: 944
		private static readonly SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x040003B1 RID: 945
		private static readonly SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x040003B2 RID: 946
		private static readonly SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x040003B3 RID: 947
		private static readonly RaycastHit2D[] m_MouseRayHits2D = new RaycastHit2D[]
		{
			default(RaycastHit2D)
		};

		// Token: 0x040003B4 RID: 948
		private static Camera[] m_Cameras;

		// Token: 0x020000FB RID: 251
		private struct HitInfo
		{
			// Token: 0x06000981 RID: 2433 RVA: 0x00015358 File Offset: 0x00013558
			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x00015368 File Offset: 0x00013568
			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x00015398 File Offset: 0x00013598
			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			// Token: 0x040003B5 RID: 949
			public GameObject target;

			// Token: 0x040003B6 RID: 950
			public Camera camera;
		}
	}
}
