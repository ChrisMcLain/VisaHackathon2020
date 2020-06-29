using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic.FileIO;

namespace VisaHackathon2020.Giveback
{
    public static class MerchantCategoryService
    {
        private static Collection<MerchantCategory> _categories;

        public static Collection<MerchantCategory> Categories => GetCategories();

        private static Collection<MerchantCategory> GetCategories()
        {
            if (_categories != null)
            {
                return _categories;
            }

            using var parser = new TextFieldParser(@"merchant_category_codes.csv")
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new []{","}
            };
                
            _categories = new Collection<MerchantCategory>();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                
                _categories.Add(new MerchantCategory
                {
                    Id = int.Parse(fields[0]),
                    Name = fields[1]
                });
            }

            return _categories;
        }
    }

    public class MerchantCategory
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}