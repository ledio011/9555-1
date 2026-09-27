using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000095 RID: 149
	public sealed class Input
	{
		// Token: 0x060006DF RID: 1759
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool GetKeyInt(int key);

		// Token: 0x060006E0 RID: 1760
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool GetKeyUpInt(int key);

		// Token: 0x060006E1 RID: 1761
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool GetKeyDownInt(int key);

		// Token: 0x060006E2 RID: 1762
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetAxis(string axisName);

		// Token: 0x060006E3 RID: 1763
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern float GetAxisRaw(string axisName);

		// Token: 0x060006E4 RID: 1764
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetButton(string buttonName);

		// Token: 0x060006E5 RID: 1765
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetButtonDown(string buttonName);

		// Token: 0x060006E6 RID: 1766 RVA: 0x00011224 File Offset: 0x0000F424
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt((int)key);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001122C File Offset: 0x0000F42C
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt((int)key);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00011234 File Offset: 0x0000F434
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUpInt((int)key);
		}

		// Token: 0x060006E9 RID: 1769
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x060006EA RID: 1770
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x060006EB RID: 1771
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x060006EC RID: 1772
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void ResetInputAxes();

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001123C File Offset: 0x0000F43C
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 result;
				Input.INTERNAL_get_mousePosition(out result);
				return result;
			}
		}

		// Token: 0x060006EE RID: 1774
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_mousePosition(out Vector3 value);

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00011254 File Offset: 0x0000F454
		public static Vector3 mouseScrollDelta
		{
			get
			{
				Vector3 result;
				Input.INTERNAL_get_mouseScrollDelta(out result);
				return result;
			}
		}

		// Token: 0x060006F0 RID: 1776
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_mouseScrollDelta(out Vector3 value);

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001126C File Offset: 0x0000F46C
		public static bool mousePresent
		{
			get
			{
				return !Input.touchSupported;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006F2 RID: 1778
		public static extern bool anyKeyDown { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00011278 File Offset: 0x0000F478
		public static Vector3 acceleration
		{
			get
			{
				Vector3 result;
				Input.INTERNAL_get_acceleration(out result);
				return result;
			}
		}

		// Token: 0x060006F4 RID: 1780
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_acceleration(out Vector3 value);

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00011290 File Offset: 0x0000F490
		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		// Token: 0x060006F6 RID: 1782
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Touch GetTouch(int index);

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006F7 RID: 1783
		public static extern int touchCount { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public static bool touchSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000175 RID: 373
		// (set) Token: 0x060006F9 RID: 1785
		public static extern IMECompositionMode imeCompositionMode { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006FA RID: 1786
		public static extern string compositionString { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000177 RID: 375
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public static Vector2 compositionCursorPos
		{
			set
			{
				Input.INTERNAL_set_compositionCursorPos(ref value);
			}
		}

		// Token: 0x060006FC RID: 1788
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_compositionCursorPos(ref Vector2 value);

		// Token: 0x040001B3 RID: 435
		private static Gyroscope m_MainGyro;

		// Token: 0x040001B4 RID: 436
		private static LocationService locationServiceInstance;

		// Token: 0x040001B5 RID: 437
		private static Compass compassInstance;
	}
}
