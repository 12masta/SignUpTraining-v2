using Microsoft.Playwright;

namespace SignUpTraining_v2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://xf-buskozdroj.gymos.pl/rezerwacja-zajec/");
            await page.FillAsync("id=Email", "staneko@icloud.com");
            await page.SelectOptionAsync("id=dob_dd", "15");
            await page.SelectOptionAsync("id=dob_mm", "5");
            await page.SelectOptionAsync("id=dob_rr", "1996");
            await page.FillAsync("id=Karta", "4201720201");
            await page.ClickAsync("[class=zaloguj]");

            await page.ClickAsync("[class='arrow right']");

            var trainingDay = new TrainingWeek().GetTrainingDaySelectors();

            foreach (var selector in trainingDay)
            {
                await page.ClickAsync(selector);
                await page.ClickAsync("id=zapisz");
                await page.ClickAsync("id=anuluj");
                await page.ReloadAsync();
                var guid = Guid.NewGuid();
                await page.ScreenshotAsync(new PageScreenshotOptions {Path = "screenshot" + guid + ".png"});
            }

            await page.ScreenshotAsync(new PageScreenshotOptions {Path = "screenshot.png"});
        }
    }
}