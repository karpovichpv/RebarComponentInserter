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
