using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using System;
namespace BlogApp.Data.Concrete.EfCore{

    public static class SeedData{

        public static void TestVerileriniDoldur(IApplicationBuilder app){
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if(context != null){
                if(context.Database.GetPendingMigrations().Any()){
                context.Database.Migrate();
            }
            if(!context.Tags.Any()){
                context.Tags.AddRange(
                    new Tag { Text = "web programlama", Url = "web-programlama", Color = TagColors.primary },
                    new Tag { Text = "backend", Url = "backend", Color = TagColors.danger },
                    new Tag { Text = "frontend", Url = "frontend", Color = TagColors.secondary },
                    new Tag { Text = "game", Url = "game", Color = TagColors.success },
                    new Tag { Text = "fullstack", Url = "full-stack", Color = TagColors.warning },
                    new Tag { Text = "mobile development", Url = "mobile-development", Color = TagColors.info },
                    new Tag { Text = "cloud computing", Url = "cloud-computing", Color = TagColors.primary },
                    new Tag { Text = "AI & Machine Learning", Url = "ai-machine-learning", Color = TagColors.secondary },
                    new Tag { Text = "cybersecurity", Url = "cybersecurity", Color = TagColors.success },
                    new Tag { Text = "IoT", Url = "iot", Color = TagColors.warning }
                );
                context.SaveChanges();
            }
            var tags = context.Tags.ToList();
            Tag GetTag(string text) =>
            tags.FirstOrDefault(t => t.Text == text)
            ?? throw new InvalidOperationException($"Tag '{text}' not found.");

            if(!context.Users.Any()){
                 context.Users.AddRange(
                    new User 
                    { 
                        UserName = "elonmusk", 
                        Image = "elonmusk.jpg", 
                        Name = "Elon Musk", 
                        Email = "elon@spacex.com", 
                        Password = "spacex@2025" 
                    },
                    new User 
                    { 
                        UserName = "timcook", 
                        Image = "timcook.jpg", 
                        Name = "Tim Cook", 
                        Email = "tim@apple.com", 
                        Password = "apple@2025" 
                    },
                    new User 
                    { 
                        UserName = "billgates", 
                        Image = "billgates.jpg", 
                        Name = "Bill Gates", 
                        Email = "bill@microsoft.com", 
                        Password = "microsoft@2025" 
                    },
                    new User 
                    { 
                        UserName = "markzuckerberg", 
                        Image = "markzuckerberg.jpg", 
                        Name = "Mark Zuckerberg", 
                        Email = "mark@facebook.com", 
                        Password = "facebook@2025" 
                    },
                    new User 
                    { 
                        UserName = "satyanadella", 
                        Image = "satyanadella.jpg", 
                        Name = "Satya Nadella", 
                        Email = "satya@microsoft.com", 
                        Password = "microsoft@2025" 
                    },
                    new User 
                    { 
                        UserName = "sundarpichai", 
                        Image = "sundarpichai.jpg", 
                        Name = "Sundar Pichai", 
                        Email = "sundar@google.com", 
                        Password = "google@2025" 
                    },
                    new User 
                    { 
                        UserName = "jeffbezos", 
                        Image = "jeffbezos.jpg", 
                        Name = "Jeff Bezos", 
                        Email = "jeff@amazon.com", 
                        Password = "amazon@2025" 
                    },
                    new User 
                    { 
                        UserName = "sherylsandberg", 
                        Image = "sherylsandberg.jpg", 
                        Name = "Sheryl Sandberg", 
                        Email = "sheryl@facebook.com", 
                        Password = "facebook@2025" 
                    }
                );
                context.SaveChanges();
            }
            if(!context.Posts.Any()){
                var postList = new List<Post> {
                    new Post
                    {
                        Title = "Exploring Web Programming: The Future of Development",
                        Content = "Web programming continues to evolve with the rise of new frameworks and tools. Understanding these technologies is essential for modern developers.",
                        Description = "An in-depth guide to web development in 2025.",
                        Url = "exploring-web-programming",
                        IsActive = true,
                        Image = "web-programming.jpg",
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = new List<Tag> { GetTag("web programlama"), GetTag("AI & Machine Learning") },
                        UserId = 1,
                        Comments = new List<Comment> {
                            new Comment {Text = "Great article! I learned a lot about web programming.", PublishedOn = DateTime.Now, UserId = 3},
                            new Comment {Text = "Very informative, I agree with the points mentioned about the future of development.", PublishedOn = DateTime.Now, UserId = 4}
                        }
                    },
                    new Post
                    {
                        Title = "Backend Development: Challenges and Solutions",
                        Content = "The backend world offers various challenges, from database management to API development. Here's how to navigate them.",
                        Description = "Explore the world of backend development and solve common problems in building a robust backend.",
                        Url = "backend-development-challenges",
                        IsActive = true,
                        Image = "backend-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("cloud computing") },
                        UserId = 2,
                        Comments = new List<Comment> {
                            new Comment {Text = "The backend challenges are well articulated. It's a tough job.", PublishedOn = DateTime.Now, UserId = 5},
                            new Comment {Text = "I disagree with some points, but overall it was an insightful post.", PublishedOn = DateTime.Now, UserId = 6}
                        }
                    },
                    new Post
                    {
                        Title = "Frontend Development Trends You Need to Know",
                        Content = "Stay ahead in frontend development by understanding the key trends shaping user interfaces in 2025.",
                        Description = "This post covers frontend trends, frameworks, and tools that are essential for developers today.",
                        Url = "frontend-development-trends",
                        IsActive = true,
                        Image = "frontend-trends.jpg",
                        PublishedOn = DateTime.Now.AddDays(-15),
                        Tags = new List<Tag> { GetTag("frontend"), GetTag("mobile development") },
                        UserId = 3,
                        Comments = new List<Comment> {
                            new Comment {Text = "Frontend frameworks are evolving rapidly, this post hits the key points.", PublishedOn = DateTime.Now, UserId = 7},
                            new Comment {Text = "Useful post for anyone looking to stay up-to-date with frontend trends.", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "The Full-Stack Developer's Toolbox",
                        Content = "Full-stack developers need to be proficient in both front-end and back-end technologies. Here's the ultimate toolbox for full-stack development.",
                        Description = "Learn about the essential tools every full-stack developer should be familiar with.",
                        Url = "full-stack-developers-toolbox",
                        IsActive = true,
                        Image = "fullstack-tools.jpg",
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = new List<Tag> { GetTag("fullstack"), GetTag("IoT") },
                        UserId = 4,
                        Comments = new List<Comment> {
                            new Comment {Text = "A full-stack developer's toolkit is essential for versatility in the field.", PublishedOn = DateTime.Now, UserId = 3},
                            new Comment {Text = "Very helpful post. It covers almost all the tools needed.", PublishedOn = DateTime.Now, UserId = 7}
                        }
                    },
                    new Post
                    {
                        Title = "Game Development: Building Interactive Experiences",
                        Content = "Game development has become more accessible, but it's still challenging. Here's how to build engaging interactive experiences.",
                        Description = "This post explores the basics of game development, including important concepts and tools.",
                        Url = "game-development-basics",
                        IsActive = true,
                        Image = "game-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-25),
                        Tags = new List<Tag> { GetTag("game"), GetTag("cybersecurity") },
                        UserId = 5,
                        Comments = new List<Comment> {
                            new Comment {Text = "Great for beginners! Game development has so much potential.", PublishedOn = DateTime.Now, UserId = 6},
                            new Comment {Text = "Cybersecurity in games is often overlooked. Glad you mentioned it.", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "The Evolution of the Web: What's Next?",
                        Content = "The web is changing fast. In this post, we’ll explore what’s next for the internet and web technologies.",
                        Description = "What does the future hold for the web? From AI to blockchain, here's a look at the future of the internet.",
                        Url = "evolution-of-the-web",
                        IsActive = true,
                        Image = "web-evolution.jpg",
                        PublishedOn = DateTime.Now.AddDays(-30),
                        Tags = new List<Tag> { GetTag("web programlama"), GetTag("cloud computing") },
                        UserId = 6,
                        Comments = new List<Comment> {
                            new Comment {Text = "I think blockchain will be a big part of the future web.", PublishedOn = DateTime.Now, UserId = 7},
                            new Comment {Text = "Interesting insights on the future of the web. AI will definitely shape things.", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "Backend vs Frontend: The Battle for Web Dominance",
                        Content = "A deep dive into the differences between backend and frontend development, and which one is more crucial for web development.",
                        Description = "Understand the core differences between backend and frontend development, and their roles in the modern web.",
                        Url = "backend-vs-frontend",
                        IsActive = true,
                        Image = "backend-frontend.jpg",
                        PublishedOn = DateTime.Now.AddDays(-35),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("frontend") },
                        UserId = 7,
                        Comments = new List<Comment> {
                            new Comment {Text = "Both are equally important, but backend is often more complex.", PublishedOn = DateTime.Now, UserId = 6},
                            new Comment {Text = "Great comparison between backend and frontend.", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "Building a Game From Scratch: A Beginner's Guide",
                        Content = "Building a game from the ground up may seem daunting, but with the right steps, it can be a fun and rewarding experience.",
                        Description = "A guide for beginners on how to start building your own games, from design to coding.",
                        Url = "building-a-game-from-scratch",
                        IsActive = true,
                        Image = "game-guide.jpg",
                        PublishedOn = DateTime.Now.AddDays(-40),
                        Tags = new List<Tag> { GetTag("game"), GetTag("mobile development") },
                        UserId = 8,
                        Comments = new List<Comment> {
                            new Comment {Text = "Great advice for anyone starting with game development.", PublishedOn = DateTime.Now, UserId = 7},
                            new Comment {Text = "Building games from scratch is tough but rewarding, as you mentioned.", PublishedOn = DateTime.Now, UserId = 6}
                        }
                    },
                    new Post
                    {
                        Title = "The Future of Full-Stack Development",
                        Content = "Full-stack development is evolving rapidly. Here’s what’s next for full-stack developers and the tools that will shape the future.",
                        Description = "Full-stack development is changing rapidly with new tools and frameworks. Here’s a peek into the future.",
                        Url = "future-of-full-stack-development",
                        IsActive = true,
                        Image = "fullstack-future.jpg",
                        PublishedOn = DateTime.Now.AddDays(-45),
                        Tags = new List<Tag> { GetTag("fullstack"), GetTag("AI & Machine Learning") },
                        UserId = 8,
                        Comments = new List<Comment> {
                            new Comment {Text = "Excited about the future of full-stack development! These trends are so useful.", PublishedOn = DateTime.Now, UserId = 5},
                            new Comment {Text = "AI and machine learning will definitely impact full-stack development.", PublishedOn = DateTime.Now, UserId = 4}
                        }
                    }
                };
                context.Posts.AddRange(postList);
                context.SaveChanges();
            }
        }
    }
    }
}