using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005E7 RID: 1511
	public class sort_item : SprotoTypeBase
	{
		// Token: 0x06002BC0 RID: 11200 RVA: 0x000B482C File Offset: 0x000B2A2C
		public sort_item() : base(sort_item.max_field_count)
		{
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000B483C File Offset: 0x000B2A3C
		public sort_item(byte[] buffer) : base(sort_item.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x000B485C File Offset: 0x000B2A5C
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x000B4864 File Offset: 0x000B2A64
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._id = value;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000B487C File Offset: 0x000B2A7C
		public bool HasId
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000B488C File Offset: 0x000B2A8C
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x000B4894 File Offset: 0x000B2A94
		public long score
		{
			get
			{
				return this._score;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._score = value;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000B48AC File Offset: 0x000B2AAC
		public bool HasScore
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000B48BC File Offset: 0x000B2ABC
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x000B48C4 File Offset: 0x000B2AC4
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._name = value;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x000B48DC File Offset: 0x000B2ADC
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000B48EC File Offset: 0x000B2AEC
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x000B48F4 File Offset: 0x000B2AF4
		public long profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._profession = value;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000B490C File Offset: 0x000B2B0C
		public bool HasProfession
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x000B491C File Offset: 0x000B2B1C
		// (set) Token: 0x06002BD0 RID: 11216 RVA: 0x000B4924 File Offset: 0x000B2B24
		public string sortType
		{
			get
			{
				return this._sortType;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._sortType = value;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000B493C File Offset: 0x000B2B3C
		public bool HasSortType
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000B494C File Offset: 0x000B2B4C
		// (set) Token: 0x06002BD3 RID: 11219 RVA: 0x000B4954 File Offset: 0x000B2B54
		public long parm1
		{
			get
			{
				return this._parm1;
			}
			set
			{
				this.has_field.set_field(5, true);
				this._parm1 = value;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000B496C File Offset: 0x000B2B6C
		public bool HasParm1
		{
			get
			{
				return this.has_field.has_field(5);
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000B497C File Offset: 0x000B2B7C
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x000B4984 File Offset: 0x000B2B84
		public Dictionary<string, dict_hash> parm2
		{
			get
			{
				return this._parm2;
			}
			set
			{
				this.has_field.set_field(6, true);
				this._parm2 = value;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000B499C File Offset: 0x000B2B9C
		public bool HasParm2
		{
			get
			{
				return this.has_field.has_field(6);
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000B49AC File Offset: 0x000B2BAC
		// (set) Token: 0x06002BD9 RID: 11225 RVA: 0x000B49B4 File Offset: 0x000B2BB4
		public long parm3
		{
			get
			{
				return this._parm3;
			}
			set
			{
				this.has_field.set_field(7, true);
				this._parm3 = value;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000B49CC File Offset: 0x000B2BCC
		public bool HasParm3
		{
			get
			{
				return this.has_field.has_field(7);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000B49DC File Offset: 0x000B2BDC
		// (set) Token: 0x06002BDC RID: 11228 RVA: 0x000B49E4 File Offset: 0x000B2BE4
		public long parm4
		{
			get
			{
				return this._parm4;
			}
			set
			{
				this.has_field.set_field(8, true);
				this._parm4 = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000B49FC File Offset: 0x000B2BFC
		public bool HasParm4
		{
			get
			{
				return this.has_field.has_field(8);
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000B4A0C File Offset: 0x000B2C0C
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.id = this.deserialize.read_integer();
					break;
				case 1:
					this.score = this.deserialize.read_integer();
					break;
				case 2:
					this.name = this.deserialize.read_string();
					break;
				case 3:
					this.profession = this.deserialize.read_integer();
					break;
				case 4:
					this.sortType = this.deserialize.read_string();
					break;
				case 5:
					this.parm1 = this.deserialize.read_integer();
					break;
				case 6:
					this.parm2 = this.deserialize.read_map<string, dict_hash>((dict_hash v) => v.id);
					break;
				case 7:
					this.parm3 = this.deserialize.read_integer();
					break;
				case 8:
					this.parm4 = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000B4B58 File Offset: 0x000B2D58
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.id, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.score, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_string(this.name, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_integer(this.profession, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_string(this.sortType, 4);
			}
			if (this.has_field.has_field(5))
			{
				this.serialize.write_integer(this.parm1, 5);
			}
			if (this.has_field.has_field(6))
			{
				this.serialize.write_obj<string, dict_hash>(this.parm2, 6);
			}
			if (this.has_field.has_field(7))
			{
				this.serialize.write_integer(this.parm3, 7);
			}
			if (this.has_field.has_field(8))
			{
				this.serialize.write_integer(this.parm4, 8);
			}
			return this.serialize.close();
		}

		// Token: 0x04001E6B RID: 7787
		private static int max_field_count = 9;

		// Token: 0x04001E6C RID: 7788
		private long _id;

		// Token: 0x04001E6D RID: 7789
		private long _score;

		// Token: 0x04001E6E RID: 7790
		private string _name;

		// Token: 0x04001E6F RID: 7791
		private long _profession;

		// Token: 0x04001E70 RID: 7792
		private string _sortType;

		// Token: 0x04001E71 RID: 7793
		private long _parm1;

		// Token: 0x04001E72 RID: 7794
		private Dictionary<string, dict_hash> _parm2;

		// Token: 0x04001E73 RID: 7795
		private long _parm3;

		// Token: 0x04001E74 RID: 7796
		private long _parm4;
	}
}
