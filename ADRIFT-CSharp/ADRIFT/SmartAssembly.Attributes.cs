using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
' Licence: Everyone is free to use the code contained in this file in any way.
Namespace SmartAssembly.Attributes

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct)> _;
public sealed class DoNotCaptureVariablesAttribute
    {
        Inherits Attribute;

    }

    <DoNotObfuscate>;
    <SmartAssembly.Attributes.DoNotPrune>;
    <AttributeUsage(AttributeTargets.Field | AttributeTargets.Class | AttributeTargets.Struct, Inherited:=true)> _;
public sealed class DoNotCaptureAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Struct)> _;
public sealed class DoNotObfuscateAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)> _;
public sealed class DoNotObfuscateTypeAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct)> _;
public sealed class DoNotPruneAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)> _;
public sealed class DoNotPruneTypeAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class)> _;
public sealed class DoNotSealTypeAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Method)> _;
public sealed class ReportExceptionAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)> _;
public sealed class ReportUsageAttribute
    {
        Inherits Attribute;

        public void New()
        {
        }

        public void New(string newName)
        {
        }
    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct)> _;
public sealed class ObfuscateControlFlowAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct)> _;
public sealed class DoNotObfuscateControlFlowAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Struct)> _;
public sealed class ObfuscateToAttribute
    {
        Inherits Attribute;

        public void New(string newName)
        {
        }
    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)> _;
public sealed class ObfuscateNamespaceToAttribute
    {
        Inherits Attribute;

        public void New(string newName)
        {
        }
    }

    <AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Module | AttributeTargets.Struct)> _;
public sealed class DoNotEncodeStringsAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct)> _;
public sealed class EncodeStringsAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Struct)> _;
public sealed class ExcludeFromMemberRefsProxyAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Delegate)>;
public sealed class StayPublicAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Method)>;
public sealed class DoNotMoveAttribute
    {
        Inherits Attribute;

    }

    <AttributeUsage(AttributeTargets.Class)>;
public sealed class DoNotMoveMethodsAttribute
    {
        Inherits Attribute;

    }

}
}