using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000622 RID: 1570
	public class tower_floor : SprotoTypeBase
	{
		// Token: 0x06002DB3 RID: 11699 RVA: 0x000B87C4 File Offset: 0x000B69C4
		public tower_floor() : base(tower_floor.max_field_count)
		{
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B87D4 File Offset: 0x000B69D4
		public tower_floor(byte[] buffer) : base(tower_floor.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06002DB6 RID: 11702 RVA: 0x000B87F0 File Offset: 0x000B69F0
		// (set) Token: 0x06002DB7 RID: 11703 RVA: 0x000B87F8 File Offset: 0x000B69F8
		public long floorID
		{
			get
			{
				return this._floorID;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._floorID = value;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000B8810 File Offset: 0x000B6A10
		public bool HasFloorID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000B8820 File Offset: 0x000B6A20
		// (set) Token: 0x06002DBA RID: 11706 RVA: 0x000B8828 File Offset: 0x000B6A28
		public long complete
		{
			get
			{
				return this._complete;
			}
			set
			{
				this.has_field.set_field(1, true);
				this._complete = value;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000B8840 File Offset: 0x000B6A40
		public bool HasComplete
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000B8850 File Offset: 0x000B6A50
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
						this.complete = this.deserialize.read_integer();
					}
				}
				else
				{
					this.floorID = this.deserialize.read_integer();
				}
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B88C8 File Offset: 0x000B6AC8
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_integer(this.floorID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.complete, 1);
			}
			return this.serialize.close();
		}

		// Token: 0x04001EFB RID: 7931
		private static int max_field_count = 2;

		// Token: 0x04001EFC RID: 7932
		private long _floorID;

		// Token: 0x04001EFD RID: 7933
		private long _complete;
	}
}
