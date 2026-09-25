using System;

namespace Sproto
{
	// Token: 0x02000813 RID: 2067
	public class SprotoTypeFieldOP
	{
		// Token: 0x060031B9 RID: 12729 RVA: 0x000C27EC File Offset: 0x000C09EC
		public SprotoTypeFieldOP(int max_field_count)
		{
			int num = max_field_count / SprotoTypeFieldOP.slot_bits_size;
			if (max_field_count % SprotoTypeFieldOP.slot_bits_size > 0)
			{
				num++;
			}
			this.has_bits = new uint[num];
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000C2830 File Offset: 0x000C0A30
		private int _get_array_idx(int bit_idx)
		{
			int num = this.has_bits.Length;
			return bit_idx / SprotoTypeFieldOP.slot_bits_size;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000C2850 File Offset: 0x000C0A50
		private int _get_slotbit_idx(int bit_idx)
		{
			int num = this.has_bits.Length;
			return bit_idx % SprotoTypeFieldOP.slot_bits_size;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000C2870 File Offset: 0x000C0A70
		public bool has_field(int field_idx)
		{
			int num = this._get_array_idx(field_idx);
			int num2 = this._get_slotbit_idx(field_idx);
			uint num3 = this.has_bits[num];
			uint num4 = 1U << num2;
			return Convert.ToBoolean(num3 & num4);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000C28A8 File Offset: 0x000C0AA8
		public void set_field(int field_idx, bool is_has)
		{
			int num = this._get_array_idx(field_idx);
			int num2 = this._get_slotbit_idx(field_idx);
			uint num3 = this.has_bits[num];
			if (is_has)
			{
				uint num4 = 1U << num2;
				this.has_bits[num] = (num3 | num4);
			}
			else
			{
				uint num5 = ~(1U << num2);
				this.has_bits[num] = (num3 & num5);
			}
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000C2900 File Offset: 0x000C0B00
		public void clear_field()
		{
			for (int i = 0; i < this.has_bits.Length; i++)
			{
				this.has_bits[i] = 0U;
			}
		}

		// Token: 0x04002146 RID: 8518
		private static readonly int slot_bits_size = 32;

		// Token: 0x04002147 RID: 8519
		public uint[] has_bits;
	}
}
