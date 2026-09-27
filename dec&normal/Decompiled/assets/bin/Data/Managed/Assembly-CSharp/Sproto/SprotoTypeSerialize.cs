using System;
using System.Collections.Generic;
using System.Text;

namespace Sproto
{
	// Token: 0x02000815 RID: 2069
	public class SprotoTypeSerialize
	{
		// Token: 0x060031CB RID: 12747 RVA: 0x000C2A80 File Offset: 0x000C0C80
		public SprotoTypeSerialize(int max_field_count)
		{
			this.header_sz = SprotoTypeSize.sizeof_header + max_field_count * SprotoTypeSize.sizeof_field;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000C2AB0 File Offset: 0x000C0CB0
		private void set_header_fn(int fn)
		{
			this.data[this.header_idx - 2] = (byte)(fn & 255);
			this.data[this.header_idx - 1] = (byte)(fn >> 8 & 255);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000C2AF8 File Offset: 0x000C0CF8
		private void write_header_record(int record)
		{
			this.data[this.header_idx + this.header_cap - 2] = (byte)(record & 255);
			this.data[this.header_idx + this.header_cap - 1] = (byte)(record >> 8 & 255);
			this.header_cap += 2;
			this.index++;
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000C2B68 File Offset: 0x000C0D68
		private void write_uint32_to_uint64_sign(bool is_negative)
		{
			byte v = (!is_negative) ? 0 : byte.MaxValue;
			this.data.WriteByte(v);
			this.data.WriteByte(v);
			this.data.WriteByte(v);
			this.data.WriteByte(v);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000C2BB8 File Offset: 0x000C0DB8
		private void write_tag(int tag, int value)
		{
			int num = tag - this.lasttag - 1;
			if (num > 0)
			{
				num = (num - 1) * 2 + 1;
				if (num > 65535)
				{
					SprotoTypeSize.error("tag is too big.");
				}
				this.write_header_record(num);
			}
			this.write_header_record(value);
			this.lasttag = tag;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000C2C0C File Offset: 0x000C0E0C
		private void write_uint32(uint v)
		{
			this.data.WriteByte((byte)(v & 255U));
			this.data.WriteByte((byte)(v >> 8 & 255U));
			this.data.WriteByte((byte)(v >> 16 & 255U));
			this.data.WriteByte((byte)(v >> 24 & 255U));
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000C2C70 File Offset: 0x000C0E70
		private void write_uint64(ulong v)
		{
			this.data.WriteByte((byte)(v & 255UL));
			this.data.WriteByte((byte)(v >> 8 & 255UL));
			this.data.WriteByte((byte)(v >> 16 & 255UL));
			this.data.WriteByte((byte)(v >> 24 & 255UL));
			this.data.WriteByte((byte)(v >> 32 & 255UL));
			this.data.WriteByte((byte)(v >> 40 & 255UL));
			this.data.WriteByte((byte)(v >> 48 & 255UL));
			this.data.WriteByte((byte)(v >> 56 & 255UL));
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000C2D34 File Offset: 0x000C0F34
		private void fill_size(int sz)
		{
			if (sz < 0)
			{
				SprotoTypeSize.error("fill invaild size.");
			}
			this.write_uint32((uint)sz);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000C2D50 File Offset: 0x000C0F50
		private int encode_integer(uint v)
		{
			this.fill_size(4);
			this.write_uint32(v);
			return SprotoTypeSize.sizeof_length + 4;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000C2D68 File Offset: 0x000C0F68
		private int encode_uint64(ulong v)
		{
			this.fill_size(8);
			this.write_uint64(v);
			return SprotoTypeSize.sizeof_length + 8;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000C2D80 File Offset: 0x000C0F80
		private int encode_string(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.fill_size(bytes.Length);
			this.data.Write(bytes, 0, bytes.Length);
			return SprotoTypeSize.sizeof_length + bytes.Length;
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000C2DBC File Offset: 0x000C0FBC
		private int encode_struct(SprotoTypeBase obj)
		{
			int position = this.data.Position;
			this.data.Seek(SprotoTypeSize.sizeof_length, 1);
			int num = obj.encode(this.data);
			int position2 = this.data.Position;
			this.data.Seek(position, 0);
			this.fill_size(num);
			this.data.Seek(position2, 0);
			return SprotoTypeSize.sizeof_length + num;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000C2E2C File Offset: 0x000C102C
		private void clear()
		{
			this.index = 0;
			this.header_idx = 2;
			this.lasttag = -1;
			this.data = null;
			this.header_cap = SprotoTypeSize.sizeof_header;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000C2E58 File Offset: 0x000C1058
		public void write_integer(long integer, int tag)
		{
			long num = integer >> 31;
			int num2 = (num != 0L && num != -1L) ? 8 : 4;
			int value = 0;
			if (num2 == 4)
			{
				uint num3 = (uint)integer;
				if (num3 < 32767U)
				{
					value = (int)((num3 + 1U) * 2U);
				}
				else
				{
					num2 = this.encode_integer(num3);
				}
			}
			else if (num2 == 8)
			{
				num2 = this.encode_uint64((ulong)integer);
			}
			else
			{
				SprotoTypeSize.error("invaild integer size.");
			}
			this.write_tag(tag, value);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000C2EDC File Offset: 0x000C10DC
		public void write_integer(List<long> integer_list, int tag)
		{
			if (integer_list == null || integer_list.Count <= 0)
			{
				return;
			}
			int position = this.data.Position;
			this.data.Seek(position + SprotoTypeSize.sizeof_length, 0);
			int position2 = this.data.Position;
			int num = 4;
			this.data.Seek(position2 + 1, 0);
			for (int i = 0; i < integer_list.Count; i++)
			{
				long num2 = integer_list[i];
				long num3 = num2 >> 31;
				int num4 = (num3 != 0L && num3 != -1L) ? 8 : 4;
				if (num4 == 4)
				{
					this.write_uint32((uint)num2);
					if (num == 8)
					{
						bool is_negative = (num2 & (long)((ulong)int.MinValue)) != 0L;
						this.write_uint32_to_uint64_sign(is_negative);
					}
				}
				else if (num4 == 8)
				{
					if (num == 4)
					{
						this.data.Seek(position2 + 1, 0);
						for (int j = 0; j < i; j++)
						{
							ulong v = (ulong)integer_list[j];
							this.write_uint64(v);
						}
						num = 8;
					}
					this.write_uint64((ulong)num2);
				}
				else
				{
					SprotoTypeSize.error("invalid integer size(" + num4 + ")");
				}
			}
			int position3 = this.data.Position;
			this.data.Seek(position2, 0);
			this.data.WriteByte((byte)num);
			int sz = position3 - position2;
			this.data.Seek(position, 0);
			this.fill_size(sz);
			this.data.Seek(position3, 0);
			this.write_tag(tag, 0);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000C3084 File Offset: 0x000C1284
		public void write_boolean(bool b, int tag)
		{
			long integer = (!b) ? 0L : 1L;
			this.write_integer(integer, tag);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000C30A8 File Offset: 0x000C12A8
		public void write_boolean(List<bool> b_list, int tag)
		{
			if (b_list == null || b_list.Count <= 0)
			{
				return;
			}
			this.fill_size(b_list.Count);
			for (int i = 0; i < b_list.Count; i++)
			{
				byte v = (!b_list[i]) ? 0 : 1;
				this.data.WriteByte(v);
			}
			this.write_tag(tag, 0);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000C3114 File Offset: 0x000C1314
		public void write_string(string str, int tag)
		{
			this.encode_string(str);
			this.write_tag(tag, 0);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000C3128 File Offset: 0x000C1328
		public void write_string(List<string> str_list, int tag)
		{
			if (str_list == null || str_list.Count <= 0)
			{
				return;
			}
			int num = 0;
			foreach (string text in str_list)
			{
				num += SprotoTypeSize.sizeof_length + Encoding.UTF8.GetByteCount(text);
			}
			this.fill_size(num);
			foreach (string str in str_list)
			{
				this.encode_string(str);
			}
			this.write_tag(tag, 0);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000C3210 File Offset: 0x000C1410
		public void write_obj(SprotoTypeBase obj, int tag)
		{
			this.encode_struct(obj);
			this.write_tag(tag, 0);
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000C3224 File Offset: 0x000C1424
		private void write_set(SprotoTypeSerialize.write_func func, int tag)
		{
			int position = this.data.Position;
			this.data.Seek(SprotoTypeSize.sizeof_length, 1);
			func();
			int position2 = this.data.Position;
			int sz = position2 - position - SprotoTypeSize.sizeof_length;
			this.data.Seek(position, 0);
			this.fill_size(sz);
			this.data.Seek(position2, 0);
			this.write_tag(tag, 0);
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000C3298 File Offset: 0x000C1498
		public void write_obj<T>(List<T> obj_list, int tag) where T : SprotoTypeBase
		{
			if (obj_list == null || obj_list.Count <= 0)
			{
				return;
			}
			SprotoTypeSerialize.write_func func = delegate()
			{
				foreach (T t in obj_list)
				{
					SprotoTypeBase obj = t;
					this.encode_struct(obj);
				}
			};
			this.write_set(func, tag);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000C32EC File Offset: 0x000C14EC
		public void write_obj<TK, TV>(Dictionary<TK, TV> map, int tag) where TV : SprotoTypeBase
		{
			if (map == null || map.Count <= 0)
			{
				return;
			}
			SprotoTypeSerialize.write_func func = delegate()
			{
				foreach (KeyValuePair<TK, TV> keyValuePair in map)
				{
					this.encode_struct(keyValuePair.Value);
				}
			};
			this.write_set(func, tag);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000C3340 File Offset: 0x000C1540
		public void open(SprotoStream stream)
		{
			this.clear();
			this.data = stream;
			this.header_idx = stream.Position + this.header_cap;
			this.data_idx = this.data.Seek(this.header_sz, 1);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000C3388 File Offset: 0x000C1588
		public int close()
		{
			this.set_header_fn(this.index);
			int up_count = this.header_sz - this.header_cap;
			this.data.MoveUp(this.data_idx, up_count);
			int result = this.data.Position - this.header_idx + SprotoTypeSize.sizeof_header;
			this.clear();
			return result;
		}

		// Token: 0x0400214C RID: 8524
		private int header_idx;

		// Token: 0x0400214D RID: 8525
		private int header_sz;

		// Token: 0x0400214E RID: 8526
		private int header_cap = SprotoTypeSize.sizeof_header;

		// Token: 0x0400214F RID: 8527
		private SprotoStream data;

		// Token: 0x04002150 RID: 8528
		private int data_idx;

		// Token: 0x04002151 RID: 8529
		private int lasttag = -1;

		// Token: 0x04002152 RID: 8530
		private int index;

		// Token: 0x02000ADC RID: 2780
		// (Invoke) Token: 0x06004FF9 RID: 20473
		private delegate void write_func();
	}
}
