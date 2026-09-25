using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003DF RID: 991
	public class guild_job_change
	{
		// Token: 0x020003E0 RID: 992
		public class request : SprotoTypeBase
		{
			// Token: 0x06001EAB RID: 7851 RVA: 0x0009AE90 File Offset: 0x00099090
			public request() : base(guild_job_change.request.max_field_count)
			{
			}

			// Token: 0x06001EAC RID: 7852 RVA: 0x0009AEA0 File Offset: 0x000990A0
			public request(byte[] buffer) : base(guild_job_change.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700083F RID: 2111
			// (get) Token: 0x06001EAE RID: 7854 RVA: 0x0009AEBC File Offset: 0x000990BC
			// (set) Token: 0x06001EAF RID: 7855 RVA: 0x0009AEC4 File Offset: 0x000990C4
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

			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0009AEDC File Offset: 0x000990DC
			public bool HasCharacterId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0009AEEC File Offset: 0x000990EC
			// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x0009AEF4 File Offset: 0x000990F4
			public long jobId
			{
				get
				{
					return this._jobId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._jobId = value;
				}
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x0009AF0C File Offset: 0x0009910C
			public bool HasJobId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001EB4 RID: 7860 RVA: 0x0009AF1C File Offset: 0x0009911C
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
							this.jobId = this.deserialize.read_integer();
						}
					}
					else
					{
						this.characterId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001EB5 RID: 7861 RVA: 0x0009AF94 File Offset: 0x00099194
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.characterId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.jobId, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001AFD RID: 6909
			private static int max_field_count = 2;

			// Token: 0x04001AFE RID: 6910
			private long _characterId;

			// Token: 0x04001AFF RID: 6911
			private long _jobId;
		}
	}
}
