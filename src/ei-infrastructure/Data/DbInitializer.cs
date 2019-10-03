using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using ei_infrastructure.Data.POCOs;

// ReSharper disable StringLiteralTypo

namespace ei_infrastructure.Data
{
    public static class DbInitializer
    {
        private static bool _settingsHaveBeenInitialized;

        public static void InitializeSettings()
        {
            if (_settingsHaveBeenInitialized) return;
            SqlMapperExtensions.TableNameMapper = type => type.Name; // https://dapper-tutorial.net/knowledge-base/32204808/dapper-use-singular-table-name
            _settingsHaveBeenInitialized = true;
        }

        public static async Task SeedAsync(IDbConnection dbConnection)
        {
            if (!dbConnection.GetAll<UserAccount>().Any())
                await dbConnection.InsertAsync(GetPreconfiguredUserAccounts());
            if (!dbConnection.GetAll<WireColor>().Any())
                await dbConnection.InsertAsync(GetPreconfiguredWireColors());
        }

        private static IEnumerable<UserAccount> GetPreconfiguredUserAccounts()
        {
            return new[]
            {
                new UserAccount {Username = "limon", Password = "limonxz123"},
                new UserAccount {Username = "rios", Password = "loerardo123"},
                new UserAccount {Username = "mota", Password = "maciaseo123"},
                new UserAccount {Username = "lesair", Password = "israel123"}
            };
        }

        private static IEnumerable<WireColor> GetPreconfiguredWireColors()
        {
            return new[]
            {
                new WireColor {Code = "L", Name = "Blue", TranslatedName = "Azul", BaseColor = "#0000FF"},
                new WireColor {Code = "O", Name = "Orange", TranslatedName = "Naranja", BaseColor = "#FFA500"},
                new WireColor {Code = "B", Name = "Black", TranslatedName = "Negro", BaseColor = "#000000"},
                new WireColor {Code = "R", Name = "Red", TranslatedName = "Rojo", BaseColor = "#FF0000"},
                new WireColor {Code = "BR", Name = "Brown", TranslatedName = "Café", BaseColor = "#A52A2A"},
                new WireColor {Code = "G", Name = "Green", TranslatedName = "Verde", BaseColor = "#008000"},
                new WireColor {Code = "Y", Name = "Yellow", TranslatedName = "Amarillo", BaseColor = "#FFFF00"},
                new WireColor {Code = "PU", Name = "Purple", TranslatedName = "Púrpura", BaseColor = "#800080"},
                new WireColor {Code = "SB", Name = "Sky Blue", TranslatedName = "Azul Cielo", BaseColor = "#87CEEB"},
                new WireColor {Code = "W", Name = "White", TranslatedName = "Blanco", BaseColor = "#FFFFFF"},
                new WireColor {Code = "LG", Name = "Light Green", TranslatedName = "Verde Claro", BaseColor = "#90EE90"},
                new WireColor {Code = "GR", Name = "Gray", TranslatedName = "Gris", BaseColor = "#808080"},
                new WireColor {Code = "P", Name = "Pink", TranslatedName = "Pink", BaseColor = "#FFC0CB"},
                new WireColor {Code = "BG", Name = "Beige", TranslatedName = "Beige", BaseColor = "#F5F5DC"},
                new WireColor {Code = "LA", Name = "Lavender", TranslatedName = "Lavanda", BaseColor = "#E6E6FA"},
                new WireColor {Code = "LA-L", Name = "Lavender-Blue", TranslatedName = "Lavanda-Azul", BaseColor = "#E6E6FA", StripeColor = "#0000FF"},
                new WireColor {Code = "LA-O", Name = "Lavender-Orange", TranslatedName = "Lavanda-Naranja", BaseColor = "#E6E6FA", StripeColor = "#FFA500"},
                new WireColor {Code = "LA-B", Name = "Lavender-Black", TranslatedName = "Lavanda-Negro", BaseColor = "#E6E6FA", StripeColor = "#000000"},
                new WireColor {Code = "LA-R", Name = "Lavender-Red", TranslatedName = "Lavanda-Rojo", BaseColor = "#E6E6FA", StripeColor = "#FF0000"},
                new WireColor {Code = "LA-BR", Name = "Lavender-Brown", TranslatedName = "Lavanda-Café", BaseColor = "#E6E6FA", StripeColor = "#A52A2A"},
                new WireColor {Code = "LA-G", Name = "Lavender-Green", TranslatedName = "Lavanda-Verde", BaseColor = "#E6E6FA", StripeColor = "#008000"},
                new WireColor {Code = "LA-Y", Name = "Lavender-Yellow", TranslatedName = "Lavanda-Amarillo", BaseColor = "#E6E6FA", StripeColor = "#FFFF00"},
                new WireColor {Code = "LA-PU", Name = "Lavender-Purple", TranslatedName = "Lavanda-Púrpura", BaseColor = "#E6E6FA", StripeColor = "#800080"},
                new WireColor {Code = "LA-SB", Name = "Lavender-Sky Blue", TranslatedName = "Lavanda-Azul Cielo", BaseColor = "#E6E6FA", StripeColor = "#87CEEB"},
                new WireColor {Code = "LA-W", Name = "Lavender-White", TranslatedName = "Lavanda-Blanco", BaseColor = "#E6E6FA", StripeColor = "#FFFFFF"},
                new WireColor {Code = "LA-LG", Name = "Lavender-Light Green", TranslatedName = "Lavanda-Verde Claro", BaseColor = "#E6E6FA", StripeColor = "#90EE90"},
                new WireColor {Code = "LA-GR", Name = "Lavender-Gray", TranslatedName = "Lavanda-Gris", BaseColor = "#E6E6FA", StripeColor = "#808080"},
                new WireColor {Code = "LA-P", Name = "Lavender-Pink", TranslatedName = "Lavanda-Rosa", BaseColor = "#E6E6FA", StripeColor = "#FFC0CB"},
                new WireColor {Code = "LA-BG", Name = "Lavender-Beige", TranslatedName = "Lavanda-Beige", BaseColor = "#E6E6FA", StripeColor = "#F5F5DC"}
            };
        }
    }
}