// Program.cs
using App.DesignPatterns.DesignPatterns.Behavioral.Observer;
using App.DesignPatterns.DesignPatterns.Creational.AbstractFactoryMethod;
using App.DesignPatterns.DesignPatterns.Creational.FactoryMethod;
using App.DesignPatterns.DesignPatterns.Structural.Adapter;
using App.DesignPatterns.DesignPatterns.Test.Factory;
using App.DesignPatterns.DesignPatterns.Test.Observer;
using Creator = App.DesignPatterns.DesignPatterns.Test.Factory.Creator;


#region AbstractFactory
//// Abstract factory #1
//AbstractFactory factory1 = new ConcreteFactory1();
//Client client1 = new Client(factory1);
//client1.Run();
//// Abstract factory #2
//AbstractFactory factory2 = new ConcreteFactory2();
//Client client2 = new Client(factory2);
//client2.Run();
#endregion

#region Adapter
//Adaptee adaptee = new Adaptee();
//ITarget target = new Adapter(adaptee);

//Console.WriteLine("Adaptee interface is incompatible with the client.");
//Console.WriteLine("But with adapter client can call it's method.");

//Console.WriteLine(target.Request());
#endregion

#region Factory Method
// An array of creators
//Creator[] creators = new Creator[2];
//creators[0] = new PenCreator();
//creators[1] = new BookCreator();
//// Iterate over creators and create products
//foreach (Creator creator in creators)
//{// the goal is here, uncouple the creates methods from clients. Thus the creation appears only one place
//    Product product = creator.FactoryMethod();
//    Console.WriteLine("Created {0}",
//      product.GetType().Name);
//}
#endregion

#region Observer
var youtubeChannel = new YoutubeChannelPublisher();

var emailSender = new EmailSender();
var smsSender = new SmsSender();

youtubeChannel.VideoPublished += emailSender.OnVideoPublished;
youtubeChannel.VideoPublished += smsSender.OnVideoPublished;

youtubeChannel.UploadVideo("Observer Pattern in C#");
    
youtubeChannel.VideoPublishedAsync += async (messeage) =>
{
    await Task.Delay(100);
    throw new Exception("Hata!");       // Yakalalanabilir!
};

youtubeChannel.UploadVideoAndTriggerAsyncHandler("Observer Pattern with custom handler");
#endregion

//App.DesignPatterns.DesignPatterns.Test.Observer.Observer observer = new();
//App.DesignPatterns.DesignPatterns.Test.Observer.Publisher publisher = new();
//publisher.Published += observer.OnPublished;
//publisher.VideoUpload("new video is uploaded");


//Creator[] creators = new Creator[2];
//creators[0] = new TestBookCreator();
//creators[1] = new TestPenCreator();

//foreach (var item in creators)
//{
//    TestProduct testProduct = item.FactoryMethod();
//    Console.WriteLine("Creatted {0}", item.GetType().Name);
//}


Console.ReadKey();