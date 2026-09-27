using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200032E RID: 814
	public class change_item_state
	{
		// Token: 0x0200032F RID: 815
		public class request : SprotoTypeBase
		{
			// Token: 0x06001766 RID: 5990 RVA: 0x0008BB80 File Offset: 0x00089D80
			public request() : base(change_item_state.request.max_field_count)
			{
			}

			// Token: 0x06001767 RID: 5991 RVA: 0x0008BB90 File Offset: 0x00089D90
			public request(byte[] buffer) : base(change_item_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06001769 RID: 5993 RVA: 0x0008BBAC File Offset: 0x00089DAC
			// (set) Token: 0x0600176A RID: 5994 RVA: 0x0008BBB4 File Offset: 0x00089DB4
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId = value;
				}
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x0600176B RID: 5995 RVA: 0x0008BBCC File Offset: 0x00089DCC
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x0600176C RID: 5996 RVA: 0x0008BBDC File Offset: 0x00089DDC
			// (set) Token: 0x0600176D RID: 5997 RVA: 0x0008BBE4 File Offset: 0x00089DE4
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x0600176E RID: 5998 RVA: 0x0008BBFC File Offset: 0x00089DFC
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600176F RID: 5999 RVA: 0x0008BC0C File Offset: 0x00089E0C
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
							this.type = this.deserialize.read_integer();
						}
					}
					else
					{
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001770 RID: 6000 RVA: 0x0008BC84 File Offset: 0x00089E84
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x040018E6 RID: 6374
			private static int max_field_count = 2;

			// Token: 0x040018E7 RID: 6375
			private long _indexId;

			// Token: 0x040018E8 RID: 6376
			private long _type;
		}
	}
}
