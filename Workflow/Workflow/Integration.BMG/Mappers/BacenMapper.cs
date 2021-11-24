using Domain.Payload;
using Integration.BMG.Schemas;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Integration.BMG.Mappers {
    public static class BacenMapper {

        public static IList<SelicTax> Map(SelicTaxResponse[] response) {
            try {
                var mappedList = new List<SelicTax>();
                foreach (var item in response) {
                    mappedList.Add(new SelicTax() {
                        Date = DateTime.ParseExact(item.data, "dd/MM/yyyy", null),
                        Value = decimal.Parse(item.valor, new CultureInfo("en-US"))
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

    }
}