using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003C6 RID: 966
	public class grant_tower_reward
	{
		// Token: 0x020003C7 RID: 967
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D76 RID: 7542 RVA: 0x000985AC File Offset: 0x000967AC
			public request() : base(grant_tower_reward.request.max_field_count)
			{
			}

			// Token: 0x06001D77 RID: 7543 RVA: 0x000985BC File Offset: 0x000967BC
			public request(byte[] buffer) : base(grant_tower_reward.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x06001D79 RID: 7545 RVA: 0x000985D8 File Offset: 0x000967D8
			// (set) Token: 0x06001D7A RID: 7546 RVA: 0x000985E0 File Offset: 0x000967E0
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x170007AE RID: 1966
			// (get) Token: 0x06001D7B RID: 7547 RVA: 0x000985F8 File Offset: 0x000967F8
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00098608 File Offset: 0x00096808
			// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00098610 File Offset: 0x00096810
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._id = value;
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00098628 File Offset: 0x00096828
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001D7F RID: 7551 RVA: 0x00098638 File Offset: 0x00096838
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
							this.id = this.deserialize.read_integer();
						}
					}
					else
					{
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001D80 RID: 7552 RVA: 0x000986B0 File Offset: 0x000968B0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.id, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AA3 RID: 6819
			private static int max_field_count = 2;

			// Token: 0x04001AA4 RID: 6820
			private long _type;

			// Token: 0x04001AA5 RID: 6821
			private long _id;
		}
	}
}
