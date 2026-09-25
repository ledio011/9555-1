using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200058D RID: 1421
	public class ret_search_guild
	{
		// Token: 0x0200058E RID: 1422
		public class request : SprotoTypeBase
		{
			// Token: 0x06002924 RID: 10532 RVA: 0x000AF3E0 File Offset: 0x000AD5E0
			public request() : base(ret_search_guild.request.max_field_count)
			{
			}

			// Token: 0x06002925 RID: 10533 RVA: 0x000AF3F0 File Offset: 0x000AD5F0
			public request(byte[] buffer) : base(ret_search_guild.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BAD RID: 2989
			// (get) Token: 0x06002927 RID: 10535 RVA: 0x000AF40C File Offset: 0x000AD60C
			// (set) Token: 0x06002928 RID: 10536 RVA: 0x000AF414 File Offset: 0x000AD614
			public guild_info guild
			{
				get
				{
					return this._guild;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._guild = value;
				}
			}

			// Token: 0x17000BAE RID: 2990
			// (get) Token: 0x06002929 RID: 10537 RVA: 0x000AF42C File Offset: 0x000AD62C
			public bool HasGuild
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BAF RID: 2991
			// (get) Token: 0x0600292A RID: 10538 RVA: 0x000AF43C File Offset: 0x000AD63C
			// (set) Token: 0x0600292B RID: 10539 RVA: 0x000AF444 File Offset: 0x000AD644
			public long rank
			{
				get
				{
					return this._rank;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._rank = value;
				}
			}

			// Token: 0x17000BB0 RID: 2992
			// (get) Token: 0x0600292C RID: 10540 RVA: 0x000AF45C File Offset: 0x000AD65C
			public bool HasRank
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x000AF46C File Offset: 0x000AD66C
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
							this.rank = this.deserialize.read_integer();
						}
					}
					else
					{
						this.guild = this.deserialize.read_obj<guild_info>();
					}
				}
			}

			// Token: 0x0600292E RID: 10542 RVA: 0x000AF4E4 File Offset: 0x000AD6E4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.guild, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.rank, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DB7 RID: 7607
			private static int max_field_count = 2;

			// Token: 0x04001DB8 RID: 7608
			private guild_info _guild;

			// Token: 0x04001DB9 RID: 7609
			private long _rank;
		}
	}
}
