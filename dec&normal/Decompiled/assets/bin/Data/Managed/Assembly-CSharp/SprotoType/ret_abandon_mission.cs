using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004F1 RID: 1265
	public class ret_abandon_mission
	{
		// Token: 0x020004F2 RID: 1266
		public class request : SprotoTypeBase
		{
			// Token: 0x06002502 RID: 9474 RVA: 0x000A7044 File Offset: 0x000A5244
			public request() : base(ret_abandon_mission.request.max_field_count)
			{
			}

			// Token: 0x06002503 RID: 9475 RVA: 0x000A7054 File Offset: 0x000A5254
			public request(byte[] buffer) : base(ret_abandon_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A39 RID: 2617
			// (get) Token: 0x06002505 RID: 9477 RVA: 0x000A7070 File Offset: 0x000A5270
			// (set) Token: 0x06002506 RID: 9478 RVA: 0x000A7078 File Offset: 0x000A5278
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

			// Token: 0x17000A3A RID: 2618
			// (get) Token: 0x06002507 RID: 9479 RVA: 0x000A7090 File Offset: 0x000A5290
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A3B RID: 2619
			// (get) Token: 0x06002508 RID: 9480 RVA: 0x000A70A0 File Offset: 0x000A52A0
			// (set) Token: 0x06002509 RID: 9481 RVA: 0x000A70A8 File Offset: 0x000A52A8
			public long ret
			{
				get
				{
					return this._ret;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._ret = value;
				}
			}

			// Token: 0x17000A3C RID: 2620
			// (get) Token: 0x0600250A RID: 9482 RVA: 0x000A70C0 File Offset: 0x000A52C0
			public bool HasRet
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x0600250B RID: 9483 RVA: 0x000A70D0 File Offset: 0x000A52D0
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
							this.ret = this.deserialize.read_integer();
						}
					}
					else
					{
						this.missionId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x0600250C RID: 9484 RVA: 0x000A7148 File Offset: 0x000A5348
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.missionId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.ret, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C8F RID: 7311
			private static int max_field_count = 2;

			// Token: 0x04001C90 RID: 7312
			private string _missionId;

			// Token: 0x04001C91 RID: 7313
			private long _ret;
		}
	}
}
