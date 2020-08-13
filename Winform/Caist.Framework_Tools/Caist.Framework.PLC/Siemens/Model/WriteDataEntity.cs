namespace Caist.Framework.PLC.Siemens.Model
{
    public class WriteDataEntity
    {
        private InstructEntity  _InstructEntity;
        private double _Double;
        private InstructGroupEntity _InstructGroupEntity;

        public InstructEntity InstructEntity { get => _InstructEntity; set => _InstructEntity = value; }
        public double Double { get => _Double; set => _Double = value; }
        public InstructGroupEntity InstructGroupEntity { get => _InstructGroupEntity; set => _InstructGroupEntity = value; }

        public WriteDataEntity(InstructGroupEntity instructGroupEntity, InstructEntity instructEntity, double _double) : base()
        {
            _InstructGroupEntity = instructGroupEntity;
            _InstructEntity = instructEntity;
            _Double = _double;
        }
    }
}
