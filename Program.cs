// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Mr.Benson!");

Console.WriteLine("What operation are you performing?\n" +
    "1. Record a New Sale\n" +
    "2. View Monthly Report\n" +
    "3. Exit");

Console.Write("->");
int choice = int.Parse(Console.ReadLine());

if (choice == 1)
{
    Console.WriteLine("Which item(s) is being sold?\n" +
        "1. Bread\n" +
        "2. Milk\n" +
        "3. Yorghurt\n" +
        "4. Soap\n" +
        "5. Washing Powder\n" +
        "6. \n");

    string nameChoice = Console.ReadLine();

}
else if (choice == 2)
{

}
else if(choice == 3)
{
    Console.WriteLine("Goodbye, Mr.Benson!");

}
else
{
    Console.WriteLine("Invalid Choice selected");
}
