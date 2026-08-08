using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    public class PluginData
    {
        [StructuresField(AttributeNames.ComponentName)]
        public string ComponentName;

        [StructuresField(AttributeNames.ComponentAttribute)]
        public string ComponentAttribute;

        [StructuresField(AttributeNames.OffsetFromEnd)]
        public double OffsetFromEnd;

        [StructuresField(AttributeNames.SpacingType)]
        public int SpacingType;

        [StructuresField(AttributeNames.Spacings)]
        public string Spacings;

        [StructuresField(AttributeNames.ClassNumber)]
        public int ClassNumber;

        [StructuresField(AttributeNames.AddPartToAssembly)]
        public int AddPartToAssembly;

        [StructuresField(AttributeNames.ComponentRawWidth)]
        public string ComponentRawWidth;

        [StructuresField(AttributeNames.ComponentRawHeight)]
        public string ComponentRawHeight;

        [StructuresField(AttributeNames.UdaStringName1)]
        public string UdaStringName1;
        [StructuresField(AttributeNames.UdaStringValue1)]
        public string UdaStringValue1;

        [StructuresField(AttributeNames.UdaStringName2)]
        public string UdaStringName2;
        [StructuresField(AttributeNames.UdaStringValue2)]
        public string UdaStringValue2;

        [StructuresField(AttributeNames.UdaDoubleName1)]
        public string UdaDoubleName1;
        [StructuresField(AttributeNames.UdaDoubleValue1)]
        public double UdaDoubleValue1;

        [StructuresField(AttributeNames.UdaDoubleName2)]
        public string UdaDoubleName2;
        [StructuresField(AttributeNames.UdaDoubleValue2)]
        public double UdaDoubleValue2;

        [StructuresField(AttributeNames.ComponentRotationAngle)]
        public double ComponentRotationAngle;

        [StructuresField(AttributeNames.OffsetFromStart)]
        public double OffsetFromStart;
    }
}
