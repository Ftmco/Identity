// See https://aka.ms/new-console-template for more information
using Identity.Package.Account;
using Identity.Package.Application;

Console.WriteLine("Hello, World!");

Application app = new("1B594D05-C77D-41AF-B20C-9C965B37EEDB", "1G14ijWA");

//var users = await app.UsersAsync();
//if (users != null)
//{
//    foreach (var item in users)
//        Console.WriteLine(item.Email);  
//}
//var login = await app.LoginAsync(new("newUser", "1234587"));
var signUp = await app.SingUpAsync(new("newClientUser", "1234587", "amirbr", "amirb@gmail.com", "02136462720"));
Console.WriteLine(signUp);