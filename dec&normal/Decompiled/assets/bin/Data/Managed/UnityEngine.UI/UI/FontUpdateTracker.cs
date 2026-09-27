using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000049 RID: 73
	public static class FontUpdateTracker
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x000068FC File Offset: 0x00004AFC
		public static void TrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out list);
			if (list == null)
			{
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt += FontUpdateTracker.RebuildForFont;
				}
				list = new List<Text>();
				FontUpdateTracker.m_Tracked.Add(t.font, list);
			}
			if (!list.Contains(t))
			{
				list.Add(t);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006980 File Offset: 0x00004B80
		private static void RebuildForFont(Font f)
		{
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(f, out list);
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].FontTextureChanged();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000069C8 File Offset: 0x00004BC8
		public static void UntrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out list);
			if (list == null)
			{
				return;
			}
			list.Remove(t);
			if (list.Count == 0)
			{
				FontUpdateTracker.m_Tracked.Remove(t.font);
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt -= FontUpdateTracker.RebuildForFont;
				}
			}
		}

		// Token: 0x040000FD RID: 253
		private static Dictionary<Font, List<Text>> m_Tracked = new Dictionary<Font, List<Text>>();
	}
}
