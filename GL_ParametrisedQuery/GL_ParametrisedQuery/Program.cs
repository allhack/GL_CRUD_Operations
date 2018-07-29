using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GL_ParametrisedQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RecordsRepository repository = new RecordsRepository())
            {
                foreach (var rec in repository.GetAll())
                {
                    ShowRecordInfo(rec);
                }
                Console.WriteLine(new String('=', 70));

                List<Record> records = repository.GetAll().ToList();
                Record recordToUpdate = records[0];
                recordToUpdate.Author = "Vasya";

                repository.Update(recordToUpdate);

                foreach (var rec in repository.GetAll())
                {
                    ShowRecordInfo(rec);
                }
                Console.WriteLine(new String('=', 70));

                repository.Delete(6);

                foreach (var rec in repository.GetAll())
                {
                    ShowRecordInfo(rec);
                }
                Console.WriteLine(new String('=', 70));

                repository.Add(new Record("Hello GlobalLogic", "Sweet kitty"));

                foreach (var rec in repository.GetAll())
                {
                    ShowRecordInfo(rec);                    
                }
                Console.WriteLine(new String('=', 70));
            }
        }

        static void ShowRecordInfo(Record rec)
        {
            Console.WriteLine(rec.Id);
            Console.WriteLine(rec.Text);
            Console.WriteLine(rec.Author);
            Console.WriteLine(rec.RecordDate);
            Console.WriteLine();
        }
    }
}
