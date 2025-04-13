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
                        UserName = "elonmusk1", 
                        Image = "elonmusk.jpg", 
                        Name = "Elon Musk", 
                        Email = "elon1@spacex.com", 
                        Password = "spacex@2025" 
                    },
                    new User 
                    { 
                        UserName = "timcook1", 
                        Image = "timcook.jpg", 
                        Name = "Tim Cook", 
                        Email = "tim1@apple.com", 
                        Password = "apple@2025" 
                    },
                    new User 
                    { 
                        UserName = "billgates1", 
                        Image = "billgates.jpg", 
                        Name = "Bill Gates", 
                        Email = "bill1@microsoft.com", 
                        Password = "microsoft@2025" 
                    },
                    new User 
                    { 
                        UserName = "markzuckerberg1", 
                        Image = "markzuckerberg.jpg", 
                        Name = "Mark Zuckerberg", 
                        Email = "mark1@facebook.com", 
                        Password = "facebook@2025" 
                    },
                    new User 
                    { 
                        UserName = "satyanadella1", 
                        Image = "satyanadella.jpg", 
                        Name = "Satya Nadella", 
                        Email = "satya1@microsoft.com", 
                        Password = "microsoft@2025" 
                    },
                    new User 
                    { 
                        UserName = "sundarpichai1", 
                        Image = "sundarpichai.jpg", 
                        Name = "Sundar Pichai", 
                        Email = "sundar1@google.com", 
                        Password = "google@2025" 
                    },
                    new User 
                    { 
                        UserName = "jeffbezos1", 
                        Image = "jeffbezos.jpg", 
                        Name = "Jeff Bezos", 
                        Email = "jeff1@amazon.com", 
                        Password = "amazon@2025" 
                    },
                    new User 
                    { 
                        UserName = "sherylsandberg1", 
                        Image = "sherylsandberg.jpg", 
                        Name = "Sheryl Sandberg", 
                        Email = "sheryl1@facebook.com", 
                        Password = "facebook@2025" 
                    },
                    new User 
                    { 
                        UserName = "larrypage1", 
                        Image = "larrypage.jpg", 
                        Name = "Larry Page", 
                        Email = "larry1@google.com", 
                        Password = "google@2025" 
                    },
                    new User 
                    { 
                        UserName = "sergeybrin1", 
                        Image = "sergeybrin.jpg", 
                        Name = "Sergey Brin", 
                        Email = "sergey1@google.com", 
                        Password = "google@2025" 
                    },
                    new User 
                    { 
                        UserName = "jackdorsey1", 
                        Image = "jackdorsey.jpg", 
                        Name = "Jack Dorsey", 
                        Email = "jack1@twitter.com", 
                        Password = "twitter@2025" 
                    },
                    new User 
                    { 
                        UserName = "evanwilliams1", 
                        Image = "evanwilliams.jpg", 
                        Name = "Evan Williams", 
                        Email = "evan1@twitter.com", 
                        Password = "twitter@2025" 
                    },
                    new User 
                    { 
                        UserName = "richardbranson1", 
                        Image = "richardbranson.jpg", 
                        Name = "Richard Branson", 
                        Email = "richard1@virgin.com", 
                        Password = "virgin@2025" 
                    },
                    new User 
                    { 
                        UserName = "mukeshambani1", 
                        Image = "mukeshambani.jpg", 
                        Name = "Mukesh Ambani", 
                        Email = "mukesh1@reliance.com", 
                        Password = "reliance@2025" 
                    },
                    new User 
                    { 
                        UserName = "jackma1", 
                        Image = "jackma.jpg", 
                        Name = "Jack Ma", 
                        Email = "jack1@alibaba.com", 
                        Password = "alibaba@2025" 
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
                    },
                    new Post
                    {
                        Title = "Cloud Native Development: What You Need to Know",
                        Content = "Cloud-native development allows developers to build applications that are scalable, resilient, and easy to manage. Here's what you need to know to get started.",
                        Description = "Learn the basics of cloud-native development and how it’s shaping the future of software engineering.",
                        Url = "cloud-native-development",
                        IsActive = true,
                        Image = "web-programming.jpg",
                        PublishedOn = DateTime.Now.AddDays(-50),
                        Tags = new List<Tag> { GetTag("cloud computing"), GetTag("backend") },
                        UserId = 9,
                        Comments = new List<Comment> {
                            new Comment {Text = "Cloud-native apps are the future! Great guide.", PublishedOn = DateTime.Now, UserId = 5},
                            new Comment {Text = "The scalability aspect is crucial for today's apps.", PublishedOn = DateTime.Now, UserId = 4}
                        }
                    },
                    new Post
                    {
                        Title = "Exploring Microservices: Architecture and Best Practices",
                        Content = "Microservices architecture is a growing trend in modern software development. Learn the advantages, challenges, and best practices for implementing microservices.",
                        Description = "A deep dive into microservices architecture, its benefits, and how to apply it to your projects.",
                        Url = "exploring-microservices",
                        IsActive = true,
                        Image = "backend-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-55),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("cloud computing") },
                        UserId = 10,
                        Comments = new List<Comment> {
                            new Comment {Text = "Microservices can be difficult to implement but really useful.", PublishedOn = DateTime.Now, UserId = 3},
                            new Comment {Text = "Great post! The section on best practices was very helpful.", PublishedOn = DateTime.Now, UserId = 6}
                        }
                    },
                    new Post
                    {
                        Title = "Building Scalable APIs with GraphQL",
                        Content = "GraphQL is a powerful query language for APIs. Learn how to build scalable, flexible APIs that improve the development process and performance.",
                        Description = "A guide to using GraphQL to create scalable APIs and the benefits it brings over REST.",
                        Url = "scalable-apis-with-graphql",
                        IsActive = true,
                        Image = "frontend-trends.jpg",
                        PublishedOn = DateTime.Now.AddDays(-60),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("fullstack") },
                        UserId = 11,
                        Comments = new List<Comment> {
                            new Comment {Text = "GraphQL is a game-changer for API development. Great read!", PublishedOn = DateTime.Now, UserId = 7},
                            new Comment {Text = "I prefer REST, but GraphQL has its advantages for sure.", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "The Importance of Cybersecurity in Modern Applications",
                        Content = "With the rise of cyber threats, ensuring the security of your applications is more important than ever. Here's why cybersecurity should be a priority in modern software development.",
                        Description = "Understand the importance of cybersecurity practices in development and how to integrate them into your workflow.",
                        Url = "importance-of-cybersecurity",
                        IsActive = true,
                        Image = "fullstack-tools.jpg",
                        PublishedOn = DateTime.Now.AddDays(-65),
                        Tags = new List<Tag> { GetTag("cybersecurity"), GetTag("backend") },
                        UserId = 12,
                        Comments = new List<Comment> {
                            new Comment {Text = "Cybersecurity is often overlooked, great that you’re focusing on it.", PublishedOn = DateTime.Now, UserId = 6},
                            new Comment {Text = "I agree. Implementing solid security practices is a must.", PublishedOn = DateTime.Now, UserId = 5}
                        }
                    },
                    new Post
                    {
                        Title = "Understanding AI in Web Development",
                        Content = "Artificial Intelligence is playing an increasingly important role in web development. Discover how AI is being used to enhance user experience, automate tasks, and improve web performance.",
                        Description = "Explore the intersection of AI and web development, and how you can leverage AI tools to boost your projects.",
                        Url = "ai-in-web-development",
                        IsActive = true,
                        Image = "backend-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-70),
                        Tags = new List<Tag> { GetTag("AI & Machine Learning"), GetTag("frontend") },
                        UserId = 13,
                        Comments = new List<Comment> {
                            new Comment {Text = "AI in web dev is such an exciting topic. Looking forward to using more AI tools.", PublishedOn = DateTime.Now, UserId = 7},
                            new Comment {Text = "AI tools can save so much time. Great insights in this post!", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "The Rise of Serverless Architecture",
                        Content = "Serverless architecture is becoming a popular choice for developers who want to reduce infrastructure management and focus on building applications. Learn about its benefits and use cases.",
                        Description = "A guide to understanding serverless architecture and how it can streamline your development process.",
                        Url = "serverless-architecture",
                        IsActive = true,
                        Image = "fullstack-future.jpg",
                        PublishedOn = DateTime.Now.AddDays(-75),
                        Tags = new List<Tag> { GetTag("cloud computing"), GetTag("backend") },
                        UserId = 14,
                        Comments = new List<Comment> {
                            new Comment {Text = "Serverless is a great choice for certain use cases. Thanks for the insights!", PublishedOn = DateTime.Now, UserId = 9},
                            new Comment {Text = "I’ve been looking into serverless for my project, this helped a lot.", PublishedOn = DateTime.Now, UserId = 6}
                        }
                    },
                    new Post
                    {
                        Title = "Building Real-Time Web Apps with WebSockets",
                        Content = "WebSockets allow you to create real-time applications where the server and client can communicate continuously. Learn how to implement WebSockets in your web applications.",
                        Description = "A deep dive into WebSockets and how they can be used to build real-time applications in web development.",
                        Url = "real-time-web-apps-with-websockets",
                        IsActive = true,
                        Image = "fullstack-tools.jpg",
                        PublishedOn = DateTime.Now.AddDays(-80),
                        Tags = new List<Tag> { GetTag("frontend"), GetTag("backend") },
                        UserId = 15,
                        Comments = new List<Comment> {
                            new Comment {Text = "Real-time web apps are essential for interactive applications. Great post!", PublishedOn = DateTime.Now, UserId = 10},
                            new Comment {Text = "This is a fantastic explanation of WebSockets. I'll implement this soon.", PublishedOn = DateTime.Now, UserId = 8}
                        }
                    },
                    new Post
                    {
                        Title = "Introduction to Continuous Integration and Continuous Deployment (CI/CD)",
                        Content = "CI/CD is a cornerstone of modern development workflows. Learn the basics of CI/CD and how to integrate it into your projects to improve efficiency and reduce bugs.",
                        Description = "An introduction to the concepts of Continuous Integration and Continuous Deployment (CI/CD), and how they help automate the development lifecycle.",
                        Url = "ci-cd-introduction",
                        IsActive = true,
                        Image = "game-guide.jpg",
                        PublishedOn = DateTime.Now.AddDays(-85),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("fullstack") },
                        UserId = 6,
                        Comments = new List<Comment> {
                            new Comment {Text = "CI/CD is essential for modern development. Thanks for the overview.", PublishedOn = DateTime.Now, UserId = 11},
                            new Comment {Text = "Setting up CI/CD has saved us so much time. Highly recommended.", PublishedOn = DateTime.Now, UserId = 12}
                        }
                    },
                    new Post
                    {
                        Title = "The Future of Mobile Development: Trends and Tools",
                        Content = "Mobile development is rapidly evolving. Stay ahead of the curve by learning about the latest trends and tools in mobile app development.",
                        Description = "An exploration of the future of mobile development, including trends and the tools that will shape the industry in the coming years.",
                        Url = "future-of-mobile-development",
                        IsActive = true,
                        Image = "backend-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-90),
                        Tags = new List<Tag> { GetTag("mobile development"), GetTag("frontend") },
                        UserId = 7,
                        Comments = new List<Comment> {
                            new Comment {Text = "The mobile dev field is evolving so fast! Excited to try these tools.", PublishedOn = DateTime.Now, UserId = 13},
                            new Comment {Text = "Great insights into mobile development. Will definitely keep an eye on these trends.", PublishedOn = DateTime.Now, UserId = 14}
                        }
                    },
                    new Post
                    {
                        Title = "Leveraging Artificial Intelligence for Web Optimization",
                        Content = "AI is revolutionizing web development by providing solutions for improving user experience, personalization, and performance. Learn how to leverage AI for web optimization.",
                        Description = "Discover how AI can be used to optimize your web applications and provide a more personalized experience for users.",
                        Url = "ai-for-web-optimization",
                        IsActive = true,
                        Image = "backend-frontend.jpg",
                        PublishedOn = DateTime.Now.AddDays(-95),
                        Tags = new List<Tag> { GetTag("AI & Machine Learning"), GetTag("frontend") },
                        UserId = 3,
                        Comments = new List<Comment> {
                            new Comment {Text = "AI in web optimization is a fascinating concept. Looking forward to applying it.", PublishedOn = DateTime.Now, UserId = 5},
                            new Comment {Text = "Personalization with AI is definitely the future of web apps. Thanks for the insights!", PublishedOn = DateTime.Now, UserId = 4}
                        }
                    },
                    new Post
                    {
                        Title = "The Importance of Code Quality in Software Development",
                        Content = "Code quality is critical for maintaining and scaling software. Learn best practices for writing clean, maintainable, and efficient code.",
                        Description = "A guide to understanding the significance of code quality and how to implement best practices in your development process.",
                        Url = "importance-of-code-quality",
                        IsActive = true,
                        Image = "web-evolution.jpg",
                        PublishedOn = DateTime.Now.AddDays(-100),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("fullstack") },
                        UserId = 1,
                        Comments = new List<Comment> {
                            new Comment {Text = "Great post! Clean code is essential for long-term project success.", PublishedOn = DateTime.Now, UserId = 2},
                            new Comment {Text = "I’ve been working on improving my code quality. This post gives great tips!", PublishedOn = DateTime.Now, UserId = 3}
                        }
                    },
                    new Post
                    {
                        Title = "How to Build Scalable Web Applications",
                        Content = "Building scalable web applications is essential for handling increased user loads and traffic. Learn strategies for designing and developing scalable systems.",
                        Description = "A practical guide to building scalable web applications and ensuring your architecture can handle growth.",
                        Url = "building-scalable-web-apps",
                        IsActive = true,
                        Image = "frontend-trends.jpg",
                        PublishedOn = DateTime.Now.AddDays(-105),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("cloud computing") },
                        UserId = 4,
                        Comments = new List<Comment> {
                            new Comment {Text = "Scalability is so important! Thanks for the clear explanation.", PublishedOn = DateTime.Now, UserId = 5},
                            new Comment {Text = "I’m working on a project that needs to scale, this is very useful.", PublishedOn = DateTime.Now, UserId = 6}
                        }
                    },
                    new Post
                    {
                        Title = "Exploring the World of Cybersecurity in Web Development",
                        Content = "Cybersecurity is one of the most important aspects of web development. Learn about key security measures every developer should implement to protect their applications.",
                        Description = "An in-depth look at the importance of cybersecurity in web development and best practices for securing web applications.",
                        Url = "cybersecurity-in-web-development",
                        IsActive = true,
                        Image = "game-guide.jpg",
                        PublishedOn = DateTime.Now.AddDays(-110),
                        Tags = new List<Tag> { GetTag("cybersecurity"), GetTag("backend") },
                        UserId = 7,
                        Comments = new List<Comment> {
                            new Comment {Text = "Security is often overlooked. Thanks for bringing this topic up.", PublishedOn = DateTime.Now, UserId = 8},
                            new Comment {Text = "The insights on securing web apps are incredibly useful. I’ll be more mindful of these best practices.", PublishedOn = DateTime.Now, UserId = 9}
                        }
                    },
                    new Post
                    {
                        Title = "Cloud-Native Development: What You Need to Know",
                        Content = "Cloud-native development allows you to build and deploy applications in the cloud, improving flexibility and scalability. Learn the key concepts and tools for cloud-native development.",
                        Description = "A comprehensive guide to understanding cloud-native development, including the benefits and tools used in cloud-based software development.",
                        Url = "cloud-native-development",
                        IsActive = true,
                        Image = "web-evolution.jpg",
                        PublishedOn = DateTime.Now.AddDays(-115),
                        Tags = new List<Tag> { GetTag("cloud computing"), GetTag("backend") },
                        UserId = 10,
                        Comments = new List<Comment> {
                            new Comment {Text = "I’ve been working on a cloud-native project and this post helped me refine my approach.", PublishedOn = DateTime.Now, UserId = 11},
                            new Comment {Text = "Cloud-native development is definitely the future. Great post!", PublishedOn = DateTime.Now, UserId = 12}
                        }
                    },
                    new Post
                    {
                        Title = "The Basics of Machine Learning for Developers",
                        Content = "Machine learning is transforming the tech landscape. As a developer, learning the fundamentals of machine learning can give you a competitive edge. Here's how to get started.",
                        Description = "A beginner-friendly guide to machine learning and how developers can integrate ML into their applications.",
                        Url = "machine-learning-for-developers",
                        IsActive = true,
                        Image = "backend-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-120),
                        Tags = new List<Tag> { GetTag("AI & Machine Learning"), GetTag("fullstack") },
                        UserId = 13,
                        Comments = new List<Comment> {
                            new Comment {Text = "Machine learning is definitely a game-changer. Great introduction to the topic!", PublishedOn = DateTime.Now, UserId = 14},
                            new Comment {Text = "I'm excited to dive into machine learning after reading this. Thanks for the insights.", PublishedOn = DateTime.Now, UserId = 15}
                        }
                    },
                    new Post
                    {
                        Title = "Understanding the Future of Quantum Computing",
                        Content = "Quantum computing is a rapidly growing field that promises to revolutionize technology. In this post, we explore the fundamentals and its potential impact on various industries.",
                        Description = "An introduction to quantum computing and its future applications for developers and researchers.",
                        Url = "future-of-quantum-computing",
                        IsActive = true,
                        Image = "fullstack-tools.jpg",
                        PublishedOn = DateTime.Now.AddDays(-100),
                        Tags = new List<Tag> { GetTag("AI & Machine Learning"), GetTag("cloud computing") },
                        UserId = 12,
                        Comments = new List<Comment> {
                            new Comment {Text = "Quantum computing is fascinating! Can't wait to see how it evolves.", PublishedOn = DateTime.Now, UserId = 13},
                            new Comment {Text = "Great overview of the topic. Definitely an area to watch in the coming years.", PublishedOn = DateTime.Now, UserId = 14}
                        }
                    },
                    new Post
                    {
                        Title = "Building Scalable Applications with Microservices",
                        Content = "Microservices architecture allows developers to build scalable, flexible, and maintainable applications. This post dives into best practices for implementing microservices in modern development.",
                        Description = "Learn how to design and implement scalable applications using microservices architecture.",
                        Url = "scalable-applications-with-microservices",
                        IsActive = true,
                        Image = "web-evolution.jpg",
                        PublishedOn = DateTime.Now.AddDays(-90),
                        Tags = new List<Tag> { GetTag("backend"), GetTag("cloud computing") },
                        UserId = 10,
                        Comments = new List<Comment> {
                            new Comment {Text = "Microservices have truly transformed how we think about scalability. Great post!", PublishedOn = DateTime.Now, UserId = 11},
                            new Comment {Text = "Helpful insights. This will be very useful for my next project.", PublishedOn = DateTime.Now, UserId = 12}
                        }
                    },
                    new Post
                    {
                        Title = "Exploring the Power of Blockchain Technology",
                        Content = "Blockchain technology is no longer just about cryptocurrency. In this post, we dive into how blockchain is reshaping industries such as finance, supply chain, and more.",
                        Description = "Discover how blockchain is being applied across various sectors and how developers can leverage its power.",
                        Url = "power-of-blockchain-technology",
                        IsActive = true,
                        Image = "game-dev.jpg",
                        PublishedOn = DateTime.Now.AddDays(-80),
                        Tags = new List<Tag> { GetTag("AI & Machine Learning"), GetTag("cybersecurity") },
                        UserId = 9,
                        Comments = new List<Comment> {
                            new Comment {Text = "Blockchain has so much potential beyond crypto. Great insights on its applications!", PublishedOn = DateTime.Now, UserId = 10},
                            new Comment {Text = "Looking forward to exploring blockchain in more depth. Thanks for the resources!", PublishedOn = DateTime.Now, UserId = 11}
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