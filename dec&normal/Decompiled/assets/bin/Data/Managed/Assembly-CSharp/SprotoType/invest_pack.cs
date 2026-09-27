using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003FB RID: 1019
	public class invest_pack : SprotoTypeBase
	{
		// Token: 0x06001F8B RID: 8075 RVA: 0x0009CB10 File Offset: 0x0009AD10
		public invest_pack() : base(invest_pack.max_field_count)
		{
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0009CB20 File Offset: 0x0009AD20
		public invest_pack(byte[] buffer) : base(invest_pack.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0009CB3C File Offset: 0x0009AD3C
		// (set) Token: 0x06001F8F RID: 8079 RVA: 0x0009CB44 File Offset: 0x0009AD44
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._ID = value;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0009CB5C File Offset: 0x0009AD5C
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0009CB6C File Offset: 0x0009AD6C
		// (set) Token: 0x06001F92 RID: 8082 RVA: 0x0009CB74 File Offset: 0x0009AD74
		public long state
		{
			get
			{
				return this._state;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._state = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x0009CB8C File Offset: 0x0009AD8C
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0009CB9C File Offset: 0x0009AD9C
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
						this.state = this.deserialize.read_integer();
					}
				}
				else
				{
					this.ID = this.deserialize.read_string();
				}
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0009CC14 File Offset: 0x0009AE14
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B39 RID: 6969
		private static int max_field_count = 2;

		// Token: 0x04001B3A RID: 6970
		private string _ID;

		// Token: 0x04001B3B RID: 6971
		private long _state;
	}
}
