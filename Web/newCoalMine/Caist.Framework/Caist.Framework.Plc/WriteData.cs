namespace Caist.Framework.Plc
{
	public class WriteData
    {
		private Tag typeTag;
		private double typeDouble;
		private TagGroup typeTagGroup;
		public Tag Tag
		{
			get
			{
				return this.typeTag;
			}
			set
			{
				this.typeTag = value;
			}
		}
		public double WrData
		{
			get
			{
				return this.typeDouble;
			}
			set
			{
				this.typeDouble = value;
			}
		}
		public TagGroup TagGroup
		{
			get
			{
				return this.typeTagGroup;
			}
			set
			{
				this.typeTagGroup = value;
			}
		}


		public WriteData(TagGroup tagGroup, Tag tag, double dble) : base()
		{
			this.typeTagGroup = tagGroup;
			this.typeTag = tag;
			this.typeDouble = dble;
		}
	}
}
