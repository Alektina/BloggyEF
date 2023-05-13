using System;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace RelEF
{
    [Index(nameof(Name), IsUnique = true)]

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BlogPost> BlogPosts { get; set; }					

    }
}