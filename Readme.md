<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128650671/16.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4152)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[MainWindow.xaml](./CS/FilteredData/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/FilteredData/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/FilteredData/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/FilteredData/MainWindow.xaml.vb))
* [MyGridControl.cs](./CS/FilteredData/MyGridControl/MyGridControl.cs) (VB: [MyGridControl.vb](./VB/FilteredData/MyGridControl/MyGridControl.vb))
* [MyTreeListDataProvider.cs](./CS/FilteredData/MyGridControl/MyTreeListDataProvider.cs) (VB: [MyTreeListDataProvider.vb](./VB/FilteredData/MyGridControl/MyTreeListDataProvider.vb))
* [MyTreeListView.cs](./CS/FilteredData/MyGridControl/MyTreeListView.cs) (VB: [MyTreeListView.vb](./VB/FilteredData/MyGridControl/MyTreeListView.vb))
* [ViewModel.cs](./CS/FilteredData/ViewModel/ViewModel.cs) (VB: [ViewModel.vb](./VB/FilteredData/ViewModel/ViewModel.vb))
<!-- default file list end -->
# How to get visible items in ViewModel when working with TreeListView


<p>By default, TreeListView does not have a property where you can bind an item collection from a ViewModel and get visible (not filtered) items. This example demonstrates how to create such a property in a custom TreeListView. To implement this, it is necessary to create GridControl, TreeListView, and TreeListDataProvider descendants. This sample is applicable only when TreeListView uses Key Field and Parent Field to create its hierarchy.</p>

<br/>


