using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003BD RID: 957
	public class general : SprotoTypeBase
	{
		// Token: 0x06001D24 RID: 7460 RVA: 0x00097B34 File Offset: 0x00095D34
		public general() : base(general.max_field_count)
		{
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00097B44 File Offset: 0x00095D44
		public general(byte[] buffer) : base(general.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00097B60 File Offset: 0x00095D60
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00097B68 File Offset: 0x00095D68
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._name = value;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00097B80 File Offset: 0x00095D80
		public bool HasName
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x00097B90 File Offset: 0x00095D90
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x00097B98 File Offset: 0x00095D98
		public long profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._profession = value;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x00097BB0 File Offset: 0x00095DB0
		public bool HasProfession
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x00097BC0 File Offset: 0x00095DC0
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x00097BC8 File Offset: 0x00095DC8
		public long lineIndex
		{
			get
			{
				return this._lineIndex;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._lineIndex = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x00097BE0 File Offset: 0x00095DE0
		public bool HasLineIndex
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x00097BF0 File Offset: 0x00095DF0
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x00097BF8 File Offset: 0x00095DF8
		public string mapInfoId
		{
			get
			{
				return this._mapInfoId;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._mapInfoId = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00097C10 File Offset: 0x00095E10
		public bool HasMapInfoId
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00097C20 File Offset: 0x00095E20
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x00097C28 File Offset: 0x00095E28
		public long tutorial
		{
			get
			{
				return this._tutorial;
			}
			set
			{
				this.has_field.set_field(4, true);
				this._tutorial = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00097C40 File Offset: 0x00095E40
		public bool HasTutorial
		{
			get
			{
				return this.has_field.has_field(4);
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00097C50 File Offset: 0x00095E50
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.name = this.deserialize.read_string();
					break;
				case 1:
					this.profession = this.deserialize.read_integer();
					break;
				case 2:
					this.lineIndex = this.deserialize.read_integer();
					break;
				case 3:
					this.mapInfoId = this.deserialize.read_string();
					break;
				case 4:
					this.tutorial = this.deserialize.read_integer();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x00097D18 File Offset: 0x00095F18
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.name, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.profession, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_integer(this.lineIndex, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.mapInfoId, 3);
			}
			if (this.has_field.has_field(4))
			{
				this.serialize.write_integer(this.tutorial, 4);
			}
			return this.serialize.close();
		}

		// Token: 0x04001A8C RID: 6796
		private static int max_field_count = 5;

		// Token: 0x04001A8D RID: 6797
		private string _name;

		// Token: 0x04001A8E RID: 6798
		private long _profession;

		// Token: 0x04001A8F RID: 6799
		private long _lineIndex;

		// Token: 0x04001A90 RID: 6800
		private string _mapInfoId;

		// Token: 0x04001A91 RID: 6801
		private long _tutorial;
	}
}
