using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200035C RID: 860
	public class consign_cancel_sale
	{
		// Token: 0x0200035D RID: 861
		public class request : SprotoTypeBase
		{
			// Token: 0x06001981 RID: 6529 RVA: 0x000902B8 File Offset: 0x0008E4B8
			public request() : base(consign_cancel_sale.request.max_field_count)
			{
			}

			// Token: 0x06001982 RID: 6530 RVA: 0x000902C8 File Offset: 0x0008E4C8
			public request(byte[] buffer) : base(consign_cancel_sale.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001984 RID: 6532 RVA: 0x000902E4 File Offset: 0x0008E4E4
			// (set) Token: 0x06001985 RID: 6533 RVA: 0x000902EC File Offset: 0x0008E4EC
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001986 RID: 6534 RVA: 0x00090304 File Offset: 0x0008E504
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001987 RID: 6535 RVA: 0x00090314 File Offset: 0x0008E514
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
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001988 RID: 6536 RVA: 0x00090370 File Offset: 0x0008E570
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001988 RID: 6536
			private static int max_field_count = 1;

			// Token: 0x04001989 RID: 6537
			private long _id;
		}
	}
}
