using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003FA RID: 1018
	public class inlay : SprotoTypeBase
	{
		// Token: 0x06001F80 RID: 8064 RVA: 0x0009C9A0 File Offset: 0x0009ABA0
		public inlay() : base(inlay.max_field_count)
		{
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0009C9B0 File Offset: 0x0009ABB0
		public inlay(byte[] buffer) : base(inlay.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0009C9CC File Offset: 0x0009ABCC
		// (set) Token: 0x06001F84 RID: 8068 RVA: 0x0009C9D4 File Offset: 0x0009ABD4
		public long index
		{
			get
			{
				return this._index;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._index = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x0009C9EC File Offset: 0x0009ABEC
		public bool HasIndex
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0009C9FC File Offset: 0x0009ABFC
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x0009CA04 File Offset: 0x0009AC04
		public string itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._itemId = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0009CA1C File Offset: 0x0009AC1C
		public bool HasItemId
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0009CA2C File Offset: 0x0009AC2C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.itemId = this.deserialize.read_string();
					}
				}
				else
				{
					this.index = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.index, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_string(this.itemId, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B36 RID: 6966
		private static int max_field_count = 2;

		// Token: 0x04001B37 RID: 6967
		private long _index;

		// Token: 0x04001B38 RID: 6968
		private string _itemId;
	}
}
