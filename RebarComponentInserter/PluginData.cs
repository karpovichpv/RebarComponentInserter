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

        [StructuresField(AttributeNames.ClassNumber)]
        public int ClassNumber;

        [StructuresField(AttributeNames.ComponentRawWidth)]
        public string ComponentRawWidth;

        [StructuresField(AttributeNames.ComponentRawHeight)]
        public string ComponentRawHeight;
    }
}
