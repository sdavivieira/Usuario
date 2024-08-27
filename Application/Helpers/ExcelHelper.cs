using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ExcelHelper<T> where T : class, new()
    {
        private readonly IExcelDataReader _reader;

        public ExcelHelper(IFormFile file)
        {
            var stream = file.OpenReadStream();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (file.FileName.EndsWith(".csv"))
            {
                _reader = ExcelReaderFactory.CreateCsvReader(stream);
            }
            else
            {
                throw new NotSupportedException("Only CSV files are supported.");
            }
        }

        public List<T> GetValues()
        {
            var list = new List<T>();
            var table = ConvertToDataTable();

            // Ensure the table has rows
            if (table.Rows.Count == 0)
            {
                throw new InvalidOperationException("The CSV file is empty or does not contain data.");
            }

            var properties = typeof(T).GetProperties().ToList();
            foreach (DataRow row in table.Rows)
            {
                var item = CreateItemFromRow<T>(row, properties);
                if (item != null)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        private DataTable ConvertToDataTable()
        {
            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var dataset = _reader.AsDataSet(conf);
            return dataset.Tables[0];
        }

        private T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            var item = new T();

            foreach (var property in properties)
            {
                if (row.Table.Columns.Contains(property.Name))
                {
                    var value = row[property.Name];
                    if (value == DBNull.Value)
                    {
                        value = null;
                    }

                    property.SetValue(item, value);
                }
            }

            return item;
        }
    }
}
