using System;
using Azure;

namespace RelEF
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogText { get; set; }

        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
    }
}