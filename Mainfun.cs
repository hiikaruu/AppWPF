using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Windows;
using System.Windows.Forms;
using System.Text.Json;
using WpfApp.Tables;
using WpfApp.Data;
using System.Text.Encodings.Web;

namespace WpfApp
{
    public class DB
    {
        public List<Person> GetTable()
        {
            return new MyDbContext().Persons.ToList();
        }


    }
    public class Excel
    {

        public void createFile()
        {
            List<Person> list = new DB().GetTable();
            createFile(list);
        }

        public void createFile(List<Person> list)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = @"" + fbd.SelectedPath + "\\Отчет отдела кадров.xlsx";
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;


                using (ExcelPackage pck = new ExcelPackage())
                {
                    var table = pck.Workbook.Worksheets.Add("Persons");
                    table.Cells[2, 1].LoadFromCollection(list, false);
                    table.Column(6).Style.Numberformat.Format = "dd-MM-yyyy";
                    table.Cells["B1"].Value = "ID сотрудника";
                    table.Cells["C1"].Value = "Имя";
                    table.Cells["D1"].Value = "Фамилия";
                    table.Cells["E1"].Value = "Отчество";
                    table.Cells["F1"].Value = "Дата рождения";
                    table.Cells["G1"].Value = "Номер елефона";
                    table.Cells["H1"].Value = "Отдел";
                    table.DeleteColumn(1);
                    table.Cells[1, 1, list.Count, 7 + 2].AutoFitColumns();
                    try
                    {
                        pck.SaveAs(new FileInfo(path));
                    }
                    catch (System.InvalidOperationException e)
                    {
                        System.Windows.MessageBox.Show("Ошибка выбора пути. Выберетие другой путь сохранения", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }
    }
    public class Json
    { 
        public void getJson()
        {
            List<Person> list = new DB().GetTable();
            getJson(list);
        }

        public void getJson(List<Person> list)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = System.Text.Json.JsonSerializer.Serialize(list, options);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = "../Reports/Отчет-Сотрудники.json";
                File.WriteAllText(path, json);  
            }
        }
    }

}