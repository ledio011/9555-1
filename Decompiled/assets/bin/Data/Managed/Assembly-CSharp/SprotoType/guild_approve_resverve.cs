using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003C8 RID: 968
	public class guild_approve_resverve
	{
		// Token: 0x020003C9 RID: 969
		public class request : SprotoTypeBase
		{
			// Token: 0x06001D82 RID: 7554 RVA: 0x00098724 File Offset: 0x00096924
			public request() : base(guild_approve_resverve.request.max_field_count)
			{
			}

			// Token: 0x06001D83 RID: 7555 RVA: 0x00098734 File Offset: 0x00096934
			public request(byte[] buffer) : base(guild_approve_resverve.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170007B1 RID: 1969
			// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00098750 File Offset: 0x00096950
			// (set) Token: 0x06001D86 RID: 7558 RVA: 0x00098758 File Offset: 0x00096958
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

			// Token: 0x170007B2 RID: 1970
			// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00098770 File Offset: 0x00096970
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170007B3 RID: 1971
			// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00098780 File Offset: 0x00096980
			// (set) Token: 0x06001D89 RID: 7561 RVA: 0x00098788 File Offset: 0x00096988
			public long isAgree
			{
				get
				{
					return this._isAgree;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._isAgree = value;
				}
			}

			// Token: 0x170007B4 RID: 1972
			// (get) Token: 0x06001D8A RID: 7562 RVA: 0x000987A0 File Offset: 0x000969A0
			public bool HasIsAgree
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001D8B RID: 7563 RVA: 0x000987B0 File Offset: 0x000969B0
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
							this.isAgree = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001D8C RID: 7564 RVA: 0x00098828 File Offset: 0x00096A28
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.isAgree, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AA6 RID: 6822
			private static int max_field_count = 2;

			// Token: 0x04001AA7 RID: 6823
			private long _characterId;

			// Token: 0x04001AA8 RID: 6824
			private long _isAgree;
		}
	}
}
