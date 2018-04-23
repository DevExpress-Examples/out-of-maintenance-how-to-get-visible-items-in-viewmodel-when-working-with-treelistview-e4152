Imports System
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.TreeList

Namespace FilteredData.MyGridControl
    Class MyTreeListDataProvider
	    Inherits TreeListDataProvider
	    Public Sub New(view As TreeListView)
		    MyBase.New(view)
	    End Sub
	    Protected Overrides Function CreateDataController() As TreeListDataController
		    Return New MyTreeListDataController(Me)
	    End Function
	    Public Delegate Sub RefreshFilteredDataEventHandler(sender As Object, e As MyFilterEventArgs)
	    Public Event FilteredData As RefreshFilteredDataEventHandler
	    Protected Friend Overridable Sub OnRefreshFilteredData(e As MyFilterEventArgs)
		    RaiseEvent FilteredData(Me, e)
	    End Sub
    End Class
    Class MyTreeListDataController
	    Inherits TreeListDataController
	    Public Sub New(provider As MyTreeListDataProvider)
		    MyBase.New(provider)
	    End Sub
	    Public Shadows ReadOnly Property DataProvider() As MyTreeListDataProvider
		    Get
			    Return DirectCast(MyBase.DataProvider, MyTreeListDataProvider)
		    End Get
	    End Property
	    Protected Overrides Function CalcNodeVisibility(node As DevExpress.Data.TreeList.TreeListNodeBase, Optional customFilterFitPredicate As Func(Of Object, Boolean) = Nothing) As Boolean
		    Dim visible As Boolean = MyBase.CalcNodeVisibility(node, customFilterFitPredicate)
		    DataProvider.OnRefreshFilteredData(New MyFilterEventArgs(DirectCast(node, TreeListNode), visible))
		    Return visible
	    End Function
    End Class
    Public Class MyFilterEventArgs
	    Inherits EventArgs
	    Public Property Node() As TreeListNode
		    Get
			    Return m_Node
		    End Get
		    Set
			    m_Node = Value
		    End Set
	    End Property
	    Private m_Node As TreeListNode
	    Public Property IsVisible() As Boolean
		    Get
			    Return m_IsVisible
		    End Get
		    Set
			    m_IsVisible = Value
		    End Set
	    End Property
	    Private m_IsVisible As Boolean
	    Public Sub New(node As TreeListNode, visible As Boolean)
		    Me.Node = node
		    IsVisible = visible
	    End Sub
    End Class
End Namespace
