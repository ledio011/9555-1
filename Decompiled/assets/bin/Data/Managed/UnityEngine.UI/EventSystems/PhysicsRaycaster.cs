using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000021 RID: 33
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000344C File Offset: 0x0000164C
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003460 File Offset: 0x00001660
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				return this.m_EventCamera ?? Camera.main;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003494 File Offset: 0x00001694
		public virtual int depth
		{
			get
			{
				return (!(this.eventCamera != null)) ? 16777215 : ((int)this.eventCamera.depth);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000034C0 File Offset: 0x000016C0
		public int finalEventMask
		{
			get
			{
				return (!(this.eventCamera != null)) ? -1 : (this.eventCamera.cullingMask & this.m_EventMask);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000034F0 File Offset: 0x000016F0
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000034F8 File Offset: 0x000016F8
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003504 File Offset: 0x00001704
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.eventCamera == null)
			{
				return;
			}
			Ray ray = this.eventCamera.ScreenPointToRay(eventData.position);
			float distance = this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane;
			RaycastHit[] array = Physics.RaycastAll(ray, distance, this.finalEventMask);
			if (array.Length > 1)
			{
				Array.Sort<RaycastHit>(array, (RaycastHit r1, RaycastHit r2) => r1.distance.CompareTo(r2.distance));
			}
			if (array.Length != 0)
			{
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					RaycastResult item = new RaycastResult
					{
						gameObject = array[i].collider.gameObject,
						module = this,
						distance = array[i].distance,
						worldPosition = array[i].point,
						worldNormal = array[i].normal,
						screenPosition = eventData.position,
						index = (float)resultAppendList.Count
					};
					resultAppendList.Add(item);
					i++;
				}
			}
		}

		// Token: 0x04000042 RID: 66
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000043 RID: 67
		protected Camera m_EventCamera;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		protected LayerMask m_EventMask = -1;
	}
}
