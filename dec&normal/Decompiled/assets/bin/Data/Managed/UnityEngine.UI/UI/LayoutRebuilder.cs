using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x0200006F RID: 111
	public struct LayoutRebuilder : IEquatable<LayoutRebuilder>, ICanvasElement
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		private LayoutRebuilder(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = this.m_ToRebuild.GetHashCode();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E708 File Offset: 0x0000C908
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E71C File Offset: 0x0000C91C
		void ICanvasElement.Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000E800 File Offset: 0x0000CA00
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && (!(e as Behaviour).enabled || !(e as Behaviour).isActiveAndEnabled));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000E828 File Offset: 0x0000CA28
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ComponentListPool.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						action(list[j]);
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000E908 File Offset: 0x0000CB08
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ComponentListPool.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null)
			{
				return;
			}
			RectTransform rectTransform = rect;
			for (;;)
			{
				RectTransform rectTransform2 = rectTransform.parent as RectTransform;
				if (!LayoutRebuilder.ValidLayoutGroup(rectTransform2))
				{
					break;
				}
				rectTransform = rectTransform2;
			}
			if (rectTransform == rect && !LayoutRebuilder.ValidController(rectTransform))
			{
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000EA00 File Offset: 0x0000CC00
		private static bool ValidLayoutGroup(RectTransform parent)
		{
			if (parent == null)
			{
				return false;
			}
			List<Component> list = ComponentListPool.Get();
			parent.GetComponents(typeof(ILayoutGroup), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			bool result = list.Count > 0;
			ComponentListPool.Release(list);
			return result;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		private static bool ValidController(RectTransform layoutRoot)
		{
			if (layoutRoot == null)
			{
				return false;
			}
			List<Component> list = ComponentListPool.Get();
			layoutRoot.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			bool result = list.Count > 0;
			ComponentListPool.Release(list);
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000EA98 File Offset: 0x0000CC98
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(new LayoutRebuilder(controller));
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		public bool Equals(LayoutRebuilder other)
		{
			return this.m_ToRebuild == other.m_ToRebuild;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		public override string ToString()
		{
			return "(Layout Rebuilder for) " + this.m_ToRebuild;
		}

		// Token: 0x040001B0 RID: 432
		private readonly RectTransform m_ToRebuild;

		// Token: 0x040001B1 RID: 433
		private readonly int m_CachedHashFromTransform;
	}
}
