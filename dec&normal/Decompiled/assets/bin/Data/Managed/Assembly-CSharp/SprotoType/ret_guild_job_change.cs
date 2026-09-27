using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200052F RID: 1327
	public class ret_guild_job_change
	{
		// Token: 0x02000530 RID: 1328
		public class request : SprotoTypeBase
		{
			// Token: 0x060026B9 RID: 9913 RVA: 0x000AA734 File Offset: 0x000A8934
			public request() : base(ret_guild_job_change.request.max_field_count)
			{
			}

			// Token: 0x060026BA RID: 9914 RVA: 0x000AA744 File Offset: 0x000A8944
			public request(byte[] buffer) : base(ret_guild_job_change.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000ADD RID: 2781
			// (get) Token: 0x060026BC RID: 9916 RVA: 0x000AA760 File Offset: 0x000A8960
			// (set) Token: 0x060026BD RID: 9917 RVA: 0x000AA768 File Offset: 0x000A8968
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

			// Token: 0x17000ADE RID: 2782
			// (get) Token: 0x060026BE RID: 9918 RVA: 0x000AA780 File Offset: 0x000A8980
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000ADF RID: 2783
			// (get) Token: 0x060026BF RID: 9919 RVA: 0x000AA790 File Offset: 0x000A8990
			// (set) Token: 0x060026C0 RID: 9920 RVA: 0x000AA798 File Offset: 0x000A8998
			public long job
			{
				get
				{
					return this._job;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._job = value;
				}
			}

			// Token: 0x17000AE0 RID: 2784
			// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000AA7B0 File Offset: 0x000A89B0
			public bool HasJob
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000AE1 RID: 2785
			// (get) Token: 0x060026C2 RID: 9922 RVA: 0x000AA7C0 File Offset: 0x000A89C0
			// (set) Token: 0x060026C3 RID: 9923 RVA: 0x000AA7C8 File Offset: 0x000A89C8
			public bool state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._state = value;
				}
			}

			// Token: 0x17000AE2 RID: 2786
			// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000AA7E0 File Offset: 0x000A89E0
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060026C5 RID: 9925 RVA: 0x000AA7F0 File Offset: 0x000A89F0
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.characterId = this.deserialize.read_integer();
						break;
					case 1:
						this.job = this.deserialize.read_integer();
						break;
					case 2:
						this.state = this.deserialize.read_boolean();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060026C6 RID: 9926 RVA: 0x000AA884 File Offset: 0x000A8A84
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.job, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_boolean(this.state, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D07 RID: 7431
			private static int max_field_count = 3;

			// Token: 0x04001D08 RID: 7432
			private long _characterId;

			// Token: 0x04001D09 RID: 7433
			private long _job;

			// Token: 0x04001D0A RID: 7434
			private bool _state;
		}
	}
}
