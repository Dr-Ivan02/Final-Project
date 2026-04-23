using System;
using System.Collections.Generic;
using System.Text;

namespace WorkOrderSystem.Models
{
    // Represents a comment associated with a work order
    public class Comment
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public Comment() { }
        public Comment(int workOrderId, string text)
        {
            WorkOrderId = workOrderId;
            Text = text;
            Date = DateTime.Now;
        }
    }
}
