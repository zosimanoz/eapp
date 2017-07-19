using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Data.Crud.Attributes
{

    /// <summary>
    /// Optional IgnoreInsert attribute.
    /// Custom attribute to exclude a property from Insert methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
    }
}
