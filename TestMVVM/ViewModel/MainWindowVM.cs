using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TestMVVM.ViewModel
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public MainWindowVM()
        {
            ResetTextCommand = new RelayCommand(ResetText);
            SetNameCommand = new RelayCommand(SetName);
            AddNewTagCommand = new RelayCommand(AddNewTag);
            DeleteTagCommand = new RelayCommand(DeleteTag);

            TagList = new ObservableCollection<Tag>();
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = dbClass.Execute_Proc("dbo.GetTags");

            foreach (DataRow Row in dt.Rows)
            {
                Tag tag = new Tag
                    (Row.Field<int?>("TagID"),
                    Row.Field<int?>("ParentCategoryID"),
                    Row.Field<string>("Name"),
                    Row.Field<string>("ColorID"),
                    Row.Field<bool?>("IsUserCreated"),
                    Row.Field<bool?>("IsFeelingGoodBad"));

                TagList.Add(tag);
            }
        }

        //======================
        public RelayCommand ResetTextCommand { get; set; }
        public RelayCommand SetNameCommand { get; set; }
        public RelayCommand AddNewTagCommand { get; set; }
        public RelayCommand DeleteTagCommand { get; set; }

        //======================
        public Tag SelectedListItem { get; set; }
        public void ResetText()
        {
            SimpleTextBoxString = "Hello World!";
        }
        public void SetName()
        {
            SimpleTextBoxString = SelectedListItem.Name;
        }

        //======================        
        public void AddNewTag()
        {
            int newTagID = dbClass.InsertTagToDB(NewTagTextBoxString, NewIsFeelingGoodBad);

            Tag newTag = new Tag
            {
                Name = NewTagTextBoxString,
                TagID = newTagID,
                IsFeelingGoodBad = NewIsFeelingGoodBad                
            };
            TagList.Add(newTag);
        }

        public void DeleteTag()
        {
            dbClass.DeleteTagFromDB(SelectedListItem.TagID.Value);

            Tag currentTag = TagList.Where(tag => SelectedListItem.TagID.Value == tag.TagID).FirstOrDefault();
            if (currentTag == null)
                return;
            TagList.Remove(currentTag);
        }

        //=====================

        private bool? _NewIsFeelingGoodBad;
        public bool? NewIsFeelingGoodBad
        {
            get { return _NewIsFeelingGoodBad; }
            set
            {
                _NewIsFeelingGoodBad = value;
                OnPropertyChanged("NewIsFeelingGoodBad");
            }
        }

        //======================

        private string _NewTagTextBoxString;
        public string NewTagTextBoxString
        {
            get { return _NewTagTextBoxString; }
            set
            {
                _NewTagTextBoxString = value;
                OnPropertyChanged("NewTagTextBoxString");
            }
        }


        //======================
        private string _simpleTextBoxString;
        public string SimpleTextBoxString
        {
            get
            {
                return _simpleTextBoxString;
            }
            set
            {
                _simpleTextBoxString = value;
                OnPropertyChanged("SimpleTextBoxString"); //this binds to the Content Tags in the XAML
            }
        }

        //======================
        private ObservableCollection<Tag> _TagList;
        public ObservableCollection<Tag> TagList
        {
            get { return _TagList; }
            set
            {
                _TagList = value;
                OnPropertyChanged("TagList");
            }
        }

        //======================
        private ObservableCollection<string> _TagNameList;
        public ObservableCollection<string> TagNameList
        {
            get { return _TagNameList; }
            set
            {
                _TagNameList = value;
                OnPropertyChanged("TagNameList");
            }
        }

        //========================================================
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
