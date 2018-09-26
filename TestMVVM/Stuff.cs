using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM
{
    public class Tag:INotifyPropertyChanged
    {

        public int? TagID { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool? IsUserCreated { get; set; }
        public bool? IsFeelingGoodBad { get; set; }

        public Tag()
        {
            TagID = null;
            ParentCategoryID = null;
            Name = null;
            Color = null;
            IsUserCreated = null;
            IsFeelingGoodBad = null;
        }

        public Tag(int? tagID, int? parentCategoryID, string name, string color, bool? isUserCreated, bool? isFeelingGoodBad)
        {
            TagID = tagID;
            ParentCategoryID = parentCategoryID;
            Name = name;
            Color = color;
            IsUserCreated = isUserCreated;
            IsFeelingGoodBad = isFeelingGoodBad;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class FoodEntry
    {
        int? FoodEntryID;
        string Picture;
        DateTime DateTime;
        string Description;
        bool? IsFeelingGoodBad;

        public FoodEntry()
        {
            FoodEntryID = null;
            Picture = null;
            DateTime = new DateTime();
            Description = null;
            IsFeelingGoodBad = null;
        }

        public FoodEntry(int? foodEntryID, string picture, DateTime dateTime, string description, bool? isFeelingGoodBad)
        {
            FoodEntryID = foodEntryID;
            Picture = picture;
            DateTime = dateTime;
            Description = description;
            IsFeelingGoodBad = isFeelingGoodBad;
        }
    }
}
