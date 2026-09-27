using System;
using System.Collections.Generic;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x0200004D RID: 77
	public class GraphicRegistry
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00007AE8 File Offset: 0x00005CE8
		protected GraphicRegistry()
		{
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007B14 File Offset: 0x00005D14
		public static GraphicRegistry instance
		{
			get
			{
				if (GraphicRegistry.s_Instance == null)
				{
					GraphicRegistry.s_Instance = new GraphicRegistry();
				}
				return GraphicRegistry.s_Instance;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007B30 File Offset: 0x00005D30
		public static void RegisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet);
			if (indexedSet != null)
			{
				indexedSet.Add(graphic);
				return;
			}
			indexedSet = new IndexedSet<Graphic>();
			indexedSet.Add(graphic);
			GraphicRegistry.instance.m_Graphics.Add(c, indexedSet);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007B8C File Offset: 0x00005D8C
		public static void UnregisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.Remove(graphic);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public static IList<Graphic> GetGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> result;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(canvas, out result))
			{
				return result;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x0400011B RID: 283
		private static GraphicRegistry s_Instance;

		// Token: 0x0400011C RID: 284
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_Graphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x0400011D RID: 285
		private static readonly List<Graphic> s_EmptyList = new List<Graphic>();
	}
}
