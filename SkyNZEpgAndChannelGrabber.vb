Imports Custom_Data_Grabber
Imports System.Threading
Imports System.Linq
Imports System.IO
Imports TvDatabase
Imports TvLibrary.Channels
Imports TvLibrary.Epg
Imports TvLibrary.Interfaces
Imports TvEngine
Imports TvControl
Imports TvLibrary.Log
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.CompilerServices
Imports DirectShowLib.BDA
Imports Microsoft.VisualBasic
Imports System.Environment
Imports TvLibrary.Interfaces.TvConstants
Imports TvService
Imports MediaPortal.Common.Utils

<Assembly: CompatibleVersion("1.1.6.27796")> 

Public Class SkyNzEpgAndChannelGrabber

    Implements ITvServerPlugin
    Dim _settings As Settings
    Dim WithEvents skygrabber As SkyGrabber
    Dim WithEvents timer As Timers.Timer

    'Private Sub Elapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)
    'OnTick()
    'End Sub

    Sub OnTick() Handles Timer.Elapsed

        If (Not _settings.IsGrabbing AndAlso _settings.AutoUpdate) Then
            If _settings.EveryHour Then
                If _settings.LastUpdate.AddHours(_settings.UpdateInterval) < Now Then
                    skygrabber.Grab()
                End If
            ElseIf (((Now.Hour = _settings.UpdateTime.Hour) And (DateTime.Compare(_settings.LastUpdate.Date, Now.Date) <> 0)) AndAlso ((Now.Minute >= _settings.UpdateTime.Minute) And (Now.Minute <= (_settings.UpdateTime.Minute + 10)))) Then
                Select Case Now.DayOfWeek
                    Case DayOfWeek.Sunday
                        If _settings.Sun Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                    Case DayOfWeek.Monday
                        If _settings.Mon Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                    Case DayOfWeek.Tuesday
                        If _settings.Tue Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                    Case DayOfWeek.Wednesday
                        If _settings.Wed Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                    Case DayOfWeek.Thursday
                        If _settings.Thu Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                    Case DayOfWeek.Friday
                        If _settings.Fri Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                    Case DayOfWeek.Saturday
                        If _settings.Sat Then
                            skygrabber.Grab()
                        End If
                        Exit Select
                End Select
            End If
        End If
    End Sub

    Private Sub skygrabber_OnMessage(ByVal text As String, ByVal updateLast As Boolean) Handles SkyGrabber.OnMessage
        If Not updateLast Then
            Log.Write("Sky Plugin : " & [Text])
        End If
    End Sub

    Public Sub Start(ByVal controller As IController) Implements ITvServerPlugin.Start
        skygrabber = New SkyGrabber
        _settings = New Settings
        _settings.IsGrabbing = False
        timer = New Timers.Timer
        timer.Interval = 1800000
        timer.Start()
    End Sub

    Public Sub Stopit() Implements ITvServerPlugin.Stop
        timer.Start()
        _settings = Nothing
        skygrabber = Nothing
    End Sub

    Public ReadOnly Property Author As String Implements ITvServerPlugin.Author
        Get
            Return "DJBlu"
        End Get
    End Property

    Public ReadOnly Property MasterOnly As Boolean Implements ITvServerPlugin.MasterOnly
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ITvServerPlugin.Name
        Get
            Return "Sky NZ Channel and EPG Grabber"
        End Get
    End Property

    Public ReadOnly Property Setup As SetupTv.SectionSettings Implements ITvServerPlugin.Setup
        Get
            Return New Setup()
        End Get
    End Property

    'Private Overridable Property skygrabber As SkyGrabber
    '    Get
    '        Return _skygrabber
    '    End Get
    '    <MethodImpl(MethodImplOptions.Synchronized)> _
    '    Set(ByVal WithEventsValue As SkyGrabber)
    '        Dim handler As SkyGrabber.OnMessageEventHandler = New SkyGrabber.OnMessageEventHandler(AddressOf skygrabber_OnMessage)
    '        If (Not _skygrabber Is Nothing) Then
    '            RemoveHandler _skygrabber.OnMessage, handler
    '        End If
    '        _skygrabber = WithEventsValue
    '        If (Not _skygrabber Is Nothing) Then
    '            AddHandler _skygrabber.OnMessage, handler
    '        End If
    '    End Set
    'End Property

    'Public Property timer As Threading.Timer
    '    Get
    '        Return _timer
    '    End Get
    '    <MethodImpl(MethodImplOptions.Synchronized)> _
    '    Set(ByVal WithEventsValue As Threading.Timer)
    '        Dim handler As ElapsedEventHandler = New ElapsedEventHandler(AddressOf OnTick)
    '        If (Not _timer Is Nothing) Then
    '            RemoveHandler _timer.Elapsed, handler
    '        End If
    '        _timer = WithEventsValue
    '        If (Not _timer Is Nothing) Then
    '            AddHandler _timer.Elapsed, handler
    '        End If
    '    End Set
    'End Property

    Public ReadOnly Property Version As String Implements ITvServerPlugin.Version
        Get
            Return "1.7.1.1"
        End Get
    End Property
End Class

Public Class Settings
    ' Fields
    Private Shared _autoupdate As Boolean
    Private Shared _bouquetid As Integer
    Private Shared _cardtouseindex As Integer
    Private Shared _deleteoldchannels As Boolean
    Private Shared _diseqc As Integer
    Private Shared _everyhour As Boolean
    Private Shared _frequency As Integer
    Private Shared _fri As Boolean
    Private Shared _grabtime As Integer
    Private Shared _ignorescrambled As Boolean
    Private Shared _isgrabbing As Boolean
    Private Shared _isloading As Boolean
    Private Shared _lastupdate As DateTime
    Private Shared _logodirectory As String
    Private Shared _modulation As Integer
    Private Shared _mon As Boolean
    Private Shared _myVar As String
    Private Shared _nid As Integer
    Private Shared _oldchannelfolder As String
    Private Shared _ondaysat As Boolean
    Private Shared _polarisation As Integer
    Private Shared _regionid As Integer
    Private Shared _regionindex As Integer
    Private Shared _replacesdwithhd As Boolean
    Private Shared _sat As Boolean
    Private Shared _serviceid As Integer
    Private Shared _settingsloaded As Boolean
    Private Shared _sun As Boolean
    Private Shared _switchingfrequency As Integer
    Private Shared _symbolrate As Integer
    Private ReadOnly _syncObj As Object = RuntimeHelpers.GetObjectValue(New Object)
    Private Shared _thu As Boolean
    Private Shared _transportid As Integer
    Private Shared _tue As Boolean
    Private Shared _updatechannels As Boolean
    Private Shared _updateepg As Boolean
    Private Shared _updateinterval As Integer
    Private Shared _updatelogos As Boolean
    Private Shared _updatetime As DateTime
    Private Shared _useextrainfo As Boolean
    Private Shared _usenotsetmodhd As Boolean
    Private Shared _usenotsetmodsd As Boolean
    Private Shared _useskycategories As Boolean
    Private Shared _useskynumbers As Boolean
    Private Shared _useskyregions As Boolean
    Private Shared _usethrottle As Boolean
    Private Shared _wed As Boolean
    Private ReadOnly _cats As Dictionary(Of Byte, String) = New Dictionary(Of Byte, String)
    Private ReadOnly _layer As TvBusinessLayer = New TvBusinessLayer
    Public _returnlist As List(Of Integer) = New List(Of Integer)
    Private ReadOnly _themes As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

    'Methods
    Public Function GetSkySetting(ByVal setting As String, ByVal defaultvalue As Object) As String
        Return _layer.GetSetting(("SKYUKPLUG_" & Setting), defaultvalue.ToString).Value
    End Function

    Public Sub UpdateSetting(ByVal _Setting As String, ByVal value As String)
        Dim setting As Setting = _layer.GetSetting(("SKYUKPLUG_" & _Setting), "0")
        setting.Value = value.ToString
        setting.Persist()
    End Sub

    Public Function GetCategoryByTextBoxNum(ByVal textBoxNum As Integer) As String
        Dim expression As Object = _syncObj
        ObjectFlowControl.CheckForSyncLockOnValueType(expression)
        SyncLock expression
            Return GetSkySetting(("Cat" & textBoxNum), "-1,., ")
        End SyncLock
    End Function

    'Properties

    Public Property Modulation As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _modulation
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _modulation) Then
                    _modulation = value
                    UpdateSetting("modulation", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property GrabTime As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _grabtime
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _grabtime) Then
                    _grabtime = value
                    UpdateSetting("GrabTime", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Frequency As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _frequency
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _frequency) Then
                    _frequency = value
                    UpdateSetting("frequency", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property SymbolRate As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _symbolrate
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _symbolrate) Then
                    _symbolrate = value
                    UpdateSetting("SymbolRate", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Nid As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _nid
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _nid) Then
                    _nid = value
                    UpdateSetting("NID", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Polarisation As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _polarisation
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _polarisation) Then
                    _polarisation = value
                    UpdateSetting("polarisation", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property ServiceId As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _serviceid
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _serviceid) Then
                    _serviceid = value
                    UpdateSetting("ServiceID", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property TransportId As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _transportid
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _transportid) Then
                    _transportid = value
                    UpdateSetting("TransportID", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property AutoUpdate As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _autoupdate
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _autoupdate) Then
                    _autoupdate = value
                    UpdateSetting("AutoUpdate", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseExtraInfo As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _useextrainfo
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _useextrainfo) Then
                    _useextrainfo = value
                    UpdateSetting("useExtraInfo", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UpdateChannels As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _updatechannels
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _updatechannels) Then
                    _updatechannels = value
                    UpdateSetting("UpdateChannels", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property EveryHour As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _everyhour
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _everyhour) Then
                    _everyhour = value
                    UpdateSetting("EveryHour", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property OnDaysAt As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _ondaysat
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _ondaysat) Then
                    _ondaysat = value
                    UpdateSetting("OnDaysAt", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    'Days

    Public Property Mon As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _mon
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _mon) Then
                    _mon = value
                    UpdateSetting("Mon", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Tue As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _tue
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _tue) Then
                    _tue = value
                    UpdateSetting("Tue", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Wed As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _wed
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _wed) Then
                    _wed = value
                    UpdateSetting("Wed", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Thu As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _thu
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _thu) Then
                    _thu = value
                    UpdateSetting("Thu", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Fri As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _fri
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _fri) Then
                    _fri = value
                    UpdateSetting("Fri", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Sat As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _sat
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _sat) Then
                    _sat = value
                    UpdateSetting("Sat", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property Sun As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _sun
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _sun) Then
                    _sun = value
                    UpdateSetting("Sun", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UpdateTime As DateTime
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _updatetime
            End SyncLock
        End Get
        Set(ByVal value As DateTime)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (DateTime.Compare(value, _updatetime) <> 0) Then
                    _updatetime = value
                    UpdateSetting("UpdateTime", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    'Functions

    Public Property ReplaceSDwithHd As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _replacesdwithhd
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _replacesdwithhd) Then
                    _replacesdwithhd = value
                    UpdateSetting("ReplaceSDwithHD", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property IgnoreScrambled As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _ignorescrambled
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _ignorescrambled) Then
                    _ignorescrambled = value
                    UpdateSetting("IgnoreScrambled", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseNotSetModSd As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _usenotsetmodsd
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _usenotsetmodsd) Then
                    _usenotsetmodsd = value
                    UpdateSetting("UseNotSetModSD", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseNotSetModHD As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _usenotsetmodhd
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _usenotsetmodhd) Then
                    _usenotsetmodhd = value
                    UpdateSetting("UseNotSetModHD", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UpdateEpg As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _updateepg
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _updateepg) Then
                    _updateepg = value
                    UpdateSetting("UpdateEPG", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UpdateInterval As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _updateinterval
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _updateinterval) Then
                    _updateinterval = value
                    UpdateSetting("UpdateInterval", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property RegionId As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _regionid
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _regionid) Then
                    _regionid = value
                    UpdateSetting("RegionID", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property BouquetId As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If _replacesdwithhd Then
                    Return (_bouquetid + 4)
                End If
                Return _bouquetid
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _bouquetid) Then
                    _bouquetid = value
                    UpdateSetting("BouquetID", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property IsLoading As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _isloading
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _isloading) Then
                    _isloading = value
                    UpdateSetting("IsLoading", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property CardMap As List(Of Integer) 'older code that works for mapping channels.
        Get
            Dim returnlist As New List(Of Integer)
            Dim stringtouse As String = GetSkySetting("CardMap", "")
            If stringtouse.Length > 0 Then
                If stringtouse.Length = 1 Then
                    returnlist.Add(Convert.ToInt32(stringtouse))
                Else
                    Dim array1() As String = stringtouse.Split(",")
                    If array1.Count > 0 Then
                        returnlist.AddRange(From str In array1 Select Convert.ToInt32(str))
                    End If
                End If
            End If
            Return returnlist
        End Get
        Set(ByVal value As List(Of Integer))
            Dim str As New StringBuilder
            If value.Count > 0 Then
                For Each num As Integer In value
                    str.Append("," & num.ToString)
                Next
                str.Remove(0, 1)
            End If
            UpdateSetting("CardMap", str.ToString)
        End Set
    End Property

    'Public Property CardMap As List(Of Integer) 'new code that doesn't appear to work.
    '    Get
    '        Dim expression As Object = _syncObj
    '        ObjectFlowControl.CheckForSyncLockOnValueType(expression)
    '        SyncLock expression
    '            Return _returnlist
    '        End SyncLock
    '    End Get
    '    Set(ByVal value As List(Of Integer))
    '        Dim expression As Object = _syncObj
    '        ObjectFlowControl.CheckForSyncLockOnValueType(expression)
    '        SyncLock expression
    '            Dim builder As New StringBuilder
    '            _returnlist.Clear()
    '            If (value.Count > 0) Then
    '                Dim num As Integer
    '                For Each num In value
    '                    If Not _returnlist.Contains(num) Then
    '                        _returnlist.Add(num)
    '                        builder.Append(("," & num.ToString))
    '                    End If
    '                Next
    '                builder.Remove(0, 1)
    '            End If
    '            UpdateSetting("CardMap", builder.ToString)
    '        End SyncLock
    '    End Set
    'End Property

    Public Property LastUpdate As DateTime
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _lastupdate
            End SyncLock
        End Get
        Set(ByVal value As DateTime)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (DateTime.Compare(value, _lastupdate) <> 0) Then
                    _lastupdate = value
                    UpdateSetting("LastUpdate", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseSkyNumbers As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _useskynumbers
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _useskynumbers) Then
                    _useskynumbers = value
                    UpdateSetting("UseSkyNumbers", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseSkyCategories As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _useskycategories
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _useskycategories) Then
                    _useskycategories = value
                    UpdateSetting("UseSkyCategories", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseSkyRegions As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _useskyregions
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _useskyregions) Then
                    _useskyregions = value
                    UpdateSetting("UseSkyRegions", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property DeleteOldChannels As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _deleteoldchannels
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _deleteoldchannels) Then
                    _deleteoldchannels = value
                    UpdateSetting("DeleteOldChannels", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property OldChannelFolder As String
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _oldchannelfolder
            End SyncLock
        End Get
        Set(ByVal value As String)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _oldchannelfolder) Then
                    _oldchannelfolder = value
                    UpdateSetting("OldChannelFolder", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property LogoDirectory As String
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _logodirectory
            End SyncLock
        End Get
        Set(ByVal value As String)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _logodirectory) Then
                    _logodirectory = value
                    UpdateSetting("LogoDirectory", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property RegionIndex As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _regionindex
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _regionindex) Then
                    _regionindex = value
                    UpdateSetting("RegionIndex", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property CardToUseIndex As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _cardtouseindex
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _cardtouseindex) Then
                    _cardtouseindex = value
                    UpdateSetting("CardToUseIndex", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property DiseqC As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _diseqc
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _diseqc) Then
                    _diseqc = value
                    UpdateSetting("DiseqC", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property SwitchingFrequency As Integer
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _switchingfrequency
            End SyncLock
        End Get
        Set(ByVal value As Integer)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _switchingfrequency) Then
                    _switchingfrequency = value
                    UpdateSetting("SwitchingFrequency", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property IsGrabbing As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _isgrabbing
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _isgrabbing) Then
                    _isgrabbing = value
                    UpdateSetting("IsGrabbing", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Function GetCategory(ByVal catByte As Byte) As String
        Dim expression As Object = _syncObj
        ObjectFlowControl.CheckForSyncLockOnValueType(expression)
        SyncLock expression
            If _cats.ContainsKey(CatByte) Then
                Return _cats.Item(CatByte)
            End If
            Return ""
        End SyncLock
    End Function

    Public Sub SetCategory(ByVal textBox As Integer, ByVal catByteText As String, ByVal name As String)
        Dim expression As Object = _syncObj
        ObjectFlowControl.CheckForSyncLockOnValueType(expression)
        SyncLock expression
            Dim num As Double
            If CatByteText.StartsWith("-") Then
                GoTo No_Category
            End If
            If Double.TryParse(CatByteText, num) Then
                If _cats.ContainsKey(CByte(Math.Round(num))) Then
                    Dim str As String = _cats.Item(CByte(Math.Round(num)))
                    If (str <> name) Then
                        _cats.Item(CByte(Math.Round(num))) = name
                        GoTo Has_Category
                    End If
                    GoTo No_Category
                End If
                _cats.Add(CByte(Math.Round(num)), name)
            End If
Has_Category:
            UpdateSetting(("Cat" & textBox), (CatByteText & ",.," & name))
No_Category:
        End SyncLock
    End Sub

    Public Function GetTheme(ByVal id As Integer) As String
        If _themes.ContainsKey(Id) Then
            Return _themes.Item(Id)
        End If
        Return ""
    End Function

    Public Sub New()
        If Not _themes.ContainsKey(0) Then
            _themes.Add(0, "No Category")
            _themes.Add(1, "")
            _themes.Add(2, "")
            _themes.Add(3, "")
            _themes.Add(4, "")
            _themes.Add(5, "")
            _themes.Add(6, "")
            _themes.Add(7, "")
            _themes.Add(8, "")
            _themes.Add(9, "")
            _themes.Add(10, "")
            _themes.Add(11, "")
            _themes.Add(12, "")
            _themes.Add(13, "")
            _themes.Add(14, "")
            _themes.Add(15, "")
            _themes.Add(&H10, "")
            _themes.Add(&H11, "")
            _themes.Add(&H12, "")
            _themes.Add(&H13, "")
            _themes.Add(20, "")
            _themes.Add(&H15, "")
            _themes.Add(&H16, "")
            _themes.Add(&H17, "")
            _themes.Add(&H18, "")
            _themes.Add(&H19, "")
            _themes.Add(&H1A, "")
            _themes.Add(&H1B, "")
            _themes.Add(&H1C, "")
            _themes.Add(&H1D, "")
            _themes.Add(30, "")
            _themes.Add(&H1F, "")
            _themes.Add(&H20, "Movie")
            _themes.Add(&H21, "Movie - Thriller")
            _themes.Add(&H22, "Movie - Action")
            _themes.Add(&H23, "Movie - Sci Fi")
            _themes.Add(&H24, "Movie - Comedy")
            _themes.Add(&H25, "Movie - Family")
            _themes.Add(&H26, "Movie - Romance")
            _themes.Add(&H27, "Movie - Historical")
            _themes.Add(40, "Movie - Factual")
            _themes.Add(&H29, "Movie - Animation")
            _themes.Add(&H2A, "Movie - Horror")
            _themes.Add(&H2B, "Movie - Documentary")
            _themes.Add(&H2C, "Movie - Documentary")
            _themes.Add(&H2D, "Movie - Documentary")
            _themes.Add(&H2E, "Movie - Western")
            _themes.Add(&H2F, "Movie - Other")
            _themes.Add(&H30, "")
            _themes.Add(&H31, "")
            _themes.Add(50, "")
            _themes.Add(&H33, "")
            _themes.Add(&H34, "")
            _themes.Add(&H35, "")
            _themes.Add(&H36, "")
            _themes.Add(&H37, "")
            _themes.Add(&H38, "")
            _themes.Add(&H39, "")
            _themes.Add(&H3A, "")
            _themes.Add(&H3B, "")
            _themes.Add(60, "")
            _themes.Add(&H3D, "")
            _themes.Add(&H3E, "")
            _themes.Add(&H3F, "")
            _themes.Add(&H40, "News & Documentaries")
            _themes.Add(&H41, "News & Documentaries - News & Weather")
            _themes.Add(&H42, "News & Documentaries - Magazine")
            _themes.Add(&H43, "News & Documentaries - Documentary")
            _themes.Add(&H44, "News & Documentaries - Discussion")
            _themes.Add(&H45, "News & Documentaries - Educational")
            _themes.Add(70, "News & Documentaries - Feature")
            _themes.Add(&H47, "News & Documentaries - Politics")
            _themes.Add(&H48, "News & Documentaries - News")
            _themes.Add(&H49, "News & Documentaries - Nature")
            _themes.Add(&H4A, "News & Documentaries - Religious")
            _themes.Add(&H4B, "News & Documentaries - Science")
            _themes.Add(&H4C, "News & Documentaries - Showbiz")
            _themes.Add(&H4D, "News & Documentaries - War Documentary")
            _themes.Add(&H4E, "News & Documentaries - Historical")
            _themes.Add(&H4F, "News & Documentaries - Other")
            _themes.Add(80, "")
            _themes.Add(&H51, "")
            _themes.Add(&H52, "")
            _themes.Add(&H53, "")
            _themes.Add(&H54, "")
            _themes.Add(&H55, "")
            _themes.Add(&H56, "")
            _themes.Add(&H57, "")
            _themes.Add(&H58, "")
            _themes.Add(&H59, "")
            _themes.Add(90, "")
            _themes.Add(&H5B, "")
            _themes.Add(&H5C, "")
            _themes.Add(&H5D, "")
            _themes.Add(&H5E, "")
            _themes.Add(&H5F, "")
            _themes.Add(&H60, "Entertainment")
            _themes.Add(&H61, "Entertainment - Contests")
            _themes.Add(&H62, "Entertainment - Magazine")
            _themes.Add(&H63, "Entertainment - Talk Show")
            _themes.Add(100, "Entertainment - Reality")
            _themes.Add(&H65, "Entertainment - Action")
            _themes.Add(&H66, "Entertainment - Drama")
            _themes.Add(&H67, "Entertainment - Comedy")
            _themes.Add(&H68, "Entertainment - Documentary")
            _themes.Add(&H69, "Entertainment - Soap")
            _themes.Add(&H6A, "Entertainment - Sci-Fi")
            _themes.Add(&H6B, "Entertainment - Crime")
            _themes.Add(&H6C, "Entertainment - Game Show")
            _themes.Add(&H6D, "Entertainment - Reality")
            _themes.Add(110, "Entertainment - Talk Show")
            _themes.Add(&H6F, "Entertainment - Other")
            _themes.Add(&H70, "Entertainment - Arts")
            _themes.Add(&H71, "Entertainment - Lifestyle")
            _themes.Add(&H72, "Entertainment - Home")
            _themes.Add(&H73, "Entertainment - Magazine")
            _themes.Add(&H74, "Entertainment - Medical")
            _themes.Add(&H75, "Entertainment - Review")
            _themes.Add(&H76, "Entertainment - Antiques")
            _themes.Add(&H77, "Entertainment - Motors")
            _themes.Add(120, "Entertainment - Art&Lit")
            _themes.Add(&H79, "Entertainment - Ballet")
            _themes.Add(&H7A, "Entertainment - Opera")
            _themes.Add(&H7B, "")
            _themes.Add(&H7C, "")
            _themes.Add(&H7D, "")
            _themes.Add(&H7E, "")
            _themes.Add(&H7F, "")
            _themes.Add(&H80, "Sports")
            _themes.Add(&H81, "Sports - Special Event")
            _themes.Add(130, "Sports - Magazine")
            _themes.Add(&H83, "Sports - Football")
            _themes.Add(&H84, "Sports - Tennis/Squash")
            _themes.Add(&H85, "Sports - Team Sports")
            _themes.Add(&H86, "Sports - Athletics")
            _themes.Add(&H87, "Sports - MotorSport")
            _themes.Add(&H88, "Sports - Water Sports")
            _themes.Add(&H89, "Sports - Winter Sports")
            _themes.Add(&H8A, "Sports - Equestrian")
            _themes.Add(&H8B, "Sports - Martial Sports")
            _themes.Add(140, "Sports - Rugby")
            _themes.Add(&H8D, "Sports - Cycling")
            _themes.Add(&H8E, "Sports - Other")
            _themes.Add(&H8F, "")
            _themes.Add(&H90, "")
            _themes.Add(&H91, "")
            _themes.Add(&H92, "")
            _themes.Add(&H93, "")
            _themes.Add(&H94, "")
            _themes.Add(&H95, "")
            _themes.Add(150, "")
            _themes.Add(&H97, "")
            _themes.Add(&H98, "")
            _themes.Add(&H99, "")
            _themes.Add(&H9A, "")
            _themes.Add(&H9B, "")
            _themes.Add(&H9C, "")
            _themes.Add(&H9D, "")
            _themes.Add(&H9E, "")
            _themes.Add(&H9F, "")
            _themes.Add(160, "Children")
            _themes.Add(&HA1, "Children - Pre-School Programmes")
            _themes.Add(&HA2, "Children - Programmes for 6-14 years")
            _themes.Add(&HA3, "Children - Programmes for 10-16 years")
            _themes.Add(&HA4, "Children - Educational")
            _themes.Add(&HA5, "Children - Cartoons")
            _themes.Add(&HA6, "")
            _themes.Add(&HA7, "")
            _themes.Add(&HA8, "Children - Factual")
            _themes.Add(&HA9, "Children - Cartoons")
            _themes.Add(170, "")
            _themes.Add(&HAB, "")
            _themes.Add(&HAC, "")
            _themes.Add(&HAD, "")
            _themes.Add(&HAE, "")
            _themes.Add(&HAF, "Children - Other")
            _themes.Add(&HB0, "")
            _themes.Add(&HB1, "")
            _themes.Add(&HB2, "")
            _themes.Add(&HB3, "")
            _themes.Add(180, "")
            _themes.Add(&HB5, "")
            _themes.Add(&HB6, "")
            _themes.Add(&HB7, "")
            _themes.Add(&HB8, "")
            _themes.Add(&HB9, "")
            _themes.Add(&HBA, "")
            _themes.Add(&HBB, "")
            _themes.Add(&HBC, "")
            _themes.Add(&HBD, "")
            _themes.Add(190, "")
            _themes.Add(&HBF, "")
            _themes.Add(&HC0, "Music")
            _themes.Add(&HC1, "Music - Rock")
            _themes.Add(&HC2, "Music - Live")
            _themes.Add(&HC3, "")
            _themes.Add(&HC4, "")
            _themes.Add(&HC5, "")
            _themes.Add(&HC6, "")
            _themes.Add(&HC7, "")
            _themes.Add(200, "")
            _themes.Add(&HC9, "")
            _themes.Add(&HCA, "")
            _themes.Add(&HCB, "")
            _themes.Add(&HCC, "")
            _themes.Add(&HCD, "")
            _themes.Add(&HCE, "")
            _themes.Add(&HCF, "Music - Other")
            _themes.Add(&HD0, "")
            _themes.Add(&HD1, "")
            _themes.Add(210, "")
            _themes.Add(&HD3, "")
            _themes.Add(&HD4, "")
            _themes.Add(&HD5, "")
            _themes.Add(&HD6, "")
            _themes.Add(&HD7, "")
            _themes.Add(&HD8, "")
            _themes.Add(&HD9, "")
            _themes.Add(&HDA, "")
            _themes.Add(&HDB, "")
            _themes.Add(220, "")
            _themes.Add(&HDD, "")
            _themes.Add(&HDE, "")
            _themes.Add(&HDF, "")
            _themes.Add(&HE0, "Arts & Culture")
            _themes.Add(&HE1, "Arts & Culture - Performing Arts")
            _themes.Add(&HE2, "Arts & Culture - Fine Arts")
            _themes.Add(&HE3, "Arts & Culture - Religion")
            _themes.Add(&HE4, "Arts & Culture - Traditional Arts")
            _themes.Add(&HE5, "")
            _themes.Add(230, "")
            _themes.Add(&HE7, "")
            _themes.Add(&HE8, "")
            _themes.Add(&HE9, "")
            _themes.Add(&HEA, "Arts & Culture - Magazine")
            _themes.Add(&HEB, "Arts & Culture - Fashions")
            _themes.Add(&HEC, "Arts & Culture - Other")
            _themes.Add(&HED, "")
            _themes.Add(&HEE, "")
            _themes.Add(&HEF, "")
            _themes.Add(240, "")
            _themes.Add(&HF1, "")
            _themes.Add(&HF2, "")
            _themes.Add(&HF3, "")
            _themes.Add(&HF4, "")
            _themes.Add(&HF5, "")
            _themes.Add(&HF6, "")
            _themes.Add(&HF7, "")
            _themes.Add(&HF8, "")
            _themes.Add(&HF9, "")
            _themes.Add(250, "")
            _themes.Add(&HFB, "")
            _themes.Add(&HFC, "")
            _themes.Add(&HFD, "")
            _themes.Add(&HFE, "")
            _themes.Add(&HFF, "")
        End If
    End Sub

    Public Sub LoadSettings()
        Dim expression As Object = _syncObj
        ObjectFlowControl.CheckForSyncLockOnValueType(expression)
        SyncLock expression
            Dim str As String
            Dim time As DateTime
            Dim num2 As Double
            _isloading = True
            Dim strArray As String() = New String(&H15 - 1) {}
            strArray(0) = "63,.,Entertainment"
            strArray(1) = "127,.,Entertainment"
            strArray(2) = "175,.,Entertainment"
            strArray(3) = "31,.,Sky Movies"
            strArray(4) = "79,.,Sky Sports"
            strArray(5) = "111,.,Music Videos"
            strArray(6) = "47,.,News & Documentaries"
            strArray(7) = "159,.,News & Documentaries"
            strArray(8) = "191,.,News & Documentaries"
            strArray(9) = "255,.,Adult"
            strArray(10) = "15,.,Interactive"
            strArray(11) = "-1,., "
            strArray(12) = "-1,., "
            strArray(13) = "-1,., "
            strArray(14) = "-1,., "
            strArray(15) = "-1,., "
            strArray(&H10) = "-1,., "
            strArray(&H11) = "-1,., "
            strArray(&H12) = "-1,., "
            strArray(&H13) = "-1,., "
            Dim separator As String() = New String() {",.,"}
            Dim num As Integer = 1

Label_00E5:
            str = GetSkySetting(("Cat" & Conversions.ToString(num)), strArray((num - 1)))
            Dim strArray3 As String() = New String(3 - 1) {}
            strArray3 = str.Split(separator, StringSplitOptions.None)
            Try
                If Not Double.TryParse(strArray3(0), num2) Then
                    GoTo Label_0185
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                ProjectData.ClearProjectError()
                GoTo Label_0185
            End Try
            Dim str3 As String = strArray3(1)
            If (((num2 > -1) And (str3 <> "")) AndAlso Not _cats.ContainsKey(CByte(Math.Round(num2)))) Then
                _cats.Add(CByte(Math.Round(num2)), str3)
            End If
Label_0185:
            num += 1
            If (num <= 20) Then
                GoTo Label_00E5
            End If
            _modulation = Convert.ToInt32(GetSkySetting("modulation", -1))
            _grabtime = Convert.ToInt32(GetSkySetting("GrabTime", 60))
            _frequency = Convert.ToInt32(GetSkySetting("frequency", 12519000))
            _symbolrate = Convert.ToInt32(GetSkySetting("SymbolRate", 22500))
            _nid = Convert.ToInt32(GetSkySetting("NID", 2))
            _polarisation = Convert.ToInt32(GetSkySetting("polarisation", 2))
            _serviceid = Convert.ToInt32(GetSkySetting("ServiceID", 3))
            _transportid = Convert.ToInt32(GetSkySetting("TransportID", 9003))
            _autoupdate = Convert.ToBoolean(GetSkySetting("AutoUpdate", True))
            _useextrainfo = Convert.ToBoolean(GetSkySetting("useExtraInfo", True))
            _updatechannels = Convert.ToBoolean(GetSkySetting("UpdateChannels", True))
            _everyhour = Convert.ToBoolean(GetSkySetting("EveryHour", True))
            _ondaysat = Convert.ToBoolean(GetSkySetting("OnDaysAt", False))
            _updatelogos = Convert.ToBoolean(GetSkySetting("UpdateLogos", False))
            _mon = Convert.ToBoolean(GetSkySetting("Mon", True))
            _tue = Convert.ToBoolean(GetSkySetting("Tue", True))
            _wed = Convert.ToBoolean(GetSkySetting("Wed", True))
            _thu = Convert.ToBoolean(GetSkySetting("Thu", True))
            _fri = Convert.ToBoolean(GetSkySetting("Fri", True))
            _sat = Convert.ToBoolean(GetSkySetting("Sat", True))
            _sun = Convert.ToBoolean(GetSkySetting("Sun", True))
            If DateTime.TryParse(GetSkySetting("UpdateTime", Now.ToString), time) Then
                _updatetime = time
            End If
            _replacesdwithhd = Convert.ToBoolean(GetSkySetting("ReplaceSDwithHD", True))
            _ignorescrambled = Convert.ToBoolean(GetSkySetting("IgnoreScrambled", False))
            _usenotsetmodsd = Convert.ToBoolean(GetSkySetting("UseNotSetModSD", False))
            _usenotsetmodhd = Convert.ToBoolean(GetSkySetting("UseNotSetModHD", False))
            _updateepg = Convert.ToBoolean(GetSkySetting("UpdateEPG", True))
            _updateinterval = Convert.ToInt32(GetSkySetting("UpdateInterval", 3))
            _regionid = Convert.ToInt32(GetSkySetting("RegionID", 0))
            _bouquetid = Convert.ToInt32(GetSkySetting("BouquetID", 0))
            _isloading = Convert.ToBoolean(GetSkySetting("IsLoading", True))
            Dim skySetting As String = GetSkySetting("CardMap", "")
            _returnlist.Clear()
            If (skySetting.Length > 0) Then
                If (skySetting.Length = 1) Then
                    If Not _returnlist.Contains(Convert.ToInt32(skySetting)) Then
                        _returnlist.Add(Convert.ToInt32(skySetting))
                    End If
                Else
                    Dim source As String() = skySetting.Split(New Char() {","c})
                    If (Enumerable.Count(Of String)(source) > 0) Then
                        'Dim str4 As String
                        For Each str4 As String In From str41 In source Where Not _returnlist.Contains(Convert.ToInt32(str41))
                            _returnlist.Add(Convert.ToInt32(str4))
                        Next
                    End If
                End If
            End If
            If DateTime.TryParse(GetSkySetting("LastUpdate", DateTime.Now.ToString), time) Then
                _lastupdate = time
            End If
            If DateTime.TryParse(GetSkySetting("UpdateTime", DateTime.Now.ToString), time) Then
                _updatetime = time
            End If
            _usethrottle = Convert.ToBoolean(GetSkySetting("UseThrottle", True))
            _logodirectory = GetSkySetting("LogoDirectory", (GetFolderPath(SpecialFolder.CommonApplicationData) & "\Team MediaPortal\MediaPortal\Thumbs\tv\logos"))
            _useskynumbers = Convert.ToBoolean(GetSkySetting("UseSkyNumbers", True))
            _useskycategories = Convert.ToBoolean(GetSkySetting("UseSkyCategories", True))
            _useskyregions = Convert.ToBoolean(GetSkySetting("UseSkyRegions", True))
            _deleteoldchannels = Convert.ToBoolean(GetSkySetting("DeleteOldChannels", True))
            _oldchannelfolder = GetSkySetting("OldChannelFolder", "Old Sky Channels")
            _regionindex = Convert.ToInt32(GetSkySetting("RegionIndex", 0))
            _cardtouseindex = Convert.ToInt32(GetSkySetting("CardToUseIndex", 0))
            _diseqc = Convert.ToInt32(GetSkySetting("DiseqC", -1))
            _switchingfrequency = Convert.ToInt32(GetSkySetting("SwitchingFrequency", 0))
            _isgrabbing = Convert.ToBoolean(GetSkySetting("IsGrabbing", False))
            _isloading = False
            _settingsloaded = True
        End SyncLock
    End Sub

    Public ReadOnly Property SettingsLoaded As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _settingsloaded
            End SyncLock
        End Get
    End Property

    Public Property UpdateLogos As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _updatelogos
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _updatelogos) Then
                    _updatelogos = value
                    UpdateSetting("UpdateLogos", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

    Public Property UseThrottle As Boolean
        Get
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                Return _usethrottle
            End SyncLock
        End Get
        Set(ByVal value As Boolean)
            Dim expression As Object = _syncObj
            ObjectFlowControl.CheckForSyncLockOnValueType(expression)
            SyncLock expression
                If (value <> _usethrottle) Then
                    _usethrottle = value
                    UpdateSetting("UseThrottle", value.ToString)
                End If
            End SyncLock
        End Set
    End Property

End Class

Public Class SkyGrabber
    'Events
    Public Event OnActivateControls As OnActivateControlsEventHandler
    Public Event OnMessage As OnMessageEventHandler

    'Fields
    Private ReadOnly _layer As TvBusinessLayer = New TvBusinessLayer
    Private _sky As CustomDataGRabber
    Public BouquetIDtoUse As Integer
    Public Bouquets As Dictionary(Of Integer, SkyBouquet) = New Dictionary(Of Integer, SkyBouquet)
    Private ReadOnly _cardstoMap As List(Of Card) = New List(Of Card)
    Public CatsDesc As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Public Channels As Dictionary(Of Integer, Sky_Channel) = New Dictionary(Of Integer, Sky_Channel)
    Public completedSummaryDataCarousels As List(Of Integer) = New List(Of Integer)
    Public completedTitleDataCarousels As List(Of Integer) = New List(Of Integer)
    Private CPUHog As Integer = 0
    Private DVBSChannel As DVBSChannel
    Public firstask As Boolean = True
    Private FirstLCN As LCNHolder
    Private GotAllSDT As Boolean = False
    Private GotAllTID As Boolean = False
    Public GrabEPG As Boolean
    Public lasttime As DateTime
    Public LogosToGet As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
    Private MapCards As List(Of Integer)
    Private MaxThisCanDo As Integer = 0
    Private nH As HuffmanTreeNode
    Public NITGot As Boolean = False
    Public NITInfo As Dictionary(Of Integer, NITSatDescriptor) = New Dictionary(Of Integer, NITSatDescriptor)
    Private numberBouquetsPopulated As Integer = 0
    Private numberSDTPopulated As String = ""
    Private numberTIDPopulated As Integer = 0
    Private ReadOnly orignH As HuffmanTreeNode = New HuffmanTreeNode
    Public PacketCount As Integer = 0
    Public RegionIDtoUse As Integer
    Public Regions As List(Of String) = New List(Of String)
    Private SDTCount As Integer = 0
    Public SDTInfo As Dictionary(Of String, SDTInfo) = New Dictionary(Of String, SDTInfo)
    Private ReadOnly Settings As Settings = New Settings
    Private start As DateTime
    Private summariesDecoded As Integer = 0
    Public summaryDataCarouselStartLookup As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
    Public titleDataCarouselStartLookup As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
    Public titlesDecoded As Integer = 0
    Public UseThrottle As Boolean = Settings.UseThrottle

    Public Sub Reset()
        Channels.Clear()
        Bouquets.Clear()
        SDTInfo.Clear()
        NITInfo.Clear()
        numberBouquetsPopulated = 0
        titlesDecoded = 0
        summariesDecoded = 0
        titleDataCarouselStartLookup.Clear()
        completedTitleDataCarousels.Clear()
        summaryDataCarouselStartLookup.Clear()
        completedSummaryDataCarousels.Clear()
        CatsDesc.Clear()
        orignH.Clear()
        CPUHog = 0
        MaxThisCanDo = 0
        start = Now
        NITGot = False
        BouquetIDtoUse = Settings.BouquetId
        RegionIDtoUse = Settings.RegionId
        numberSDTPopulated = ""
        GotAllSDT = False
        numberTIDPopulated = 0
        GotAllTID = False
    End Sub

    'Nested Types
    Public Delegate Sub OnActivateControlsEventHandler()
    Public Delegate Sub OnMessageEventHandler(ByVal text As String, ByVal updateLast As Boolean)

    'Methods
    Public Function AreAllTitlesPopulated() As Object
        Return (completedTitleDataCarousels.Count = 8)
    End Function

    Public Function DoesTidCarryEpgTitleData(ByVal tableId As Integer) As Boolean
        Return ((((TableID = &HA0) Or (TableID = &HA1)) Or (TableID = &HA2)) Or (TableID = &HA3))
    End Function

    Public Function IsTitleDataCarouselOnPidComplete(ByVal pid As Integer) As Boolean
        Return completedTitleDataCarousels.Any(Function(pid1) (pid1 = pid))
    End Function

    Private Sub OnTitleReceived(ByVal pid As Integer, ByVal titleChannelEventUnionId As String)
        If titleDataCarouselStartLookup.ContainsKey(pid) Then
            If (titleDataCarouselStartLookup.Item(pid) = titleChannelEventUnionId) Then
                completedTitleDataCarousels.Add(pid)
            End If
        Else
            titleDataCarouselStartLookup.Add(pid, titleChannelEventUnionId)
        End If
    End Sub

    Public Function AreAllSummariesPopulated() As Object
        Return (completedSummaryDataCarousels.Count = 8)
    End Function

    Private Sub OnSummaryReceived(ByVal pid As Integer, ByVal summaryChannelEventUnionId As String)
        If summaryDataCarouselStartLookup.ContainsKey(pid) Then
            If (summaryDataCarouselStartLookup.Item(pid) = summaryChannelEventUnionId) Then
                completedSummaryDataCarousels.Add(pid)
                If Not Conversions.ToBoolean(AreAllSummariesPopulated) Then
                End If
            End If
        Else
            summaryDataCarouselStartLookup.Add(pid, summaryChannelEventUnionId)
        End If
    End Sub

    Public Function IsSummaryDataCarouselOnPidComplete(ByVal pid As Integer) As Boolean
        Return completedSummaryDataCarousels.Any(Function(pid1) (pid1 = pid))
    End Function

    Public Sub UpdateEPGEvent(ByRef channelId As Integer, ByVal eventId As Integer, ByVal SkyEvent As SkyEvent)
        If (Channels.ContainsKey(channelId) AndAlso Channels.Item(channelId).Events.ContainsKey(eventId)) Then
            Channels.Item(channelId).Events.Item(eventId) = SkyEvent
        End If
    End Sub

    Public Sub UpdateChannel(ByVal ChannelId As Integer, ByVal Channel As Sky_Channel)
        If Channels.ContainsKey(ChannelId) Then
            Channels.Item(ChannelId) = Channel
        End If
    End Sub

    Public Function GetEpgEvent(ByVal channelId As Long, ByVal eventId As Integer) As SkyEvent
        Dim channel As Sky_Channel = GetChannel(CInt(channelId))
        If Not channel.Events.ContainsKey(eventId) Then
            channel.Events.Add(eventId, New SkyEvent)
        End If
        Return channel.Events.Item(eventId)
    End Function

    Private Sub OnTitleSectionReceived(ByVal pid As Integer, ByVal section As Custom_Data_Grabber.Section)
        Try
            If (Not IsTitleDataCarouselOnPidComplete(pid) AndAlso DoesTidCarryEpgTitleData(section.table_id)) Then
                Dim data As Byte() = section.Data
                Dim num5 As Integer = ((((data(1) And 15) * &H100) + data(2)) - 2)
                If (section.section_length >= 20) Then
                    Dim channelId As Long = CLng(Math.Round(CDbl(((data(3) * 256) + data(4)))))
                    Dim num4 As Long = CLng(Math.Round(CDbl(((data(8) * 256) + data(9)))))
                    If Not ((channelId = 0) Or (num4 = 0)) Then
                        Dim num2 As Integer = 10
                        Dim num3 As Integer = 0
                        Do While (num2 < num5)
                            If (num3 > &H200) Then
                                Return
                            End If
                            num3 += 1
                            Dim eventId As Integer = CInt(Math.Round(CDbl(((data((num2 + 0)) * 256) + data((num2 + 1))))))
                            Dim num11 As Double = ((data((num2 + 2)) And 240) >> 4)
                            Dim num6 As Integer = CInt(Math.Round(CDbl((((data((num2 + 2)) And 15) * 256) + data((num2 + 3))))))
                            Dim titleChannelEventUnionId As String = (channelId.ToString & ":" & eventId.ToString)
                            OnTitleReceived(pid, titleChannelEventUnionId)
                            If IsTitleDataCarouselOnPidComplete(pid) Then
                                Return
                            End If
                            Dim epgEvent As SkyEvent = GetEpgEvent(channelId, eventId)
                            If (epgEvent Is Nothing) Then
                                Return
                            End If
                            epgEvent.mjdStart = num4
                            epgEvent.EventID = eventId
                            Const num10 As Integer = 4
                            Dim num7 As Integer = (num2 + num10)
                            Dim num12 As Integer = data((num7 + 0))
                            Dim length As Integer = (data((num7 + 1)) - 7)
                            If (num12 = &HB5) Then
                                epgEvent.StartTime = CInt((CLng(Math.Round(CDbl((data((num7 + 2)) * 512)))) Or CLng(Math.Round(CDbl((data((num7 + 3)) * 2))))))
                                epgEvent.Duration = CInt((CLng(Math.Round(CDbl((data((num7 + 4)) * 512)))) Or CLng(Math.Round(CDbl((data((num7 + 5)) * 2))))))
                                Dim num13 As Byte = data((num7 + 6))
                                epgEvent.Category = Conversions.ToString(num13)
                                epgEvent.SetFlags(data((num7 + 7)))
                                epgEvent.SetCategory(data((num7 + 8)))
                                epgEvent.seriesTermination = (((data((num7 + 8)) And &H40) >> 6) Xor 1)
                                If (length <= 0) Then
                                    num2 = (num2 + (num10 + num6))
                                End If
                                If (epgEvent.Title <> "") Then
                                    Return
                                End If
                                Dim destinationArray As Byte() = New Byte(&H1001 - 1) {}
                                If (((num7 + 9) + length) > data.Length) Then
                                    Return
                                End If
                                Array.Copy(data, (num7 + 9), destinationArray, 0, length)
                                epgEvent.Title = NewHuffman(destinationArray, length)
                                If (epgEvent.Title <> "") Then
                                    OnTitleDecoded()
                                End If
                                Dim num14 As Integer = CInt(channelId)
                                UpdateEPGEvent(num14, epgEvent.EventID, epgEvent)
                                channelId = num14
                            End If
                            num2 = (num2 + (num6 + num10))
                        Loop
                        If (num2 <> (num5 + 1)) Then
                        End If
                    End If
                End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            If (Not OnMessageEvent Is Nothing) Then
                OnMessageEvent.Invoke(("Error decoding title, " & exception.Message), False)
            End If
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Public Sub ParseNIT(ByVal data As Custom_Data_Grabber.Section, ByVal length As Integer)
        Try
            If Not NITGot Then
                Dim buf As Byte() = data.Data
                Dim num9 As Integer = (buf(1) And &H80)
                Dim num7 As Integer = (((buf(1) And 15) * &H100) Or buf(2))
                Dim num5 As Integer = ((buf(3) * &H100) Or buf(4))
                Dim num11 As Integer = (CByte((buf(5) >> 1)) And &H1F)
                Dim num As Integer = (buf(5) And 1)
                Dim num8 As Integer = buf(6)
                Dim num3 As Integer = buf(7)
                Dim num4 As Integer = (((buf(8) And 15) * &H100) Or buf(9))
                Dim num2 As Integer = num4
                Dim index As Integer = 10
                Dim maxLen As Integer = 0
                Do While (num2 > 0)
                    Dim num13 As Integer = buf(index)
                    maxLen = (buf((index + 1)) + 2)
                    If (num13 = &H40) Then
                        Dim str As String = Encoding.GetEncoding("iso-8859-1").GetString(buf, (index + 2), (maxLen - 2))
                    End If
                    num2 = (num2 - maxLen)
                    index = (index + maxLen)
                Loop
                index = (10 + num4)
                If (index <= num7) Then
                    num2 = (((buf(index) And 15) * &H100) + buf((index + 1)))
                    index = (index + 2)
                    Do While (num2 > 0)
                        If ((index + 2) > num7) Then
                            Return
                        End If
                        Dim transportID As Integer = ((buf(index) * &H100) + buf((index + 1)))
                        Dim networkID As Integer = ((buf((index + 2)) * &H100) + buf((index + 3)))
                        Dim num16 As Integer = (((buf((index + 4)) And 15) * &H100) + buf((index + 5)))
                        index = (index + 6)
                        num2 = (num2 - 6)
                        Dim num14 As Integer = num16
                        Do While (num14 > 0)
                            If ((index + 2) > num7) Then
                                Return
                            End If
                            Dim num18 As Integer = buf(index)
                            maxLen = (buf((index + 1)) + 2)
                            If (num18 = &H43) Then
                                DVB_GetSatDelivSys(buf, index, maxLen, networkID, transportID)
                            End If
                            index = (index + maxLen)
                            num14 = (num14 - maxLen)
                            num2 = (num2 - maxLen)
                        Loop
                    Loop
                End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            If (Not OnMessageEvent Is Nothing) Then
                OnMessageEvent.Invoke("Error Parsing NIT" & exception.Message, False)
            End If
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Public Sub DVB_GetSatDelivSys(ByVal b As Byte(), ByVal pointer As Integer, ByVal maxLen As Integer, ByVal NetworkID As Integer, ByVal TransportID As Integer)
        If ((b((pointer + 0)) = &H43) And (maxLen >= 13)) Then
            Dim num2 As Byte = b((pointer + 0))
            Dim num As Byte = b((pointer + 1))
            If (num <= 13) Then
                Dim descriptor As New NITSatDescriptor With { _
                    .TID = TransportID, _
                    .Frequency = (&H5F5E100 * (CByte((b((pointer + 2)) >> 4)) And 15)) _
                }
                Dim descriptor2 As NITSatDescriptor = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (100000000 * (b((pointer + 2)) And 15)))
                descriptor2 = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (1000000 * (CByte((b((pointer + 3)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (100000 * (b((pointer + 3)) And 15)))
                descriptor2 = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (10000 * (CByte((b((pointer + 4)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (1000 * (b((pointer + 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (100 * (CByte((b((pointer + 5)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.Frequency = (descriptor2.Frequency + (10 * (b((pointer + 5)) And 15)))
                descriptor2 = descriptor
                descriptor2.OrbitalPosition = (descriptor2.OrbitalPosition + (1000 * (CByte((b((pointer + 6)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.OrbitalPosition = (descriptor2.OrbitalPosition + (100 * (b((pointer + 6)) And 15)))
                descriptor2 = descriptor
                descriptor2.OrbitalPosition = (descriptor2.OrbitalPosition + (10 * (CByte((b((pointer + 7)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.OrbitalPosition = (descriptor2.OrbitalPosition + (b((pointer + 7)) And 15))

                descriptor.WestEastFlag = ((b((pointer + 8)) And &H80) >> 7)

                Dim num4 As Integer = ((b((pointer + 8)) And &H60) >> 5)
                descriptor.Polarisation = (num4 + 1)
                descriptor.isS2 = ((b((pointer + 8)) And 4) >> 2)
                If (descriptor.isS2 > 0) Then
                    Select Case ((b((pointer + 8)) And &H18) >> 3)
                        Case 0
                            descriptor.RollOff = 3
                            Exit Select
                        Case 1
                            descriptor.RollOff = 2
                            Exit Select
                        Case 2
                            descriptor.RollOff = 1
                            Exit Select
                    End Select
                Else
                    descriptor.RollOff = -1
                End If

                descriptor.Modulation = (b((pointer + 8)) And 3)

                descriptor.Symbolrate = (100000 * (CByte((b((pointer + 9)) >> 4)) And 15))
                descriptor2 = descriptor
                descriptor2.Symbolrate = (descriptor2.Symbolrate + (10000 * (b((pointer + 9)) And 15)))
                descriptor2 = descriptor
                descriptor2.Symbolrate = (descriptor2.Symbolrate + (1000 * (CByte((b((pointer + 10)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.Symbolrate = (descriptor2.Symbolrate + (100 * (b((pointer + 10)) And 15)))
                descriptor2 = descriptor
                descriptor2.Symbolrate = (descriptor2.Symbolrate + (10 * (CByte((b((pointer + 11)) >> 4)) And 15)))
                descriptor2 = descriptor
                descriptor2.Symbolrate = (descriptor2.Symbolrate + (1 * (b((pointer + 11)) And 15)))

                Dim num3 As Integer = (b((pointer + 12)) And 15)

                Select Case num3
                    Case 0
                        num3 = 0
                        Exit Select
                    Case 1
                        num3 = 1
                        Exit Select
                    Case 2
                        num3 = 2
                        Exit Select
                    Case 3
                        num3 = 3
                        Exit Select
                    Case 4
                        num3 = 6
                        Exit Select
                    Case 5
                        num3 = 8
                        Exit Select
                    Case 6
                        num3 = 13
                        Exit Select
                    Case 7
                        num3 = 4
                        Exit Select
                    Case 8
                        num3 = 5
                        Exit Select
                    Case 9
                        num3 = 14
                        Exit Select
                    Case Else
                        num3 = 0
                        Exit Select
                End Select

                descriptor.FECInner = num3
                If Not NITInfo.ContainsKey(TransportID) Then
                    NITInfo.Add(TransportID, descriptor)
                Else
                    If Not GotAllTID Then
                        If (Not OnMessageEvent Is Nothing) Then
                            RaiseEvent OnMessage(("Got Network Information, " & NITInfo.Count & " transponders"), False)
                        End If
                    End If
                    GotAllTID = True
                End If
            End If
        End If
    End Sub

    Public Sub OnTSPacket(ByVal Pid As Integer, ByVal Length As Integer, ByVal Data As Custom_Data_Grabber.Section)
        If (PacketCount > 500) Then
            PacketCount = 0
        End If
        PacketCount += 1
        Select Case Pid
            Case &H10
                If Not GotAllTID Then
                    ParseNIT(Data, Length)
                End If
                Exit Select
            Case &H11
                ParseChannels(Data, Length)
                Exit Select
            Case &H30, &H31, 50, &H33, &H34, &H35, &H36, &H37
                OnTitleSectionReceived(Pid, Data)
                Exit Select
            Case &H40, &H41, &H42, &H43, &H44, &H45, 70, &H47
                OnSummarySectionReceived(Pid, Data)
                Exit Select
        End Select
        If IsEverythingGrabbed() Then
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Everything Grabbed", False)
            End If
            Sky.SendComplete(0)
        End If
    End Sub

    Public Sub CreateGroups()
        If Settings.UseSkyCategories Then
            Dim list As New List(Of String)
            Dim separator As String() = New String() {",.,"}
            Dim textBoxNum As Integer = 1
            Do
                Dim categoryByTextBoxNum As String = Settings.GetCategoryByTextBoxNum(textBoxNum)
                If Not categoryByTextBoxNum.StartsWith("-1") Then
                    Dim strArray2 As String() = New String(3 - 1) {}
                    strArray2 = categoryByTextBoxNum.Split(separator, StringSplitOptions.None)
                    If Not list.Contains(strArray2(1)) Then
                        list.Add(strArray2(1))
                    End If
                End If
                textBoxNum += 1
            Loop While (textBoxNum <= 20)
            Dim num As Integer = 1
            Dim str2 As String
            For Each str2 In list
                _layer.CreateGroup(str2)
                Dim groupByName As ChannelGroup = _layer.GetGroupByName(str2)
                groupByName.SortOrder = num
                groupByName.Persist()
                num += 1
            Next
        End If
        'Original Sky code.
        'If Settings.UseSkyCategories Then
        '    Dim groups As New List(Of String)
        '    If Settings.GetSkySetting("CatByte20", "-1") <> "-1" And Settings.GetSkySetting("CatText20", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText20", ""))
        '    If Settings.GetSkySetting("CatByte19", "-1") <> "-1" And Settings.GetSkySetting("CatText19", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText19", ""))
        '    If Settings.GetSkySetting("CatByte18", "-1") <> "-1" And Settings.GetSkySetting("CatText18", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText18", ""))
        '    If Settings.GetSkySetting("CatByte17", "-1") <> "-1" And Settings.GetSkySetting("CatText17", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText17", ""))
        '    If Settings.GetSkySetting("CatByte16", "-1") <> "-1" And Settings.GetSkySetting("CatText16", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText16", ""))
        '    If Settings.GetSkySetting("CatByte15", "-1") <> "-1" And Settings.GetSkySetting("CatText15", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText15", ""))
        '    If Settings.GetSkySetting("CatByte14", "-1") <> "-1" And Settings.GetSkySetting("CatText14", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText14", ""))
        '    If Settings.GetSkySetting("CatByte13", "-1") <> "-1" And Settings.GetSkySetting("CatText13", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText13", ""))
        '    If Settings.GetSkySetting("CatByte12", "-1") <> "-1" And Settings.GetSkySetting("CatText12", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText12", ""))
        '    If Settings.GetSkySetting("CatByte11", "-1") <> "-1" And Settings.GetSkySetting("CatText11", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText11", ""))
        '    If Settings.GetSkySetting("CatByte10", "-1") <> "-1" And Settings.GetSkySetting("CatText10", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText10", ""))
        '    If Settings.GetSkySetting("CatByte9", "-1") <> "-1" And Settings.GetSkySetting("CatText9", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText9", ""))
        '    If Settings.GetSkySetting("CatByte8", "-1") <> "-1" And Settings.GetSkySetting("CatText8", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText8", ""))
        '    If Settings.GetSkySetting("CatByte7", "-1") <> "-1" And Settings.GetSkySetting("CatText7", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText7", ""))
        '    If Settings.GetSkySetting("CatByte6", "-1") <> "-1" And Settings.GetSkySetting("CatText6", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText6", ""))
        '    If Settings.GetSkySetting("CatByte5", "-1") <> "-1" And Settings.GetSkySetting("CatText5", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText5", ""))
        '    If Settings.GetSkySetting("CatByte4", "-1") <> "-1" And Settings.GetSkySetting("CatText4", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText4", ""))
        '    If Settings.GetSkySetting("CatByte3", "-1") <> "-1" And Settings.GetSkySetting("CatText3", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText3", ""))
        '    If Settings.GetSkySetting("CatByte2", "-1") <> "-1" And Settings.GetSkySetting("CatText2", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText2", ""))
        '    If Settings.GetSkySetting("CatByte1", "-1") <> "-1" And Settings.GetSkySetting("CatText1", "") <> "" Then groups.Add(Settings.GetSkySetting("CatText1", ""))
        '    Dim a As Integer = groups.Count
        '    For Each Name As String In groups
        '        _layer.CreateGroup(Name)
        '        Dim group1 As ChannelGroup
        '        group1 = _layer.GetGroupByName(Name)
        '        group1.SortOrder = a
        '        group1.Persist()
        '        a -= 1
        '    Next
        'End If

    End Sub

    Public Sub UpdateAddChannels()
        Try
            Dim diseqC As Integer = Settings.DiseqC
            Dim useSkyNumbers As Boolean = Settings.UseSkyNumbers
            Dim switchingFrequency As Integer = Settings.SwitchingFrequency
            Dim useSkyRegions As Boolean = Settings.UseSkyRegions
            Dim useSkyCategories As Boolean = Settings.UseSkyCategories
            Dim ChannelsAdded As Integer = 0
            Dim useNotSetModSD As Boolean = Settings.UseNotSetModSd
            Dim useNotSetModHD As Boolean = Settings.UseNotSetModHD
            Dim ignoreScrambled As Boolean = Settings.IgnoreScrambled
            Dim str As String = (Settings.LogoDirectory & "\")
            Dim updateLogos As Boolean = Settings.UpdateLogos
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("", False)
            End If
            Dim pair As KeyValuePair(Of Integer, Sky_Channel)
            For Each pair In Channels
                Dim detail As TuningDetail
                ChannelsAdded += 1
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("(" & ChannelsAdded & "/" & Channels.Count & ") Channels sorted", True)
                End If
                Dim scannedchannel As Sky_Channel = pair.Value
                Dim key As Integer = pair.Key
                If (key < 1) Then
                    Continue For
                End If
                Dim channel As New DVBSChannel
                If (((scannedchannel.NID = 0) Or (scannedchannel.TID = 0)) Or (scannedchannel.SID = 0)) Then
                    Continue For
                End If
                Dim channelbySid As SDTInfo = GetChannelbySID(scannedchannel.NID & "-" & scannedchannel.TID & "-" & scannedchannel.SID)
                If (channelbySid Is Nothing) Then
                    Continue For
                End If
                If (channelbySid.SID < 1) Then
                    channelbySid.SID = scannedchannel.SID
                End If
                If (channelbySid.ChannelName = "") Then
                    channelbySid.ChannelName = channelbySid.SID
                End If
                If channelbySid.Provider = "" Then
                    channelbySid.Provider = "SkyNZ"
                End If
                If (ignoreScrambled And channelbySid.isFTA) Then
                    Continue For
                End If
                Dim channelbyExternalID As Channel = _layer.GetChannelbyExternalID(("SkyNZ:" & scannedchannel.ChannelID.ToString))
                If (Not channelbyExternalID Is Nothing) Then
                    Dim list As List(Of TuningDetail) = DirectCast(channelbyExternalID.ReferringTuningDetail, List(Of TuningDetail))
                    Dim detail2 As TuningDetail
                    For Each detail2 In list
                        If ((detail2.ChannelType = 3) And (detail2.NetworkId = 169 Or detail2.NetworkId = 47)) Then
                            detail = detail2
                            Exit For
                        End If
                    Next
                End If
                If Not detail Is Nothing Then
                    GoTo Label_072B
                End If
Label_0299:
                If Not NITInfo.ContainsKey(scannedchannel.TID) Then
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("No NIT found for : " & scannedchannel.SID, False)
                    End If

                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("", False)
                    End If
                    Continue For
                End If
                Dim channelNumber As Integer = 10000
                Dim visibleinguide As Boolean = True
                If (useSkyNumbers AndAlso Operators.ConditionalCompareObjectGreater(scannedchannel.LCNCount, 0, False)) Then
                    If scannedchannel.ContainsLCN(BouquetIDtoUse, RegionIDtoUse) Then
                        channelNumber = scannedchannel.GetLCN(BouquetIDtoUse, RegionIDtoUse).SkyNum
                    ElseIf scannedchannel.ContainsLCN(BouquetIDtoUse, 255) Then
                        channelNumber = scannedchannel.GetLCN(BouquetIDtoUse, 255).SkyNum
                    End If
                    If channelNumber > 800 Then
                        visibleinguide = False
                    End If
                End If
                Dim DBChannel As Channel = _layer.AddNewChannel(channelbySid.ChannelName, channelNumber)
                Dim descriptor As NITSatDescriptor = NITInfo.Item(scannedchannel.TID)
                DVBSChannel.BandType = BandType.Universal
                DVBSChannel.DisEqc = DirectCast(diseqC, DisEqcType)
                DVBSChannel.FreeToAir = True
                DVBSChannel.Frequency = descriptor.Frequency
                DVBSChannel.SymbolRate = descriptor.Symbolrate
                DVBSChannel.InnerFecRate = DirectCast(descriptor.FECInner, BinaryConvolutionCodeRate)
                DVBSChannel.IsRadio = channelbySid.isRadio
                DVBSChannel.IsTv = channelbySid.isTV
                DVBSChannel.FreeToAir = Not channelbySid.isFTA
                DBChannel.ChannelNumber = channelNumber
                DBChannel.SortOrder = channelNumber
                DVBSChannel.LogicalChannelNumber = channelNumber
                DBChannel.VisibleInGuide = visibleinguide
                If (((descriptor.isS2 And -(useNotSetModHD > False)) Or -(((descriptor.isS2 = 0) And useNotSetModSD) > False)) > 0) Then
                    DVBSChannel.ModulationType = ModulationType.ModNotSet
                Else
                    Select Case descriptor.Modulation
                        Case 1
                            If (descriptor.isS2 <= 0) Then
                                Exit Select
                            End If
                            DVBSChannel.ModulationType = ModulationType.ModNbcQpsk
                            GoTo Label_0520
                        Case 2
                            If (descriptor.isS2 <= 0) Then
                                GoTo Label_0506
                            End If
                            DVBSChannel.ModulationType = ModulationType.ModNbc8Psk
                            GoTo Label_0520
                        Case Else
                            DVBSChannel.ModulationType = ModulationType.ModNotDefined
                            GoTo Label_0520
                    End Select
                    DVBSChannel.ModulationType = ModulationType.ModQpsk
                End If
                GoTo Label_0520
Label_0506:
                DVBSChannel.ModulationType = ModulationType.ModNotDefined
Label_0520:
                DVBSChannel.Name = channelbySid.ChannelName
                DVBSChannel.NetworkId = scannedchannel.NID
                DVBSChannel.Pilot = Pilot.NotSet
                DVBSChannel.Rolloff = RollOff.NotSet
                If (descriptor.isS2 = 1) Then
                    DVBSChannel.Rolloff = DirectCast(descriptor.RollOff, RollOff)
                End If
                DVBSChannel.PmtPid = 0
                DVBSChannel.Polarisation = DirectCast(descriptor.Polarisation, Polarisation)
                DVBSChannel.Provider = channelbySid.Provider
                DVBSChannel.ServiceId = scannedchannel.SID
                DVBSChannel.TransportId = scannedchannel.TID
                DVBSChannel.SwitchingFrequency = switchingFrequency
                DBChannel.IsRadio = channelbySid.isRadio
                DBChannel.IsTv = channelbySid.isTV
                DBChannel.ExternalId = ("SkyNZ:" & scannedchannel.ChannelID.ToString)
                DBChannel.Persist()
                _layer.AddTuningDetails(DBChannel, DVBSChannel)
                MapChannelToCards(DBChannel)
                AddChannelToGroups(DBChannel, channelbySid, DVBSChannel, useSkyCategories)
                Continue For
Label_072B:
                DBChannel = detail.ReferencedChannel
                If (DBChannel.ExternalId <> ("SkyNZ:" & key.ToString)) Then
                    GoTo Label_0299
                End If
                Dim tuningChannel As DVBSChannel = DirectCast(_layer.GetTuningChannel(detail), DVBSChannel)
                If tuningChannel Is Nothing OrElse DBChannel Is Nothing OrElse Not SDTInfo.ContainsKey(scannedchannel.NID & "-" & scannedchannel.TID & "-" & scannedchannel.SID) Then
                    Continue For
                End If
                Dim flag10 As Boolean = False
                Dim flag9 As Boolean = False
                Dim sDT As SDTInfo = SDTInfo.Item(scannedchannel.NID & "-" & scannedchannel.TID & "-" & scannedchannel.SID)
                If ((DBChannel.DisplayName <> sDT.ChannelName) Or (detail.Name <> sDT.ChannelName)) Then

                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage(("Channel " & DBChannel.DisplayName & " name changed to " & sDT.ChannelName), False)
                    End If
                    DBChannel.DisplayName = sDT.ChannelName
                    tuningChannel.Name = sDT.ChannelName
                    If Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectGreater(scannedchannel.LCNCount, 0, False), Not DBChannel.VisibleInGuide)) Then
                        DBChannel.VisibleInGuide = True
                        If (Not OnMessageEvent Is Nothing) Then
                            RaiseEvent OnMessage("Channel " & DBChannel.DisplayName & " is now part of the EPG making visible " & sDT.ChannelName & ".", False)
                        End If
                    End If
                    flag10 = True
                End If
                If (tuningChannel.Provider <> sDT.Provider) Then
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("Channel " & DBChannel.DisplayName & " Provider name changed to " & sDT.Provider & ".", False)
                    End If
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("", False)
                    End If
                    tuningChannel.Provider = sDT.Provider
                    flag10 = True
                End If
                If (detail.TransportId = scannedchannel.TID) Then
                    GoTo Label_0BD2
                End If
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage(("Channel : " & DBChannel.DisplayName & " tuning details changed."), False)
                End If
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("", False)
                End If
                If Not NITInfo.ContainsKey(scannedchannel.TID) Then
                    Continue For
                End If
                Dim descriptor2 As NITSatDescriptor = NITInfo.Item(scannedchannel.TID)
                tuningChannel.BandType = BandType.Universal
                tuningChannel.Frequency = descriptor2.Frequency
                tuningChannel.SymbolRate = descriptor2.Symbolrate
                tuningChannel.InnerFecRate = DirectCast(descriptor2.FECInner, BinaryConvolutionCodeRate)
                If (((descriptor2.isS2 And -(useNotSetModHD > False)) Or -(((descriptor2.isS2 = 0) And useNotSetModSD) > False)) > 0) Then
                    tuningChannel.ModulationType = ModulationType.ModNotSet
                Else
                    Select Case descriptor2.Modulation
                        Case 1
                            If (descriptor2.isS2 <= 0) Then
                                Exit Select
                            End If
                            tuningChannel.ModulationType = ModulationType.ModNbcQpsk
                            GoTo Label_0B34
                        Case 2
                            If (descriptor2.isS2 <= 0) Then
                                GoTo Label_0B22
                            End If
                            tuningChannel.ModulationType = ModulationType.ModNbc8Psk
                            GoTo Label_0B34
                        Case Else
                            tuningChannel.ModulationType = ModulationType.ModNotDefined
                            GoTo Label_0B34
                    End Select
                    tuningChannel.ModulationType = ModulationType.ModQpsk
                End If
                GoTo Label_0B34
Label_0B22:
                tuningChannel.ModulationType = ModulationType.ModNotDefined
Label_0B34:
                tuningChannel.Pilot = Pilot.NotSet
                tuningChannel.Rolloff = RollOff.NotSet
                If (descriptor2.isS2 = 1) Then
                    tuningChannel.Rolloff = DirectCast(descriptor2.RollOff, RollOff)
                End If
                tuningChannel.PmtPid = 0
                tuningChannel.Polarisation = DirectCast(descriptor2.Polarisation, Polarisation)
                tuningChannel.TransportId = scannedchannel.TID
                tuningChannel.SwitchingFrequency = switchingFrequency
                flag10 = True
                flag9 = True
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage(("Channel : " & DBChannel.DisplayName & " tuning details changed."), False)
                End If
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("", False)
                End If
Label_0BD2:
                If (detail.ServiceId <> scannedchannel.SID) Then
                    tuningChannel.ServiceId = scannedchannel.SID
                    tuningChannel.PmtPid = 0
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage(("Channel : " & DBChannel.DisplayName & " serviceID changed."), False)
                    End If
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("", False)
                    End If
                    flag10 = True
                    flag9 = True
                End If
                If useSkyRegions Then
                    Dim skyNum As Integer = 10000
                    If (useSkyNumbers AndAlso Operators.ConditionalCompareObjectGreater(scannedchannel.LCNCount, 0, False)) Then
                        If scannedchannel.ContainsLCN(BouquetIDtoUse, RegionIDtoUse) Then
                            skyNum = scannedchannel.GetLCN(BouquetIDtoUse, RegionIDtoUse).SkyNum
                        ElseIf scannedchannel.ContainsLCN(BouquetIDtoUse, 255) Then
                            skyNum = scannedchannel.GetLCN(BouquetIDtoUse, 255).SkyNum
                        End If
                        If (((detail.ChannelNumber <> skyNum) And (skyNum < 800)) Or ((skyNum > 800) And (DBChannel.ChannelNumber <> 10000))) Then
                            If (Not OnMessageEvent Is Nothing) Then
                                RaiseEvent OnMessage("Channel : " & DBChannel.DisplayName & " number has changed from : " & tuningChannel.LogicalChannelNumber & " to : " & skyNum & ".", False)
                            End If
                            If (Not OnMessageEvent Is Nothing) Then
                                RaiseEvent OnMessage("", False)
                            End If
                            DBChannel.RemoveFromAllGroups()
                            detail.ChannelNumber = skyNum
                            DBChannel.ChannelNumber = skyNum
                            tuningChannel.LogicalChannelNumber = skyNum
                            DBChannel.SortOrder = skyNum
                            DBChannel.VisibleInGuide = True
                            flag10 = True
                            AddChannelToGroups(DBChannel, sDT, tuningChannel, useSkyCategories)
                        End If
                    End If
                End If
                If flag10 Then
                    If (updateLogos AndAlso ((DBChannel.DisplayName <> sDT.ChannelName) Or (detail.Name <> sDT.ChannelName))) Then
                        Dim str5 As String = sDT.ChannelName.Replace("\", "_").Replace("/", "_").Replace("|", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("<", "_").Replace(">", "_")
                        Dim str4 As String = (str & str5 & ".png")
                        If Not LogosToGet.ContainsKey(key) Then
                            LogosToGet.Add(key, str4)
                        End If
                    End If
                    DBChannel.Persist()
                    _layer.UpdateTuningDetails(DBChannel, tuningChannel, detail).Persist()
                    MapChannelToCards(DBChannel)
                    If flag9 Then
                        _layer.RemoveAllPrograms(DBChannel.IdChannel)
                    End If
                ElseIf updateLogos Then
                    Dim str7 As String = sDT.ChannelName.Replace("\", "_").Replace("/", "_").Replace("|", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("<", "_").Replace(">", "_")
                    Dim path As String = (str & str7 & ".png")
                    If (Not File.Exists(path) AndAlso Not LogosToGet.ContainsKey(key)) Then
                        LogosToGet.Add(key, path)
                    End If
                End If
            Next
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage((exception.Message & "-" & exception.Source & "-" & exception.StackTrace), False)
            End If
            ProjectData.ClearProjectError()
        End Try
    End Sub

    '    Public Sub UpdateAddChannelsNew()
    '        Dim enumerator As IEnumerator(Of Channel)
    '        Dim enumerator2 As Collections.Generic.Dictionary(Of Integer, Sky_Channel).Enumerator
    '        enumerator = Nothing
    '        enumerator2 = Nothing
    '        Dim diseqC As Integer = Settings.DiseqC
    '        Dim useSkyNumbers As Boolean = Settings.UseSkyNumbers
    '        Dim switchingFrequency As Integer = Settings.SwitchingFrequency
    '        Dim useSkyRegions As Boolean = Settings.UseSkyRegions
    '        Dim useSkyCategories As Boolean = Settings.UseSkyCategories
    '        Dim useNotSetModSD As Boolean = Settings.UseNotSetModSd
    '        Dim useNotSetModHD As Boolean = Settings.UseNotSetModHD
    '        Dim ignoreScrambled As Boolean = Settings.IgnoreScrambled
    '        Dim str As String = (Settings.LogoDirectory & "\")
    '        Dim updateLogos As Boolean = Settings.UpdateLogos
    '        If (Not OnMessageEvent Is Nothing) Then
    '            RaiseEvent OnMessage("", False)
    '        End If
    '        Dim list As IList(Of Channel) = Channel.ListAll
    '        Dim dictionary As New Dictionary(Of Integer, Channel)
    '        Try
    '            enumerator = list.GetEnumerator
    '            Do While enumerator.MoveNext
    '                Dim current As Channel = enumerator.Current
    '                If current.ExternalId.StartsWith("SKYUK:") Then
    '                    Dim key As Integer = Convert.ToInt32(current.ExternalId.Replace("SKYUK:", ""))
    '                    If ((key <> 0) AndAlso Not dictionary.ContainsKey(key)) Then
    '                        If (current.ReferringTuningDetail.Count < 1) Then
    '                            current.Delete()
    '                            current.Persist()
    '                        End If
    '                        dictionary.Add(key, current)
    '                    End If
    '                End If
    '            Loop
    '        Finally
    '            If (Not enumerator Is Nothing) Then
    '                enumerator.Dispose()
    '            End If
    '        End Try
    '        Try
    '            enumerator2 = Channels.GetEnumerator
    '            Do While enumerator2.MoveNext
    '                Dim channel2 As Channel
    '                Dim pair As KeyValuePair(Of Integer, Sky_Channel) = enumerator2.Current
    '                'enumerator2.Current = pair
    '                Dim channel3 As Sky_Channel = pair.Value
    '                Dim num5 As Integer = pair.Key
    '                If (num5 < 1) Then
    '                    Continue Do
    '                End If
    '                If dictionary.ContainsKey(num5) Then
    '                    channel2 = dictionary.Item(num5)
    '                    GoTo Label_059B
    '                End If
    '                If Not NITInfo.ContainsKey(channel3.TID) Then
    '                    If (Not OnMessageEvent Is Nothing) Then
    '                        RaiseEvent OnMessage(("No NIT found for : " & Conversions.ToString(channel3.SID)), False)
    '                    End If
    '                    If (Not OnMessageEvent Is Nothing) Then
    '                        RaiseEvent OnMessage("", False)
    '                    End If
    '                    Continue Do
    '                End If
    '                If (((channel3.NID = 0) Or (channel3.TID = 0)) Or (channel3.SID = 0)) Then
    '                    Continue Do
    '                End If
    '                Dim channelbySID As SDTInfo = GetChannelbySID(String.Concat(New String() {Conversions.ToString(channel3.NID), "-", Conversions.ToString(channel3.TID), "-", Conversions.ToString(channel3.SID)}))
    '                If ((channelbySID Is Nothing) OrElse (ignoreScrambled And channelbySID.isFTA)) Then
    '                    Continue Do
    '                End If
    '                Dim channelNumber As Integer = &H2710
    '                Dim flag8 As Boolean = True
    '                If (useSkyNumbers AndAlso Operators.ConditionalCompareObjectGreater(channel3.LCNCount, 0, False)) Then
    '                    If channel3.ContainsLCN(BouquetIDtoUse, RegionIDtoUse) Then
    '                        channelNumber = channel3.GetLCN(BouquetIDtoUse, RegionIDtoUse).SkyNum
    '                    ElseIf channel3.ContainsLCN(BouquetIDtoUse, &HFF) Then
    '                        channelNumber = channel3.GetLCN(BouquetIDtoUse, &HFF).SkyNum
    '                    End If
    '                    If (channelNumber = &H2710) Then
    '                        flag8 = False
    '                    End If
    '                End If
    '                channel2 = _layer.AddNewChannel(channel3.Channel_Name, channelNumber)
    '                Dim descriptor As NITSatDescriptor = NITInfo.Item(channel3.TID)
    '                DVBSChannel.BandType = BandType.Universal
    '                DVBSChannel.DisEqc = DirectCast(diseqC, DisEqcType)
    '                DVBSChannel.FreeToAir = True
    '                DVBSChannel.Frequency = descriptor.Frequency
    '                DVBSChannel.SymbolRate = descriptor.Symbolrate
    '                DVBSChannel.InnerFecRate = DirectCast(descriptor.FECInner, BinaryConvolutionCodeRate)
    '                DVBSChannel.IsRadio = channelbySID.isRadio
    '                DVBSChannel.IsTv = channelbySID.isTV
    '                DVBSChannel.FreeToAir = Not channelbySID.isFTA
    '                channel2.SortOrder = channelNumber
    '                DVBSChannel.LogicalChannelNumber = channelNumber
    '                channel2.VisibleInGuide = flag8
    '                If (((descriptor.isS2 And -(useNotSetModHD > False)) Or -(((descriptor.isS2 = 0) And useNotSetModSD) > False)) > 0) Then
    '                    DVBSChannel.ModulationType = ModulationType.ModNotSet
    '                Else
    '                    Select Case descriptor.Modulation
    '                        Case 1
    '                            If (descriptor.isS2 <= 0) Then
    '                                Exit Select
    '                            End If
    '                            DVBSChannel.ModulationType = ModulationType.ModNbcQpsk
    '                            GoTo Label_0470
    '                        Case 2
    '                            If (descriptor.isS2 <= 0) Then
    '                                GoTo Label_0456
    '                            End If
    '                            DVBSChannel.ModulationType = ModulationType.ModNbc8Psk
    '                            GoTo Label_0470
    '                        Case Else
    '                            DVBSChannel.ModulationType = ModulationType.ModNotDefined
    '                            GoTo Label_0470
    '                    End Select
    '                    DVBSChannel.ModulationType = ModulationType.ModQpsk
    '                End If
    '                GoTo Label_0470
    'Label_0456:
    '                DVBSChannel.ModulationType = ModulationType.ModNotDefined
    'Label_0470:
    '                DVBSChannel.Name = channelbySID.ChannelName
    '                DVBSChannel.NetworkId = channel3.NID
    '                DVBSChannel.Pilot = Pilot.NotSet
    '                DVBSChannel.Rolloff = RollOff.NotSet
    '                If (descriptor.isS2 = 1) Then
    '                    DVBSChannel.Rolloff = DirectCast(descriptor.RollOff, RollOff)
    '                End If
    '                DVBSChannel.PmtPid = 0
    '                DVBSChannel.Polarisation = DirectCast(descriptor.Polarisation, Polarisation)
    '                DVBSChannel.Provider = channelbySID.Provider
    '                DVBSChannel.ServiceId = channel3.SID
    '                DVBSChannel.TransportId = channel3.TID
    '                DVBSChannel.SwitchingFrequency = switchingFrequency
    '                channel2.IsRadio = channelbySID.isRadio
    '                channel2.IsTv = channelbySID.isTV
    '                channel2.ExternalId = ("SKYUK:" & channel3.ChannelID.ToString)
    '                channel2.Persist()
    '                MapChannelToCards(channel2)
    '                AddChannelToGroups(channel2, channelbySID, DVBSChannel, useSkyCategories)
    '                _layer.AddTuningDetails(channel2, DVBSChannel)
    'Label_059B:
    '                If (channel2 Is Nothing) Then
    '                    If (OnMessageEvent Is Nothing) Then
    '                        Continue Do
    '                    End If
    '                    RaiseEvent OnMessage("Error Adding Channel to database, continuing", False)
    '                End If
    '            Loop
    '        Finally
    '            enumerator2.Dispose()
    '        End Try
    '    End Sub

    Private Sub MapChannelToCards(ByVal DBChannel As Channel)
        Dim enumerator As IEnumerator(Of Card)
        enumerator = Nothing
        Try
            enumerator = _cardstoMap.GetEnumerator
            Do While enumerator.MoveNext
                Dim current As Card = enumerator.Current
                _layer.MapChannelToCard(current, DBChannel, False)
            Loop
        Finally
            enumerator.Dispose()
        End Try
    End Sub

    Private Sub AddChannelToGroups(ByVal DBChannel As Channel, ByVal SDT As SDTInfo, ByVal DVBSChannel As DVBSChannel, ByVal UseSkyCategories As Boolean)
        If DBChannel.IsTv Then
            _layer.AddChannelToGroup(DBChannel, TvGroupNames.AllChannels)
            If ((DVBSChannel.LogicalChannelNumber < 1000) AndAlso UseSkyCategories) Then
                If (Settings.GetCategory(CByte(SDT.Category)) <> SDT.Category.ToString) Then
                    _layer.AddChannelToGroup(DBChannel, Settings.GetCategory(CByte(SDT.Category)))
                End If
                If SDT.isHD Then
                    _layer.AddChannelToGroup(DBChannel, "HD Channels")
                End If
                If SDT.is3D Then
                    _layer.AddChannelToGroup(DBChannel, "3D Channels")
                End If
            End If
        ElseIf DBChannel.IsRadio Then
            _layer.AddChannelToRadioGroup(DBChannel, RadioGroupNames.AllChannels)
        Else
            _layer.AddChannelToGroup(DBChannel, TvGroupNames.AllChannels)
        End If
    End Sub

    Public Sub UpdateEPG()
        Dim time8 As DateTime
        Dim epgEvents As New TVController
        Dim updater As New EpgDBUpdater(epgEvents, "Sky TV EPG Updater", False)
        Dim list As New List(Of EpgChannel)
        Dim useExtraInfo As Boolean = Settings.UseExtraInfo
        Dim now As DateTime = DateAndTime.Now
        If (_layer.GetPrograms(DateAndTime.Now, now.AddDays(1)).Count < 1) Then
            Dim aProgramList As New ProgramList
            Dim pair As KeyValuePair(Of Integer, Sky_Channel)
            For Each pair In Channels
                Dim channel2 As Sky_Channel = pair.Value
                Dim channel As Channel = _layer.GetChannelByTuningDetail(channel2.NID, channel2.TID, channel2.SID)
                If (Not channel Is Nothing) Then
                    Dim enumerator As Collections.Generic.Dictionary(Of Integer, SkyEvent).Enumerator
                    Try
                        enumerator = channel2.Events.GetEnumerator
                        Do While enumerator.MoveNext
                            Dim current As KeyValuePair(Of Integer, SkyEvent) = enumerator.Current
                            Dim event2 As SkyEvent = current.Value
                            If ((current.Value.EventID <> 0) And (current.Value.Title <> "")) Then
                                Dim summary As String
                                now = New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                time8 = now
                                Dim startTime As DateTime = time8.AddSeconds((((event2.mjdStart + 2400000.5) - 2440587.5) * 86400)).AddSeconds(CDbl(event2.StartTime)).ToLocalTime
                                Dim endTime As DateTime = startTime.AddSeconds(CDbl(event2.Duration))
                                If useExtraInfo Then
                                    summary = (event2.Summary & " " & event2.DescriptionFlag)
                                Else
                                    summary = event2.Summary
                                End If
                                time8 = New DateTime(&H76C, 1, 1)
                                Dim item As New Program(channel.IdChannel, startTime, endTime, event2.Title, summary, Settings.GetTheme(Convert.ToInt32(event2.Category)), Program.ProgramState.None, time8, "", "", "", "", 0, event2.ParentalCategory, 0, event2.SeriesID.ToString, event2.seriesTermination)
                                aProgramList.Add(item)
                            End If
                        Loop
                    Finally
                        enumerator.Dispose()
                    End Try
                End If
            Next
            _layer.InsertPrograms(aProgramList, ThreadPriority.Highest)
        Else
            Dim num2 As Integer = 1
            RaiseEvent OnMessage("", False)
            Dim num As Integer = 0
            Dim pair3 As KeyValuePair(Of Integer, Sky_Channel)
            For Each pair3 In Channels
                num += 1
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage(String.Concat(New String() {"(", Conversions.ToString(num), "/", Conversions.ToString(Me.Channels.Count), ") Channels Updated"}), True)
                End If
                Dim channel5 As Sky_Channel = pair3.Value
                Dim epgChannel As New EpgChannel
                Dim channel3 As DVBBaseChannel = New DVBSChannel With { _
                    .NetworkId = channel5.NID, _
                    .TransportId = channel5.TID, _
                    .ServiceId = channel5.SID, _
                    .Name = String.Empty _
                }
                epgChannel.Channel = channel3
                Dim pair4 As KeyValuePair(Of Integer, SkyEvent)
                For Each pair4 In channel5.Events
                    Dim event3 As SkyEvent = pair4.Value
                    If ((pair4.Value.EventID <> 0) And (pair4.Value.Title <> "")) Then
                        Dim text As EpgLanguageText
                        time8 = New DateTime(&H7B2, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                        now = time8
                        Dim time6 As DateTime = now.AddSeconds((((event3.mjdStart + 2400000.5) - 2440587.5) * 86400)).AddSeconds(CDbl(event3.StartTime)).ToLocalTime
                        Dim time4 As DateTime = time6.AddSeconds(CDbl(event3.Duration))
                        If useExtraInfo Then
                            [text] = New EpgLanguageText("ALL", event3.Title, (event3.Summary & " " & event3.DescriptionFlag), Settings.GetTheme(Convert.ToInt32(event3.Category)), 0, event3.ParentalCategory, -1)
                        Else
                            [text] = New EpgLanguageText("ALL", event3.Title, event3.Summary, Settings.GetTheme(Convert.ToInt32(event3.Category)), 0, event3.ParentalCategory, -1)
                        End If
                        Dim program2 As New EpgProgram(time6, time4)
                        program2.Text.Add([text])
                        program2.SeriesId = event3.SeriesID.ToString
                        program2.SeriesTermination = event3.seriesTermination
                        epgChannel.Programs.Add(program2)
                    End If
                Next
                If (epgChannel.Programs.Count > 0) Then
                    updater.UpdateEpgForChannel(epgChannel)
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage(("(" & Conversions.ToString(num2) & " Channels Updated"), True)
                    End If
                    num2 += 1
                End If
            Next
        End If
        epgEvents = Nothing
        updater = Nothing
        If (Not OnMessageEvent Is Nothing) Then
            RaiseEvent OnMessage("EPG Update Complete", False)
        End If
    End Sub

    Public Sub UpdateDataBase(ByVal err As Boolean, ByVal errormessage As String)
        If Not err Then
            If (Channels.Count < 100) Then
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage(("Error : Less than 100 channels found, Grabber found : " & Conversions.ToString(Me.Channels.Count)), False)
                End If
            Else
                CreateGroups()
                If Settings.UpdateChannels Then
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("Moving/Deleting Old Channels", False)
                    End If
                    DeleteOldChannels()
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("Moving/Deleting Old Channels Complete", False)
                    End If
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("Updating/Adding New Channels", False)
                    End If
                    UpdateAddChannels()
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("Updating/Adding New Channels Complete", False)
                    End If
                End If
                If Settings.UpdateEpg Then
                    If (Not OnMessageEvent Is Nothing) Then
                        RaiseEvent OnMessage("Updating EPG, please wait ... This can take upto 15 mins", False)
                    End If
                    UpdateEPG()
                End If
                Settings.LastUpdate = Now
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage(("Database Update Complete, took " & Conversions.ToString(Int(Now.Subtract(start).TotalSeconds)) & " Seconds"), False)
                End If
            End If
        Else
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage(("Error Occured:- " & errormessage), False)
            End If
        End If
        If (Not OnActivateControlsEvent Is Nothing) Then
            RaiseEvent OnActivateControls()
        End If
        Settings.IsGrabbing = False
    End Sub

    Private Sub DeleteOldChannels() 'This is the old code from the original plugin until the new code works.
        Dim UseRegions As Boolean = Settings.UseSkyRegions
        Dim DeleteOld As Boolean = Settings.DeleteOldChannels
        Dim OldFolder As String = Settings.OldChannelFolder
        RegionIDtoUse = Settings.RegionId

        Dim ChannelstoCheck As List(Of Channel) = _layer.Channels

        For Each Channelto As Channel In ChannelstoCheck
            If Channelto.ExternalId.Count > 1 Then
                'Delete channels that are no longer transmitted
                Dim ExternalID() As String = Channelto.ExternalId.Split(":") ' Get NID and ChannelID
                Dim NetworkID As Integer
                Dim ChannelID As Integer
                Try
                    NetworkID = Convert.ToInt32(ExternalID(0))
                    ChannelID = Convert.ToInt32(ExternalID(1))
                Catch
                    Continue For
                End Try
                If (NetworkID <> 169 Or NetworkID <> 47) Then Continue For 'Not a Sky NZ channel, original code uses 2 for NetworkID.
                If Channels.ContainsKey(ChannelID) = False Then
                    removechannel(Channelto, DeleteOld, OldFolder)
                    Continue For
                End If

                If UseRegions Then
                    'Move Channels that are not in this Bouquet
                    Dim ScannedChannel As Sky_Channel = Channels(ChannelID)
                    If ScannedChannel.ContainsLCN(BouquetIDtoUse, RegionIDtoUse) Or ScannedChannel.ContainsLCN(BouquetIDtoUse, 255) Then
                        Continue For
                    End If
                    If (Channelto.IsTv And Channelto.VisibleInGuide = True) Then
                        Channelto.RemoveFromAllGroups()
                        Channelto.VisibleInGuide = False
                        Channelto.Persist()
                        _layer.AddChannelToGroup(Channelto, TvConstants.TvGroupNames.AllChannels)
                        RaiseEvent OnMessage("Channel " & Channelto.DisplayName & " isn't used in this region, moved to all channels.", False)
                    End If
                End If
            End If
        Next
    End Sub

    'Private Sub DeleteOldChannels() - New style code
    '    Dim enumerator As Collections.Generic.Dictionary(Of Integer, Channel).Enumerator
    '    Dim useSkyRegions As Boolean = Settings.UseSkyRegions
    '    Dim deleteOldChannels As Boolean = Settings.DeleteOldChannels
    '    Dim oldChannelFolder As String = Settings.OldChannelFolder
    '    RegionIDtoUse = Settings.RegionID
    '    Try
    '        enumerator = Channels.GetEnumerator
    '        Do While enumerator.MoveNext
    '            Dim current As Channel = enumerator.Current
    '            If (Enumerable.Count(Of Char)(current.ExternalId) > 1) Then
    '                Dim num As Integer
    '                Dim str2 As String
    '                Dim strArray As String() = current.ExternalId.Split(New Char() {":"c})
    '                Try
    '                    str2 = strArray(0)
    '                    num = Convert.ToInt32(strArray(1))
    '                Catch exception1 As Exception
    '                    ProjectData.SetProjectError(exception1)
    '                    ProjectData.ClearProjectError()
    '                    Continue Do
    '                End Try
    '                If (str2 = "SKYUK") Then
    '                    If Not Channels.ContainsKey(num) Then
    '                        removechannel(current, deleteOldChannels, oldChannelFolder)
    '                    ElseIf useSkyRegions Then
    '                        Dim channel2 As Sky_Channel = Channels.Item(num)
    '                        If (Not (channel2.ContainsLCN(BouquetIDtoUse, RegionIDtoUse) Or channel2.ContainsLCN(BouquetIDtoUse, &HFF)) AndAlso (current.IsTv And current.VisibleInGuide)) Then
    '                            current.RemoveFromAllGroups()
    '                            current.VisibleInGuide = False
    '                            current.Persist()
    '                            _layer.AddChannelToGroup(current, TvGroupNames.AllChannels)
    '                            If (Not OnMessageEvent Is Nothing) Then
    '                                RaiseEvent OnMessage(("Channel " & current.DisplayName & " isn't used in this region, moved to all channels."), False)
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Loop
    '    Finally
    '        enumerator.Dispose()
    '    End Try
    'End Sub

    Public Sub removechannel(ByVal DBchannel As Channel, ByVal DeleteOld As Boolean, ByVal OldChannelFolder As String)
        If DeleteOld Then
            DBchannel.Delete()
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage(("Channel " & DBchannel.DisplayName & " no longer exists in the EPG, Deleted."), False)
            End If
        Else
            DBchannel.RemoveFromAllGroups()
            DBchannel.Persist()
            _layer.AddChannelToGroup(DBchannel, OldChannelFolder)
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Channel " & DBchannel.DisplayName & " no longer exists in the EPG, moved to " & OldChannelFolder & ".", False)
            End If
        End If
    End Sub

    Private Sub Grabit()
        Sky = New CustomDataGRabber
        MapCards = Settings.CardMap
        If ((MapCards Is Nothing) Or (MapCards.Count = 0)) Then
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("No cards are selected for use, please correct this before continuing", False)
            End If
            Settings.IsGrabbing = False
            If (Not OnActivateControlsEvent Is Nothing) Then
                RaiseEvent OnActivateControls()
            End If
        Else
            Dim channel As Channel
            Dim str As String
            For Each str In My.Settings.UKCats.Split(New Char() {ChrW(13)})
                Dim strArray2 As String() = str.Split(New Char() {"="c})
                If (Asc(strArray2(0).Substring(0, 1)) = 10) Then
                    strArray2(0) = strArray2(0).Replace(ChrW(10), "")
                End If
                If Not CatsDesc.ContainsKey(strArray2(0)) Then
                    CatsDesc.Add(strArray2(0), strArray2(1))
                End If
            Next
            LoadHuffman(0)
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Huffman Loaded", False)
            End If
            Dim pids As New List(Of Integer) From {&H10, &H11}
            If Settings.UpdateEpg Then
                Dim num As Integer = 0
                Do
                    pids.Add((&H30 + num))
                    pids.Add((&H40 + num))
                    num += 1
                Loop While (num <= 7)
            End If
            GrabEPG = Settings.UpdateEpg
            DVBSChannel = New DVBSChannel
            Dim channelsByName As List(Of Channel) = DirectCast(_layer.GetChannelsByName("Sky NZ Grabber"), List(Of Channel))
            If (channelsByName.Count = 0) Then
                channel = _layer.AddNewChannel("Sky NZ Grabber", 10000)
                channel.VisibleInGuide = False
                channel.IsRadio = True
                channel.IsTv = False
                DVBSChannel.BandType = BandType.Universal
                DVBSChannel.DisEqc = DirectCast(Settings.DiseqC, DisEqcType)
                DVBSChannel.FreeToAir = True
                DVBSChannel.Frequency = Settings.Frequency
                DVBSChannel.SymbolRate = Settings.SymbolRate
                DVBSChannel.InnerFecRate = BinaryConvolutionCodeRate.RateNotSet
                DVBSChannel.IsRadio = True
                DVBSChannel.IsTv = False
                DVBSChannel.ModulationType = DirectCast((Settings.Modulation - 1), ModulationType)
                DVBSChannel.Name = "Sky NZ Grabber"
                DVBSChannel.NetworkId = Settings.Nid
                DVBSChannel.Pilot = Pilot.NotSet
                DVBSChannel.PmtPid = 0
                DVBSChannel.Polarisation = DirectCast((Settings.Polarisation - 1), Polarisation)
                DVBSChannel.Provider = "DJBlu"
                DVBSChannel.Rolloff = RollOff.NotSet
                DVBSChannel.ServiceId = Settings.ServiceId
                DVBSChannel.TransportId = Settings.TransportId
                DVBSChannel.SatelliteIndex = 16
                DVBSChannel.SwitchingFrequency = Settings.SwitchingFrequency
                channel.Persist()
                _layer.AddTuningDetails(channel, DVBSChannel)
                Dim item As Integer = -1
                Dim card As Card
                For Each card In card.ListAll
                    If (RemoteControl.Instance.Type(card.IdCard) = CardType.DvbS) Then
                        item += 1
                        If MapCards.Contains(item) Then
                            _cardstoMap.Add(card)
                            _layer.MapChannelToCard(card, channel, False)
                        End If
                    End If
                Next
                _layer.AddChannelToRadioGroup(channel, RadioGroupNames.AllChannels)
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("Sky NZ Grabber channel added to database", False)
                End If
            Else
                channel = channelsByName.Item(0)
                Dim num3 As Integer = -1
                Dim card2 As Card
                For Each card2 In Card.ListAll
                    If (RemoteControl.Instance.Type(card2.IdCard) = CardType.DvbS) Then
                        num3 += 1
                        If MapCards.Contains(num3) Then
                            _cardstoMap.Add(card2)
                        End If
                    End If
                Next
            End If
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Grabbing Data", False)
            End If
            If (channel Is Nothing) Then
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("Channel was lost somewhere, try clicking on Grab data again", False)
                End If
            Else
                Dim grabTime As Integer = Settings.GrabTime
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage(("Grabber set to grab " & grabTime & " seconds of data"), False)
                End If
                Sky.GrabData(channel.IdChannel, grabTime, pids)
            End If
        End If
    End Sub

    Public Sub Grab()
        If (Not OnMessageEvent Is Nothing) Then
            RaiseEvent OnMessage("Sky NZ Channel and EPG Grabber initialised", False)
        End If
        If Not Settings.IsGrabbing Then
            Settings.IsGrabbing = True
            Reset()
            Dim back As Thread = New Thread(AddressOf Grabit)
            back.Start()
        End If
    End Sub

    Public Shared Sub ManualGrab()
    End Sub

    Public Function IsGrabbing() As Boolean
        Return Settings.IsGrabbing
    End Function

    Private Sub LoadHuffman(ByVal type As Integer)
        Dim str2 As String
        Dim str3 As String
        Dim uKDict As String = My.Resources.UKDict
        If (Not nH Is Nothing) Then
            nH.Clear()
            orignH.Clear()
        End If
        Dim strArray As String() = uKDict.Split(New Char() {ChrW(13)})
        Dim upperBound As Integer = strArray.GetUpperBound(0)
        Dim i As Integer = 0
        Do While (i <= upperBound)
            strArray(i) = strArray(i).Replace(ChrW(10), "")
            str3 = strArray(i).Substring((strArray(i).LastIndexOf("=") + 1), ((strArray(i).Length - strArray(i).LastIndexOf("=")) - 1))
            str2 = strArray(i).Substring(0, strArray(i).LastIndexOf("="))
            nH = orignH
            Dim num4 As Integer = (str3.Length - 1)
            Dim j As Integer = 0
            Do While (j <= num4)
                If (Conversions.ToString(str3.Chars(j)) = "1") Then
                    If (nH.P1 Is Nothing) Then
                        nH.P1 = New HuffmanTreeNode
                        nH = nH.P1
                    Else
                        nH = nH.P1
                    End If
                ElseIf (nH.P0 Is Nothing) Then
                    nH.P0 = New HuffmanTreeNode
                    nH = nH.P0
                Else
                    nH = nH.P0
                End If
                j += 1
            Loop
            nH.Value = str2
            i += 1
        Loop
        nH = orignH
        strArray = Nothing
        str2 = ""
        str3 = ""
    End Sub

    '    Public Function NewHuffman(ByVal Data As Byte(), ByVal Length As Integer) As String 'latest UK 1.4.0.6
    '        Dim num As Byte
    '        Dim obj2 As Object
    '        Dim obj7 As Object
    '        Dim builder2 As New StringBuilder
    '        Dim builder As New StringBuilder
    '        Dim flag3 As Boolean = False
    '        nH = orignH
    '        Dim left As Object = 0
    '        Dim obj4 As Object = 0
    '        builder2.Length = 0
    '        builder.Length = 0
    '        Dim flag As Boolean = False
    '        Dim flag2 As Boolean = False
    '        Dim index As Byte = 0
    '        Dim num3 As Byte = 0
    '        nH = orignH
    '        If Not ObjectFlowControl.ForLoopControl.ForLoopInitObj(obj2, 0, (Length - 1), 1, obj7, obj2) Then
    '            GoTo Label_02BC
    '        End If
    'Label_0074:
    '        num = Data(Conversions.ToInteger(obj2))
    '        Dim num4 As Byte = &H80
    '        If Operators.ConditionalCompareObjectEqual(obj2, 0, False) Then
    '            If ((num And &H20) = 1) Then
    '                flag3 = True
    '            End If
    '            num4 = &H20
    '            index = Conversions.ToByte(obj2)
    '            num3 = num4
    '        End If
    'Label_00B0:
    '        If flag2 Then
    '            index = Conversions.ToByte(obj2)
    '            num3 = num4
    '            flag2 = False
    '        End If
    '        If ((num And num4) = 0) Then
    '            If flag Then
    '                builder.Append("0x30")
    '                obj4 = Operators.AddObject(obj4, 1)
    '                GoTo Label_029D
    '            End If
    '            If (Not nH.P0 Is Nothing) Then
    '                nH = nH.P0
    '                If (nH.Value <> "") Then
    '                    If (nH.Value <> "!!!") Then
    '                        builder2.Append(nH.Value)
    '                    End If
    '                    left = Operators.AddObject(left, Len(nH.Value))
    '                    nH = orignH
    '                    flag2 = True
    '                End If
    '                GoTo Label_029D
    '            End If
    '            left = Operators.AddObject(left, 9)
    '            obj2 = index
    '            num = Data(index)
    '            num4 = num3
    '            flag = True
    '            GoTo Label_00B0
    '        End If
    '        If flag Then
    '            builder.Append("0x31")
    '            obj4 = Operators.AddObject(obj4, 1)
    '        ElseIf (Not nH.P1 Is Nothing) Then
    '            nH = nH.P1
    '            If (nH.Value <> "") Then
    '                If (nH.Value <> "!!!") Then
    '                    builder2.Append(nH.Value)
    '                End If
    '                left = Operators.AddObject(left, Len(nH.Value))
    '                nH = orignH
    '                flag2 = True
    '            End If
    '        Else
    '            left = Operators.AddObject(left, 9)
    '            obj2 = index
    '            num = Data(index)
    '            num4 = num3
    '            flag = True
    '            GoTo Label_00B0
    '        End If
    'Label_029D:
    '        num4 = CByte((num4 >> 1))
    '        If (num4 > 0) Then
    '            GoTo Label_00B0
    '        End If
    '        If ObjectFlowControl.ForLoopControl.ForNextCheckObj(obj2, obj7, obj2) Then
    '            GoTo Label_0074
    '        End If
    'Label_02BC:
    '        Return builder2.ToString
    '    End Function

    Public Function NewHuffman(ByVal Data() As Byte, ByVal Length As Integer) As String 'Original Sky UK 1.2.0.7 source

        Dim DecodeText, DecodeErrorText As New StringBuilder
        Dim i, p, q
        Dim CodeError, IsFound As Boolean
        Dim showatend As Boolean = False
        Dim Byter, lastByte, Mask, lastMask As Byte
        nH = orignH
        p = 0
        q = 0
        DecodeText.Length = 0
        DecodeErrorText.Length = 0
        CodeError = False
        IsFound = False
        lastByte = 0
        lastMask = 0
        nH = orignH

        For i = 0 To Length - 1
            Byter = Data(i)
            Mask = &H80
            If (i = 0) Then
                If (Byter And &H20) = 1 Then
                    showatend = True

                End If

                Mask = &H20
                lastByte = i
                lastMask = Mask
            End If
loop1:
            If (IsFound) Then
                lastByte = i
                lastMask = Mask
                IsFound = False
            End If

            If ((Byter And Mask) = 0) Then

                If (CodeError) Then

                    DecodeErrorText.Append("0x30")
                    q += 1
                    GoTo nextloop1
                End If

                If (nH.P0 Is Nothing) = False Then
                    nH = nH.P0
                    If (nH.Value <> "") Then
                        If nH.Value <> "!!!" Then
                            DecodeText.Append(nH.Value)
                        End If

                        p += Len(nH.Value)
                        nH = orignH
                        IsFound = True
                    End If
                Else
                    p += 9
                    i = lastByte
                    Byter = Data(lastByte)
                    Mask = lastMask
                    CodeError = True
                    GoTo loop1
                End If

            Else

                If (CodeError) Then

                    DecodeErrorText.Append("0x31")
                    q += 1
                    GoTo nextloop1
                End If
                If (nH.P1 Is Nothing) = False Then
                    nH = nH.P1
                    If (nH.Value <> "") Then
                        If nH.Value <> "!!!" Then
                            DecodeText.Append(nH.Value)
                        End If
                        p += Len(nH.Value)
                        nH = orignH
                        IsFound = True
                    End If

                Else

                    p += 9
                    i = lastByte
                    Byter = Data(lastByte)
                    Mask = lastMask
                    CodeError = True
                    GoTo loop1
                End If
            End If
nextloop1:
            Mask = Mask >> 1
            If (Mask > 0) Then
                GoTo loop1
            End If


        Next

        Return DecodeText.ToString

    End Function

    Private Sub OnTitleDecoded()
        titlesDecoded += 1
    End Sub

    Private Sub OnSummaryDecoded()
        summariesDecoded += 1
    End Sub

    Private Sub ParseSDT(ByVal Data As Custom_Data_Grabber.Section, ByVal Length As Integer)
        Try
            If Not GotAllSDT Then
                Dim section As Byte() = Data.Data
                Dim num11 As Integer = ((section(3) * &H100) + section(4))
                Dim num7 As Long = ((section(8) * &H100) + section(9))
                Dim num5 As Integer = ((Length - 11) - 4)
                Dim index As Integer = 11
                Dim num12 As Integer = 0
                Do While (num5 > 0)
                    Dim num10 As Long = ((section(index) * &H100) + section((index + 1)))
                    Dim num3 As Integer = (CByte((section((index + 2)) >> 1)) And 1)
                    Dim num2 As Integer = (section((index + 2)) And 1)
                    Dim num9 As Integer = (CByte((section((index + 3)) >> 5)) And 7)
                    Dim num4 As Integer = (CByte((section((index + 3)) >> 4)) And 1)
                    Dim num As Integer = (((section((index + 3)) And 15) * &H100) + section((index + 4)))
                    index = (index + 5)
                    num5 = (num5 - 5)
                    Dim num6 As Integer = num
                    Dim info As New SDTInfo
                    Do While (num6 > 0)
                        Dim num13 As Integer = section(index)
                        num12 = 0
                        num12 = (section((index + 1)) + 2)
                        Select Case num13
                            Case &H48
                                DVB_GetServiceNew(section, index, info)
                                If (info.ChannelName = "") Then
                                    info.ChannelName = ("SID " & num10.ToString)
                                End If
                                info.SID = CInt(num10)
                                info.isFTA = (num4 > 0)
                                Exit Select
                            Case &HB2
                                If ((section((index + 4)) And 1) = 1) Then
                                    info.Category = section((index + 5))
                                Else
                                    info.Category = section((index + 4))
                                End If
                                Exit Select
                        End Select
                        num6 = (num6 - num12)
                        index = (index + num12)
                        num5 = (num5 - num12)
                        If Not SDTInfo.ContainsKey(String.Concat(New String() {Conversions.ToString(num7), "-", Conversions.ToString(num11), "-", Conversions.ToString(num10)})) Then
                            SDTInfo.Add(String.Concat(New String() {Conversions.ToString(num7), "-", Conversions.ToString(num11), "-", Conversions.ToString(num10)}), info)
                            SDTCount += 1
                        End If
                        If ((AreAllBouquetsPopulated() AndAlso (SDTCount >= Channels.Count)) AndAlso Not GotAllSDT) Then
                            GotAllSDT = True
                            If (Not OnMessageEvent Is Nothing) Then
                                RaiseEvent OnMessage(("Got All SDT Info, " & Conversions.ToString(SDTInfo.Count) & " Channels found"), False)
                            End If
                        End If
                    Loop
                Loop
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Error Parsing SDT" & exception.Message, False)
            End If
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Public Sub DVB_GetServiceNew(ByVal b As Byte(), ByVal x As Integer, ByRef info As SDTInfo)
        Dim num3 As Integer = 0
        Dim num2 As Integer = b((x + 0))
        Dim num As Integer = b((x + 1))
        If (b((x + 2)) = 2) Then
            info.isRadio = True
            info.isTV = False
            info.isHD = False
            info.is3D = False
        Else
            info.isRadio = False
            info.isTV = True
            info.isHD = False
            info.is3D = False
            If ((b((x + 2)) = &H19) Or (b((x + 2)) = &H11)) Then
                info.isHD = True
            End If
            If ((b((x + 2)) >= &H80) And (b((x + 2)) <= &H84)) Then
                info.is3D = True
            End If
        End If
        Dim length As Integer = b((x + 3))
        num3 = 4
        info.Provider = GetString(b, (num3 + x), length, False)
        num3 = (num3 + length)
        Dim num4 As Integer = b((x + num3))
        num3 += 1
        info.ChannelName = GetString(b, (num3 + x), num4, False)
    End Sub

    Public Function GetString(ByVal byteData As Byte(), ByVal offset As Integer, ByVal length As Integer, ByVal replace As Boolean) As String
        Dim buffer As Byte()
        Dim str As String
        If (length = 0) Then
            Return ""
        End If
        Dim name As String = Nothing
        Dim num2 As Integer = 0
        If (byteData(offset) >= &H20) Then
            name = "iso-8859-1"
        Else
            Select Case byteData(offset)
                Case 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
                    Dim num5 As Integer = (byteData(offset) + 4)
                    name = ("iso-8859-" & num5.ToString)
                    num2 = 1
                    GoTo Label_016E
                Case &H10
                    If (byteData((offset + 1)) <> 0) Then
                        OnMessageEvent = Nothing
                        If (Not OnMessageEvent Is Nothing) Then
                            RaiseEvent OnMessage("Invalid DVB text string: byte 2 is not a valid value", replace)
                        End If
                    ElseIf ((byteData((offset + 2)) = 0) OrElse (byteData((offset + 2)) = 12)) Then
                        OnMessageEvent = Nothing
                        If (Not OnMessageEvent Is Nothing) Then
                            RaiseEvent OnMessage("Invalid DVB text string: byte 3 is not a valid value", replace)
                        End If
                    Else
                        name = ("iso-8859-" & CInt(byteData((offset + 2))).ToString)
                        num2 = 3
                    End If
                    GoTo Label_016E
                Case &H1F
                    If ((byteData((offset + 1)) <> 1) AndAlso (byteData((offset + 1)) <> 2)) Then
                        OnMessageEvent = Nothing
                        If (Not OnMessageEvent Is Nothing) Then
                            RaiseEvent OnMessage("Invalid DVB text string: Custom text specifier is not recognized", replace)
                        End If
                    End If
                    GoTo Label_016E
            End Select
            Return "Invalid DVB text string: byte 1 is not a valid value"
        End If
Label_016E:
        buffer = New Byte(((length - 1) + 1) - 1) {}
        Dim index As Integer = 0
        Dim num6 As Integer = (length - 1)
        Dim i As Integer = num2
        Do While (i <= num6)
            If (byteData((offset + i)) > &H1F) Then
                If ((byteData((offset + i)) < &H80) OrElse (byteData((offset + i)) > &H9F)) Then
                    buffer(index) = byteData((offset + i))
                    index += 1
                End If
            ElseIf replace Then
                buffer(index) = &H20
                index += 1
            End If
            i += 1
        Loop
        If (index = 0) Then
            Return String.Empty
        End If
        Try
            Dim encoding As Encoding = encoding.GetEncoding(name)
            If (encoding Is Nothing) Then
                encoding = encoding.GetEncoding("iso-8859-1")
            End If
            str = encoding.GetString(buffer, 0, index)
        Catch exception1 As ArgumentException
            ProjectData.SetProjectError(exception1)
            Dim exception As ArgumentException = exception1
            OnMessageEvent = Nothing
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("** ERROR DECODING STRING - SEE COLLECTION LOG **" & exception.Message, replace)
            End If
            ProjectData.ClearProjectError()
        End Try
        Return str
    End Function

    Private Sub ParseChannels(ByVal Data As Custom_Data_Grabber.Section, ByVal Length As Integer)
        Try
            If (Data.table_id <> &H4A) Then
                If (((Data.table_id = &H42) Or (Data.table_id = 70)) AndAlso Not GotAllSDT) Then
                    ParseSDT(Data, Length)
                End If
            ElseIf Not AreAllBouquetsPopulated() Then
                Dim buffer As Byte() = Data.Data
                Dim bouquetId As Integer = ((buffer(3) * 256) + buffer(4))
                Dim num2 As Integer = (((buffer(8) And 15) * 256) + buffer(9))
                Dim bouquet As SkyBouquet = GetBouquet(bouquetId)
                If Not bouquet.isPopulated Then
                    Dim num10 As Integer
                    If Not bouquet.isInitialized Then
                        bouquet.firstReceivedSectionNumber = CByte(Data.section_number)
                        bouquet.isInitialized = True
                    ElseIf (Data.section_number = bouquet.firstReceivedSectionNumber) Then
                        bouquet.isPopulated = True
                        NotifyBouquetPopulated()
                        Return
                    End If
                    Dim num As Integer = (10 + num2)
                    Dim num4 As Integer = (((buffer((num + 0)) And 15) * &H100) + buffer((num + 1)))
                    Dim num6 As Integer = ((num + num4) + 2)
                    Dim i As Integer = (num + 2)
                    Do While (i < num6)
                        Dim num11 As Integer = ((buffer((i + 0)) * &H100) + buffer((i + 1)))
                        Dim num9 As Integer = ((buffer((i + 2)) * &H100) + buffer((i + 3)))
                        num10 = (((buffer((i + 4)) And 15) * &H100) + buffer((i + 5)))
                        Dim index As Integer = (i + 6)
                        Dim num8 As Integer = (index + num10)
                        Do While (index < num8)
                            Dim num14 As Byte = buffer(index)
                            Dim num13 As Integer = buffer((index + 1))
                            Dim num12 As Integer = (index + 2)
                            Dim num15 As Integer = ((num12 + num13) - 2)
                            If (num14 = &HB1) Then
                                Dim rID As Integer = buffer((index + 3))
                                Do While (num12 < num15)
                                    Dim num18 As Integer = ((buffer((num12 + 2)) * &H100) + buffer((num12 + 3)))
                                    Dim channelID As Integer = ((buffer((num12 + 5)) * &H100) + buffer((num12 + 6)))
                                    Dim skyLCN As Integer = ((buffer((num12 + 7)) * &H100) + buffer((num12 + 8)))
                                    Dim channel As Sky_Channel = GetChannel(channelID)
                                    Dim lCNHold As New LCNHolder(bouquetId, rID, skyLCN)
                                    If Not channel.isPopulated Then
                                        channel.NID = num9
                                        channel.TID = num11
                                        channel.SID = num18
                                        channel.ChannelID = channelID
                                        If channel.AddSkyLCN(lCNHold) Then
                                            channel.isPopulated = True
                                        End If
                                        UpdateChannel(channel.ChannelID, channel)
                                    Else
                                        channel.AddSkyLCN(lCNHold)
                                        UpdateChannel(channel.ChannelID, channel)
                                    End If
                                    num12 = (num12 + 9)
                                Loop
                            End If
                            index = (index + (num13 + 2))
                        Loop
                        i = (i + (num10 + 6))
                    Loop
                End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Error Parsing BAT" & exception.Message, False)
            End If
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Public Function GetBouquet(ByVal bouquetId As Integer) As SkyBouquet
        If Not Bouquets.ContainsKey(bouquetId) Then
            Bouquets.Add(bouquetId, New SkyBouquet)
        End If
        Return Bouquets.Item(bouquetId)
    End Function

    Public Function GetChannel(ByVal ChannelID As Integer) As Sky_Channel
        If Channels.ContainsKey(ChannelID) Then
            Return Channels.Item(ChannelID)
        End If
        Channels.Add(ChannelID, New Sky_Channel)
        Dim channel2 As Sky_Channel = Channels.Item(ChannelID)
        channel2.ChannelID = ChannelID
        Return channel2
    End Function

    Public Function GetChannelbySID(ByVal SID As String) As SDTInfo
        If SDTInfo.ContainsKey(SID) Then
            Return SDTInfo.Item(SID)
        End If
        Return Nothing
    End Function

    Public Function AreAllBouquetsPopulated() As Boolean
        Return ((Bouquets.Count > 0) And (Bouquets.Count = numberBouquetsPopulated))
    End Function

    Public Function IsEverythingGrabbed() As Boolean
        If GrabEPG Then
            If Not (AreAllBouquetsPopulated() And GotAllSDT) Then
                Return False
            End If
            If (Not AreAllSummariesPopulated() And AreAllTitlesPopulated() And GotAllTID) Then
                Return False
            End If
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Everything grabbed:- Titles(" & titlesDecoded & ") : Summaries(" & summariesDecoded & ")", False)
            End If
            Return True
        End If
        If Not ((Bouquets.Count = numberBouquetsPopulated) And GotAllSDT) Then
            Return False
        End If
        Return GotAllTID
    End Function

    Public Sub NotifyBouquetPopulated()
        numberBouquetsPopulated += 1
        If (Bouquets.Count = numberBouquetsPopulated) Then
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Bouquet scan complete.  ", False)
            End If
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage("Found " & Channels.Count & " channels in " & Bouquets.Count & " bouquets, searching SDT Information", False)
            End If
        End If
    End Sub

    Private Sub NotifySkyChannelPopulated(ByVal TID As Integer, ByVal NID As Integer, ByVal SID As Integer)
        If Not GotAllSDT Then
            If (numberSDTPopulated = "") Then
                numberSDTPopulated = String.Concat(New String() {NID.ToString, "-", TID.ToString, "-", SID.ToString})
            ElseIf (String.Concat(New String() {NID.ToString, "-", TID.ToString, "-", SID.ToString}) = numberSDTPopulated) Then
                GotAllSDT = True
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("Got all SDT Info, count: " & SDTInfo.Count, False)
                End If
            End If
        End If
    End Sub

    Private Sub NotifyTIDPopulated(ByVal TID As Integer)
        If Not GotAllTID Then
            If (numberTIDPopulated = 0) Then
                numberTIDPopulated = TID
            ElseIf (TID = numberTIDPopulated) Then
                GotAllTID = True
                If (Not OnMessageEvent Is Nothing) Then
                    RaiseEvent OnMessage("Got all Network Information", False)
                End If
            End If
        End If
    End Sub

    Public Function DoesTidCarryEpgSummaryData(ByVal TableID As Integer) As Boolean
        Return ((((TableID = &HA8) Or (TableID = &HA9)) Or (TableID = 170)) Or (TableID = &HAB))
    End Function

    Public Sub OnSummarySectionReceived(ByVal pid As Integer, ByVal section As Custom_Data_Grabber.Section)
        Try
            If (Not IsSummaryDataCarouselOnPidComplete(pid) AndAlso DoesTidCarryEpgSummaryData(section.table_id)) Then
                Dim buffer As Byte() = section.Data
                Dim num5 As Integer = ((((buffer(1) And 15) * &H100) + buffer(2)) - 2)
                If (section.section_length >= 14) Then
                    Dim channelId As Long = ((buffer(3) * &H100) + buffer(4))
                    Dim num4 As Long = ((buffer(8) * &H100) + buffer(9))
                    If Not ((channelId = 0) Or (num4 = 0)) Then
                        Dim num2 As Integer = 10
                        Dim num3 As Integer = 0
                        Do While (num2 < num5)
                            Dim num6 As Integer
                            Dim epgEvent As SkyEvent
                            Dim num11 As Integer
                            If (num3 <= &H200) Then
                                num3 += 1
                                Dim eventId As Integer = ((buffer((num2 + 0)) * &H100) Or buffer((num2 + 1)))
                                Dim num12 As Byte = CByte(((buffer((num2 + 2)) And 240) >> 4))
                                num6 = (((buffer((num2 + 2)) And 15) * &H100) Or buffer((num2 + 3)))
                                Dim summaryChannelEventUnionId As String = (channelId.ToString & ":" & eventId.ToString)
                                OnSummaryReceived(pid, summaryChannelEventUnionId)
                                If Not IsSummaryDataCarouselOnPidComplete(pid) Then
                                    epgEvent = GetEpgEvent(channelId, eventId)
                                    If (Not epgEvent Is Nothing) Then
                                        Select Case num12
                                            Case 15
                                                num11 = 7
                                                GoTo Label_0132
                                            Case 11
                                                num11 = 4
                                                GoTo Label_0132
                                        End Select
                                    End If
                                End If
                            End If
                            Return
Label_0132:
                            If (num6 < 3) Then
                                num2 = (num2 + (num11 + num6))
                            End If
                            Dim num7 As Integer = (num2 + num11)
                            Dim num13 As Integer = buffer((num7 + 0))
                            Dim length As Integer = buffer((num7 + 1))
                            If (num13 = &HB9) Then
                                If (epgEvent.Summary = "") Then
                                    Dim destinationArray As Byte() = New Byte(&H1001 - 1) {}
                                    If (((num7 + 2) + length) > buffer.Length) Then
                                        Return
                                    End If
                                    Array.Copy(buffer, (num7 + 2), destinationArray, 0, length)
                                    epgEvent.Summary = NewHuffman(destinationArray, length)
                                    OnSummaryDecoded()
                                    Dim num16 As Integer = CInt(channelId)
                                    UpdateEPGEvent(num16, epgEvent.EventID, epgEvent)
                                    channelId = num16
                                End If
                            ElseIf (num13 <> &HBB) Then
                                Return
                            End If
                            Dim num10 As Integer = ((num6 - length) - 2)
                            If (num10 >= 4) Then
                                Dim num15 As Integer = ((num7 + 2) + length)
                                Dim num14 As Integer = buffer((num15 + 0))
                                If (num14 = &HC1) Then
                                    epgEvent.SeriesID = ((buffer((num15 + 2)) * &H100) + buffer((num15 + 3)))
                                End If
                            End If
                            num2 = (num2 + (num6 + num11))
                        Loop
                        If (num2 <> (num5 + 1)) Then
                        End If
                    End If
                End If
            End If
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            Dim exception As Exception = exception1
            If (Not OnMessageEvent Is Nothing) Then
                RaiseEvent OnMessage(("Error decoding Summary, " & exception.Message), False)
            End If
            ProjectData.ClearProjectError()
        End Try
    End Sub

    ' Properties
    Public Overridable Property Sky As CustomDataGRabber
        Get
            Return _Sky
        End Get
        Set(ByVal withEventsValue As CustomDataGRabber)
            Dim handler As CustomDataGRabber.OnCompleteEventHandler = New CustomDataGRabber.OnCompleteEventHandler(AddressOf UpdateDataBase)
            Dim handler2 As CustomDataGRabber.OnPacketEventHandler = New CustomDataGRabber.OnPacketEventHandler(AddressOf OnTSPacket)
            If (Not _sky Is Nothing) Then
                RemoveHandler _sky.OnComplete, handler
                RemoveHandler _sky.OnPacket, handler2
            End If
            _sky = WithEventsValue
            If (Not _sky Is Nothing) Then
                AddHandler _sky.OnComplete, handler
                AddHandler _sky.OnPacket, handler2
            End If
        End Set
    End Property

End Class

Public Class LCNHolder
    ' Fields
    Private _BID As Integer
    Private _RID As Integer
    Private _SkyNum As Integer

    ' Methods
    Public Sub New(ByVal BID As Integer, ByVal RID As Integer, ByVal SkyLCN As Integer)
        _BID = BID
        _RID = RID
        _SkyNum = SkyLCN
    End Sub

    ' Properties
    Public Property BID As Integer
        Get
            Return _BID
        End Get
        Set(ByVal value As Integer)
            _BID = value
        End Set
    End Property

    Public Property RID As Integer
        Get
            Return _RID
        End Get
        Set(ByVal value As Integer)
            _RID = value
        End Set
    End Property

    Public Property SkyNum As Integer
        Get
            Return _SkyNum
        End Get
        Set(ByVal value As Integer)
            _SkyNum = value
        End Set
    End Property
End Class

Public Class Sky_Channel
    ' Fields
    Private _Channel_Name As String
    Private _ChannelId As Integer
    Private _encrypted As Boolean
    Private ReadOnly _epgChannelNumber As Dictionary(Of String, LCNHolder) = New Dictionary(Of String, LCNHolder)
    Private _HasChanged As Boolean
    Private _isPopulated As Boolean
    Private _Name As String
    Private _NewChannelRequired As Boolean
    Private _NID As Integer
    Private _SID As Integer
    Private _TID As Integer
    Public Events As Dictionary(Of Integer, SkyEvent) = New Dictionary(Of Integer, SkyEvent)

    ' Methods
    Public Function AddSkyLCN(ByVal LCNHold As LCNHolder) As Boolean
        If Not _epgChannelNumber.ContainsKey(LCNHold.BID & "-" & LCNHold.RID) Then
            _epgChannelNumber.Add(LCNHold.BID & "-" & LCNHold.RID, LCNHold)
            Return False
        End If
        Return True
    End Function

    Public Function ContainsLCN(ByVal BouquetID As Integer, ByVal RegionId As Integer) As Boolean
        Return _epgChannelNumber.ContainsKey((BouquetID.ToString & "-" & RegionId.ToString))
    End Function

    Public Function GetLCN(ByVal BouquetID As Integer, ByVal RegionId As Integer) As LCNHolder
        If _epgChannelNumber.ContainsKey(BouquetID.ToString & "-" & RegionId.ToString) Then
            Return _epgChannelNumber.Item(BouquetID.ToString & "-" & RegionId.ToString)
        End If
        Return Nothing
    End Function

    ' Properties
    Public Property AddChannelRequired As Boolean
        Get
            Return _NewChannelRequired
        End Get
        Set(ByVal value As Boolean)
            _NewChannelRequired = value
        End Set
    End Property

    Public Property Channel_Name As String
        Get
            Return _Channel_Name
        End Get
        Set(ByVal value As String)
            _Channel_Name = value
        End Set
    End Property

    Public Property ChannelID As Integer
        Get
            Return _ChannelId
        End Get
        Set(ByVal value As Integer)
            _ChannelId = value
        End Set
    End Property

    Public Property HasChanged As Boolean
        Get
            Return _HasChanged
        End Get
        Set(ByVal value As Boolean)
            _HasChanged = value
        End Set
    End Property

    Public Property isPopulated As Boolean
        Get
            Return _isPopulated
        End Get
        Set(ByVal value As Boolean)
            _isPopulated = value
        End Set
    End Property

    Public ReadOnly Property LCNCount As Object
        Get
            Return _epgChannelNumber.Count
        End Get
    End Property

    Public ReadOnly Property LCNS As Object
        Get
            Return _epgChannelNumber.Values
        End Get
    End Property

    Public Property NID As Integer
        Get
            Return _NID
        End Get
        Set(ByVal value As Integer)
            _NID = value
        End Set
    End Property

    Public Property SID As Integer
        Get
            Return _SID
        End Get
        Set(ByVal value As Integer)
            _SID = value
        End Set
    End Property

    Public Property TID As Integer
        Get
            Return _TID
        End Get
        Set(ByVal value As Integer)
            _TID = value
        End Set
    End Property

End Class

Public Class SkyEvent
    ' Fields
    Private _AD As Boolean = False
    Private _Category As String = ""
    Private _ChannelID As Integer = -1
    Private _CP As Boolean = False
    Private _duration As Integer = -1
    Private _EventID As Integer = -1
    Private _HD As Boolean = False
    Private _mjdStart As Long = 0
    Private _ParentalCategory As String = ""
    Private _SeriesID As Integer = 0
    Private _seriesTermination As Integer = 0
    Private _SoundType As Integer = -1
    Private _StartTime As Integer = -1
    Private _Subs As Boolean = False
    Private _Summary As String = ""
    Private _Title As String = ""
    Private _WS As Boolean = False
    Private Flags As String = ""

    ' Methods
    Public Sub SetFlags(ByVal IntegerNumber As Integer)
        _AD = ((IntegerNumber And 1) > 0)
        _CP = ((IntegerNumber And 2) > 0)
        _HD = ((IntegerNumber And 4) > 0)
        _WS = ((IntegerNumber And 8) > 0)
        _Subs = ((IntegerNumber And &H10) > 0)
        _SoundType = (IntegerNumber >> 6)
    End Sub

    Public Sub SetCategory(ByVal Category As Integer)
        Select Case (Category And 15)
            Case 1
                _ParentalCategory = "U"
                Exit Select
            Case 2
                _ParentalCategory = "PG"
                Exit Select
            Case 3
                _ParentalCategory = "12"
                Exit Select
            Case 4
                _ParentalCategory = "15"
                Exit Select
            Case 5
                _ParentalCategory = "18"
                Exit Select
            Case Else
                _ParentalCategory = ""
                Exit Select
        End Select
    End Sub

    ' Properties
    Public ReadOnly Property ParentalCategory As String
        Get
            Return _ParentalCategory
        End Get
    End Property

    Public ReadOnly Property DescriptionFlag As String
        Get
            Flags = ""
            If _AD Then
                Flags = (Flags & "[AD]")
            End If
            If _CP Then
                If (Flags <> "") Then
                    Flags = (Flags & ",")
                End If
                Flags = (Flags & "[CP]")
            End If
            If _HD Then
                If (Flags <> "") Then
                    Flags = (Flags & ",")
                End If
                Flags = (Flags & "[HD]")
            End If
            If _WS Then
                If (Flags <> "") Then
                    Flags = (Flags & ",")
                End If
                Flags = (Flags & "[W]")
            End If
            If _Subs Then
                If (Flags <> "") Then
                    Flags = (Flags & ",")
                End If
                Flags = (Flags & "[SUB]")
            End If

            Select Case _SoundType
                Case 1
                    If (Flags <> "") Then
                        Flags = (Flags & ",")
                    End If
                    Flags = (Flags & "[S]")
                    Exit Select
                Case 2
                    If (Flags <> "") Then
                        Flags = (Flags & ",")
                    End If
                    Flags = (Flags & "[DS]")
                    Exit Select
                Case 3
                    If (Flags <> "") Then
                        Flags = (Flags & ",")
                    End If
                    Flags = (Flags & "[DD]")
                    Exit Select
            End Select
            Return Flags
        End Get
    End Property

    Public Property Summary As String
        Get
            Return _Summary
        End Get
        Set(ByVal value As String)
            _Summary = value
        End Set
    End Property

    Public Property EventID As Integer
        Get
            Return _EventID
        End Get
        Set(ByVal value As Integer)
            _EventID = value
        End Set
    End Property

    Public Property StartTime As Integer
        Get
            Return _StartTime
        End Get
        Set(ByVal value As Integer)
            _StartTime = value
        End Set
    End Property

    Public Property Duration As Integer
        Get
            Return _duration
        End Get
        Set(ByVal value As Integer)
            _duration = value
        End Set
    End Property

    Public Property ChannelID As Integer
        Get
            Return _ChannelID
        End Get
        Set(ByVal value As Integer)
            _ChannelID = value
        End Set
    End Property

    Public Property Title As String
        Get
            Return _Title
        End Get
        Set(ByVal value As String)
            _Title = value
        End Set
    End Property

    Public Property Category As String
        Get
            Return _Category
        End Get
        Set(ByVal value As String)
            _Category = value
        End Set
    End Property

    Public Property SeriesID As Integer
        Get
            Return _SeriesID
        End Get
        Set(ByVal value As Integer)
            _SeriesID = value
        End Set
    End Property

    Public Property mjdStart As Long
        Get
            Return _mjdStart
        End Get
        Set(ByVal value As Long)
            _mjdStart = value
        End Set
    End Property

    Public Property seriesTermination As Integer
        Get
            Return _seriesTermination
        End Get
        Set(ByVal value As Integer)
            _seriesTermination = value
        End Set
    End Property

End Class

Public Class SkyBouquet
    'Fields
    Private _firstReceivedSectionNumber As Byte
    Private _isInitialized As Boolean = False
    Private _isPopulated As Boolean = False

    'Properties
    Public Property firstReceivedSectionNumber As Byte
        Get
            Return _firstReceivedSectionNumber
        End Get
        Set(ByVal value As Byte)
            _firstReceivedSectionNumber = value
        End Set
    End Property

    Public Property isInitialized As Boolean
        Get
            Return _isInitialized
        End Get
        Set(ByVal value As Boolean)
            _isInitialized = value
        End Set
    End Property

    Public Property isPopulated As Boolean
        Get
            Return _isPopulated
        End Get
        Set(ByVal value As Boolean)
            _isPopulated = value
        End Set
    End Property



End Class

Public Class HuffHolder
    ' Fields
    Private _buff As Byte()
    Private _Length As Integer
    Private _NextID As String

    ' Properties
    Public Property NextID As String
        Get
            Return _NextID
        End Get
        Set(ByVal value As String)
            _NextID = value
        End Set
    End Property

    Public Property Buff As Byte()
        Get
            Return _buff
        End Get
        Set(ByVal value As Byte())
            _buff = value
        End Set
    End Property

    Public Property Length As Integer
        Get
            Return _Length
        End Get
        Set(ByVal value As Integer)
            _Length = value
        End Set
    End Property

End Class

Public Class HuffmanTreeNode
    ' Fields
    Public P0 As HuffmanTreeNode
    Public P1 As HuffmanTreeNode
    Public Parent As HuffmanTreeNode
    Public Value As String
    Dim _strPath As String

    ' Methods
    Public Function Clear() As Boolean
        If (Not P1 Is Nothing) Then
            P1 = Nothing
            If (Not P0 Is Nothing) Then
                P0 = Nothing
            End If
        End If
        Return True
    End Function

    Private Overloads Function Equals() As Boolean
        Return False
    End Function

    Private Overloads Function ReferenceEquals() As Boolean
        Return False
    End Function

    ' Properties
    Public ReadOnly Property Path As String
        Get
            If _strPath Is Nothing Then
                If Not (Parent Is Nothing) Then
                    If (Parent.P0 Is Me) Then _strPath = "0"
                    If (Parent.P1 Is Me) Then _strPath = "1"
                    _strPath = Parent.Path & _strPath
                End If
            End If
            Return _strPath
        End Get
    End Property

End Class

Public Class SDTInfo
    ' Fields
    Private _Cat As Byte
    Private _ChannelName As String
    Private _is3d As Boolean
    Private _isFTA As Boolean
    Private _isHD As Boolean
    Private _isRadio As Boolean
    Private _isTV As Boolean
    Private _Provider As String
    Private _sid As Integer

    ' Properties
    Public Property SID As Integer
        Get
            Return _sid
        End Get
        Set(ByVal value As Integer)
            _sid = value
        End Set
    End Property

    Public Property ChannelName As String
        Get
            Return _ChannelName
        End Get
        Set(ByVal value As String)
            _ChannelName = value
        End Set
    End Property

    Public Property Provider As String
        Get
            Return _Provider
        End Get
        Set(ByVal value As String)
            _Provider = value
        End Set
    End Property

    Public Property Category As Integer
        Get
            Return _Cat
        End Get
        Set(ByVal value As Integer)
            _Cat = CByte(value)
        End Set
    End Property

    Public Property isFTA As Boolean
        Get
            Return _isFTA
        End Get
        Set(ByVal value As Boolean)
            _isFTA = value
        End Set
    End Property

    Public Property isRadio As Boolean
        Get
            Return _isRadio
        End Get
        Set(ByVal value As Boolean)
            _isRadio = value
        End Set
    End Property

    Public Property isTV As Boolean
        Get
            Return _isTV
        End Get
        Set(ByVal value As Boolean)
            _isTV = value
        End Set
    End Property

    Public Property isHD As Boolean
        Get
            Return _isHD
        End Get
        Set(ByVal value As Boolean)
            _isHD = value
        End Set
    End Property

    Public Property is3D As Boolean
        Get
            Return _is3d
        End Get
        Set(ByVal value As Boolean)
            _isHD = value
        End Set
    End Property

End Class

Public Class NITSatDescriptor
    ' Fields
    Private _FECInner As Integer
    Private _Frequency As Single
    Private _isS2 As Integer
    Private _Modulation As Integer
    Private _NetworkName As String
    Private _OrbitalPosition As Integer
    Private _Polarisation As Integer
    Private _RollOff As Integer
    Private _Symbolrate As Integer
    Private _TransportID As Integer
    Private _WestEastFlag As Integer

    ' Properties
    Public Property TID As Integer
        Get
            Return _TransportID
        End Get
        Set(ByVal value As Integer)
            _TransportID = value
        End Set
    End Property

    Public Property Frequency As Integer
        Get
            Return CInt(Math.Round(CDbl(_Frequency)))
        End Get
        Set(ByVal value As Integer)
            _Frequency = value
        End Set
    End Property

    Public Property OrbitalPosition As Integer
        Get
            Return _OrbitalPosition
        End Get
        Set(ByVal value As Integer)
            _OrbitalPosition = value
        End Set
    End Property

    Public Property WestEastFlag As Integer
        Get
            Return _WestEastFlag
        End Get
        Set(ByVal value As Integer)
            _WestEastFlag = value
        End Set
    End Property

    Public Property Polarisation As Integer
        Get
            Return _Polarisation
        End Get
        Set(ByVal value As Integer)
            _Polarisation = value
        End Set
    End Property

    Public Property Modulation As Integer
        Get
            Return _Modulation
        End Get
        Set(ByVal value As Integer)
            _Modulation = value
        End Set
    End Property

    Public Property Symbolrate As Integer
        Get
            Return _Symbolrate
        End Get
        Set(ByVal value As Integer)
            _Symbolrate = value
        End Set
    End Property

    Public Property FECInner As Integer
        Get
            Return _FECInner
        End Get
        Set(ByVal value As Integer)
            _FECInner = value
        End Set
    End Property

    Public Property RollOff As Integer
        Get
            Return _RollOff
        End Get
        Set(ByVal value As Integer)
            _RollOff = value
        End Set
    End Property

    Public Property isS2 As Integer
        Get
            Return _isS2
        End Get
        Set(ByVal value As Integer)
            _isS2 = value
        End Set
    End Property

    Public Property NetworkName As String
        Get
            Return _NetworkName
        End Get
        Set(ByVal value As String)
            _NetworkName = value
        End Set
    End Property

End Class

Public Class [Region]
    ' Fields
    Private _BouquetID As Integer
    Private _RegionID As Integer

    ' Properties
    Public Property BouquetID As Integer
        Get
            Return _BouquetID
        End Get
        Set(ByVal value As Integer)
            _BouquetID = value
        End Set
    End Property

    Public Property RegionID As Integer
        Get
            Return _RegionID
        End Get
        Set(ByVal value As Integer)
            _RegionID = value
        End Set
    End Property

End Class

