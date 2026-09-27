using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000509 RID: 1289
	public class ret_complete_mission
	{
		// Token: 0x0200050A RID: 1290
		public class request : SprotoTypeBase
		{
			// Token: 0x060025B7 RID: 9655 RVA: 0x000A8714 File Offset: 0x000A6914
			public request() : base(ret_complete_mission.request.max_field_count)
			{
			}

			// Token: 0x060025B8 RID: 9656 RVA: 0x000A8724 File Offset: 0x000A6924
			public request(byte[] buffer) : base(ret_complete_mission.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A81 RID: 2689
			// (get) Token: 0x060025BA RID: 9658 RVA: 0x000A8740 File Offset: 0x000A6940
			// (set) Token: 0x060025BB RID: 9659 RVA: 0x000A8748 File Offset: 0x000A6948
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

			// Token: 0x17000A82 RID: 2690
			// (get) Token: 0x060025BC RID: 9660 RVA: 0x000A8760 File Offset: 0x000A6960
			public bool HasMissionId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000A83 RID: 2691
			// (get) Token: 0x060025BD RID: 9661 RVA: 0x000A8770 File Offset: 0x000A6970
			// (set) Token: 0x060025BE RID: 9662 RVA: 0x000A8778 File Offset: 0x000A6978
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

			// Token: 0x17000A84 RID: 2692
			// (get) Token: 0x060025BF RID: 9663 RVA: 0x000A8790 File Offset: 0x000A6990
			public bool HasRet
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060025C0 RID: 9664 RVA: 0x000A87A0 File Offset: 0x000A69A0
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

			// Token: 0x060025C1 RID: 9665 RVA: 0x000A8818 File Offset: 0x000A6A18
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

			// Token: 0x04001CC0 RID: 7360
			private static int max_field_count = 2;

			// Token: 0x04001CC1 RID: 7361
			private string _missionId;

			// Token: 0x04001CC2 RID: 7362
			private long _ret;
		}
	}
}
