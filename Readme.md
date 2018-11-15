<!-- default file list -->
*Files to look at*:

* **[MainWindow.xaml](./CS/FilteredData/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/FilteredData/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/FilteredData/MainWindow.xaml.cs) (VB: [MainWindow.xaml](./VB/FilteredData/MainWindow.xaml))
* [MyGridControl.cs](./CS/FilteredData/MyGridControl/MyGridControl.cs) (VB: [MyGridControl.vb](./VB/FilteredData/MyGridControl/MyGridControl.vb))
* [MyTreeListDataProvider.cs](./CS/FilteredData/MyGridControl/MyTreeListDataProvider.cs) (VB: [MyTreeListDataProvider.vb](./VB/FilteredData/MyGridControl/MyTreeListDataProvider.vb))
* [MyTreeListView.cs](./CS/FilteredData/MyGridControl/MyTreeListView.cs) (VB: [MyTreeListView.vb](./VB/FilteredData/MyGridControl/MyTreeListView.vb))
* [ViewModel.cs](./CS/FilteredData/ViewModel/ViewModel.cs) (VB: [ViewModel.vb](./VB/FilteredData/ViewModel/ViewModel.vb))
<!-- default file list end -->
# How to get visible items in ViewModel when working with TreeListView


<p>By default, TreeListView does not have a property where you can bind an item collection from a ViewModel and get visible (not filtered) items. This example demonstrates how to create such a property in a custom TreeListView. To implement this, it is necessary to create GridControl, TreeListView, and TreeListDataProvider descendants. This sample is applicable only when TreeListView uses Key Field and Parent Field to create its hierarchy.</p>

<br/>


