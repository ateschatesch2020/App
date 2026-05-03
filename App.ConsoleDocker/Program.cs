Console.WriteLine("Hello, World!");
int i = 0;

while(i<10000)
{
    Console.WriteLine($"Current value of i: {i}");
    i++;
    Thread.Sleep(1000); 
}