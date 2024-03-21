﻿using System;
using System.Collections.Generic;
using static Store.Contractors.Field;

namespace Store.Contractors
{
    public class PostamateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>()
        {
            {"1", "Москва" },
            {"2", "Питер" },
        };

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            { 
                "1",
                new Dictionary<string, string> 
                {
                    {"1", "Каpанский вокзал" },
                    {"2", "Охотничий ряд"},
                    {"3", "Охотничий ряд"},
                }
            },

            {
                "2",
                new Dictionary<string, string>
                {
                    {"4", "Московский вокзал" },
                    {"5", "Гостинный двор"},
                    {"6", "Петропавловская крепость"},
                }
            }
        };

        public string UniqueCode => "Postamate";

        public string Title => "Доставка через постаматы в Москве и Питере";

        public Form CreateForm(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            return new Form(UniqueCode, order.Id, 1, false, new[]
            {
                new SelectionField("Город", "city", "1", cities),
            });
        }

        public Form MoveNext(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("Город", "city", "1"),
                        new SelectionField("Постамат", "postamate", "1", postamates["1"])
                    });
                }

                else if (values["city"] == "2")
                {
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("Город", "city", "2"),
                        new SelectionField("Постамат", "postamate", "4", postamates["4"])
                    });
                }

                else
                    throw new InvalidOperationException("Invalid postamate");
            }

            else if (step == 2)
            {
                return new Form(UniqueCode, orderId, 3, true, new Field[]
                {
                    new HiddenField("Город", "city", values["city"]),
                    new HiddenField("Постамат", "postamate", values["postamate"])
                });
            }

            else
                throw new InvalidOperationException("Invalid postamate step");
        }
    }
}
