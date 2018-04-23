using System;
using System.Collections.ObjectModel;

namespace FilteredData.Model {
    class DataHelper {
        public static ObservableCollection<GridItem> GetDataSource(int count) {
            ObservableCollection<GridItem> collection = new ObservableCollection<GridItem>();
            Random rand = new Random();
            for (int i = 0; i < count; i++) {
                collection.Add(new GridItem(DateTime.Now.AddMinutes(count * i).AddDays((i - count / 2) * count), String.Format("Name{0}", i), i, i % count / 2));
            }
            return collection;
        }
    }

    public class GridItem {
       public GridItem(DateTime date, string name, int iD, int parentID) {
            _Date = date;
            _Name = name;
            _ID = iD;
            _ParentID = parentID;
        }
        public GridItem() {

        }

        private int _ID;
        public int ID {
            get { return _ID; }
            set {
                _ID = value;
            }
        }

        private int _ParentID;
        public int ParentID {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        private string _Name;
        public string Name {
            get { return _Name; }
            set {
                _Name = value;
            }
        }

        private DateTime _Date;
        public DateTime Date {
            get { return _Date; }
            set {
                _Date = value;

            }
        }
    }
}
