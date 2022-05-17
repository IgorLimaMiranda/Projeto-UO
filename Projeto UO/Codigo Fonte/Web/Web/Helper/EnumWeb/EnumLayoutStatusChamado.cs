using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Helper.EnumWeb {
    public enum EnumLayoutStatusChamado {
        [Description("label label-warning")]
        WARNING,

        [Description("label label-success")]
        SUCCESS,

        [Description("label label-danger")]
        DANGER,

        [Description("label label-info")]
        INFO
    }
}