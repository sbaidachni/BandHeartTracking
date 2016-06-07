﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace BandClient
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
    private global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider _provider;

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace BandClient.BandClient_XamlTypeInfo
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[8];
            _typeNameTable[0] = "BandClient.Code.HeartData";
            _typeNameTable[1] = "Object";
            _typeNameTable[2] = "System.Collections.ObjectModel.ObservableCollection`1<Int32>";
            _typeNameTable[3] = "System.Collections.ObjectModel.Collection`1<Int32>";
            _typeNameTable[4] = "Int32";
            _typeNameTable[5] = "BandClient.MainPage";
            _typeNameTable[6] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[7] = "Windows.UI.Xaml.Controls.UserControl";

            _typeTable = new global::System.Type[8];
            _typeTable[0] = typeof(global::BandClient.Code.HeartData);
            _typeTable[1] = typeof(global::System.Object);
            _typeTable[2] = typeof(global::System.Collections.ObjectModel.ObservableCollection<global::System.Int32>);
            _typeTable[3] = typeof(global::System.Collections.ObjectModel.Collection<global::System.Int32>);
            _typeTable[4] = typeof(global::System.Int32);
            _typeTable[5] = typeof(global::BandClient.MainPage);
            _typeTable[6] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[7] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_HeartData() { return new global::BandClient.Code.HeartData(); }
        private object Activate_2_ObservableCollection() { return new global::System.Collections.ObjectModel.ObservableCollection<global::System.Int32>(); }
        private object Activate_3_Collection() { return new global::System.Collections.ObjectModel.Collection<global::System.Int32>(); }
        private object Activate_5_MainPage() { return new global::BandClient.MainPage(); }
        private void VectorAdd_2_ObservableCollection(object instance, object item)
        {
            var collection = (global::System.Collections.Generic.ICollection<global::System.Int32>)instance;
            var newItem = (global::System.Int32)item;
            collection.Add(newItem);
        }
        private void VectorAdd_3_Collection(object instance, object item)
        {
            var collection = (global::System.Collections.Generic.ICollection<global::System.Int32>)instance;
            var newItem = (global::System.Int32)item;
            collection.Add(newItem);
        }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::BandClient.BandClient_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::BandClient.BandClient_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  BandClient.Code.HeartData
                userType = new global::BandClient.BandClient_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_0_HeartData;
                userType.AddMemberName("Rates");
                userType.AddMemberName("CurrentRateIndex");
                userType.AddMemberName("CurrentRate");
                userType.AddMemberName("MinRate");
                userType.AddMemberName("MaxRate");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Object
                xamlType = new global::BandClient.BandClient_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  System.Collections.ObjectModel.ObservableCollection`1<Int32>
                userType = new global::BandClient.BandClient_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("System.Collections.ObjectModel.Collection`1<Int32>"));
                userType.CollectionAdd = VectorAdd_2_ObservableCollection;
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 3:   //  System.Collections.ObjectModel.Collection`1<Int32>
                userType = new global::BandClient.BandClient_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_3_Collection;
                userType.CollectionAdd = VectorAdd_3_Collection;
                xamlType = userType;
                break;

            case 4:   //  Int32
                xamlType = new global::BandClient.BandClient_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 5:   //  BandClient.MainPage
                userType = new global::BandClient.BandClient_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_5_MainPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 6:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::BandClient.BandClient_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 7:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::BandClient.BandClient_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;
            }
            return xamlType;
        }


        private object get_0_HeartData_Rates(object instance)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            return that.Rates;
        }
        private void set_0_HeartData_Rates(object instance, object Value)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            that.Rates = (global::System.Collections.ObjectModel.ObservableCollection<global::System.Int32>)Value;
        }
        private object get_1_HeartData_CurrentRateIndex(object instance)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            return that.CurrentRateIndex;
        }
        private void set_1_HeartData_CurrentRateIndex(object instance, object Value)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            that.CurrentRateIndex = (global::System.Int32)Value;
        }
        private object get_2_HeartData_CurrentRate(object instance)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            return that.CurrentRate;
        }
        private void set_2_HeartData_CurrentRate(object instance, object Value)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            that.CurrentRate = (global::System.Int32)Value;
        }
        private object get_3_HeartData_MinRate(object instance)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            return that.MinRate;
        }
        private void set_3_HeartData_MinRate(object instance, object Value)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            that.MinRate = (global::System.Int32)Value;
        }
        private object get_4_HeartData_MaxRate(object instance)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            return that.MaxRate;
        }
        private void set_4_HeartData_MaxRate(object instance, object Value)
        {
            var that = (global::BandClient.Code.HeartData)instance;
            that.MaxRate = (global::System.Int32)Value;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::BandClient.BandClient_XamlTypeInfo.XamlMember xamlMember = null;
            global::BandClient.BandClient_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "BandClient.Code.HeartData.Rates":
                userType = (global::BandClient.BandClient_XamlTypeInfo.XamlUserType)GetXamlTypeByName("BandClient.Code.HeartData");
                xamlMember = new global::BandClient.BandClient_XamlTypeInfo.XamlMember(this, "Rates", "System.Collections.ObjectModel.ObservableCollection`1<Int32>");
                xamlMember.Getter = get_0_HeartData_Rates;
                xamlMember.Setter = set_0_HeartData_Rates;
                break;
            case "BandClient.Code.HeartData.CurrentRateIndex":
                userType = (global::BandClient.BandClient_XamlTypeInfo.XamlUserType)GetXamlTypeByName("BandClient.Code.HeartData");
                xamlMember = new global::BandClient.BandClient_XamlTypeInfo.XamlMember(this, "CurrentRateIndex", "Int32");
                xamlMember.Getter = get_1_HeartData_CurrentRateIndex;
                xamlMember.Setter = set_1_HeartData_CurrentRateIndex;
                break;
            case "BandClient.Code.HeartData.CurrentRate":
                userType = (global::BandClient.BandClient_XamlTypeInfo.XamlUserType)GetXamlTypeByName("BandClient.Code.HeartData");
                xamlMember = new global::BandClient.BandClient_XamlTypeInfo.XamlMember(this, "CurrentRate", "Int32");
                xamlMember.Getter = get_2_HeartData_CurrentRate;
                xamlMember.Setter = set_2_HeartData_CurrentRate;
                break;
            case "BandClient.Code.HeartData.MinRate":
                userType = (global::BandClient.BandClient_XamlTypeInfo.XamlUserType)GetXamlTypeByName("BandClient.Code.HeartData");
                xamlMember = new global::BandClient.BandClient_XamlTypeInfo.XamlMember(this, "MinRate", "Int32");
                xamlMember.Getter = get_3_HeartData_MinRate;
                xamlMember.Setter = set_3_HeartData_MinRate;
                break;
            case "BandClient.Code.HeartData.MaxRate":
                userType = (global::BandClient.BandClient_XamlTypeInfo.XamlUserType)GetXamlTypeByName("BandClient.Code.HeartData");
                xamlMember = new global::BandClient.BandClient_XamlTypeInfo.XamlMember(this, "MaxRate", "Int32");
                xamlMember.Getter = get_4_HeartData_MaxRate;
                xamlMember.Setter = set_4_HeartData_MaxRate;
                break;
            }
            return xamlMember;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::BandClient.BandClient_XamlTypeInfo.XamlSystemBaseType
    {
        global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::BandClient.BandClient_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}

