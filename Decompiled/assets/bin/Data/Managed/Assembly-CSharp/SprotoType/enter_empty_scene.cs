using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000381 RID: 897
	public class enter_empty_scene
	{
		// Token: 0x02000382 RID: 898
		public class request : SprotoTypeBase
		{
			// Token: 0x06001B1B RID: 6939 RVA: 0x00093868 File Offset: 0x00091A68
			public request() : base(enter_empty_scene.request.max_field_count)
			{
			}

			// Token: 0x06001B1C RID: 6940 RVA: 0x00093878 File Offset: 0x00091A78
			public request(byte[] buffer) : base(enter_empty_scene.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006B3 RID: 1715
			// (get) Token: 0x06001B1E RID: 6942 RVA: 0x00093894 File Offset: 0x00091A94
			// (set) Token: 0x06001B1F RID: 6943 RVA: 0x0009389C File Offset: 0x00091A9C
			public string mapInfoId
			{
				get
				{
					return this._mapInfoId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mapInfoId = value;
				}
			}

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000938B4 File Offset: 0x00091AB4
			public bool HasMapInfoId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06001B21 RID: 6945 RVA: 0x000938C4 File Offset: 0x00091AC4
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
						this.mapInfoId = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06001B22 RID: 6946 RVA: 0x00093920 File Offset: 0x00091B20
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mapInfoId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040019FC RID: 6652
			private static int max_field_count = 1;

			// Token: 0x040019FD RID: 6653
			private string _mapInfoId;
		}
	}
}
