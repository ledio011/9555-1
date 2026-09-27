using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005CC RID: 1484
	public class set_mission_param
	{
		// Token: 0x020005CD RID: 1485
		public class request : SprotoTypeBase
		{
			// Token: 0x06002AC7 RID: 10951 RVA: 0x000B27E8 File Offset: 0x000B09E8
			public request() : base(set_mission_param.request.max_field_count)
			{
			}

			// Token: 0x06002AC8 RID: 10952 RVA: 0x000B27F8 File Offset: 0x000B09F8
			public request(byte[] buffer) : base(set_mission_param.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C3F RID: 3135
			// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000B2814 File Offset: 0x000B0A14
			// (set) Token: 0x06002ACB RID: 10955 RVA: 0x000B281C File Offset: 0x000B0A1C
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

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x06002ACC RID: 10956 RVA: 0x000B2834 File Offset: 0x000B0A34
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000C41 RID: 3137
			// (get) Token: 0x06002ACD RID: 10957 RVA: 0x000B2844 File Offset: 0x000B0A44
			// (set) Token: 0x06002ACE RID: 10958 RVA: 0x000B284C File Offset: 0x000B0A4C
			public long paramindex
			{
				get
				{
					return this._paramindex;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._paramindex = value;
				}
			}

			// Token: 0x17000C42 RID: 3138
			// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000B2864 File Offset: 0x000B0A64
			public bool HasParamindex
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000C43 RID: 3139
			// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000B2874 File Offset: 0x000B0A74
			// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x000B287C File Offset: 0x000B0A7C
			public long param
			{
				get
				{
					return this._param;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._param = value;
				}
			}

			// Token: 0x17000C44 RID: 3140
			// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x000B2894 File Offset: 0x000B0A94
			public bool HasParam
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002AD3 RID: 10963 RVA: 0x000B28A4 File Offset: 0x000B0AA4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.missionId = this.deserialize.read_string();
						break;
					case 1:
						this.paramindex = this.deserialize.read_integer();
						break;
					case 2:
						this.param = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002AD4 RID: 10964 RVA: 0x000B2938 File Offset: 0x000B0B38
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.paramindex, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.param, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E26 RID: 7718
			private static int max_field_count = 3;

			// Token: 0x04001E27 RID: 7719
			private string _missionId;

			// Token: 0x04001E28 RID: 7720
			private long _paramindex;

			// Token: 0x04001E29 RID: 7721
			private long _param;
		}
	}
}
