using System;
using System.Collections.Generic;

namespace POEApi.Infrastructure
{
    //WTB Bi-directional Dictionary in .NET

    public sealed class CurrencyAbbreviationMap
    {
        private static volatile CurrencyAbbreviationMap instance;
        private static object syncRoot = new Object();

        private static Dictionary<string, string> currencyToAbbreviation = new Dictionary<string, string>();
        private static Dictionary<string, string> abbreviationToCurrency = new Dictionary<string, string>();

        private CurrencyAbbreviationMap()
        {
            addItem("Цветная сфера", "цвет");
            addItem("Сфера перемен", "перем");
            addItem("Сфера златокузнеца", "злат");
            addItem("Сфера удачи", "удач");
            addItem("Резец картографа", "рез");
            addItem("Сфера соединения", "соед");
            addItem("Сфера алхимии", "алх");
            addItem("Сфера очищения", "очищ");
            addItem("Благодатная сфера", "благ");
            addItem("Сфера хаоса", "хаос");
            addItem("Сфера раскаяния", "раск");
            addItem("Сфера царей", "цар");
            addItem("Призма камнереза", "пкр");
            addItem("Божественная сфера", "бож");
            addItem("Сфера возвышения", "возв");
            addItem("Сфера ваал", "ваал");
        }

        private static void addItem(string currency, string abbreviation)
        {
            currencyToAbbreviation.Add(currency, abbreviation);
            abbreviationToCurrency.Add(abbreviation, currency);
        }

        public string FromAbbreviation(string abbreviation)
        {
            if (!abbreviationToCurrency.ContainsKey(abbreviation))
                return string.Empty;

            return abbreviationToCurrency[abbreviation];
        }

        public string FromCurrency(string currency)
        {
            if (!currencyToAbbreviation.ContainsKey(currency))
                return string.Empty;

            return currencyToAbbreviation[currency];
        }

        public static CurrencyAbbreviationMap Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                        if (instance == null)
                            instance = new CurrencyAbbreviationMap();

                return instance;
            }
        }
    }
}
