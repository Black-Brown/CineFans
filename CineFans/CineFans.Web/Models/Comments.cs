﻿using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
