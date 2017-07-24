using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Data.Crud.Attributes
{
    /// <summary>
    /// Optional IgnoreSelect attribute.
    /// Attribute to exclude a property from Select methods
    /// </summary>

    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }
}
