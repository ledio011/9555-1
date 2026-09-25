using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000644 RID: 1604
	public class update_misison_complete
	{
		// Token: 0x02000645 RID: 1605
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E7C RID: 11900 RVA: 0x000B9FD8 File Offset: 0x000B81D8
			public request() : base(update_misison_complete.request.max_field_count)
			{
			}

			// Token: 0x06002E7D RID: 11901 RVA: 0x000B9FE8 File Offset: 0x000B81E8
			public request(byte[] buffer) : base(update_misison_complete.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DAD RID: 3501
			// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000BA004 File Offset: 0x000B8204
			// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000BA00C File Offset: 0x000B820C
			public string missionId
			{
				get
				{
					return this._missionId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._missionId = value;
				}
			}

			// Token: 0x17000DAE RID: 3502
			// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000BA024 File Offset: 0x000B8224
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E82 RID: 11906 RVA: 0x000BA034 File Offset: 0x000B8234
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
						this.missionId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002E83 RID: 11907 RVA: 0x000BA090 File Offset: 0x000B8290
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F2C RID: 7980
			private static int max_field_count = 1;

			// Token: 0x04001F2D RID: 7981
			private string _missionId;
		}
	}
}
