﻿using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
