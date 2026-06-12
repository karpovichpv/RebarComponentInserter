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
        private string _componentName = string.Empty;

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

        private string _componentAttribute = string.Empty;

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

        private string _componentRawWidth = string.Empty;

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

        private string _componentRawHeight = string.Empty;

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

        private TD.Distance _offsetFromEnd = new();

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
