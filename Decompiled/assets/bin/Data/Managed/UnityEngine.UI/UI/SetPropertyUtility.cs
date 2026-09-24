using System;

namespace UnityEngine.UI
{
	// Token: 0x02000085 RID: 133
	internal static class SetPropertyUtility
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x000124A0 File Offset: 0x000106A0
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00012500 File Offset: 0x00010700
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00012524 File Offset: 0x00010724
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
