using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000070 RID: 112
	public static class LayoutUtility
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		public static float GetMinSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetMinWidth(rect);
			}
			return LayoutUtility.GetMinHeight(rect);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000EB74 File Offset: 0x0000CD74
		public static float GetPreferredSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetPreferredWidth(rect);
			}
			return LayoutUtility.GetPreferredHeight(rect);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public static float GetFlexibleSize(RectTransform rect, int axis)
		{
			if (axis == 0)
			{
				return LayoutUtility.GetFlexibleWidth(rect);
			}
			return LayoutUtility.GetFlexibleHeight(rect);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public static float GetMinWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public static float GetPreferredWidth(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minWidth, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredWidth, 0f));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public static float GetFlexibleWidth(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleWidth, 0f);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000EC60 File Offset: 0x0000CE60
		public static float GetMinHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public static float GetPreferredHeight(RectTransform rect)
		{
			return Mathf.Max(LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.minHeight, 0f), LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.preferredHeight, 0f));
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public static float GetFlexibleHeight(RectTransform rect)
		{
			return LayoutUtility.GetLayoutProperty(rect, (ILayoutElement e) => e.flexibleHeight, 0f);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue)
		{
			ILayoutElement layoutElement;
			return LayoutUtility.GetLayoutProperty(rect, property, defaultValue, out layoutElement);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000ED34 File Offset: 0x0000CF34
		public static float GetLayoutProperty(RectTransform rect, Func<ILayoutElement, float> property, float defaultValue, out ILayoutElement source)
		{
			source = null;
			if (rect == null)
			{
				return 0f;
			}
			float num = defaultValue;
			int num2 = int.MinValue;
			List<Component> list = ComponentListPool.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			for (int i = 0; i < list.Count; i++)
			{
				ILayoutElement layoutElement = list[i] as ILayoutElement;
				if (!(layoutElement is Behaviour) || ((layoutElement as Behaviour).enabled && (layoutElement as Behaviour).isActiveAndEnabled))
				{
					int layoutPriority = layoutElement.layoutPriority;
					if (layoutPriority >= num2)
					{
						float num3 = property(layoutElement);
						if (num3 >= 0f)
						{
							if (layoutPriority > num2)
							{
								num = num3;
								num2 = layoutPriority;
								source = layoutElement;
							}
							else if (num3 > num)
							{
								num = num3;
								source = layoutElement;
							}
						}
					}
				}
			}
			ComponentListPool.Release(list);
			return num;
		}
	}
}
