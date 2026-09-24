using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000533 RID: 1331
	public class ret_guild_kick
	{
		// Token: 0x02000534 RID: 1332
		public class request : SprotoTypeBase
		{
			// Token: 0x060026D1 RID: 9937 RVA: 0x000AAA24 File Offset: 0x000A8C24
			public request() : base(ret_guild_kick.request.max_field_count)
			{
			}

			// Token: 0x060026D2 RID: 9938 RVA: 0x000AAA34 File Offset: 0x000A8C34
			public request(byte[] buffer) : base(ret_guild_kick.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AE5 RID: 2789
			// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000AAA50 File Offset: 0x000A8C50
			// (set) Token: 0x060026D5 RID: 9941 RVA: 0x000AAA58 File Offset: 0x000A8C58
			public long characterId
			{
				get
				{
					return this._characterId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._characterId = value;
				}
			}

			// Token: 0x17000AE6 RID: 2790
			// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000AAA70 File Offset: 0x000A8C70
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AE7 RID: 2791
			// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000AAA80 File Offset: 0x000A8C80
			// (set) Token: 0x060026D8 RID: 9944 RVA: 0x000AAA88 File Offset: 0x000A8C88
			public bool state
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

			// Token: 0x17000AE8 RID: 2792
			// (get) Token: 0x060026D9 RID: 9945 RVA: 0x000AAAA0 File Offset: 0x000A8CA0
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060026DA RID: 9946 RVA: 0x000AAAB0 File Offset: 0x000A8CB0
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
							this.state = this.deserialize.read_boolean();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x060026DB RID: 9947 RVA: 0x000AAB28 File Offset: 0x000A8D28
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_boolean(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D0D RID: 7437
			private static int max_field_count = 2;

			// Token: 0x04001D0E RID: 7438
			private long _characterId;

			// Token: 0x04001D0F RID: 7439
			private bool _state;
		}
	}
}
