using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Tekla.Structures.Dialog;
using TD = Tekla.Structures.Datatype;

namespace RebarComponentInserter
{
    /// <summary>
    /// Data logic for MainWindow
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string BuildDate { get; } = ReadBuildDate();

        private static string ReadBuildDate()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using Stream stream = assembly.GetManifestResourceStream(
                    "RebarComponentInserter.Resources.BuildDate.md"
                );
                if (stream == null)
                    return "unknown";
                using StreamReader reader = new(stream);
                return reader.ReadToEnd().Trim();
            }
            catch
            {
                return "unknown";
            }
        }

        private string _componentName = "Каркас_1";

        [StructuresDialog(AttributeNames.ComponentName, typeof(TD.String))]
        public string ComponentName
        {
            get { return _componentName; }
            set
            {
                _componentName = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringName1 = "udaStringName1";

        [StructuresDialog(AttributeNames.UdaStringName1, typeof(TD.String))]
        public string UdaStringName1
        {
            get { return _udaStringName1; }
            set
            {
                _udaStringName1 = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringName2 = "udaStringName2";

        [StructuresDialog(AttributeNames.UdaStringName2, typeof(TD.String))]
        public string UdaStringName2
        {
            get { return _udaStringName2; }
            set
            {
                _udaStringName2 = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringValue1 = "udaStringValue1";

        [StructuresDialog(AttributeNames.UdaStringValue1, typeof(TD.String))]
        public string UdaStringValue1
        {
            get { return _udaStringValue1; }
            set
            {
                _udaStringValue1 = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringValue2 = "udaStringValue2";

        [StructuresDialog(AttributeNames.UdaStringValue2, typeof(TD.String))]
        public string UdaStringValue2
        {
            get { return _udaStringValue2; }
            set
            {
                _udaStringValue2 = value;
                OnPropertyChanged();
            }
        }

        private string _udaDoubleName1 = "udaDoubleName1";

        [StructuresDialog(AttributeNames.UdaDoubleName1, typeof(TD.String))]
        public string UdaDoubleName1
        {
            get { return _udaDoubleName1; }
            set
            {
                _udaDoubleName1 = value;
                OnPropertyChanged();
            }
        }

        private string _udaDoubleName2 = "udaDoubleName2";

        [StructuresDialog(AttributeNames.UdaDoubleName2, typeof(TD.String))]
        public string UdaDoubleName2
        {
            get { return _udaDoubleName2; }
            set
            {
                _udaDoubleName2 = value;
                OnPropertyChanged();
            }
        }

        private TD.Distance _udaDoubleValue1 = new(1000d);

        [StructuresDialog(AttributeNames.UdaDoubleValue1, typeof(TD.Distance))]
        public TD.Distance UdaDoubleValue1
        {
            get { return _udaDoubleValue1; }
            set
            {
                _udaDoubleValue1 = value;
                OnPropertyChanged();
            }
        }

        private TD.Distance _udaDoubleValue2 = new(1000d);

        [StructuresDialog(AttributeNames.UdaDoubleValue2, typeof(TD.Distance))]
        public TD.Distance UdaDoubleValue2
        {
            get { return _udaDoubleValue2; }
            set
            {
                _udaDoubleValue2 = value;
                OnPropertyChanged();
            }
        }

        private string _componentAttribute = "standard";

        [StructuresDialog(AttributeNames.ComponentAttribute, typeof(TD.String))]
        public string ComponentAttribute
        {
            get { return _componentAttribute; }
            set
            {
                _componentAttribute = value;
                OnPropertyChanged();
            }
        }

        private string _componentRawWidth = "PanelWidth";

        [StructuresDialog(AttributeNames.ComponentRawWidth, typeof(TD.String))]
        public string ComponentRawWidth
        {
            get { return _componentRawWidth; }
            set
            {
                _componentRawWidth = value;
                OnPropertyChanged();
            }
        }

        private string _componentRawHeight = "PanelHeight";

        [StructuresDialog(AttributeNames.ComponentRawHeight, typeof(TD.String))]
        public string ComponentRawHeight
        {
            get { return _componentRawHeight; }
            set
            {
                _componentRawHeight = value;
                OnPropertyChanged();
            }
        }

        private TD.Distance _offsetFromEnd = new(50d);

        [StructuresDialog(AttributeNames.OffsetsFromEnd, typeof(TD.Distance))]
        public TD.Distance OffsetFromEnd
        {
            get { return _offsetFromEnd; }
            set
            {
                _offsetFromEnd = value;
                OnPropertyChanged();
            }
        }

        private int _spacingType = 0;

        [StructuresDialog(AttributeNames.SpacingType, typeof(TD.Integer))]
        public int SpacingType
        {
            get { return _spacingType; }
            set
            {
                _spacingType = value;
                OnPropertyChanged();
            }
        }

        private TD.String _spacings = new("400");

        [StructuresDialog(AttributeNames.Spacings, typeof(TD.String))]
        public TD.String Spacings
        {
            get { return _spacings; }
            set
            {
                _spacings = value;
                OnPropertyChanged();
            }
        }

        private int _classNumber = 0;

        [StructuresDialog(AttributeNames.ClassNumber, typeof(TD.Integer))]
        public int ClassNumber
        {
            get { return _classNumber; }
            set
            {
                _classNumber = value;
                OnPropertyChanged();
            }
        }

        private int _addPartToAssembly = 0;

        [StructuresDialog(AttributeNames.AddPartToAssembly, typeof(TD.Integer))]
        public int AddPartToAssembly
        {
            get { return _addPartToAssembly; }
            set
            {
                _addPartToAssembly = value;
                OnPropertyChanged();
            }
        }

        private TD.Distance _componentRotationAngle = new(0d);

        [StructuresDialog(AttributeNames.ComponentRotationAngle, typeof(TD.Distance))]
        public TD.Distance ComponentRotationAngle
        {
            get { return _componentRotationAngle; }
            set
            {
                _componentRotationAngle = value;
                OnPropertyChanged();
            }
        }

        private TD.Distance _offsetFromStart = new(50d);

        [StructuresDialog(AttributeNames.OffsetFromStart, typeof(TD.Distance))]
        public TD.Distance OffsetFromStart
        {
            get { return _offsetFromStart; }
            set
            {
                _offsetFromStart = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
