using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins.Core
{
	// Token: 0x0200002B RID: 43
	internal static class PluginsManager
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004E5C File Offset: 0x0000305C
		internal static ABSTweenPlugin<T1, T2, TPlugOptions> GetDefaultPlugin<T1, T2, TPlugOptions>() where TPlugOptions : struct, IPlugOptions
		{
			Type typeFromHandle = typeof(T1);
			Type typeFromHandle2 = typeof(T2);
			ITweenPlugin tweenPlugin = null;
			if (typeFromHandle == typeof(Vector3) && typeFromHandle == typeFromHandle2)
			{
				if (PluginsManager._vector3Plugin == null)
				{
					PluginsManager._vector3Plugin = new Vector3Plugin();
				}
				tweenPlugin = PluginsManager._vector3Plugin;
			}
			else if (typeFromHandle == typeof(Vector3) && typeFromHandle2 == typeof(Vector3[]))
			{
				if (PluginsManager._vector3ArrayPlugin == null)
				{
					PluginsManager._vector3ArrayPlugin = new Vector3ArrayPlugin();
				}
				tweenPlugin = PluginsManager._vector3ArrayPlugin;
			}
			else if (typeFromHandle == typeof(Quaternion))
			{
				if (typeFromHandle2 == typeof(Quaternion))
				{
					Debugger.LogError("Quaternion tweens require a Vector3 endValue");
				}
				else
				{
					if (PluginsManager._quaternionPlugin == null)
					{
						PluginsManager._quaternionPlugin = new QuaternionPlugin();
					}
					tweenPlugin = PluginsManager._quaternionPlugin;
				}
			}
			else if (typeFromHandle == typeof(Vector2))
			{
				if (PluginsManager._vector2Plugin == null)
				{
					PluginsManager._vector2Plugin = new Vector2Plugin();
				}
				tweenPlugin = PluginsManager._vector2Plugin;
			}
			else if (typeFromHandle == typeof(float))
			{
				if (PluginsManager._floatPlugin == null)
				{
					PluginsManager._floatPlugin = new FloatPlugin();
				}
				tweenPlugin = PluginsManager._floatPlugin;
			}
			else if (typeFromHandle == typeof(Color))
			{
				if (PluginsManager._colorPlugin == null)
				{
					PluginsManager._colorPlugin = new ColorPlugin();
				}
				tweenPlugin = PluginsManager._colorPlugin;
			}
			else if (typeFromHandle == typeof(int))
			{
				if (PluginsManager._intPlugin == null)
				{
					PluginsManager._intPlugin = new IntPlugin();
				}
				tweenPlugin = PluginsManager._intPlugin;
			}
			else if (typeFromHandle == typeof(Vector4))
			{
				if (PluginsManager._vector4Plugin == null)
				{
					PluginsManager._vector4Plugin = new Vector4Plugin();
				}
				tweenPlugin = PluginsManager._vector4Plugin;
			}
			else if (typeFromHandle == typeof(Rect))
			{
				if (PluginsManager._rectPlugin == null)
				{
					PluginsManager._rectPlugin = new RectPlugin();
				}
				tweenPlugin = PluginsManager._rectPlugin;
			}
			else if (typeFromHandle == typeof(RectOffset))
			{
				if (PluginsManager._rectOffsetPlugin == null)
				{
					PluginsManager._rectOffsetPlugin = new RectOffsetPlugin();
				}
				tweenPlugin = PluginsManager._rectOffsetPlugin;
			}
			else if (typeFromHandle == typeof(uint))
			{
				if (PluginsManager._uintPlugin == null)
				{
					PluginsManager._uintPlugin = new UintPlugin();
				}
				tweenPlugin = PluginsManager._uintPlugin;
			}
			else if (typeFromHandle == typeof(string))
			{
				if (PluginsManager._stringPlugin == null)
				{
					PluginsManager._stringPlugin = new StringPlugin();
				}
				tweenPlugin = PluginsManager._stringPlugin;
			}
			else if (typeFromHandle == typeof(Color2))
			{
				if (PluginsManager._color2Plugin == null)
				{
					PluginsManager._color2Plugin = new Color2Plugin();
				}
				tweenPlugin = PluginsManager._color2Plugin;
			}
			else if (typeFromHandle == typeof(long))
			{
				if (PluginsManager._longPlugin == null)
				{
					PluginsManager._longPlugin = new LongPlugin();
				}
				tweenPlugin = PluginsManager._longPlugin;
			}
			else if (typeFromHandle == typeof(ulong))
			{
				if (PluginsManager._ulongPlugin == null)
				{
					PluginsManager._ulongPlugin = new UlongPlugin();
				}
				tweenPlugin = PluginsManager._ulongPlugin;
			}
			else if (typeFromHandle == typeof(double))
			{
				if (PluginsManager._doublePlugin == null)
				{
					PluginsManager._doublePlugin = new DoublePlugin();
				}
				tweenPlugin = PluginsManager._doublePlugin;
			}
			if (tweenPlugin != null)
			{
				return tweenPlugin as ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			return null;
		}

		// Token: 0x040000DB RID: 219
		private static ITweenPlugin _floatPlugin;

		// Token: 0x040000DC RID: 220
		private static ITweenPlugin _doublePlugin;

		// Token: 0x040000DD RID: 221
		private static ITweenPlugin _intPlugin;

		// Token: 0x040000DE RID: 222
		private static ITweenPlugin _uintPlugin;

		// Token: 0x040000DF RID: 223
		private static ITweenPlugin _longPlugin;

		// Token: 0x040000E0 RID: 224
		private static ITweenPlugin _ulongPlugin;

		// Token: 0x040000E1 RID: 225
		private static ITweenPlugin _vector2Plugin;

		// Token: 0x040000E2 RID: 226
		private static ITweenPlugin _vector3Plugin;

		// Token: 0x040000E3 RID: 227
		private static ITweenPlugin _vector4Plugin;

		// Token: 0x040000E4 RID: 228
		private static ITweenPlugin _quaternionPlugin;

		// Token: 0x040000E5 RID: 229
		private static ITweenPlugin _colorPlugin;

		// Token: 0x040000E6 RID: 230
		private static ITweenPlugin _rectPlugin;

		// Token: 0x040000E7 RID: 231
		private static ITweenPlugin _rectOffsetPlugin;

		// Token: 0x040000E8 RID: 232
		private static ITweenPlugin _stringPlugin;

		// Token: 0x040000E9 RID: 233
		private static ITweenPlugin _vector3ArrayPlugin;

		// Token: 0x040000EA RID: 234
		private static ITweenPlugin _color2Plugin;

		// Token: 0x040000EB RID: 235
		private const int _MaxCustomPlugins = 20;

		// Token: 0x040000EC RID: 236
		private static Dictionary<Type, ITweenPlugin> _customPlugins;
	}
}
