using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E4 RID: 228
	public sealed class RectTransformUtility
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x000147C0 File Offset: 0x000129C0
		public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
		{
			return RectTransformUtility.INTERNAL_CALL_RectangleContainsScreenPoint(rect, ref screenPoint, cam);
		}

		// Token: 0x0600090C RID: 2316
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool INTERNAL_CALL_RectangleContainsScreenPoint(RectTransform rect, ref Vector2 screenPoint, Camera cam);

		// Token: 0x0600090D RID: 2317 RVA: 0x000147CC File Offset: 0x000129CC
		public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
		{
			Vector2 result;
			RectTransformUtility.PixelAdjustPoint(point, elementTransform, canvas, out result);
			return result;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000147E4 File Offset: 0x000129E4
		private static void PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 output)
		{
			RectTransformUtility.INTERNAL_CALL_PixelAdjustPoint(ref point, elementTransform, canvas, out output);
		}

		// Token: 0x0600090F RID: 2319
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_PixelAdjustPoint(ref Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 output);

		// Token: 0x06000910 RID: 2320
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas);

		// Token: 0x06000911 RID: 2321 RVA: 0x000147F0 File Offset: 0x000129F0
		public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
		{
			worldPoint = Vector2.zero;
			Ray ray = RectTransformUtility.ScreenPointToRay(cam, screenPoint);
			Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
			float distance;
			if (!plane.Raycast(ray, out distance))
			{
				return false;
			}
			worldPoint = ray.GetPoint(distance);
			return true;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00014854 File Offset: 0x00012A54
		public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
		{
			localPoint = Vector2.zero;
			Vector3 position;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out position))
			{
				localPoint = rect.InverseTransformPoint(position);
				return true;
			}
			return false;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00014890 File Offset: 0x00012A90
		public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
		{
			if (cam != null)
			{
				return cam.ScreenPointToRay(screenPos);
			}
			Vector3 origin = screenPos;
			origin.z -= 100f;
			return new Ray(origin, Vector3.forward);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000148DC File Offset: 0x00012ADC
		public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
		{
			if (rect == null)
			{
				return;
			}
			if (recursive)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					RectTransform rectTransform = rect.GetChild(i) as RectTransform;
					if (rectTransform != null)
					{
						RectTransformUtility.FlipLayoutOnAxis(rectTransform, axis, false, true);
					}
				}
			}
			Vector2 pivot = rect.pivot;
			pivot[axis] = 1f - pivot[axis];
			rect.pivot = pivot;
			if (keepPositioning)
			{
				return;
			}
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = -anchoredPosition[axis];
			rect.anchoredPosition = anchoredPosition;
			Vector2 anchorMin = rect.anchorMin;
			Vector2 anchorMax = rect.anchorMax;
			float num = anchorMin[axis];
			anchorMin[axis] = 1f - anchorMax[axis];
			anchorMax[axis] = 1f - num;
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000149D0 File Offset: 0x00012BD0
		public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
		{
			if (rect == null)
			{
				return;
			}
			if (recursive)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					RectTransform rectTransform = rect.GetChild(i) as RectTransform;
					if (rectTransform != null)
					{
						RectTransformUtility.FlipLayoutAxes(rectTransform, false, true);
					}
				}
			}
			rect.pivot = RectTransformUtility.GetTransposed(rect.pivot);
			rect.sizeDelta = RectTransformUtility.GetTransposed(rect.sizeDelta);
			if (keepPositioning)
			{
				return;
			}
			rect.anchoredPosition = RectTransformUtility.GetTransposed(rect.anchoredPosition);
			rect.anchorMin = RectTransformUtility.GetTransposed(rect.anchorMin);
			rect.anchorMax = RectTransformUtility.GetTransposed(rect.anchorMax);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00014A84 File Offset: 0x00012C84
		private static Vector2 GetTransposed(Vector2 input)
		{
			return new Vector2(input.y, input.x);
		}

		// Token: 0x04000361 RID: 865
		private static Vector3[] s_Corners = new Vector3[4];
	}
}
