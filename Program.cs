#region data
using CA230601;

List<Person> people = new()
{
    new Person()
    {
        Id = 1,
        Name = "John Doe",
        BirthDate = new DateTime(1993, 07, 03),
        FavBooks = new() { "Harr Potter I.", "1984", "Shining", },
        Workplace = "Microsoft",
        NoChild = 1,
        Sex = true,
    },
    new Person()
    {
        Id = 2,
        Name = "Edgar Crispin",
        BirthDate = new DateTime(1973, 01, 20),
        FavBooks = new() { "Lord of the Rigs", "Song of Ice and Fire", "Wheel of Time", },
        Workplace = "Apple",
        NoChild = 0,
        Sex = true,
    },
    new Person()
    {
        Id = 3,
        Name = "Martin Stone",
        BirthDate = new DateTime(2008, 02, 14),
        FavBooks = new() { "Aranyember", "Az Ember Tragédiája", },
        Workplace = null,
        NoChild = 0,
        Sex = true,
    },
    new Person()
    {
        Id = 4,
        Name = "Suzi Jaylen",
        BirthDate = new DateTime(1991, 02, 15),
        FavBooks = new() { "The Great Gatsby", "Fifty Shades of Gray", },
        Workplace = "Microsoft",
        NoChild = 4,
        Sex = false,
    },
    new Person()
    {
        Id = 5,
        Name = "Alexa Stirling",
        BirthDate = new DateTime(1985, 12, 29),
        FavBooks = new() { "Invisible Man", "1984", "Watchman", },
        Workplace = "Microsoft",
        NoChild = 2,
        Sex = false,
    },
};
#endregion

//sorozatszámítás (összegzés)

//a listában lévő emberek gyermekeinek számának összege
int f1gyszo = people.Sum(p => p.NoChild);
Console.WriteLine($"osszes gyerek szama: {f1gyszo}");

//megszámlálás
int f2micsoft = people.Count(p => p.Workplace == "Microsoft");
Console.WriteLine($"microsoft dolgozok szama: {f2micsoft}");
int f2ffszam = people.Count(p => p.Sex);
Console.WriteLine($"ferfiak szama: {f2ffszam}");
int f2no1984 = people.Count(p => p.FavBooks.Contains("1984"));
Console.WriteLine($"olvasta az 1984et: {f2no1984}");
int f2vangyerek = people.Count(p => p.NoChild > 0);
Console.WriteLine($"van gyereke: {f2vangyerek}");

//szélsőérték meghat (hely, érték)
Person f3legtobbgyerek = people.MaxBy(p => p.NoChild);
Console.WriteLine($"legtobb gyerek: {f3legtobbgyerek.Name}");
Person f3legkevesebb = people.MinBy(p => p.NoChild);
Console.WriteLine($"legkevesebb gyerek: {f3legkevesebb.Name}");
DateTime f3legfiatalabb = people.Max(p => p.BirthDate);
Console.WriteLine($"a legfiatalabb szuldat: {f3legfiatalabb}");
int f3legrovidebb = people.Min(p => p.Name.Length);
Console.WriteLine($"legrovidebb nev karakterszama: {f3legrovidebb}");

//kiválasztás

//ha van találat
//  -> a találatok közül visszaadja az elsőt
//ha nincs találat
//  -> exception
Person f4doe = people.First(p => p.Name.Contains("Doe"));
Console.WriteLine($"keresett fullname: {f4doe.Name}");

//ha van találat
//  -> a találatok közül visszaadja az elsőt
//ha nincs találat
//  és amiben keresel az value type:
//  -> default értéket ad vissza (általában zezo érték)
//  és amiben keresel az ref type
//  -> null értéket ad vissza
Person f4doe2 = people.FirstOrDefault(p => p.Name.Contains("Zoli"));
Console.WriteLine($"keresett fullname: {f4doe2 is null}");

Person f4elsovangy = people.First(p => p.NoChild > 0);
Console.WriteLine($"elso, akinek van gyereke: {f4elsovangy.Name}");

Person f4utcsovangy = people.Last(p => p.NoChild > 0);
Console.WriteLine($"utcso, akinek van gyereke: {f4utcsovangy.Name}");

//_ = people.Last();
//_ = people.LastOrDefault();
// -----------------------
// ha pontosan 1 találat van:
//  -> visszaadja azt az obj-t
// ha több találat van:
//  -> exceptiont dob
// ha nincs találat
// -> exceptiont dob

Person f4egyikdolg = people.Single(p => p.Workplace == "Apple");
Console.WriteLine($"dolgozo neve: {f4egyikdolg.Name}");


// ha pontosan 1 találat van:
//  -> visszaadja azt az obj-t
// ha több találat van:
//  -> exceptiont dob
// ha nincs találat
// -> null vagy zero értéket ad vissza (attól függően, hogy val vagy ref a dataset)
//_ = people.SingleOrDefault();

Person f4masikdolg = people.SingleOrDefault(p => p.Workplace == "Lidl");
Console.WriteLine($"dolgozo neve: {f4masikdolg is null}");

bool vanIlyen = people.Any(p => p.Name == "Zolikaaa");
//eldöntés -> LINQ 'nincs', de a Collections.Gener-ben van pl a .Contains() -> bool

//kiválogatás [szelekció]
IEnumerable<Person> f5micsoft = people.Where(p => p.Workplace == "Microsoft");
Console.WriteLine("microsoft dolgozoi:");
foreach (var p in f5micsoft)
{
    Console.WriteLine($"\t-{p.Name}");
}

var f6lszgymd = people
    .Where(p => p.Workplace == "Microsoft")
    .Where(p => p.NoChild > 0)
    .MinBy(p => p.BirthDate);

Console.WriteLine($"microsoftnal dolgozo gyermekes emberek kozul a legidossebb neve: {f6lszgymd.Name}");

//projekció
var f7osszeskonyv = people.SelectMany(p => p.FavBooks)
    .Distinct();
Console.WriteLine("osszes konyv:");
foreach (var konyv in f7osszeskonyv)
{
    Console.WriteLine("\t" + konyv);
}

var f8osszesmh = people
    .Where(p => p.Workplace is not null)
    .Select(p => p.Workplace)
    .Distinct();

var f8masiksyntax = (from p in people
                    where p.Workplace is not null
                    select p.Workplace)
                    .Distinct();

Console.WriteLine("osszes mh:");
foreach (var mh in f8osszesmh)
{
    Console.WriteLine($"\t- {mh}");
}

//group by [szétválogatás]
IEnumerable<IGrouping<bool, Person>> f9gb = people.GroupBy(p => p.Sex);

foreach (var cs in f9gb)
{
    Console.WriteLine(cs.Key ? "férfiak:" : "nők:");
    foreach (var p in cs)
    {
        Console.WriteLine($"\t-{p.Name}");
    }

}
IEnumerable<IGrouping<string, Person>> f9mhgb = people.GroupBy(p => p.Workplace);

foreach (var cs in f9mhgb)
{
    if (cs.Key is null) Console.Write("munkanélküli ");
    else Console.Write($"{cs.Key}:");
    Console.WriteLine($" ({cs.Count()} fő)");
    foreach (var p in cs)
    {
        Console.WriteLine($"\t-{p.Name}");
    }
}
//.Copy(), ez sem LINQ

//rendezés (egyszerű cserés)
IOrderedEnumerable<Person> f10rendszul = people
    .OrderByDescending(p => p.BirthDate)
    .ThenBy(p => p.NoChild);

Console.WriteLine("szul szerint rendezver csokkenoben:");
foreach (var p in f10rendszul)
{
    Console.WriteLine($"\t-{p.Name} -- {p.BirthDate.Year}");
}

