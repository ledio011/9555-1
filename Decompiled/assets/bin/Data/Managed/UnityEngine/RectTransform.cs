using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E0 RID: 224
	public sealed class RectTransform : Transform
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060008DD RID: 2269 RVA: 0x000141D0 File Offset: 0x000123D0
		// (remove) Token: 0x060008DE RID: 2270 RVA: 0x000141E8 File Offset: 0x000123E8
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00014200 File Offset: 0x00012400
		public Rect rect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rect(out result);
				return result;
			}
		}

		// Token: 0x060008E0 RID: 2272
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00014218 File Offset: 0x00012418
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00014230 File Offset: 0x00012430
		public Vector2 anchorMin
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchorMin(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchorMin(ref value);
			}
		}

		// Token: 0x060008E3 RID: 2275
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_anchorMin(out Vector2 value);

		// Token: 0x060008E4 RID: 2276
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_anchorMin(ref Vector2 value);

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001423C File Offset: 0x0001243C
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00014254 File Offset: 0x00012454
		public Vector2 anchorMax
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchorMax(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchorMax(ref value);
			}
		}

		// Token: 0x060008E7 RID: 2279
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_anchorMax(out Vector2 value);

		// Token: 0x060008E8 RID: 2280
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_anchorMax(ref Vector2 value);

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00014260 File Offset: 0x00012460
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00014298 File Offset: 0x00012498
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000142DC File Offset: 0x000124DC
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x000142F4 File Offset: 0x000124F4
		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchoredPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchoredPosition(ref value);
			}
		}

		// Token: 0x060008ED RID: 2285
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_anchoredPosition(out Vector2 value);

		// Token: 0x060008EE RID: 2286
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_anchoredPosition(ref Vector2 value);

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00014300 File Offset: 0x00012500
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00014318 File Offset: 0x00012518
		public Vector2 sizeDelta
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_sizeDelta(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_sizeDelta(ref value);
			}
		}

		// Token: 0x060008F1 RID: 2289
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_sizeDelta(out Vector2 value);

		// Token: 0x060008F2 RID: 2290
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_sizeDelta(ref Vector2 value);

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00014324 File Offset: 0x00012524
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0001433C File Offset: 0x0001253C
		public Vector2 pivot
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_pivot(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_pivot(ref value);
			}
		}

		// Token: 0x060008F5 RID: 2293
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_get_pivot(out Vector2 value);

		// Token: 0x060008F6 RID: 2294
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void INTERNAL_set_pivot(ref Vector2 value);

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008F7 RID: 2295
		// (set) Token: 0x060008F8 RID: 2296
		internal extern Object drivenByObject { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008F9 RID: 2297
		// (set) Token: 0x060008FA RID: 2298
		internal extern DrivenTransformProperties drivenProperties { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x060008FB RID: 2299 RVA: 0x00014348 File Offset: 0x00012548
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			if (RectTransform.reapplyDrivenProperties != null)
			{
				RectTransform.reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00014360 File Offset: 0x00012560
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null || fourCornersArray.Length < 4)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
				return;
			}
			Rect rect = this.rect;
			float x = rect.x;
			float y = rect.y;
			float xMax = rect.xMax;
			float yMax = rect.yMax;
			fourCornersArray[0] = new Vector3(x, y, 0f);
			fourCornersArray[1] = new Vector3(x, yMax, 0f);
			fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
			fourCornersArray[3] = new Vector3(xMax, y, 0f);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00014414 File Offset: 0x00012614
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null || fourCornersArray.Length < 4)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
				return;
			}
			this.GetLocalCorners(fourCornersArray);
			Transform transform = base.transform;
			for (int i = 0; i < 4; i++)
			{
				fourCornersArray[i] = transform.TransformPoint(fourCornersArray[i]);
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001447C File Offset: 0x0001267C
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 a = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			Transform parent = base.transform.parent;
			if (parent)
			{
				RectTransform component = parent.GetComponent<RectTransform>();
				if (component)
				{
					a += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += a.x;
			rect.y += a.y;
			return rect;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00014524 File Offset: 0x00012724
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x00014544 File Offset: 0x00012744
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x000145B0 File Offset: 0x000127B0
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x000145D8 File Offset: 0x000127D8
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00014644 File Offset: 0x00012844
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int index = (edge != RectTransform.Edge.Top && edge != RectTransform.Edge.Bottom) ? 0 : 1;
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float value = (float)((!flag) ? 0 : 1);
			Vector2 vector = this.anchorMin;
			vector[index] = value;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[index] = value;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[index] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[index] = ((!flag) ? (inset + size * this.pivot[index]) : (-inset - size * (1f - this.pivot[index])));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00014720 File Offset: 0x00012920
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00014778 File Offset: 0x00012978
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x020000E1 RID: 225
		public enum Axis
		{
			// Token: 0x0400035A RID: 858
			Horizontal,
			// Token: 0x0400035B RID: 859
			Vertical
		}

		// Token: 0x020000E2 RID: 226
		public enum Edge
		{
			// Token: 0x0400035D RID: 861
			Left,
			// Token: 0x0400035E RID: 862
			Right,
			// Token: 0x0400035F RID: 863
			Top,
			// Token: 0x04000360 RID: 864
			Bottom
		}

		// Token: 0x020000E3 RID: 227
		// (Invoke) Token: 0x06000907 RID: 2311
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
