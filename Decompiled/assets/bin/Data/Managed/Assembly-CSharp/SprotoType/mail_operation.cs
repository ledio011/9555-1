using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000412 RID: 1042
	public class mail_operation
	{
		// Token: 0x02000413 RID: 1043
		public class request : SprotoTypeBase
		{
			// Token: 0x06002044 RID: 8260 RVA: 0x0009E240 File Offset: 0x0009C440
			public request() : base(mail_operation.request.max_field_count)
			{
			}

			// Token: 0x06002045 RID: 8261 RVA: 0x0009E250 File Offset: 0x0009C450
			public request(byte[] buffer) : base(mail_operation.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008DB RID: 2267
			// (get) Token: 0x06002047 RID: 8263 RVA: 0x0009E26C File Offset: 0x0009C46C
			// (set) Token: 0x06002048 RID: 8264 RVA: 0x0009E274 File Offset: 0x0009C474
			public long mailId
			{
				get
				{
					return this._mailId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mailId = value;
				}
			}

			// Token: 0x170008DC RID: 2268
			// (get) Token: 0x06002049 RID: 8265 RVA: 0x0009E28C File Offset: 0x0009C48C
			public bool HasMailId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170008DD RID: 2269
			// (get) Token: 0x0600204A RID: 8266 RVA: 0x0009E29C File Offset: 0x0009C49C
			// (set) Token: 0x0600204B RID: 8267 RVA: 0x0009E2A4 File Offset: 0x0009C4A4
			public long operation
			{
				get
				{
					return this._operation;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._operation = value;
				}
			}

			// Token: 0x170008DE RID: 2270
			// (get) Token: 0x0600204C RID: 8268 RVA: 0x0009E2BC File Offset: 0x0009C4BC
			public bool HasOperation
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600204D RID: 8269 RVA: 0x0009E2CC File Offset: 0x0009C4CC
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
							this.operation = this.deserialize.read_integer();
						}
					}
					else
					{
						this.mailId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600204E RID: 8270 RVA: 0x0009E344 File Offset: 0x0009C544
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.mailId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.operation, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B6A RID: 7018
			private static int max_field_count = 2;

			// Token: 0x04001B6B RID: 7019
			private long _mailId;

			// Token: 0x04001B6C RID: 7020
			private long _operation;
		}
	}
}
