using System;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	public class CanvasUpdateRegistry
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005AE8 File Offset: 0x00003CE8
		protected CanvasUpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdate;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005B2C File Offset: 0x00003D2C
		public static CanvasUpdateRegistry instance
		{
			get
			{
				if (CanvasUpdateRegistry.s_Instance == null)
				{
					CanvasUpdateRegistry.s_Instance = new CanvasUpdateRegistry();
				}
				return CanvasUpdateRegistry.s_Instance;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005B48 File Offset: 0x00003D48
		private bool ObjectValidForUpdate(ICanvasElement element)
		{
			bool result = element != null;
			bool flag = element is Object;
			if (flag)
			{
				result = (element as Object != null);
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005B7C File Offset: 0x00003D7C
		private void PerformUpdate()
		{
			this.m_LayoutRebuildQueue.RemoveAll((ICanvasElement x) => x == null || x.IsDestroyed());
			this.m_GraphicRebuildQueue.RemoveAll((ICanvasElement x) => x == null || x.IsDestroyed());
			this.m_PerformingLayoutUpdate = true;
			this.m_LayoutRebuildQueue.Sort(CanvasUpdateRegistry.s_SortLayoutFunction);
			for (int i = 0; i <= 2; i++)
			{
				for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
				{
					try
					{
						if (this.ObjectValidForUpdate(CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j]))
						{
							CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j].Rebuild((CanvasUpdate)i);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception, CanvasUpdateRegistry.instance.m_LayoutRebuildQueue[j].transform);
					}
				}
			}
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Clear();
			this.m_PerformingLayoutUpdate = false;
			this.m_PerformingGraphicUpdate = true;
			for (int k = 3; k < 5; k++)
			{
				for (int l = 0; l < CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Count; l++)
				{
					try
					{
						ICanvasElement canvasElement = CanvasUpdateRegistry.instance.m_GraphicRebuildQueue[l];
						if (this.ObjectValidForUpdate(canvasElement))
						{
							canvasElement.Rebuild((CanvasUpdate)k);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2, CanvasUpdateRegistry.instance.m_GraphicRebuildQueue[l].transform);
					}
				}
			}
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Clear();
			this.m_PerformingGraphicUpdate = false;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005D4C File Offset: 0x00003F4C
		private static int ParentCount(Transform child)
		{
			if (child == null)
			{
				return 0;
			}
			Transform parent = child.parent;
			int num = 0;
			while (parent != null)
			{
				num++;
				parent = parent.parent;
			}
			return num;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005D90 File Offset: 0x00003F90
		private static int SortLayoutList(ICanvasElement x, ICanvasElement y)
		{
			Transform transform = x.transform;
			Transform transform2 = y.transform;
			return CanvasUpdateRegistry.ParentCount(transform) - CanvasUpdateRegistry.ParentCount(transform2);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005DC8 File Offset: 0x00003FC8
		private void InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			this.m_LayoutRebuildQueue.Add(element);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005DE8 File Offset: 0x00003FE8
		private void InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to add {0} for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.", element));
				return;
			}
			this.m_GraphicRebuildQueue.Add(element);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005E14 File Offset: 0x00004014
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			CanvasUpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005E2C File Offset: 0x0000402C
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			if (this.m_PerformingLayoutUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			CanvasUpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005E5C File Offset: 0x0000405C
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			if (this.m_PerformingGraphicUpdate)
			{
				Debug.LogError(string.Format("Trying to remove {0} from rebuild list while we are already inside a rebuild loop. This is not supported.", element));
				return;
			}
			CanvasUpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005E8C File Offset: 0x0000408C
		public static bool IsRebuildingLayout()
		{
			return CanvasUpdateRegistry.instance.m_PerformingLayoutUpdate;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005E98 File Offset: 0x00004098
		public static bool IsRebuildingGraphics()
		{
			return CanvasUpdateRegistry.instance.m_PerformingGraphicUpdate;
		}

		// Token: 0x040000C6 RID: 198
		private static CanvasUpdateRegistry s_Instance;

		// Token: 0x040000C7 RID: 199
		private bool m_PerformingLayoutUpdate;

		// Token: 0x040000C8 RID: 200
		private bool m_PerformingGraphicUpdate;

		// Token: 0x040000C9 RID: 201
		private readonly IndexedSet<ICanvasElement> m_LayoutRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x040000CA RID: 202
		private readonly IndexedSet<ICanvasElement> m_GraphicRebuildQueue = new IndexedSet<ICanvasElement>();

		// Token: 0x040000CB RID: 203
		private static readonly Comparison<ICanvasElement> s_SortLayoutFunction = new Comparison<ICanvasElement>(CanvasUpdateRegistry.SortLayoutList);
	}
}
