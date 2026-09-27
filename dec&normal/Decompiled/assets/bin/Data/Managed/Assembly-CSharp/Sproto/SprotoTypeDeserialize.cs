using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Sproto
{
	// Token: 0x02000812 RID: 2066
	public class SprotoTypeDeserialize
	{
		// Token: 0x060031A1 RID: 12705 RVA: 0x000C2024 File Offset: 0x000C0224
		public SprotoTypeDeserialize()
		{
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000C2034 File Offset: 0x000C0234
		public SprotoTypeDeserialize(byte[] data)
		{
			this.init(data, 0, 0);
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000C204C File Offset: 0x000C024C
		public SprotoTypeDeserialize(SprotoTypeReader reader)
		{
			this.init(reader);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000C2064 File Offset: 0x000C0264
		public void init(byte[] data, int offset = 0, int len = 0)
		{
			this.clear();
			this.reader = new SprotoTypeReader(data, offset, len);
			this.init();
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000C2080 File Offset: 0x000C0280
		public void init(SprotoTypeReader reader)
		{
			this.clear();
			this.reader = reader;
			this.init();
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000C2098 File Offset: 0x000C0298
		private void init()
		{
			this.fn = this.read_word();
			int num = SprotoTypeSize.sizeof_header + this.fn * SprotoTypeSize.sizeof_field;
			this.begin_data_pos = num;
			this.cur_field_pos = this.reader.Position;
			if (this.reader.Length < num)
			{
				SprotoTypeSize.error("invalid decode header.");
			}
			this.reader.Seek(this.begin_data_pos);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000C2108 File Offset: 0x000C0308
		private ulong expand64(uint v)
		{
			ulong num = (ulong)v;
			if ((num & (ulong)-2147483648) != 0UL)
			{
				num |= 18446744069414584320UL;
			}
			return num;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000C2134 File Offset: 0x000C0334
		private int read_word()
		{
			return (int)this.reader.ReadByte() | (int)this.reader.ReadByte() << 8;
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000C2150 File Offset: 0x000C0350
		private uint read_dword()
		{
			return (uint)((int)this.reader.ReadByte() | (int)this.reader.ReadByte() << 8 | (int)this.reader.ReadByte() << 16 | (int)this.reader.ReadByte() << 24);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000C2194 File Offset: 0x000C0394
		private uint read_array_size()
		{
			if (this.value >= 0)
			{
				SprotoTypeSize.error("invalid array value.");
			}
			uint num = this.read_dword();
			if (num < 0U)
			{
				SprotoTypeSize.error("error array size(" + num + ")");
			}
			return num;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000C21E0 File Offset: 0x000C03E0
		public int read_tag()
		{
			int position = this.reader.Position;
			this.reader.Seek(this.cur_field_pos);
			while (this.reader.Position < this.begin_data_pos)
			{
				this.tag++;
				int num = this.read_word();
				if ((num & 1) == 0)
				{
					this.cur_field_pos = this.reader.Position;
					this.reader.Seek(position);
					this.value = num / 2 - 1;
					return this.tag;
				}
				this.tag += num / 2;
			}
			this.reader.Seek(position);
			return -1;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000C2290 File Offset: 0x000C0490
		public long read_integer()
		{
			if (this.value >= 0)
			{
				return (long)this.value;
			}
			uint num = this.read_dword();
			if (num == 4U)
			{
				return (long)this.expand64(this.read_dword());
			}
			if (num == 8U)
			{
				uint num2 = this.read_dword();
				uint num3 = this.read_dword();
				return (long)((ulong)num2 | (ulong)num3 << 32);
			}
			SprotoTypeSize.error("read invalid integer size (" + num + ")");
			return 0L;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000C230C File Offset: 0x000C050C
		public List<long> read_integer_list()
		{
			List<long> list = null;
			uint num = this.read_array_size();
			if (num == 0U)
			{
				return new List<long>();
			}
			int num2 = (int)this.reader.ReadByte();
			num -= 1U;
			if (num2 == 4)
			{
				if (num % 4U != 0U)
				{
					SprotoTypeSize.error("error array size(" + num + ")@sizeof(Uint32)");
				}
				list = new List<long>();
				int num3 = 0;
				while ((long)num3 < (long)((ulong)(num / 4U)))
				{
					ulong num4 = this.expand64(this.read_dword());
					list.Add((long)num4);
					num3++;
				}
			}
			else if (num2 == 8)
			{
				if (num % 8U != 0U)
				{
					SprotoTypeSize.error("error array size(" + num + ")@sizeof(Uint64)");
				}
				list = new List<long>();
				int num5 = 0;
				while ((long)num5 < (long)((ulong)(num / 8U)))
				{
					uint num6 = this.read_dword();
					uint num7 = this.read_dword();
					ulong num8 = (ulong)num6 | (ulong)num7 << 32;
					list.Add((long)num8);
					num5++;
				}
			}
			else
			{
				SprotoTypeSize.error("error intlen(" + num2 + ")");
			}
			return list;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000C242C File Offset: 0x000C062C
		public bool read_boolean()
		{
			if (this.value < 0)
			{
				SprotoTypeSize.error("read invalid boolean.");
				return false;
			}
			return this.value != 0;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000C2464 File Offset: 0x000C0664
		public List<bool> read_boolean_list()
		{
			uint num = this.read_array_size();
			List<bool> list = new List<bool>();
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				bool flag = this.reader.ReadByte() != 0;
				list.Add(flag);
				num2++;
			}
			return list;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000C24B4 File Offset: 0x000C06B4
		public string read_string()
		{
			uint num = this.read_dword();
			byte[] array = new byte[num];
			this.reader.Read(array, 0, array.Length);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000C24EC File Offset: 0x000C06EC
		public List<string> read_string_list()
		{
			uint num = this.read_array_size();
			List<string> list = new List<string>();
			uint num2 = 0U;
			while (num > 0U)
			{
				if ((ulong)num < (ulong)((long)SprotoTypeSize.sizeof_length))
				{
					SprotoTypeSize.error("error array size.");
				}
				uint num3 = this.read_dword();
				num -= (uint)SprotoTypeSize.sizeof_length;
				if (num3 > num)
				{
					SprotoTypeSize.error("error array object.");
				}
				byte[] array = new byte[num3];
				this.reader.Read(array, 0, array.Length);
				string @string = Encoding.UTF8.GetString(array);
				list.Add(@string);
				num -= num3;
				num2 += 1U;
			}
			return list;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000C2588 File Offset: 0x000C0788
		public T read_obj<T>() where T : SprotoTypeBase, new()
		{
			int num = (int)this.read_dword();
			SprotoTypeReader sprotoTypeReader = new SprotoTypeReader(this.reader.Buffer, this.reader.Offset, num);
			this.reader.Seek(this.reader.Position + num);
			T result = Activator.CreateInstance<T>();
			result.init(sprotoTypeReader);
			return result;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000C25E8 File Offset: 0x000C07E8
		private T read_element<T>(SprotoTypeReader reader, uint sz, out uint read_size) where T : SprotoTypeBase, new()
		{
			read_size = 0U;
			if ((ulong)sz < (ulong)((long)SprotoTypeSize.sizeof_length))
			{
				SprotoTypeSize.error("error array size.");
			}
			uint num = this.read_dword();
			sz -= (uint)SprotoTypeSize.sizeof_length;
			read_size += (uint)SprotoTypeSize.sizeof_length;
			if (num > sz)
			{
				SprotoTypeSize.error("error array object.");
			}
			reader.Init(this.reader.Buffer, this.reader.Offset, (int)num);
			this.reader.Seek(this.reader.Position + (int)num);
			T result = Activator.CreateInstance<T>();
			result.init(reader);
			read_size += num;
			return result;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000C268C File Offset: 0x000C088C
		public List<T> read_obj_list<T>() where T : SprotoTypeBase, new()
		{
			uint num = this.read_array_size();
			List<T> list = new List<T>();
			SprotoTypeReader sprotoTypeReader = new SprotoTypeReader();
			uint num2 = 0U;
			while (num > 0U)
			{
				uint num3;
				list.Add(this.read_element<T>(sprotoTypeReader, num, out num3));
				num -= num3;
				num2 += 1U;
			}
			return list;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000C26D4 File Offset: 0x000C08D4
		public Dictionary<TK, TV> read_map<TK, TV>(SprotoTypeDeserialize.gen_key_func<TK, TV> func) where TV : SprotoTypeBase, new()
		{
			uint num = this.read_array_size();
			Dictionary<TK, TV> dictionary = new Dictionary<TK, TV>();
			SprotoTypeReader sprotoTypeReader = new SprotoTypeReader();
			uint num2 = 0U;
			while (num > 0U)
			{
				uint num3;
				TV tv = this.read_element<TV>(sprotoTypeReader, num, out num3);
				TK tk = func(tv);
				if (!dictionary.ContainsKey(tk))
				{
					dictionary.Add(tk, tv);
				}
				else
				{
					Debug.Log(tk.ToString() + " :key error:" + tv.ToString());
				}
				num -= num3;
				num2 += 1U;
			}
			return dictionary;
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000C2768 File Offset: 0x000C0968
		public void read_unknow_data()
		{
			if (this.value < 0)
			{
				int num = (int)this.read_dword();
				this.reader.Seek(num + this.reader.Position);
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000C27A0 File Offset: 0x000C09A0
		public int size()
		{
			return this.reader.Position;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000C27B0 File Offset: 0x000C09B0
		public void clear()
		{
			this.fn = 0;
			this.tag = -1;
			this.value = 0;
			if (this.reader != null)
			{
				this.reader.Seek(0);
			}
		}

		// Token: 0x04002140 RID: 8512
		private SprotoTypeReader reader;

		// Token: 0x04002141 RID: 8513
		private int begin_data_pos;

		// Token: 0x04002142 RID: 8514
		private int cur_field_pos;

		// Token: 0x04002143 RID: 8515
		private int fn;

		// Token: 0x04002144 RID: 8516
		private int tag = -1;

		// Token: 0x04002145 RID: 8517
		private int value;

		// Token: 0x02000ADB RID: 2779
		// (Invoke) Token: 0x06004FF5 RID: 20469
		public delegate TK gen_key_func<TK, TV>(TV v);
	}
}
