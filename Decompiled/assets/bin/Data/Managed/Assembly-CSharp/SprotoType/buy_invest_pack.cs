using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000324 RID: 804
	public class buy_invest_pack
	{
		// Token: 0x02000325 RID: 805
		public class request : SprotoTypeBase
		{
			// Token: 0x0600171B RID: 5915 RVA: 0x0008B204 File Offset: 0x00089404
			public request() : base(buy_invest_pack.request.max_field_count)
			{
			}

			// Token: 0x0600171C RID: 5916 RVA: 0x0008B214 File Offset: 0x00089414
			public request(byte[] buffer) : base(buy_invest_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x0600171E RID: 5918 RVA: 0x0008B230 File Offset: 0x00089430
			// (set) Token: 0x0600171F RID: 5919 RVA: 0x0008B238 File Offset: 0x00089438
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

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06001720 RID: 5920 RVA: 0x0008B250 File Offset: 0x00089450
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001721 RID: 5921 RVA: 0x0008B260 File Offset: 0x00089460
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
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001722 RID: 5922 RVA: 0x0008B2BC File Offset: 0x000894BC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018D2 RID: 6354
			private static int max_field_count = 1;

			// Token: 0x040018D3 RID: 6355
			private string _ID;
		}
	}
}
