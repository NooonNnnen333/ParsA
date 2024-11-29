using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
namespace testLayers;

class Program
{
    static async Task Main(string[] args)
    {
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        // Дожидаемся окончания загрузки с помощью await.
        var html = await context.OpenAsync("https://www.susu.ru/ru");
        //string htmlText = html.Body.InnerHtml;
        //Console.WriteLine(htmlText);

        string htmlText = @"<a href=""/ru/about/campus"" title=""Карта"">Челябинск, проспект Ленина, 76</a><br> 
                            <a href=""/ru/about/campus"" title=""Карта"">Челябинск, проспект Ленина, 76</a><br>";
        string menuPattern = @"(title=""Карта"">Челябинск, проспект)(\s\w+)(?:,\s76)(</a><br>)";

        // Получаем все совпадения и извлекаем группы (начиная с 1)
        List<string> massivSilok = Regex.Matches(htmlText, menuPattern, RegexOptions.Singleline)
            .Cast<Match>()
            .Select(match => string.Concat(Enumerable.Range(1, match.Length) // Группы от 1 до 3
                .Select(i => match.Groups[i].Value)))
            .ToList();

        // Выводим список строк
        foreach (var part in massivSilok)
        {
            Console.WriteLine(part);
        }

        
    }
}
        
 