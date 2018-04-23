Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Input

Namespace FilteredData.Commands
	Public Class DelegateCommand
		Implements ICommand
		#Region "Constructors"

		''' <summary>
		'''     Constructor
		''' </summary>
		Public Sub New(ByVal executeMethod As Action)
			Me.New(executeMethod, Nothing, False)
		End Sub

		''' <summary>
		'''     Constructor
		''' </summary>
		Public Sub New(ByVal executeMethod As Action, ByVal canExecuteMethod As Func(Of Boolean))
			Me.New(executeMethod, canExecuteMethod, False)
		End Sub

		''' <summary>
		'''     Constructor
		''' </summary>
		Public Sub New(ByVal executeMethod As Action, ByVal canExecuteMethod As Func(Of Boolean), ByVal isAutomaticRequeryDisabled As Boolean)
			If executeMethod Is Nothing Then
				Throw New ArgumentNullException("executeMethod")
			End If

			_executeMethod = executeMethod
			_canExecuteMethod = canExecuteMethod
			_isAutomaticRequeryDisabled = isAutomaticRequeryDisabled
		End Sub

		#End Region

		#Region "Public Methods"

		''' <summary>
		'''     Method to determine if the command can be executed
		''' </summary>
		Public Function CanExecute() As Boolean
			If _canExecuteMethod IsNot Nothing Then
				Return _canExecuteMethod()
			End If
			Return True
		End Function

		''' <summary>
		'''     Execution of the command
		''' </summary>
		Public Sub Execute()
			If _executeMethod IsNot Nothing Then
				_executeMethod()
			End If
		End Sub

		''' <summary>
		'''     Property to enable or disable CommandManager's automatic requery on this command
		''' </summary>
		Public Property IsAutomaticRequeryDisabled() As Boolean
			Get
				Return _isAutomaticRequeryDisabled
			End Get
			Set(ByVal value As Boolean)
				If _isAutomaticRequeryDisabled <> value Then
					If value Then
						CommandManagerHelper.RemoveHandlersFromRequerySuggested(_canExecuteChangedHandlers)
					Else
						CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangedHandlers)
					End If
					_isAutomaticRequeryDisabled = value
				End If
			End Set
		End Property

		''' <summary>
		'''     Raises the CanExecuteChaged event
		''' </summary>
		Public Sub RaiseCanExecuteChanged()
			OnCanExecuteChanged()
		End Sub

		''' <summary>
		'''     Protected virtual method to raise CanExecuteChanged event
		''' </summary>
		Protected Overridable Sub OnCanExecuteChanged()
			CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangedHandlers)
		End Sub

		#End Region

		#Region "ICommand Members"

		''' <summary>
		'''     ICommand.CanExecuteChanged implementation
		''' </summary>
		Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
			AddHandler(ByVal value As EventHandler)
				If (Not _isAutomaticRequeryDisabled) Then
					AddHandler CommandManager.RequerySuggested, value
				End If
				CommandManagerHelper.AddWeakReferenceHandler(_canExecuteChangedHandlers, value, 2)
			End AddHandler
			RemoveHandler(ByVal value As EventHandler)
				If (Not _isAutomaticRequeryDisabled) Then
					RemoveHandler CommandManager.RequerySuggested, value
				End If
				CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
			End RaiseEvent
		End Event

		Private Function ICommand_CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
			Return CanExecute()
		End Function

		Private Sub ICommand_Execute(ByVal parameter As Object) Implements ICommand.Execute
			Execute()
		End Sub

		#End Region

		#Region "Data"

		Private ReadOnly _executeMethod As Action = Nothing
		Private ReadOnly _canExecuteMethod As Func(Of Boolean) = Nothing
		Private _isAutomaticRequeryDisabled As Boolean = False
		Private _canExecuteChangedHandlers As List(Of WeakReference)

		#End Region
	End Class

	''' <summary>
	'''     This class allows delegating the commanding logic to methods passed as parameters,
	'''     and enables a View to bind commands to objects that are not part of the element tree.
	''' </summary>
	''' <typeparam name="T">Type of the parameter passed to the delegates</typeparam>
	Public Class DelegateCommand(Of T)
		Implements ICommand
		#Region "Constructors"

		''' <summary>
		'''     Constructor
		''' </summary>
		Public Sub New(ByVal executeMethod As Action(Of T))
			Me.New(executeMethod, Nothing, False)
		End Sub

		''' <summary>
		'''     Constructor
		''' </summary>
		Public Sub New(ByVal executeMethod As Action(Of T), ByVal canExecuteMethod As Func(Of T, Boolean))
			Me.New(executeMethod, canExecuteMethod, False)
		End Sub

		''' <summary>
		'''     Constructor
		''' </summary>
		Public Sub New(ByVal executeMethod As Action(Of T), ByVal canExecuteMethod As Func(Of T, Boolean), ByVal isAutomaticRequeryDisabled As Boolean)
			If executeMethod Is Nothing Then
				Throw New ArgumentNullException("executeMethod")
			End If

			_executeMethod = executeMethod
			_canExecuteMethod = canExecuteMethod
			_isAutomaticRequeryDisabled = isAutomaticRequeryDisabled
		End Sub

		#End Region

		#Region "Public Methods"

		''' <summary>
		'''     Method to determine if the command can be executed
		''' </summary>
		Public Function CanExecute(ByVal parameter As T) As Boolean
			If _canExecuteMethod IsNot Nothing Then
				Return _canExecuteMethod(parameter)
			End If
			Return True
		End Function

		''' <summary>
		'''     Execution of the command
		''' </summary>
		Public Sub Execute(ByVal parameter As T)
			If _executeMethod IsNot Nothing Then
				_executeMethod(parameter)
			End If
		End Sub

		''' <summary>
		'''     Raises the CanExecuteChaged event
		''' </summary>
		Public Sub RaiseCanExecuteChanged()
			OnCanExecuteChanged()
		End Sub

		''' <summary>
		'''     Protected virtual method to raise CanExecuteChanged event
		''' </summary>
		Protected Overridable Sub OnCanExecuteChanged()
			CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangedHandlers)
		End Sub

		''' <summary>
		'''     Property to enable or disable CommandManager's automatic requery on this command
		''' </summary>
		Public Property IsAutomaticRequeryDisabled() As Boolean
			Get
				Return _isAutomaticRequeryDisabled
			End Get
			Set(ByVal value As Boolean)
				If _isAutomaticRequeryDisabled <> value Then
					If value Then
						CommandManagerHelper.RemoveHandlersFromRequerySuggested(_canExecuteChangedHandlers)
					Else
						CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangedHandlers)
					End If
					_isAutomaticRequeryDisabled = value
				End If
			End Set
		End Property

		#End Region

		#Region "ICommand Members"

		''' <summary>
		'''     ICommand.CanExecuteChanged implementation
		''' </summary>
		Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
			AddHandler(ByVal value As EventHandler)
				If (Not _isAutomaticRequeryDisabled) Then
					AddHandler CommandManager.RequerySuggested, value
				End If
				CommandManagerHelper.AddWeakReferenceHandler(_canExecuteChangedHandlers, value, 2)
			End AddHandler
			RemoveHandler(ByVal value As EventHandler)
				If (Not _isAutomaticRequeryDisabled) Then
					RemoveHandler CommandManager.RequerySuggested, value
				End If
				CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As System.Object, ByVal e As System.EventArgs)
			End RaiseEvent
		End Event

		Private Function ICommand_CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
			' if T is of value type and the parameter is not
			' set yet, then return false if CanExecute delegate
			' exists, else return true
			If parameter Is Nothing AndAlso GetType(T).IsValueType Then
				Return (_canExecuteMethod Is Nothing)
			End If
			Return CanExecute(CType(parameter, T))
		End Function

		Private Sub ICommand_Execute(ByVal parameter As Object) Implements ICommand.Execute
			Execute(CType(parameter, T))
		End Sub

		#End Region

		#Region "Data"

		Private ReadOnly _executeMethod As Action(Of T) = Nothing
		Private ReadOnly _canExecuteMethod As Func(Of T, Boolean) = Nothing
		Private _isAutomaticRequeryDisabled As Boolean = False
		Private _canExecuteChangedHandlers As List(Of WeakReference)

		#End Region
	End Class

	''' <summary>
	'''     This class contains methods for the CommandManager that help avoid memory leaks by
	'''     using weak references.
	''' </summary>
	Friend Class CommandManagerHelper
		Friend Shared Sub CallWeakReferenceHandlers(ByVal handlers As List(Of WeakReference))
			If handlers IsNot Nothing Then
				' Take a snapshot of the handlers before we call out to them since the handlers
				' could cause the array to me modified while we are reading it.

				Dim callees(handlers.Count - 1) As EventHandler
				Dim count As Integer = 0

				For i As Integer = handlers.Count - 1 To 0 Step -1
					Dim reference As WeakReference = handlers(i)
					Dim handler As EventHandler = TryCast(reference.Target, EventHandler)
					If handler Is Nothing Then
						' Clean up old handlers that have been collected
						handlers.RemoveAt(i)
					Else
						callees(count) = handler
						count += 1
					End If
				Next i

				' Call the handlers that we snapshotted
				For i As Integer = 0 To count - 1
					Dim handler As EventHandler = callees(i)
					handler(Nothing, EventArgs.Empty)
				Next i
			End If
		End Sub

		Friend Shared Sub AddHandlersToRequerySuggested(ByVal handlers As List(Of WeakReference))
			If handlers IsNot Nothing Then
				For Each handlerRef As WeakReference In handlers
					Dim handler As EventHandler = TryCast(handlerRef.Target, EventHandler)
					If handler IsNot Nothing Then
						AddHandler CommandManager.RequerySuggested, handler
					End If
				Next handlerRef
			End If
		End Sub

		Friend Shared Sub RemoveHandlersFromRequerySuggested(ByVal handlers As List(Of WeakReference))
			If handlers IsNot Nothing Then
				For Each handlerRef As WeakReference In handlers
					Dim handler As EventHandler = TryCast(handlerRef.Target, EventHandler)
					If handler IsNot Nothing Then
						RemoveHandler CommandManager.RequerySuggested, handler
					End If
				Next handlerRef
			End If
		End Sub

		Friend Shared Sub AddWeakReferenceHandler(ByRef handlers As List(Of WeakReference), ByVal handler As EventHandler)
			AddWeakReferenceHandler(handlers, handler, -1)
		End Sub

		Friend Shared Sub AddWeakReferenceHandler(ByRef handlers As List(Of WeakReference), ByVal handler As EventHandler, ByVal defaultListSize As Integer)
			If handlers Is Nothing Then
				handlers = (If(defaultListSize > 0, New List(Of WeakReference)(defaultListSize), New List(Of WeakReference)()))
			End If

			handlers.Add(New WeakReference(handler))
		End Sub

		Friend Shared Sub RemoveWeakReferenceHandler(ByVal handlers As List(Of WeakReference), ByVal handler As EventHandler)
			If handlers IsNot Nothing Then
				For i As Integer = handlers.Count - 1 To 0 Step -1
					Dim reference As WeakReference = handlers(i)
					Dim existingHandler As EventHandler = TryCast(reference.Target, EventHandler)
					If (existingHandler Is Nothing) OrElse (existingHandler Is handler) Then
						' Clean up old handlers that have been collected
						' in addition to the handler that is to be removed.
						handlers.RemoveAt(i)
					End If
				Next i
			End If
		End Sub
	End Class
End Namespace
