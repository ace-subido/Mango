Mango: a simple MongoDB data-access tool written in C#
======

Mango is the result of trying to learn new things with MongoDB; it's some of the sweet things I learned, simplified it and packed it up tightly. 

It's a single-file wrapper for the MongoDB C# driver.  It's less than 200 lines of code. Yes, it's dead simple (I actually think this readme is longer than the code). Does the world actually need another wrapper for the official 10gen mongoDB C# driver? I don't really think about that stuff :) I just want to share this with fellow developers and hopefully help someone out in the process.

Design
--------
I designed Mango with 3 purposes in mind:

1. **Out-of-the-box dead-easy to use.** - I thought of the developers in hackathons and the amount of time they shouldn't waste in making a data access tool. Drop in the code, edit it, cut it, paste it, it's easy to use and understand.
2. **As a .NET primer on NoSQL technologies.** - I thought of the .NET developers that want to start learning MongoDB.
3. **Easily editable and flexible.** - I intended this as a base template for things created with craziness in your data access layer.

How to use
---------
- Get your MongoDB instance ready. Add a connection string to that database in your web.config under the connectionStrings markup.
- Create a class that represents the documents in your MongoDB collections. Wrap that class with **MangoModel**.
- Create a Repository class for that POCO which will serve as your access to that collection. Wrap that class with **MangoRepository** and specify the name for that collection.
- Do the LAMBDA and LINQ statement dance.

Sample Code
--------
Suppose we want to access a collection of documents for a "User" class. First we place our connection string in our web.config. 

    <connectionStrings>
       <add name="MangoDBTest" connectionString="mongodb://localhost/MangoTest" />
    </connectionStrings>

(The connection string name does not matter, Mango will get the first connection string it sees.)

We then take a "Person" POCO and wrap it with **MangoModel**:

    public class Person : MangoModel
    {
        public string Username { get; set; }
        public string Email { get; set; }        
        public DateTime DateCreated { get; set; }
    }

*What does this do?*

The MangoModel class supplies our POCO with the Id attribute (BsonId type). It basically makes our Person object mongo ready :)

Now that we've modelled our document, let's create our Repository class that serves as our medium for the mongodb collection. We have our collection called "UserCollection" in our MongoDB instance:

    public class PersonRepository : MangoRepository<Person>
    {
        public PersonRepository()
            : base("UserCollection")
        { }
    }    

Everything is settled, we can now start playing with the collection in our Mongo database :) You can also implement your own methods in this Repository class. It's that easy! If you want to add some more stuff in the MangoRepository class, just go ahead and tweak it to your preference.

**Inserts**

    //create the repository
    PersonRepository repository = new PersonRepository();            
    Console.ReadKey();

    //create person objct
    Person template = new Person();

    template.Email = "person@gmail.com";
    template.Username = "person";
    template.DateCreated = DateTime.Now;

    //simple add method that returns the added object with the mongodb generated Id
    Person templateForUpdate = repository.Add(template);

**Select and Update**

    //select person using LAMBDA
    Person templateForUpdate = repository.GetSingle(x => x.Username == "person");
      
    //simple update
    templateForUpdate.Username = "personupdate";
    templateForUpdate.Email = "personemail@gmail.com";
    template.DateCreated = DateTime.Now;

    repository.Update(templateForUpdate);

**Deletes**

    Person templateForDelete = templateForUpdate;

Other out-of-the-box methods
--------
**Count**

The **Count** method returns a the number of documents in a collection.

**BatchInsert**

Feed the **Add** method a List of the models. Make sure SafeMode is set to true in your connectionString. Read the MongoDB API for further details.

**Exists**

**Exists** method on the Repository class takes in a LAMBDA expression and brings back a boolean result.

##Nuget##
    PM > Install-Package Mango

##Got a bug to report? Want to suggest something?##

https://github.com/ace-subido/Mango/issues is the perfect place for that. Drop me a message at devstuff@acesubido.com

##Acknowledgements##

Thanks to mongorepository.codeplex.com, it was a huge influence in the code-base. Thanks to 10gen for making MongoDB such a great piece of software!

##Author##

My name is Ace Subido, I'm a software developer from the Philippines.

##License##

It's in the repo.













