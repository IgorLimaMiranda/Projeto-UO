using Backend.Enum;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Web.Helper {
    public class FrontEnumHelper {
        public static List<SelectListItem> AtribuirItensEnum<T>(IEnumerable<T> elements) where T : struct {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements) {
                selectList.Add(new SelectListItem {
                    Value = element.GetDescription(),
                    Text = element.GetDescription()
                });
            }
            return selectList;
        }

        public static List<SelectListItem> AtribuirItensEnumInt<T>(IEnumerable<T> elements) where T : struct {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements) {
                selectList.Add(new SelectListItem {
                    Value = (Convert.ToInt32(element)).ToString(),
                    Text = element.GetDescription()
                });
            }
            return selectList;
        }
    }
}