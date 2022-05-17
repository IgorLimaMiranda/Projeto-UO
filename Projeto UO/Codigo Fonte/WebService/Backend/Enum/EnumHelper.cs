using System;
using System.Collections.Generic;

namespace Backend.Enum {
    public static class EnumHelper {
        public static List<string> GetEnumDescriptions<T>() where T : struct {
            Array enums = System.Enum.GetValues(typeof(T));

            List<string> enumDescriptions = new List<string>();
            foreach (T e in System.Enum.GetValues(typeof(T))) {
                enumDescriptions.Add(e.GetDescription());
            }

            return enumDescriptions;
        }

        public static List<T> GetEnums<T>() {
            List<T> enums = new List<T>();
            foreach (T e in System.Enum.GetValues(typeof(T))){
			    enums.Add(e);
		    }

		    return enums;
	    }
    }
}
