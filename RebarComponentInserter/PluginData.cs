using Tekla.Structures.Plugins;

namespace RebarComponentInserter
{
    public class PluginData
    {
        [StructuresField(AttributeNames.ComponentName)]
        public string ComponentName;

        [StructuresField(AttributeNames.ComponentAttribute)]
        public string ComponentAttribute;

        [StructuresField(AttributeNames.OffsetsFromEnd)]
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

        [StructuresField(AttributeNames.UDAStringName1)]
        public string UDAStringName1;
        [StructuresField(AttributeNames.UDAStringValue1)]
        public string UDAStringValue1;

        [StructuresField(AttributeNames.UDAStringName2)]
        public string UDAStringName2;
        [StructuresField(AttributeNames.UDAStringValue2)]
        public string UDAStringValue2;

        [StructuresField(AttributeNames.UDADoubleName1)]
        public string UDADoubleName1;
        [StructuresField(AttributeNames.UDADoubleValue1)]
        public double UDADoubleValue1;

        [StructuresField(AttributeNames.UDADoubleName2)]
        public string UDADoubleName2;
        [StructuresField(AttributeNames.UDADoubleValue2)]
        public double UDADoubleValue2;
    }
}
