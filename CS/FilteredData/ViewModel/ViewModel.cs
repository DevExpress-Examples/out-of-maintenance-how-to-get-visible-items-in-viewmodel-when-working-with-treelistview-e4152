﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Grid;
using FilteredData.Model;
using FilteredData.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace FilteredData.ViewModel
{
    class ViewModel
    {
        public ViewModel()
        {
            FilteredData = new ObservableCollection<object>();
        }

        // Fields...
        public ObservableCollection<object> FilteredData
        {
            get;
            set;
        }

        private ObservableCollection<GridItem> _DataSource;
        public ObservableCollection<GridItem> DataSource
        {
            get
            {
                if (_DataSource == null)
                    _DataSource = DataHelper.GetDataSource(20);
                return _DataSource;
            }
            set
            {
                _DataSource = value;

            }
        }

        private DelegateCommand addFilteredCommand;
        public ICommand AddFilteredCommand
        {
            get
            {
                if (addFilteredCommand == null)
                {
                    addFilteredCommand = new DelegateCommand(AddFiltered);
                }
                return addFilteredCommand;
            }
        }

        private void AddFiltered()
        {
            if (!FilteredData.Contains(_DataSource[1]))
                FilteredData.Add(_DataSource[1]);
        }


        private DelegateCommand removeFilteredCommand;
        public ICommand RemoveFilteredCommand
        {
            get
            {
                if (removeFilteredCommand == null)
                {
                    removeFilteredCommand = new DelegateCommand(RemoveFiltered);
                }
                return removeFilteredCommand;
            }
        }

        private void RemoveFiltered()
        {
            FilteredData.Remove(_DataSource[1]);
        }
    }
}
