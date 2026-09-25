using System;
using System.Collections.Generic;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x02000008 RID: 8
	public class DispatcherCache
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000027F8 File Offset: 0x000009F8
		public Dispatcher Get(DispatcherKey key, DispatcherCache.DispatcherFactory factory)
		{
			Dispatcher dispatcher;
			if (!DispatcherCache._cache.TryGetValue(key, out dispatcher))
			{
				Dictionary<DispatcherKey, Dispatcher> cache = DispatcherCache._cache;
				lock (cache)
				{
					if (!DispatcherCache._cache.TryGetValue(key, out dispatcher))
					{
						dispatcher = factory();
						DispatcherCache._cache.Add(key, dispatcher);
					}
				}
			}
			return dispatcher;
		}

		// Token: 0x0400000C RID: 12
		private static Dictionary<DispatcherKey, Dispatcher> _cache = new Dictionary<DispatcherKey, Dispatcher>(DispatcherKey.EqualityComparer);

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000049 RID: 73
		public delegate Dispatcher DispatcherFactory();
	}
}
