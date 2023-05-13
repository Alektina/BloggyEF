using System;
using Microsoft.EntityFrameworkCore;
using RelEF;

MyDbContext Context = new MyDbContext();

Console.WriteLine("Blog Post manager");
Console.WriteLine();

bool KeepRunning = true;
while (KeepRunning)
{
    Console.WriteLine("Menu options");
    Console.WriteLine("1. Display all posts");
    Console.WriteLine("2. Display the name of all the categories");
    Console.WriteLine("3. Add new blog post");
    Console.WriteLine("4. Add av new category");
    Console.WriteLine("5. Display all the blog title from a category that the user chooses");
    Console.WriteLine("6. Add an existing blog post to an existing category");
    Console.WriteLine("7. Create a new tag");
    Console.WriteLine("8. Add blog post to tag");
    Console.WriteLine("9. Display all the blog posts that are tagged with a tag");

    Console.WriteLine("Press q to exit");
    Console.WriteLine(new string('-', 50));
    Console.WriteLine();
    Console.Write("Your choice: ");


    string UserChoice = Console.ReadLine();
    Console.WriteLine();

    if (UserChoice == "1")
    {
        var BlogPosts = Context.BlogPosts
             .Where(x => x.Id > 0)
             .OrderBy(x => x.Id);
        foreach (BlogPost x in BlogPosts)
        {
            Console.WriteLine
                ("Id ".PadRight(5)
                + "BlogTitle ".PadRight(25)
                + "BlogText ".PadRight(25));

            Console.WriteLine
                ($"{x.Id}".PadRight(5)
                + $"{x.BlogTitle}".PadRight(25)
                + $"{x.BlogText}".PadRight(25));

            Console.WriteLine();
            Console.WriteLine(new string('-', 50));
        }

    }
    else if (UserChoice == "2")
    {

        var Categories = Context.Categories
            .Where(x => x.Id > 0)
            .OrderBy(x => x.Id);

        foreach (Category x in Categories)
        {
            Console.WriteLine
                ("Id".PadRight(5)
                + "Name".PadRight(15));

            Console.WriteLine
                ($"{x.Id}".PadRight(5)
                + $"{x.Name}".PadRight(15));
            Console.WriteLine();
            Console.WriteLine(new string('-', 50));
        }
    }
    else if (UserChoice == "3")
    {

        Console.Write("Add a new blog title: ");
        string BlogTitle = Console.ReadLine();
        Console.Write("Add a blog text: ");
        string BlogText = Console.ReadLine();

        BlogPost UserBlogPost = new BlogPost();
        UserBlogPost.BlogTitle = BlogTitle;
        UserBlogPost.BlogText = BlogText;

        Context.BlogPosts.Add(UserBlogPost);
        Context.SaveChanges();
        Console.WriteLine("The data has been stored in the database.");

        Console.WriteLine();
        Console.WriteLine(new string('-', 50));
    }
    else if (UserChoice == "4")
    {
        Console.WriteLine("Add a new category");
        Console.Write("Add a category name: ");
        string CategoryName = Console.ReadLine();

        Category UserCategory = new Category();
        UserCategory.Name = CategoryName;
        Context.Categories.Add(UserCategory);
        Context.SaveChanges();

        Console.WriteLine("The data has been stored in the database.");

        Console.WriteLine();
        Console.WriteLine(new string('-', 50));
    }
    else if (UserChoice == "5")
    {
        Console.Write("Choose a category Id: ");
        string CategoryIdString = Console.ReadLine();
        int CategoryId = Convert.ToInt32(CategoryIdString);

        List<BlogPost> UserChoiceBP = Context.BlogPosts.Include(x => x.Categories)
            .Where(b => b.Categories.Select(c => c.Id).Contains(CategoryId))
            .ToList();

        foreach (BlogPost c in UserChoiceBP)
        {
            Console.WriteLine(new string('_', 30));
            Console.WriteLine($"- Blog title: {c.BlogTitle}");
            Console.WriteLine($"  Blog text: {c.BlogText}");
            Console.WriteLine();

            foreach (Category b in c.Categories)
            {
                Console.WriteLine($"Category: {b.Name}");
                Console.WriteLine();
            }
        }
        Console.WriteLine(new string('-', 50));
    }
    else if (UserChoice == "6")
    {
        Console.WriteLine("Add an existing blog to an existing category");

        Console.Write("Choose a blog post Id: ");
        string BlogPostIdstring = Console.ReadLine();
        int BlogPostId = Convert.ToInt32(BlogPostIdstring);
        BlogPost UserBP = Context.BlogPosts.FirstOrDefault(b => b.Id == BlogPostId);

        Console.Write("Choose a category Id: ");
        string CategoryIdString = Console.ReadLine();
        int CategoryId = Convert.ToInt32(CategoryIdString);
        Category UserCategory = Context.Categories.FirstOrDefault(x => x.Id == CategoryId);

        UserCategory.BlogPosts = new List<BlogPost>();
        UserBP.Categories = new List<Category>();

        UserCategory.BlogPosts.Add(UserBP);
        UserBP.Categories.Add(UserCategory);
        Context.SaveChanges();

        Console.WriteLine("The data has been stored in the database.");
        Console.WriteLine();
        Console.WriteLine(new string('-', 50));
    }
    else if (UserChoice == "7")
    {
        Console.Write("Add a new tag (begin with #): ");
        string UserTag = Console.ReadLine();

        Tag NewUserTag = new Tag();
        NewUserTag.Name = UserTag;

        Context.Tags.Add(NewUserTag);
        Context.SaveChanges();
        Console.WriteLine("The data has been stored in the database.");

        Console.WriteLine();
        Console.WriteLine(new string('-', 50));

    }
    else if (UserChoice == "8")
    {
        Console.WriteLine("Add an existing blog to an existing tag");
        Console.Write("Choose a blog post Id: ");
        string BlogPostIdstring = Console.ReadLine();

        int BlogPostId = Convert.ToInt32(BlogPostIdstring);
        BlogPost UserBlogPost = Context.BlogPosts.FirstOrDefault(b => b.Id == BlogPostId);

        Console.Write("Choose a tag Id: ");
        string TagIdString = Console.ReadLine();
        int TagId = Convert.ToInt32(TagIdString);
        Tag UserTag = Context.Tags.FirstOrDefault(x => x.Id == TagId);

        UserTag.BlogPosts = new List<BlogPost>();
        UserBlogPost.Tags = new List<Tag>();

        UserTag.BlogPosts.Add(UserBlogPost);
        UserBlogPost.Tags.Add(UserTag);
        Context.SaveChanges();

        Console.WriteLine("The data has been stored in the database.");
        Console.WriteLine();
        Console.WriteLine(new string('-', 50));

    }
    else if (UserChoice == "9")
    {
        List<Tag> AllTags = Context.Tags.Include(x => x.BlogPosts).ToList();
        foreach (Tag t in AllTags)
        {
            Console.WriteLine(new string('-', 30));
            Console.WriteLine($" Tag name: {t.Name}");
            Console.WriteLine();
            foreach (BlogPost b in t.BlogPosts)
            {
                Console.WriteLine($" Blog title: {b.BlogTitle}");
                Console.WriteLine($" Blog text: {b.BlogText}");
                Console.WriteLine();
            }
        }
        Console.WriteLine(new string('-', 50));
    }
    else if (UserChoice == "q")
    {
        KeepRunning = false;
        var BlogPosts = Context.BlogPosts
             .Where(x => x.Id > 0)
             .OrderBy(x => x.Id);
        foreach (BlogPost x in BlogPosts)
        {
            Console.WriteLine
                ("Id".PadRight(5)
                + "BlogTitle".PadRight(25)
                + "BlogText".PadRight(25));

            Console.WriteLine
                ($"{x.Id}".PadRight(5)
                + $"{x.BlogTitle}".PadRight(25)
                + $"{x.BlogText}".PadRight(25));

            Console.WriteLine();
            Console.WriteLine(new string('-', 50));
        }
    }
    Console.ReadKey(true);
}


