using System;

namespace System
{
	// Token: 0x02000168 RID: 360
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		// Token: 0x06000D7A RID: 3450 RVA: 0x00033F9C File Offset: 0x0003219C
		public Nullable(T value)
		{
			this.has_value = true;
			this.value = value;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00033FAC File Offset: 0x000321AC
		public bool HasValue
		{
			get
			{
				return this.has_value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00033FB4 File Offset: 0x000321B4
		public T Value
		{
			get
			{
				if (!this.has_value)
				{
					throw new InvalidOperationException("Nullable object must have a value.");
				}
				return this.value;
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00033FD4 File Offset: 0x000321D4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return !this.has_value;
			}
			return other is T? && this.Equals((T?)other);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00034000 File Offset: 0x00032200
		private bool Equals(T? other)
		{
			return other.has_value == this.has_value && (!this.has_value || other.value.Equals(this.value));
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00034040 File Offset: 0x00032240
		public override int GetHashCode()
		{
			if (!this.has_value)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00034060 File Offset: 0x00032260
		public T GetValueOrDefault()
		{
			return (!this.has_value) ? default(T) : this.value;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003408C File Offset: 0x0003228C
		public T GetValueOrDefault(T defaultValue)
		{
			return (!this.has_value) ? defaultValue : this.value;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000340A8 File Offset: 0x000322A8
		public override string ToString()
		{
			if (this.has_value)
			{
				return this.value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000340CC File Offset: 0x000322CC
		private static object Box(T? o)
		{
			if (!o.has_value)
			{
				return null;
			}
			return o.value;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000340E8 File Offset: 0x000322E8
		private static T? Unbox(object o)
		{
			if (o == null)
			{
				return null;
			}
			return new T?((T)((object)o));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00034110 File Offset: 0x00032310
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00034118 File Offset: 0x00032318
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x04000583 RID: 1411
		internal T value;

		// Token: 0x04000584 RID: 1412
		internal bool has_value;
	}
}
