using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helper {
    public static class WebHelper {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) {
            if (source == null || !source.Any())
                return true;
            
            return false;
        }
    }
}