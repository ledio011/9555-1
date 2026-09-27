using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003AA RID: 938
	public class equip_refine
	{
		// Token: 0x020003AB RID: 939
		public class request : SprotoTypeBase
		{
			// Token: 0x06001C19 RID: 7193 RVA: 0x00095778 File Offset: 0x00093978
			public request() : base(equip_refine.request.max_field_count)
			{
			}

			// Token: 0x06001C1A RID: 7194 RVA: 0x00095788 File Offset: 0x00093988
			public request(byte[] buffer) : base(equip_refine.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000709 RID: 1801
			// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000957A4 File Offset: 0x000939A4
			// (set) Token: 0x06001C1D RID: 7197 RVA: 0x000957AC File Offset: 0x000939AC
			public string Id
			{
				get
				{
					return this._Id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._Id = value;
				}
			}

			// Token: 0x1700070A RID: 1802
			// (get) Token: 0x06001C1E RID: 7198 RVA: 0x000957C4 File Offset: 0x000939C4
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x1700070B RID: 1803
			// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000957D4 File Offset: 0x000939D4
			// (set) Token: 0x06001C20 RID: 7200 RVA: 0x000957DC File Offset: 0x000939DC
			public string preId
			{
				get
				{
					return this._preId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._preId = value;
				}
			}

			// Token: 0x1700070C RID: 1804
			// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000957F4 File Offset: 0x000939F4
			public bool HasPreId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x1700070D RID: 1805
			// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00095804 File Offset: 0x00093A04
			// (set) Token: 0x06001C23 RID: 7203 RVA: 0x0009580C File Offset: 0x00093A0C
			public string curId
			{
				get
				{
					return this._curId;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._curId = value;
				}
			}

			// Token: 0x1700070E RID: 1806
			// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00095824 File Offset: 0x00093A24
			public bool HasCurId
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x1700070F RID: 1807
			// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00095834 File Offset: 0x00093A34
			// (set) Token: 0x06001C26 RID: 7206 RVA: 0x0009583C File Offset: 0x00093A3C
			public long partId
			{
				get
				{
					return this._partId;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._partId = value;
				}
			}

			// Token: 0x17000710 RID: 1808
			// (get) Token: 0x06001C27 RID: 7207 RVA: 0x00095854 File Offset: 0x00093A54
			public bool HasPartId
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x17000711 RID: 1809
			// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00095864 File Offset: 0x00093A64
			// (set) Token: 0x06001C29 RID: 7209 RVA: 0x0009586C File Offset: 0x00093A6C
			public long level
			{
				get
				{
					return this._level;
				}
				set
				{
					this.has_field.set_field(4, true);
					this._level = value;
				}
			}

			// Token: 0x17000712 RID: 1810
			// (get) Token: 0x06001C2A RID: 7210 RVA: 0x00095884 File Offset: 0x00093A84
			public bool HasLevel
			{
				get
				{
					return this.has_field.has_field(4);
				}
			}

			// Token: 0x17000713 RID: 1811
			// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00095894 File Offset: 0x00093A94
			// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0009589C File Offset: 0x00093A9C
			public bool safe
			{
				get
				{
					return this._safe;
				}
				set
				{
					this.has_field.set_field(5, true);
					this._safe = value;
				}
			}

			// Token: 0x17000714 RID: 1812
			// (get) Token: 0x06001C2D RID: 7213 RVA: 0x000958B4 File Offset: 0x00093AB4
			public bool HasSafe
			{
				get
				{
					return this.has_field.has_field(5);
				}
			}

			// Token: 0x06001C2E RID: 7214 RVA: 0x000958C4 File Offset: 0x00093AC4
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.Id = this.deserialize.read_string();
						break;
					case 1:
						this.preId = this.deserialize.read_string();
						break;
					case 2:
						this.curId = this.deserialize.read_string();
						break;
					case 3:
						this.partId = this.deserialize.read_integer();
						break;
					case 4:
						this.level = this.deserialize.read_integer();
						break;
					case 5:
						this.safe = this.deserialize.read_boolean();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001C2F RID: 7215 RVA: 0x000959A4 File Offset: 0x00093BA4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.Id, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.preId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_string(this.curId, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.partId, 3);
				}
				if (this.has_field.has_field(4))
				{
					this.serialize.write_integer(this.level, 4);
				}
				if (this.has_field.has_field(5))
				{
					this.serialize.write_boolean(this.safe, 5);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A3C RID: 6716
			private static int max_field_count = 6;

			// Token: 0x04001A3D RID: 6717
			private string _Id;

			// Token: 0x04001A3E RID: 6718
			private string _preId;

			// Token: 0x04001A3F RID: 6719
			private string _curId;

			// Token: 0x04001A40 RID: 6720
			private long _partId;

			// Token: 0x04001A41 RID: 6721
			private long _level;

			// Token: 0x04001A42 RID: 6722
			private bool _safe;
		}

		// Token: 0x020003AC RID: 940
		public class response : SprotoTypeBase
		{
			// Token: 0x06001C30 RID: 7216 RVA: 0x00095A9C File Offset: 0x00093C9C
			public response() : base(equip_refine.response.max_field_count)
			{
			}

			// Token: 0x06001C31 RID: 7217 RVA: 0x00095AAC File Offset: 0x00093CAC
			public response(byte[] buffer) : base(equip_refine.response.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000715 RID: 1813
			// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00095AC8 File Offset: 0x00093CC8
			// (set) Token: 0x06001C34 RID: 7220 RVA: 0x00095AD0 File Offset: 0x00093CD0
			public long state
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

			// Token: 0x17000716 RID: 1814
			// (get) Token: 0x06001C35 RID: 7221 RVA: 0x00095AE8 File Offset: 0x00093CE8
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000717 RID: 1815
			// (get) Token: 0x06001C36 RID: 7222 RVA: 0x00095AF8 File Offset: 0x00093CF8
			// (set) Token: 0x06001C37 RID: 7223 RVA: 0x00095B00 File Offset: 0x00093D00
			public long partId
			{
				get
				{
					return this._partId;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._partId = value;
				}
			}

			// Token: 0x17000718 RID: 1816
			// (get) Token: 0x06001C38 RID: 7224 RVA: 0x00095B18 File Offset: 0x00093D18
			public bool HasPartId
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00095B28 File Offset: 0x00093D28
			// (set) Token: 0x06001C3A RID: 7226 RVA: 0x00095B30 File Offset: 0x00093D30
			public long level
			{
				get
				{
					return this._level;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._level = value;
				}
			}

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00095B48 File Offset: 0x00093D48
			public bool HasLevel
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00095B58 File Offset: 0x00093D58
			// (set) Token: 0x06001C3D RID: 7229 RVA: 0x00095B60 File Offset: 0x00093D60
			public long allstar
			{
				get
				{
					return this._allstar;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._allstar = value;
				}
			}

			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00095B78 File Offset: 0x00093D78
			public bool HasAllstar
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x06001C3F RID: 7231 RVA: 0x00095B88 File Offset: 0x00093D88
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.partId = this.deserialize.read_integer();
						break;
					case 2:
						this.level = this.deserialize.read_integer();
						break;
					case 3:
						this.allstar = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06001C40 RID: 7232 RVA: 0x00095C34 File Offset: 0x00093E34
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.partId, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.level, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.allstar, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A43 RID: 6723
			private static int max_field_count = 4;

			// Token: 0x04001A44 RID: 6724
			private long _state;

			// Token: 0x04001A45 RID: 6725
			private long _partId;

			// Token: 0x04001A46 RID: 6726
			private long _level;

			// Token: 0x04001A47 RID: 6727
			private long _allstar;
		}
	}
}
