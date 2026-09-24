using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200032A RID: 810
	public class car_chase_result
	{
		// Token: 0x0200032B RID: 811
		public class request : SprotoTypeBase
		{
			// Token: 0x0600173C RID: 5948 RVA: 0x0008B5FC File Offset: 0x000897FC
			public request() : base(car_chase_result.request.max_field_count)
			{
			}

			// Token: 0x0600173D RID: 5949 RVA: 0x0008B60C File Offset: 0x0008980C
			public request(byte[] buffer) : base(car_chase_result.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x0600173F RID: 5951 RVA: 0x0008B628 File Offset: 0x00089828
			// (set) Token: 0x06001740 RID: 5952 RVA: 0x0008B630 File Offset: 0x00089830
			public bool state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x06001741 RID: 5953 RVA: 0x0008B648 File Offset: 0x00089848
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06001742 RID: 5954 RVA: 0x0008B658 File Offset: 0x00089858
			// (set) Token: 0x06001743 RID: 5955 RVA: 0x0008B660 File Offset: 0x00089860
			public long param1
			{
				get
				{
					return this._param1;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._param1 = value;
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06001744 RID: 5956 RVA: 0x0008B678 File Offset: 0x00089878
			public bool HasParam1
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x06001745 RID: 5957 RVA: 0x0008B688 File Offset: 0x00089888
			// (set) Token: 0x06001746 RID: 5958 RVA: 0x0008B690 File Offset: 0x00089890
			public string param2
			{
				get
				{
					return this._param2;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._param2 = value;
				}
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x06001747 RID: 5959 RVA: 0x0008B6A8 File Offset: 0x000898A8
			public bool HasParam2
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06001748 RID: 5960 RVA: 0x0008B6B8 File Offset: 0x000898B8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_boolean();
						break;
					case 1:
						this.param1 = this.deserialize.read_integer();
						break;
					case 2:
						this.param2 = this.deserialize.read_string();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001749 RID: 5961 RVA: 0x0008B74C File Offset: 0x0008994C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_boolean(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.param1, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.param2, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x040018DA RID: 6362
			private static int max_field_count = 3;

			// Token: 0x040018DB RID: 6363
			private bool _state;

			// Token: 0x040018DC RID: 6364
			private long _param1;

			// Token: 0x040018DD RID: 6365
			private string _param2;
		}
	}
}
