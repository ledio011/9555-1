using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200045A RID: 1114
	public class re_name
	{
		// Token: 0x0200045B RID: 1115
		public class request : SprotoTypeBase
		{
			// Token: 0x06002299 RID: 8857 RVA: 0x000A2E40 File Offset: 0x000A1040
			public request() : base(re_name.request.max_field_count)
			{
			}

			// Token: 0x0600229A RID: 8858 RVA: 0x000A2E50 File Offset: 0x000A1050
			public request(byte[] buffer) : base(re_name.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170009CD RID: 2509
			// (get) Token: 0x0600229C RID: 8860 RVA: 0x000A2E6C File Offset: 0x000A106C
			// (set) Token: 0x0600229D RID: 8861 RVA: 0x000A2E74 File Offset: 0x000A1074
			public string name
			{
				get
				{
					return this._name;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._name = value;
				}
			}

			// Token: 0x170009CE RID: 2510
			// (get) Token: 0x0600229E RID: 8862 RVA: 0x000A2E8C File Offset: 0x000A108C
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600229F RID: 8863 RVA: 0x000A2E9C File Offset: 0x000A109C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.name = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060022A0 RID: 8864 RVA: 0x000A2EF8 File Offset: 0x000A10F8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.name, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C0D RID: 7181
			private static int max_field_count = 1;

			// Token: 0x04001C0E RID: 7182
			private string _name;
		}
	}
}
