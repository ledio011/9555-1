using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000410 RID: 1040
	public class mail_delete
	{
		// Token: 0x02000411 RID: 1041
		public class request : SprotoTypeBase
		{
			// Token: 0x0600203B RID: 8251 RVA: 0x0009E138 File Offset: 0x0009C338
			public request() : base(mail_delete.request.max_field_count)
			{
			}

			// Token: 0x0600203C RID: 8252 RVA: 0x0009E148 File Offset: 0x0009C348
			public request(byte[] buffer) : base(mail_delete.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170008D9 RID: 2265
			// (get) Token: 0x0600203E RID: 8254 RVA: 0x0009E164 File Offset: 0x0009C364
			// (set) Token: 0x0600203F RID: 8255 RVA: 0x0009E16C File Offset: 0x0009C36C
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

			// Token: 0x170008DA RID: 2266
			// (get) Token: 0x06002040 RID: 8256 RVA: 0x0009E184 File Offset: 0x0009C384
			public bool HasMailId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002041 RID: 8257 RVA: 0x0009E194 File Offset: 0x0009C394
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
						this.mailId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002042 RID: 8258 RVA: 0x0009E1F0 File Offset: 0x0009C3F0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.mailId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001B68 RID: 7016
			private static int max_field_count = 1;

			// Token: 0x04001B69 RID: 7017
			private long _mailId;
		}
	}
}
