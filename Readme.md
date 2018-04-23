# How to get visible items in ViewModel when working with TreeListView


<p>By default, TreeListView does not have a property where you can bind an item collection from a ViewModel and get visible (not filtered) items. This example demonstrates how to create such a property in a custom TreeListView. To implement this, it is necessary to create GridControl, TreeListView, and TreeListDataProvider descendants. This sample is applicable only when TreeListView uses Key Field and Parent Field to create its hierarchy.</p>

<br/>


