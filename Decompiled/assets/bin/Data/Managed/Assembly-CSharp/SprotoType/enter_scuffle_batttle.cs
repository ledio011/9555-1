using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200038F RID: 911
	public class enter_scuffle_batttle
	{
		// Token: 0x02000390 RID: 912
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B63 RID: 7011 RVA: 0x000940E0 File Offset: 0x000922E0
			public request() : base(enter_scuffle_batttle.request.max_field_count)
			{
			}

			// Token: 0x06001B64 RID: 7012 RVA: 0x000940F0 File Offset: 0x000922F0
			public request(byte[] buffer) : base(enter_scuffle_batttle.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0009410C File Offset: 0x0009230C
			// (set) Token: 0x06001B67 RID: 7015 RVA: 0x00094114 File Offset: 0x00092314
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

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0009412C File Offset: 0x0009232C
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0009413C File Offset: 0x0009233C
			// (set) Token: 0x06001B6A RID: 7018 RVA: 0x00094144 File Offset: 0x00092344
			public long floor
			{
				get
				{
					return this._floor;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._floor = value;
				}
			}

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0009415C File Offset: 0x0009235C
			public bool HasFloor
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001B6C RID: 7020 RVA: 0x0009416C File Offset: 0x0009236C
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
							this.floor = this.deserialize.read_integer();
						}
					}
					else
					{
						this.ID = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B6D RID: 7021 RVA: 0x000941E4 File Offset: 0x000923E4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.floor, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A0D RID: 6669
			private static int max_field_count = 2;

			// Token: 0x04001A0E RID: 6670
			private string _ID;

			// Token: 0x04001A0F RID: 6671
			private long _floor;
		}
	}
}
