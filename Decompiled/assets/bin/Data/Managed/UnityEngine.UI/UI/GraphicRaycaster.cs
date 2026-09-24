using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200004B RID: 75
	[RequireComponent(typeof(Canvas))]
	[AddComponentMenu("Event/Graphic Raycaster")]
	public class GraphicRaycaster : BaseRaycaster
	{
		// Token: 0x06000214 RID: 532 RVA: 0x000074D0 File Offset: 0x000056D0
		protected GraphicRaycaster()
		{
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007504 File Offset: 0x00005704
		public override int sortOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007528 File Offset: 0x00005728
		public override int renderOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000754C File Offset: 0x0000574C
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00007554 File Offset: 0x00005754
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00007560 File Offset: 0x00005760
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00007568 File Offset: 0x00005768
		public GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return this.m_BlockingObjects;
			}
			set
			{
				this.m_BlockingObjects = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007574 File Offset: 0x00005774
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000075A0 File Offset: 0x000057A0
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			Vector2 vector;
			if (this.eventCamera == null)
			{
				vector = new Vector2(eventData.position.x / (float)Screen.width, eventData.position.y / (float)Screen.height);
			}
			else
			{
				vector = this.eventCamera.ScreenToViewportPoint(eventData.position);
			}
			if (vector.x < 0f || vector.x > 1f || vector.y < 0f || vector.y > 1f)
			{
				return;
			}
			float num = float.MaxValue;
			Ray ray = default(Ray);
			if (this.eventCamera != null)
			{
				ray = this.eventCamera.ScreenPointToRay(eventData.position);
			}
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && this.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float num2 = this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane;
				RaycastHit raycastHit;
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && Physics.Raycast(ray, out raycastHit, num2, this.m_BlockingMask))
				{
					num = raycastHit.distance;
				}
				if (this.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All)
				{
					RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, num2, this.m_BlockingMask);
					if (raycastHit2D.collider != null)
					{
						num = raycastHit2D.fraction * num2;
					}
				}
			}
			this.m_RaycastResults.Clear();
			GraphicRaycaster.Raycast(this.canvas, this.eventCamera, eventData.position, this.m_RaycastResults);
			for (int i = 0; i < this.m_RaycastResults.Count; i++)
			{
				GameObject gameObject = this.m_RaycastResults[i].gameObject;
				bool flag = true;
				if (this.ignoreReversedGraphics)
				{
					if (this.eventCamera == null)
					{
						Vector3 rhs = gameObject.transform.rotation * Vector3.forward;
						flag = (Vector3.Dot(Vector3.forward, rhs) > 0f);
					}
					else
					{
						Vector3 lhs = this.eventCamera.transform.rotation * Vector3.forward;
						Vector3 rhs2 = gameObject.transform.rotation * Vector3.forward;
						flag = (Vector3.Dot(lhs, rhs2) > 0f);
					}
				}
				if (flag)
				{
					float num3;
					if (this.eventCamera == null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						num3 = 0f;
					}
					else
					{
						Transform transform = gameObject.transform;
						Vector3 forward = transform.forward;
						num3 = Vector3.Dot(forward, transform.position - ray.origin) / Vector3.Dot(forward, ray.direction);
						if (num3 < 0f)
						{
							goto IL_3B8;
						}
					}
					if (num3 < num)
					{
						RaycastResult item = new RaycastResult
						{
							gameObject = gameObject,
							module = this,
							distance = num3,
							screenPosition = eventData.position,
							index = (float)resultAppendList.Count,
							depth = this.m_RaycastResults[i].depth,
							sortingLayer = this.canvas.cachedSortingLayerValue,
							sortingOrder = this.canvas.sortingOrder
						};
						resultAppendList.Add(item);
					}
				}
				IL_3B8:;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00007980 File Offset: 0x00005B80
		public override Camera eventCamera
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || (this.canvas.renderMode == RenderMode.ScreenSpaceCamera && this.canvas.worldCamera == null))
				{
					return null;
				}
				return (!(this.canvas.worldCamera != null)) ? Camera.main : this.canvas.worldCamera;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000079F4 File Offset: 0x00005BF4
		private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, List<Graphic> results)
		{
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			GraphicRaycaster.s_SortedGraphics.Clear();
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth != -1)
				{
					if (RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera))
					{
						if (graphic.Raycast(pointerPosition, eventCamera))
						{
							GraphicRaycaster.s_SortedGraphics.Add(graphic);
						}
					}
				}
			}
			GraphicRaycaster.s_SortedGraphics.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			for (int j = 0; j < GraphicRaycaster.s_SortedGraphics.Count; j++)
			{
				results.Add(GraphicRaycaster.s_SortedGraphics[j]);
			}
		}

		// Token: 0x0400010E RID: 270
		protected const int kNoEventMaskSet = -1;

		// Token: 0x0400010F RID: 271
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics = true;

		// Token: 0x04000110 RID: 272
		[SerializeField]
		[FormerlySerializedAs("blockingObjects")]
		private GraphicRaycaster.BlockingObjects m_BlockingObjects;

		// Token: 0x04000111 RID: 273
		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		// Token: 0x04000112 RID: 274
		private Canvas m_Canvas;

		// Token: 0x04000113 RID: 275
		[NonSerialized]
		private List<Graphic> m_RaycastResults = new List<Graphic>();

		// Token: 0x04000114 RID: 276
		[NonSerialized]
		private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

		// Token: 0x0200004C RID: 76
		public enum BlockingObjects
		{
			// Token: 0x04000117 RID: 279
			None,
			// Token: 0x04000118 RID: 280
			TwoD,
			// Token: 0x04000119 RID: 281
			ThreeD,
			// Token: 0x0400011A RID: 282
			All
		}
	}
}
