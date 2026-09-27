using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000646 RID: 1606
	public class update_misison_parm
	{
		// Token: 0x02000647 RID: 1607
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E85 RID: 11909 RVA: 0x000BA0E0 File Offset: 0x000B82E0
			public request() : base(update_misison_parm.request.max_field_count)
			{
			}

			// Token: 0x06002E86 RID: 11910 RVA: 0x000BA0F0 File Offset: 0x000B82F0
			public request(byte[] buffer) : base(update_misison_parm.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DAF RID: 3503
			// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000BA10C File Offset: 0x000B830C
			// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000BA114 File Offset: 0x000B8314
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

			// Token: 0x17000DB0 RID: 3504
			// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000BA12C File Offset: 0x000B832C
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DB1 RID: 3505
			// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000BA13C File Offset: 0x000B833C
			// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000BA144 File Offset: 0x000B8344
			public long paramType
			{
				get
				{
					return this._paramType;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._paramType = value;
				}
			}

			// Token: 0x17000DB2 RID: 3506
			// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000BA15C File Offset: 0x000B835C
			public bool HasParamType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DB3 RID: 3507
			// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000BA16C File Offset: 0x000B836C
			// (set) Token: 0x06002E8F RID: 11919 RVA: 0x000BA174 File Offset: 0x000B8374
			public long paramValue
			{
				get
				{
					return this._paramValue;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._paramValue = value;
				}
			}

			// Token: 0x17000DB4 RID: 3508
			// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000BA18C File Offset: 0x000B838C
			public bool HasParamValue
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002E91 RID: 11921 RVA: 0x000BA19C File Offset: 0x000B839C
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
						this.paramType = this.deserialize.read_integer();
						break;
					case 2:
						this.paramValue = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002E92 RID: 11922 RVA: 0x000BA230 File Offset: 0x000B8430
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.paramType, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.paramValue, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F2E RID: 7982
			private static int max_field_count = 3;

			// Token: 0x04001F2F RID: 7983
			private string _missionId;

			// Token: 0x04001F30 RID: 7984
			private long _paramType;

			// Token: 0x04001F31 RID: 7985
			private long _paramValue;
		}
	}
}
