using System;

namespace Entities
{
    public class Record
    {
        #region Properties
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime RecordDate { get; set; }
        #endregion

        #region Constructors
        public Record()
        {
            RecordDate = DateTime.Now;
        }

        public Record(string text, string author)
        {
            Text = text;
            Author = author;
            RecordDate = DateTime.Now;
        }
        #endregion
    }
}
