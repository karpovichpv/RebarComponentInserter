using System.ComponentModel;
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

        [StructuresDialog(AttributeNames.UDAStringName1, typeof(TD.String))]
        public string UDAStringName1
        {
            get { return _udaStringName1; }
            set
            {
                _udaStringName1 = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringName2 = "udaStringName2";

        [StructuresDialog(AttributeNames.UDAStringName2, typeof(TD.String))]
        public string UDAStringName2
        {
            get { return _udaStringName2; }
            set
            {
                _udaStringName2 = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringValue1 = "udaStringValue1";

        [StructuresDialog(AttributeNames.UDAStringValue1, typeof(TD.String))]
        public string UDAStringValue1
        {
            get { return _udaStringValue1; }
            set
            {
                _udaStringValue1 = value;
                OnPropertyChanged();
            }
        }

        private string _udaStringValue2 = "udaStringValue2";

        [StructuresDialog(AttributeNames.UDAStringValue2, typeof(TD.String))]
        public string UDAStringValue2
        {
            get { return _udaStringValue2; }
            set
            {
                _udaStringValue2 = value;
                OnPropertyChanged();
            }
        }

        private string _udaDoubleName1 = "udaDoubleName1";

        [StructuresDialog(AttributeNames.UDADoubleName1, typeof(TD.Double))]
        public string UDADoubleName1
        {
            get { return _udaDoubleName1; }
            set
            {
                _udaDoubleName1 = value;
                OnPropertyChanged();
            }
        }

        private string _udaDoubleName2 = "udaDoubleName2";

        [StructuresDialog(AttributeNames.UDADoubleName2, typeof(TD.Double))]
        public string UDADoubleName2
        {
            get { return _udaDoubleName2; }
            set
            {
                _udaDoubleName2 = value;
                OnPropertyChanged();
            }
        }

        private double _udaDoubleValue1 = 1000d;

        [StructuresDialog(AttributeNames.UDADoubleValue1, typeof(TD.Double))]
        public double UDADoubleValue1
        {
            get { return _udaDoubleValue1; }
            set
            {
                _udaDoubleValue1 = value;
                OnPropertyChanged();
            }
        }

        private double _udaDoubleValue2 = 1000d;

        [StructuresDialog(AttributeNames.UDADoubleValue2, typeof(TD.Double))]
        public double UDADoubleValue2
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
